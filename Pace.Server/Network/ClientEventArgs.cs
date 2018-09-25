using Pace.Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Server.Network
{
    public class ClientEventArgs : EventArgs
    {
        public PaceClient Client { get; set; }

        public ClientEventArgs(PaceClient client)
        {
            Client = client;
        }
    }
}