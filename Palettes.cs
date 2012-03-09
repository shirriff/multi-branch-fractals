using System;
using System.Drawing;


namespace Microsoft
{
    public class Palettes
    {
        /// <summary>Create the color palette to be used for all fractals.</summary>
        /// <returns>A 256-color array that can be stored into an 8bpp Bitmap's ColorPalette.</returns>
        public static Color[] CreatePaletteColors(String name)
        {
            Color[] paletteColors = new Color[256];
            if ("Blue".Equals(name))
            {
                paletteColors[0] = Color.Black;
                for (int i = 1; i < 256; i++) paletteColors[i] = Color.FromArgb(0, i * 5 % 256, i * 5 % 256);
            }
            else if ("Blue1".Equals(name))
            {
                paletteColors[0] = Color.Black;
                for (int i = 1; i < 256; i++) paletteColors[i] = Color.FromArgb(0, 255-i, 255-i);
            }
            else if ("128".Equals(name))
            {
                paletteColors[0] = Color.Black;
                for (int i = 1; i < 128; i++) paletteColors[i] = Color.FromArgb(0, i, i);
                for (int i = 0; i < 128; i++) paletteColors[i + 128] = Color.FromArgb(i, 0, 0);
            }
            else if ("Hue".Equals(name))
            {
                paletteColors[0] = Color.Black;
                for (int i = 1; i < 256; i++) paletteColors[i] = fromHls(i / 256.0 * 360, .5, 1);
            }
            else if ("Thing1".Equals(name))
            {
                paletteColors[0] = Color.Black;
                Random r = new Random(0);

                for (int i = 1; i < 128; i++) paletteColors[i] = Color.FromArgb(255, r.Next(256), r.Next(256), 0);
                for (int i = 128; i < 256; i++) paletteColors[i] = Color.FromArgb(255, r.Next(256), 0, r.Next(256));
                paletteColors[255] = Color.FromArgb(255, 255, 255, 255);
                paletteColors[254] = Color.FromArgb(255, 200, 200, 200);
                paletteColors[253] = Color.FromArgb(255, 180, 180, 180);
                paletteColors[252] = Color.FromArgb(255, 120, 120, 120);
            }
            else if ("Cycles".Equals(name))
            {
                for (int i = 0; i < 256; i++)
                {
                    paletteColors[i] = Color.White;
                }
                paletteColors[0] = Color.Black;
                paletteColors[1] = Color.Red;
                paletteColors[2] = Color.Green;
                paletteColors[3] = Color.Blue;
                paletteColors[4] = Color.Orange;
                paletteColors[5] = Color.Brown;
                paletteColors[6] = Color.Purple;

            }
            else if ("Random".Equals(name))
            {
                paletteColors[0] = Color.Black;
                Random r = new Random(0);
                for (int i = 1; i < 256; i++) paletteColors[i] = Color.FromArgb(255, r.Next(256), r.Next(256), r.Next(256));
            }
            else if ("InOut".Equals(name))
            {
                for (int i = 0; i < 256-32; i++) paletteColors[i] = fromHls(i / 128.0 * 360, .5, 1);
                for (int i = 256-32; i < 256; i++)
                {
                    byte g = (byte) ((255-i) * 16);
                    paletteColors[i] = Color.FromArgb(255, g, g, g);
                }                
            }
            else if ("Grayscale".Equals(name))
            {
                for (int i = 0; i < 256; i++) paletteColors[i] = Color.FromArgb(255, i, i, i);
            }
            return paletteColors;
        }

        static Color fromHls(double h, double l, double s) // h from 0 to 360, l, s from 0 to 1
        {
            // http://en.wikipedia.org/wiki/HLS_color_space
            double h1 = h / 60.0;
            double c = (1 - Math.Abs(2 * l - 1)) * s;
            double hmod2 = h1 - 2 * (int) (h1 / 2);
            double x = c * (1 - Math.Abs(hmod2 - 1));
            int ci = (int) (255 * c);
            int xi = (int) (255 * x);
            if (h1 < 1) {
                return Color.FromArgb(255, ci, xi, 0);
            } else if (h1 < 2) {
                return Color.FromArgb(255, xi, ci, 0);
            } else if (h1 < 3) {
                return Color.FromArgb(255, 0, ci, xi);
            } else if (h1 < 4) {
                return Color.FromArgb(255, 0, xi, ci);
            } else if (h1 < 5) {
                return Color.FromArgb(255, xi, 0, ci);
            } else if (h1 < 6) {
                return Color.FromArgb(255, ci, 0, xi);
            } else {
                return Color.Green;
            }
        }
    }
}
