// Stephen Toub

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Numerics;

namespace Microsoft
{
    /// <summary>Represents the bounds and location of the mandelbrot fractal to be rendered.</summary>
    public class MandelbrotPosition
    {
        public static MandelbrotPosition Default
        {
            get
            {
                MandelbrotPosition pos = new MandelbrotPosition();
                pos.Height = 3.4;  //2.27;
                pos.Width = pos.Height * MainForm.WIDTH / MainForm.HEIGHT; //2.9;
                // pos.Width = pos.Height;
                pos.CenterX = 0; //  -.75;
                pos.CenterY = 0; // .006;
                return pos;
            }
        }
        public double Width, Height;
        public double CenterX, CenterY;
    }

    /// <summary>Generates mandelbrot fractals.</summary>
    public class MandelbrotGenerator
    {
        /// <summary>The 256 color palette to use for all fractals.</summary>
        private static Color[] _paletteColors;

        /// <summary>Copy our precreated color palette into the target Bitmap.</summary>
        /// <param name="bmp">The Bitmap to be updated.</param>
        private static void UpdatePalette(Bitmap bmp)
        {
            ColorPalette p = bmp.Palette;
            Array.Copy(_paletteColors, p.Entries, _paletteColors.Length);
            bmp.Palette = p; // The Bitmap will only update when the Palette property's setter is used
        }

        /// <summary>Renders a mandelbrot fractal.</summary>
        /// <param name="position">The MandelbrotPosition representing the fractal boundaries to be rendered.</param>
        /// <param name="imageWidth">The width in pixels of the image to create.</param>
        /// <param name="imageHeight">The height in pixels of the image to create.</param>
        /// <param name="parallelRendering">Whether to render the image in parallel.</param>
        /// <returns>The rendered Bitmap.</returns>
        public unsafe static Bitmap Create(MandelProps mandelProps, MandelbrotPosition position, int imageWidth, int imageHeight, CancellationToken cancellationToken, bool parallelRendering)
        {
            // The maximum number of iterations to perform for each pixel.  Higher number means better
            // quality but also slower.
            int maxIterations = mandelProps.max_iter;
            _paletteColors = Palettes.CreatePaletteColors(mandelProps.cmap);

            // In order to use the Bitmap ctor that accepts a stride, the stride must be divisible by four.
            // We're using imageWidth as the stride, so shift it to be divisible by 4 as necessary.
            if (imageWidth % 4 != 0) imageWidth = (imageWidth / 4) * 4;

            // Based on the fractal bounds, determine its upper right coordinate
            double right = position.CenterX - (position.Width / 2);
            double top = position.CenterY + (position.Height / 2);

            // Get the factors that can be multiplied by row and col to arrive at specific x and y values
            double colToXTranslation = position.Width / (double)imageWidth;
            double rowToYTranslation = -position.Height / (double)imageHeight;

            // Create the byte array that will store the rendered color indices
            int pixels = imageWidth * imageHeight;
            byte[] data = new byte[pixels]; // initialized to all 0s, which equates to all black based on the default palette

            // Generate the fractal using the mandelbrot formula : z = z^2 + c
            try
            {
                // Parallel implementation
                Evaluator evaluator = Evaluators.evaluatorFactory(mandelProps);
                if (parallelRendering)
                {
                    var options = new ParallelOptions { CancellationToken = cancellationToken };
                    Parallel.For(0, imageHeight, options, row =>
                    {
                        double initialY = row * rowToYTranslation + top;
                        fixed (byte* ptr = data)
                        {
                            byte* currentPixel = &ptr[row * imageWidth];
                            for (int col = 0; col < imageWidth; col++, currentPixel++)
                            {
                                double initialX = col * colToXTranslation + right;
                                byte iteration = (byte) evaluator.eval(initialX, initialY);
                                *currentPixel =  iteration;
                            }
                        }
                    });
                }
                // Sequential implementation
                else
                {
                    for (int row = 0; row < imageHeight; row++)
                    {
                        // if (cancellationToken.IsCancellationRequested) break;
                        cancellationToken.ThrowIfCancellationRequested();

                        double initialY = row * rowToYTranslation + top;
                        fixed (byte* ptr = data)
                        {
                            byte* currentPixel = &ptr[row * imageWidth];
                            for (int col = 0; col < imageWidth; col++, currentPixel++)
                            {
                                double initialX = col * colToXTranslation + right;
                                byte iteration = (byte) evaluator.eval(initialX, initialY);
                                *currentPixel = iteration;
                            }
                        }
                    };
                }
            }
            catch (Exception)
            {
            }

            // Produce a Bitmap from the byte array of color indices and return it
            fixed (byte* ptr = data)
            {
                using (Bitmap tempBitmap = new Bitmap(imageWidth, imageHeight, imageWidth, PixelFormat.Format8bppIndexed, (IntPtr)ptr))
                {
                    Bitmap bitmap = tempBitmap.Clone(new Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height), PixelFormat.Format8bppIndexed);
                    UpdatePalette(bitmap);
                    return bitmap;
                }
            }
        }
    }
}