using Pace.Common.Network;
using Pace.Common.Network.Packets.Server;
using Pace.Server.Network;
using System;
using System.IO;
using System.Windows.Forms;

namespace Pace.Server.Forms
{
    public partial class DownloadWebFileForm : Form
    {
        private PaceServer server;
        private PaceClient client;

        public DownloadWebFileForm(PaceServer server, PaceClient client)
        {
            InitializeComponent();

            this.server = server;
            this.client = client;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string url = urlTextBox.Text;

            using (var ms = new MemoryStream())
            {
                var packet = new DownloadFileRequestPacket(url);
                server.Serializer.Serialize(ms, packet);

                byte[] packetBytes = ms.ToArray();

                client.SendData(packetBytes);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}