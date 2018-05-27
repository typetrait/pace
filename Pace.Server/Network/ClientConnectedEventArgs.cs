using Pace.Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Server.Network
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public PaceClient Client { get; set; }

        public ClientConnectedEventArgs(PaceClient client)
        {
            Client = client;
        }
    }
}