// Stephen Toub

using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
using System.IO;

namespace Microsoft
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            mandelProps = new MandelProps(this, _mandelbrotWindow);
            dials = new Dials(10);
            dials.Show();
            props = new PropsDialog(this);
            props.loadFields(mandelProps);
            props.Show();
        }

        PropsDialog props;

        /// <summary>Describes the bounds of the fractal to render.</summary>
        MandelbrotPosition _mandelbrotWindow = MandelbrotPosition.Default;
        /// <summary>The last known size of the main window.</summary>
        private Size _lastWindowSize = Size.Empty;
        /// <summary>The last known position of the mouse.</summary>
        private Point _lastMousePosition;
        /// <summary>The most recent cancellation source to cancel the last task.</summary>
        private CancellationTokenSource _lastCancellation;
        /// <summary>The last time the image was updated.</summary>
        private DateTime _lastUpdateTime = DateTime.MinValue;
        /// <summary>Whether the right mouse button is currently pressed on the picture box.</summary>
        private bool _rightMouseDown = false;
        /// <summary>
        /// The format string to use for the main form's title; {0} should be set to the number of
        /// pixels per second rendered.
        /// </summary>
        private const string _formTitle = "Interactive Fractal ({0}x) - PPMS: {1} - Time: {2}";
        /// <summary>Whether to use parallel rendering.</summary>
        private bool _parallelRendering = true;

        private MandelProps mandelProps;
        private Dials dials;
        private Bitmap bmp;
        private Task currentTask;

        void mandelbrotPb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Center the image on the selected location
            _mandelbrotWindow.CenterX += ((e.X - (mandelbrotPb.Width / 2.0)) / mandelbrotPb.Width) * _mandelbrotWindow.Width;
            _mandelbrotWindow.CenterY += ((e.Y - (mandelbrotPb.Height / 2.0)) / mandelbrotPb.Height) * _mandelbrotWindow.Height;

            // If the right mouse button was used, zoom in by a factor of 2; if the right mouse button, zoom
            // out by a factor of 2
            double factor = e.Button == MouseButtons.Left ? .5 : 2;
            _mandelbrotWindow.Width *= factor;
            _mandelbrotWindow.Height *= factor;

            // Update the image
            UpdateImageAsync();
        }

        void mandelbrotPb_VisibleChanged(object sender, EventArgs e)
        {
            // When the picture box becomes visible, render it
            if (mandelbrotPb.Visible)
            {
                _lastWindowSize = Size;
                UpdateImageAsync();
            }
        }

        void mandelbrotPb_Resize(object sender, EventArgs e)
        {
            // If the window has been resized
            if (Size != _lastWindowSize)
            {
                // Scale the mandelbrot image by the same factor so that its visual size doesn't change
                if (_lastWindowSize.Width != 0)
                {
                    double xFactor = Size.Width / (double)_lastWindowSize.Width;
                    _mandelbrotWindow.Width *= xFactor;
                }

                if (_lastWindowSize.Height != 0)
                {
                    double yFactor = Size.Height / (double)_lastWindowSize.Height;
                    _mandelbrotWindow.Height *= yFactor;
                }

                // Record the new window size
                _lastWindowSize = Size;

                // Update the image
                UpdateImageAsync();

            }
        }

        void mandelbrotPb_MouseMove(object sender, MouseEventArgs e)
        {
            props.toolStripStatusLabel1.Text = "Mouse move " + e.X + " " + e.Y + " " + pixToCoordX(e.X) + "," + pixToCoordY(e.Y) + " " + _lastWindowSize.Height + " " + mandelbrotPb.Height + " " + _mandelbrotWindow.Height;
            // Determine how far the mouse has moved.  If it moved at all...
            Point delta = new Point(e.X - _lastMousePosition.X, e.Y - _lastMousePosition.Y);
            if (delta != Point.Empty)
            {
                // And if the right mouse button is down...
                if (_rightMouseDown)
                {
                    // Determine how much the mouse moved in fractal coordinates
                    double fractalMoveX = delta.X * _mandelbrotWindow.Width / mandelbrotPb.Width;
                    double fractalMoveY = delta.Y * _mandelbrotWindow.Height / mandelbrotPb.Height;

                    // Shift the fractal window accordingly
                    _mandelbrotWindow.CenterX -= fractalMoveX;
                    _mandelbrotWindow.CenterY -= fractalMoveY;

                    // And update the image
                    UpdateImageAsync();
                }
                // Record the new mouse position
                _lastMousePosition = e.Location;
            }


            
            if (inBand)
            {
                if (mouse_x1 >= 0)
                {
                    DrawReversibleRectangle();
                }
                mouse_x1 = e.X;
                mouse_y1 = e.Y;
                props.toolStripStatusLabel1.Text = "Mouse motion" + e.X + " " + e.Y;
                DrawReversibleRectangle();
            }
        }

        void mandelbrotPb_MouseDown(object sender, MouseEventArgs e)
        {
            // Record that mouse button is being pressed
            if (e.Button == MouseButtons.Right)
            {
                _lastMousePosition = e.Location;
                _rightMouseDown = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                inBand = true;
                mouse_x0 = e.X;
                mouse_y0 = e.Y;
                mouse_x1 = -1;
                mouse_y1 = -1;
                props.toolStripStatusLabel1.Text = "Mouse start" + pixToCoordX(e.X) + " " + pixToCoordY(e.Y);
            }
        }

        double pixToCoordX(int mx)
        {
            return (mx / (double)mandelbrotPb.Width * _mandelbrotWindow.Width) +
                _mandelbrotWindow.CenterX - _mandelbrotWindow.Width / 2;
        }

        double pixToCoordY(int my)
        {
            return (mandelbrotPb.Height - my) / (double)mandelbrotPb.Height * _mandelbrotWindow.Height +
                (_mandelbrotWindow.CenterY - _mandelbrotWindow.Height / 2);
        }

        int coordToPixX(double x)
        {
            return (int)((x - (_mandelbrotWindow.CenterX - _mandelbrotWindow.Width / 2)) / _mandelbrotWindow.Width * mandelbrotPb.Width);
        }

        int coordToPixY(double y)
        {
            return mandelbrotPb.Height - (int)((y - (_mandelbrotWindow.CenterY - _mandelbrotWindow.Height / 2)) / _mandelbrotWindow.Height * mandelbrotPb.Height);
        }

        void mandelbrotPb_MouseUp(object sender, MouseEventArgs e)
        {
            props.toolStripStatusLabel1.Text = "Mouse up a " + e.X + " " + e.Y;
            // Record that the mouse button is being released
            if (e.Button == MouseButtons.Right)
            {
                _lastMousePosition = e.Location;
                _rightMouseDown = false;
            }
            else if (e.Button == MouseButtons.Left)
            {
                props.toolStripStatusLabel1.Text = "Mouse up" + e.X + " " + e.Y;
                if (inBand)
                {
                    DrawReversibleRectangle();
                    inBand = false;
                    if (Math.Abs(mouse_x0 - mouse_x1) < 10 ||
                        Math.Abs(mouse_y0 - mouse_y1) < 10)
                    {
                        // Ignore click
                    }
                    else
                    {
                        double new_x0 = pixToCoordX(mouse_x0);
                        double new_x1 = pixToCoordX(mouse_x1);
                        double new_y0 = pixToCoordY(mouse_y0);
                        double new_y1 = pixToCoordY(mouse_y1);
                        props.toolStripStatusLabel1.Text = "Mouse up" + e.X + " " + e.Y + " " + new_x0 + "," + new_y0 + " " + new_x0 + "," + new_y1;
                        _mandelbrotWindow.CenterX = (new_x0 + new_x1) / 2;
                        _mandelbrotWindow.CenterY = (new_y0 + new_y1) / 2;
                        double oldHeight = _mandelbrotWindow.Height;
                        _mandelbrotWindow.Height = Math.Abs(new_y1 - new_y0);
                        // _mandelbrotWindow.Width = Math.Abs(new_x1 - new_x0);
                        _mandelbrotWindow.Width = _mandelbrotWindow.Height * _mandelbrotWindow.Width / oldHeight;
                        UpdateImageAsync();
                        // zoom
                    }

                }
                else
                {
                    props.toolStripStatusLabel1.Text = "ignored2";
                }
            }
        }

        public void UpdateImageAsync()
        {
            // If there's currently an active task, cancel it!  We don't care about it anymore.
            if (_lastCancellation != null) _lastCancellation.Cancel();

            // Get the current size of the picture box
            Size renderSize = mandelbrotPb.Size;


            props.setDims(pixToCoordX(0), pixToCoordY(0), pixToCoordX(renderSize.Width), pixToCoordY(renderSize.Height));

            // Keep track of the time this request was made.  If multiple requests are executing,
            // we want to only render the most recent one available rather than overwriting a more
            // recent image with an older one.
            DateTime timeOfRequest = DateTime.UtcNow;

            // Start a task to asynchronously render the fractal, and store the task
            // so we can cancel it later as necessary
            _lastCancellation = new CancellationTokenSource();
            var token = _lastCancellation.Token;
            currentTask = Task.Factory.StartNew(() =>
            {
                // For diagnostic reasons, time how long the rendering takes
                Stopwatch sw = Stopwatch.StartNew();

                // Render the fractal
                bmp = MandelbrotGenerator.Create(mandelProps, _mandelbrotWindow, renderSize.Width, renderSize.Height, token, _parallelRendering);
                mandelProps.julia = false;
                if (bmp != null)
                {
                    sw.Stop();
                    double ppms = (renderSize.Width * renderSize.Height) / (double)sw.ElapsedMilliseconds;

                    // Update the fractal image asynchronously on the UI thread
                    Image old = null;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        // If this image is the most recent, store it into the picture box
                        // making sure to free the resources for the one currently in use.
                        // And update the form's title to reflect the rendering time.
                        if (timeOfRequest > _lastUpdateTime)
                        {
                            old = mandelbrotPb.Image;
                            mandelbrotPb.Image = bmp;
                            _lastUpdateTime = timeOfRequest;
                            this.Text = string.Format(_formTitle, _parallelRendering ? Environment.ProcessorCount : 1, ppms.ToString("F2"), sw.ElapsedMilliseconds.ToString());
                        }
                        // If the image isn't the most recent, just get rid of it
                        else bmp.Dispose();
                    });
                    if (old != null) old.Dispose();
                }
            }, token);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.R)
            {
                _mandelbrotWindow = MandelbrotPosition.Default;

                using (MainForm tempForm = new MainForm())
                {
                    double xFactor = Size.Width / (double)tempForm.Width, yFactor = Size.Height / (double)tempForm.Height;
                    _mandelbrotWindow.Width *= xFactor;
                    _mandelbrotWindow.Height *= yFactor;
                }

                UpdateImageAsync();
            }
            else if (e.KeyCode == Keys.S)
            {
                _parallelRendering = false;
                UpdateImageAsync();
            }
            else if (e.KeyCode == Keys.P)
            {
                _parallelRendering = true;
                UpdateImageAsync();
            }
        }

        bool inBand = false;
        int mouse_x0;
        int mouse_x1;
        int mouse_y0;
        int mouse_y1;

        public void runLoop()
        {
            double step = 0.0002;
            String dir = "E:\\Users\\Ken\\Desktop\\images#" + mandelProps.juliax + "#" + mandelProps.juliay + "#" + step + "#"
                + mandelProps.max_iter + "#" + mandelProps.algorithm + "#" + mandelProps.cmap;
            Directory.CreateDirectory(dir);
            for (int i = 0; i < 2000; i++)
            {
                mandelProps.julia = true;
                props.windowLabel.Text = "" + i;
                UpdateImageAsync();
                currentTask.Wait();
                String file = dir + "\\pic" + i + ".png";                
                FileStream myStream = new FileStream(file, FileMode.Create);
                using (myStream)
                {
                    bmp.Save(myStream, ImageFormat.Png);
                }
                mandelProps.juliax += step;
                //mandelProps.juliay += step;
               
            }
        }

        public static Bitmap Indexed2Image(Image img, System.Drawing.Imaging.PixelFormat fmt)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height, fmt);
            Graphics gr = Graphics.FromImage(bmp);
            gr.DrawImage(img, 0, 0);
            gr.Dispose();
            return bmp;
        }

        public void overlay()
        {

            Bitmap bmp2 = Indexed2Image(bmp, PixelFormat.Format32bppArgb);
            int x = coordToPixX(mandelProps.juliax);
            int y = coordToPixY(mandelProps.juliay);
            //props.juliaTextBoxX.Text = "" + x + " " + y + " " + mandelProps.juliax + " " + mandelProps.juliay;

                bmp2.SetPixel(x-1, y, Color.White);
                bmp2.SetPixel(x, y, Color.White);
                bmp2.SetPixel(x+1, y, Color.White);
                bmp2.SetPixel(x, y-1, Color.White);
                bmp2.SetPixel(x, y+1, Color.White);
            _lastCancellation = new CancellationTokenSource();
            var token = _lastCancellation.Token;
            BeginInvoke((MethodInvoker)delegate
                 {
                     mandelbrotPb.Image = bmp2;
                     bmp = bmp2;
                 }, token);
        }

        private void DrawReversibleRectangle()
        {
            Rectangle rc = new Rectangle();
            Point p1 = PointToScreen(new Point(mouse_x0, mouse_y0));
            Point p2 = PointToScreen(new Point(mouse_x1, mouse_y1));
            rc.X = Math.Min(p1.X, p2.X);
            rc.Y = Math.Min(p1.Y, p2.Y);
            rc.Width = Math.Abs(p1.X - p2.X);
            rc.Height = Math.Abs(p1.Y - p2.Y);
            ControlPaint.DrawReversibleFrame(rc, Color.Red, FrameStyle.Thick);
        }

        private void props_Click(object sender, EventArgs e)
        {
            PropsDialog dlg1 = new PropsDialog(this);
            dlg1.loadFields(mandelProps);
            dlg1.Show();
        }

        private void mandelbrotPb_MouseClick(object sender, MouseEventArgs e)
        {
           if (e.Button == MouseButtons.Middle)
           {
               props.windowLabel.Text = "" + pixToCoordX(e.X) + " " + pixToCoordY(e.Y);
               mandelProps.filename = String.Format("julia#{0:F5}#{1:F5}.png", pixToCoordX(e.X), pixToCoordY(e.Y));
               mandelProps.julia = true;
               mandelProps.juliax = pixToCoordX(e.X);
               mandelProps.juliay = pixToCoordY(e.Y);
               props.juliaTextBoxX.Text = "" + mandelProps.juliax;
               props.juliaTextBoxY.Text = "" + mandelProps.juliay;
               UpdateImageAsync();
               // Orbits orbits = new Orbits(dials, 16);
               // orbits.show(pixToCoordX(e.X), pixToCoordY(e.Y));
            }
        }

        internal void enlarge()
        {
            this.Size = new System.Drawing.Size(1000, 1000);
        }

        internal void save()
        {
            Stream myStream = null;
            SaveFileDialog openFileDialog1 = new SaveFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "png files (*.png)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = mandelProps.filename;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            bmp.Save(myStream, ImageFormat.Png);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            
        }
    }
}


