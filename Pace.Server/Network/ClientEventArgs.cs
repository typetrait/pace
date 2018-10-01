using Pace.Common.Network;
using System;

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