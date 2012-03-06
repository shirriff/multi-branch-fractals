namespace Microsoft
{
    partial class Dials
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Dials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 322);
            this.Name = "Dials";
            this.Text = "Dials";
            this.Load += new System.EventHandler(this.Dials_Load);
            this.Shown += new System.EventHandler(this.Dials_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Dials_Paint);
            this.Resize += new System.EventHandler(this.Dials_Resize);
            this.ResumeLayout(false);

        }

        #endregion

    }
}