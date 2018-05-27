using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Common.Network.Packets.Client
{
    [Serializable]
    public class TakeScreenshotResponsePacket : IPacket
    {
        public byte[] ImageData { get; set; }

        public TakeScreenshotResponsePacket(byte[] imageData)
        {
            ImageData = imageData;
        }
    }
}