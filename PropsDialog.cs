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
    public partial class PropsDialog : Form
    {

        private MandelProps mandelProps;
        private MainForm mainForm;

        public PropsDialog(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        public void loadFields(MandelProps mandelProps)
        {
            this.mandelProps = mandelProps;
            textBox1.Text = "" + mandelProps.max_iter;
            planeBox.Text = "" + mandelProps.plane;
            depthBox.Text = "" + mandelProps.depth;
            tolerance.Text = "" + mandelProps.tol;
            scaleCmapBox.Checked = mandelProps.scaleCmap;
            juliaTextBoxX.Text = "" + mandelProps.juliax;
            juliaTextBoxY.Text = "" + mandelProps.juliay;

            int index = comboBox1.FindString(mandelProps.algorithm);
            comboBox1.SelectedIndex = index;
            index = comboBox2.FindString(mandelProps.cmap);
            comboBox2.SelectedIndex = index;
            windowLabel.Text = "w/h/center: " + mandelProps.mandelbrotWindow.Width + " " + mandelProps.mandelbrotWindow.Height
                + mandelProps.mandelbrotWindow.CenterX + " " + mandelProps.mandelbrotWindow.CenterY;
        }

        public void apply()
        {
            try
            {
                mandelProps.max_iter = Convert.ToInt32(textBox1.Text);
                mandelProps.plane = Convert.ToInt32(planeBox.Text);
                mandelProps.depth = Convert.ToInt32(depthBox.Text);
                mandelProps.tol = Convert.ToDouble(tolerance.Text);
                mandelProps.juliax = Convert.ToDouble(juliaTextBoxX.Text);
                mandelProps.juliay = Convert.ToDouble(juliaTextBoxY.Text);
            }
            catch (FormatException)
            {
                return;
            }

            mandelProps.algorithm = comboBox1.SelectedItem.ToString();
            mandelProps.cmap = comboBox2.SelectedItem.ToString();
            windowLabel.Text = "w/h/center: " + mandelProps.mandelbrotWindow.Width + " " + mandelProps.mandelbrotWindow.Height
    + mandelProps.mandelbrotWindow.CenterX + " " + mandelProps.mandelbrotWindow.CenterY;
            mandelProps.scaleCmap = scaleCmapBox.Checked;
            mandelProps.julia = juliaBox.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            apply();
            mandelProps.mainForm.UpdateImageAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mandelProps.mandelbrotWindow.CenterX = MandelbrotPosition.Default.CenterX;
            mandelProps.mandelbrotWindow.CenterY = MandelbrotPosition.Default.CenterY;
            mandelProps.mandelbrotWindow.Width = MandelbrotPosition.Default.Width;
            mandelProps.mandelbrotWindow.Height = MandelbrotPosition.Default.Height;
            mandelProps.mainForm.UpdateImageAsync();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void redraw_Click(object sender, EventArgs e)
        {
            mandelProps.mainForm.UpdateImageAsync();
        }



        internal void setDims(double x0, double y0, double x1, double y1)
        {
            dimensionsLabel.Text = "(" + x0 + "," + y0 + ") (" + x1 + "," + y1 + ")";
        }

        private void resizeButton_Click(object sender, EventArgs e)
        {
            mainForm.enlarge();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            mainForm.save();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            mainForm.runLoop();
        }

        private void overlay(object sender, EventArgs e)
        {
            mandelProps.juliax = Convert.ToDouble(juliaTextBoxX.Text);
            mandelProps.juliay = Convert.ToDouble(juliaTextBoxY.Text);
            mainForm.overlay();
        }
    }
}
