using Pace.Common.Network;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using Pace.Server.Network;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Pace.Server.Forms
{
    public partial class FileExplorerForm : Form
    {
        private PaceServer server;
        private PaceClient client;

        private DirectoryInfo currentDirectory;

        public FileExplorerForm()
        {
            InitializeComponent();
        }

        public FileExplorerForm(PaceServer server, PaceClient client) : this()
        {
            this.server = server;
            this.client = client;
        }

        private void FileExplorerForm_Load(object sender, EventArgs e)
        {
            server.PacketChannel.RegisterHandler<GetDirectoryResponsePacket>(HandleGetDirectory);

            Navigate(Path.GetPathRoot(Environment.SystemDirectory));
        }

        private void directoryListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (directoryListView.SelectedItems.Count > 0)
            {
                var item = directoryListView.SelectedItems[0];

                if (item.ImageIndex == 1)
                    return;

                var path = Path.Combine(currentDirectory.FullName, item.Text);
                Navigate(path);
            }
        }

        private void fileContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (directoryListView.SelectedItems.Count < 1)
            {
                e.Cancel = true;
            }
        }

        private void pathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Navigate(pathTextBox.Text);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {

        }

        private void forwardButton_Click(object sender, EventArgs e)
        {

        }

        private void upButton_Click(object sender, EventArgs e)
        {
            Navigate(Directory.GetParent(pathTextBox.Text).FullName);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Navigate(pathTextBox.Text);
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in directoryListView.SelectedItems)
            {
                var packet = new DeleteFileRequestPacket(Path.Combine(currentDirectory.FullName, item.Text));
                client.SendPacket(packet);
            }
        }

        private void Navigate(string path)
        {
            var getDirectoryPacket = new GetDirectoryRequestPacket(path);
            client.SendPacket(getDirectoryPacket);

            currentDirectory = new DirectoryInfo(getDirectoryPacket.Path);
            pathTextBox.Text = getDirectoryPacket.Path;
        }

        private void HandleGetDirectory(object packet)
        {
            var response = (GetDirectoryResponsePacket)packet;

            Invoke(new Action(() => directoryListView.Clear()));

            foreach (var folder in response.Folders)
            {
                var dirInfo = new DirectoryInfo(folder);
                Invoke(new Action(() => directoryListView.Items.Add(dirInfo.Name, 0)));
            }

            foreach (var file in response.Files)
            {
                var fileInfo = new FileInfo(file);
                Invoke(new Action(() => directoryListView.Items.Add(fileInfo.Name, 1)));
            }
        }
    }
}