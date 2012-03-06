using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Microsoft
{
    public partial class Dials : Form
    {
        int n;
        int dialW, dialH;
        int w, h;
        int maxSize;
        double xmin, xmax, ymin, ymax;
        Graphics surface;
        Brush brush = new SolidBrush(Color.Red);
        int[] count;

        public Dials(int n)
        {
            InitializeComponent();
            w = 100;
            h = 100;
            this.n = n;
            dialW = 90;
            dialH = 90;
            xmin = -2;
            xmax = 2;
            ymin = -2;
            ymax = 2;
        }

        public void set(int n, double x, double y, Brush brush)
        {
            if (brush == null)
            {
                brush = this.brush;
            }
            int boxX = getBoxX(n);
            int boxY = getBoxY(n);
            int xpix = (int) ((x - xmin) / (xmax - xmin) * dialW);
            int ypix = (int) ((y - ymin) / (ymax - ymin) * dialH);
            xpix = Math.Min(dialW, Math.Max(0, xpix));
            ypix = Math.Min(dialH, Math.Max(0, ypix));
            surface.FillRectangle(brush, dialW * boxX + xpix, dialH * boxY + ypix, 2, 2);
            count[n]++;
        }

        private int getBoxX(int n)
        {
            int perRow = w / dialW;
            return n % perRow;
        }

        private int getBoxY(int n)
        {
            int perRow = w / dialW;
            return n / perRow;
        }

        private void Dials_Resize(object sender, EventArgs e)
        {
            reset();
        }
        
        internal void reset() {
            surface = this.CreateGraphics();
            Brush brush = new SolidBrush(Color.White);
            surface.FillRectangle(brush, 0, 0, w, h);
            brush.Dispose();
            w = this.Width - 2 * SystemInformation.FrameBorderSize.Width;
            h = this.Height - 2 * SystemInformation.FrameBorderSize.Width - SystemInformation.CaptionHeight;
            Pen blackPen = new Pen(Color.Black, 1);
            Brush redBrush = new SolidBrush(Color.Gray);
            int i;
            for (i = 0; ; i++)
            {
                int boxX = getBoxX(i);
                int boxY = getBoxY(i);
                if ((boxY + 1) * dialH >= h) break;
                surface.DrawRectangle(blackPen, dialW * boxX, dialH * boxY, dialW, dialH);
                //set(i, 0, 0, redBrush);
            }
            maxSize = i;
            //surface.Dispose();
            //surface = null;
            count = new int[25];
        }

        private void Dials_Shown(object sender, EventArgs e)
        {
            //Dials_Resize(sender, e);
        }

        private void Dials_Load(object sender, EventArgs e)
        {
            //Dials_Resize(sender, e);
        }

        private void Dials_Paint(object sender, PaintEventArgs e)
        {
            reset();
        }

        internal void counts()
        {
            Brush brush = new SolidBrush(Color.Black);
            Font font = DefaultFont;
            for (int i = 0; i < 20; i++)
            {
                int boxX = getBoxX(i);
                int boxY = getBoxY(i);
                surface.DrawString("" + count[i], font, brush, dialW * boxX, dialH * boxY);
            }
            brush.Dispose();
        }
    }
}
