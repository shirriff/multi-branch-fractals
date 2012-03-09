namespace Microsoft
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public static int WIDTH = 512;
        public static int HEIGHT = 512;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mandelbrotPb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mandelbrotPb)).BeginInit();
            this.SuspendLayout();
            // 
            // mandelbrotPb
            // 
            this.mandelbrotPb.BackColor = System.Drawing.Color.Black;
            this.mandelbrotPb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mandelbrotPb.Location = new System.Drawing.Point(0, 0);
            this.mandelbrotPb.Name = "mandelbrotPb";
            this.mandelbrotPb.Size = new System.Drawing.Size(512, 512);
            this.mandelbrotPb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.mandelbrotPb.TabIndex = 0;
            this.mandelbrotPb.TabStop = false;
            this.mandelbrotPb.VisibleChanged += new System.EventHandler(this.mandelbrotPb_VisibleChanged);
            this.mandelbrotPb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mandelbrotPb_MouseClick);
            this.mandelbrotPb.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mandelbrotPb_MouseDoubleClick);
            this.mandelbrotPb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mandelbrotPb_MouseDown);
            this.mandelbrotPb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mandelbrotPb_MouseMove);
            this.mandelbrotPb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mandelbrotPb_MouseUp);
            this.mandelbrotPb.Resize += new System.EventHandler(this.mandelbrotPb_Resize);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 512);
            this.Controls.Add(this.mandelbrotPb);
            this.Name = "MainForm";
            this.Text = "Mandelbrot Fractals";
            ((System.ComponentModel.ISupportInitialize)(this.mandelbrotPb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mandelbrotPb;
    }
}

