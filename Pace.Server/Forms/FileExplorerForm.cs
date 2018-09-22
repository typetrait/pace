using NetSerializer;
using Pace.Common.Network;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pace.Server.Forms
{
    public partial class FileExplorerForm : Form
    {
        private PaceClient client;

        public FileExplorerForm()
        {
            InitializeComponent();
        }

        public FileExplorerForm(PaceClient client) : this()
        {
            this.client = client;
        }

        private void FileExplorerForm_Load(object sender, EventArgs e)
        {
            Navigate(Path.GetPathRoot(Environment.SystemDirectory));
        }

        private void directoryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Navigate(directoryListView.SelectedItems[0].Text);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            Navigate(pathTextBox.Text);
        }

        private void Navigate(string path)
        {
            var getDirectoryPacket = new GetDirectoryRequestPacket(path);
            client.SendPacket(getDirectoryPacket);

            var response = (GetDirectoryResponsePacket)client.ReadPacket();

            directoryListView.Clear();

            foreach (var folder in response.Folders)
            {
                directoryListView.Items.Add(folder, 0);
            }

            foreach (var file in response.Files)
            {
                directoryListView.Items.Add(file, 1);
            }

            pathTextBox.Text = getDirectoryPacket.Path;
        }
    }
}