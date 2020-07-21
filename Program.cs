using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace k32
{
    class Program
    {
        static void Main(string[] args)
        {
            int w = 32000;
            int h = 18000;
            var pixels = new byte[w * h * 3];

            var t0 = Environment.TickCount64;

            Parallel.For(0, h, j =>
            {
                var t = j * w * 3;
                for (var i = 0; i < w; i++)
                {
                    var r = Math.Abs((i - 16000.0) * (i - 16000.0) + (j - 4823.0854637602087165059658528943) * (j - 4823.0854637602087165059658528943)) / 90867.786682558701517302896126521;
                    var g = Math.Abs((i - 11176.914536239791283494034147106) * (i - 11176.914536239791283494034147106) + (j - 13176.914536239791283494034147106) * (j - 13176.914536239791283494034147106)) / 90867.786682558701517302896126521;
                    var b = Math.Abs((i - 20823.085463760208716505965852894) * (i - 20823.085463760208716505965852894) + (j - 13176.914536239791283494034147106) * (j - 13176.914536239791283494034147106)) / 90867.786682558701517302896126521;
                    pixels[t++] = (byte)b;
                    pixels[t++] = (byte)g;
                    pixels[t++] = (byte)r;
                }
            });

            Console.WriteLine(Environment.TickCount64 - t0);

            var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            var bitmap = new Bitmap(w, h, w * 3, PixelFormat.Format24bppRgb, handle.AddrOfPinnedObject());
            bitmap.Save("venn.png", ImageFormat.Png);
            bitmap.Save("venn.jpg", ImageFormat.Jpeg);
        }
    }
}
