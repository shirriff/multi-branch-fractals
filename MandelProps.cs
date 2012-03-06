using System;

namespace Microsoft
{
    public class MandelProps
    {
        public int max_iter = 100;
        public String algorithm = "Mandel";
        public String cmap = "Random";
        public MandelbrotPosition mandelbrotWindow;
        public MainForm mainForm;
        public int plane = 0;
        public int depth = 2;
        public double tol = .001;
        public bool scaleCmap = false;
        public bool julia = false;
        public double juliax, juliay;
        public String filename;

        public MandelProps(MainForm mainForm, MandelbrotPosition mandelbrotWindow)
        {
            this.mandelbrotWindow = mandelbrotWindow;
            this.mainForm = mainForm;
        }
    }

}
