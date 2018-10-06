using Pace.Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Server.Model
{
    public class Client
    {
        public PaceClient Owner { get; set; }
        public string Identifier { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string ComputerName { get; set; }
        public string OS { get; set; }
    }
}