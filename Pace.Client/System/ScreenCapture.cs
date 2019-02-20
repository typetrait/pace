using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Pace.Client.System
{
    public static class ScreenCapture
    {
        public static Image Take()
        {
            var screenBounds = Screen.PrimaryScreen.Bounds;

            var bitmap = new Bitmap(screenBounds.Width, screenBounds.Height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(
                    screenBounds.X, screenBounds.Y, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy
                );
            }

            return bitmap;
        }

        public static byte[] ImageToBytes(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}