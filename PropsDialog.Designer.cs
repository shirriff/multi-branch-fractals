namespace Microsoft
{
    partial class PropsDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.windowLabel = new System.Windows.Forms.Label();
            this.reset = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.planeBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tolerance = new System.Windows.Forms.TextBox();
            this.redraw = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.depthBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.scaleCmapBox = new System.Windows.Forms.CheckBox();
            this.dimensionsLabel = new System.Windows.Forms.Label();
            this.resizeButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.juliaTextBoxX = new System.Windows.Forms.TextBox();
            this.juliaTextBoxY = new System.Windows.Forms.TextBox();
            this.juliaBox = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Iterations";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(84, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Algorithm";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Mandel",
            "Frac25",
            "Frac25a",
            "Frac25c",
            "Frac25d",
            "Frac25Multi",
            "Frac25Planes",
            "Frac25Planes2",
            "Cycles",
            "Cycles1",
            "Cycles2",
            "Zeros",
            "MandelZeros",
            "Frac15Multi",
            "Frac375Multi",
            "Escape25"});
            this.comboBox1.Location = new System.Drawing.Point(84, 53);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Colormap";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Blue",
            "Blue1",
            "Hue",
            "128",
            "Random",
            "Thing1",
            "Cycles",
            "Thing2",
            "Grayscale",
            "InOut"});
            this.comboBox2.Location = new System.Drawing.Point(84, 95);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(196, 333);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // windowLabel
            // 
            this.windowLabel.AutoSize = true;
            this.windowLabel.Location = new System.Drawing.Point(19, 226);
            this.windowLabel.Name = "windowLabel";
            this.windowLabel.Size = new System.Drawing.Size(46, 13);
            this.windowLabel.TabIndex = 7;
            this.windowLabel.Text = "Window";
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(12, 333);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(75, 23);
            this.reset.TabIndex = 8;
            this.reset.Text = "Reset";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Plane";
            // 
            // planeBox
            // 
            this.planeBox.Location = new System.Drawing.Point(84, 161);
            this.planeBox.Name = "planeBox";
            this.planeBox.Size = new System.Drawing.Size(58, 20);
            this.planeBox.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Tol";
            // 
            // tolerance
            // 
            this.tolerance.Location = new System.Drawing.Point(84, 195);
            this.tolerance.Name = "tolerance";
            this.tolerance.Size = new System.Drawing.Size(100, 20);
            this.tolerance.TabIndex = 13;
            // 
            // redraw
            // 
            this.redraw.Location = new System.Drawing.Point(104, 333);
            this.redraw.Name = "redraw";
            this.redraw.Size = new System.Drawing.Size(75, 23);
            this.redraw.TabIndex = 14;
            this.redraw.Text = "Redraw";
            this.redraw.UseVisualStyleBackColor = true;
            this.redraw.Click += new System.EventHandler(this.redraw_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(171, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Depth";
            // 
            // depthBox
            // 
            this.depthBox.Location = new System.Drawing.Point(213, 161);
            this.depthBox.Name = "depthBox";
            this.depthBox.Size = new System.Drawing.Size(58, 20);
            this.depthBox.TabIndex = 16;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 359);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(446, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // scaleCmapBox
            // 
            this.scaleCmapBox.AutoSize = true;
            this.scaleCmapBox.Location = new System.Drawing.Point(224, 98);
            this.scaleCmapBox.Name = "scaleCmapBox";
            this.scaleCmapBox.Size = new System.Drawing.Size(53, 17);
            this.scaleCmapBox.TabIndex = 19;
            this.scaleCmapBox.Text = "Scale";
            this.scaleCmapBox.UseVisualStyleBackColor = true;
            // 
            // dimensionsLabel
            // 
            this.dimensionsLabel.AutoSize = true;
            this.dimensionsLabel.Location = new System.Drawing.Point(19, 260);
            this.dimensionsLabel.Name = "dimensionsLabel";
            this.dimensionsLabel.Size = new System.Drawing.Size(35, 13);
            this.dimensionsLabel.TabIndex = 20;
            this.dimensionsLabel.Text = "label8";
            // 
            // resizeButton
            // 
            this.resizeButton.Location = new System.Drawing.Point(359, 13);
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.Size = new System.Drawing.Size(75, 23);
            this.resizeButton.TabIndex = 21;
            this.resizeButton.Text = "Resize";
            this.resizeButton.UseVisualStyleBackColor = true;
            this.resizeButton.Click += new System.EventHandler(this.resizeButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(359, 53);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 22;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // juliaTextBoxX
            // 
            this.juliaTextBoxX.Location = new System.Drawing.Point(19, 289);
            this.juliaTextBoxX.Name = "juliaTextBoxX";
            this.juliaTextBoxX.Size = new System.Drawing.Size(58, 20);
            this.juliaTextBoxX.TabIndex = 24;
            // 
            // juliaTextBoxY
            // 
            this.juliaTextBoxY.Location = new System.Drawing.Point(104, 290);
            this.juliaTextBoxY.Name = "juliaTextBoxY";
            this.juliaTextBoxY.Size = new System.Drawing.Size(58, 20);
            this.juliaTextBoxY.TabIndex = 25;
            // 
            // juliaBox
            // 
            this.juliaBox.AutoSize = true;
            this.juliaBox.Location = new System.Drawing.Point(174, 293);
            this.juliaBox.Name = "juliaBox";
            this.juliaBox.Size = new System.Drawing.Size(47, 17);
            this.juliaBox.TabIndex = 26;
            this.juliaBox.Text = "Julia";
            this.juliaBox.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(359, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 27;
            this.button2.Text = "RunLoop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(227, 288);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 28;
            this.button3.Text = "Overlay";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.overlay);
            // 
            // PropsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 381);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.juliaBox);
            this.Controls.Add(this.juliaTextBoxY);
            this.Controls.Add(this.juliaTextBoxX);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.resizeButton);
            this.Controls.Add(this.dimensionsLabel);
            this.Controls.Add(this.scaleCmapBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.depthBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.redraw);
            this.Controls.Add(this.tolerance);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.planeBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.windowLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "PropsDialog";
            this.Text = "Properties";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label windowLabel;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox planeBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tolerance;
        private System.Windows.Forms.Button redraw;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox depthBox;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.CheckBox scaleCmapBox;
        private System.Windows.Forms.Label dimensionsLabel;
        private System.Windows.Forms.Button resizeButton;
        private System.Windows.Forms.Button saveButton;
        public System.Windows.Forms.TextBox juliaTextBoxX;
        public System.Windows.Forms.TextBox juliaTextBoxY;
        private System.Windows.Forms.CheckBox juliaBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}