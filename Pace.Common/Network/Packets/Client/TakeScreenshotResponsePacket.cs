using System;

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