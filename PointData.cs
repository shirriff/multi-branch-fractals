using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using MathLib;

namespace Microsoft
{
    public class Element {
        public double x;
        public double y;
        public Element(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class PointData
    {
        public ArrayList[] points;
        public int max;

        public PointData()
        {

            

        }

        public void create(double x, double y, int max)
        {
            this.max = max;
            for (int i = 0; i < max; i++) {
                points[i] = new ArrayList();
            }

            createInt(x, y, x, y, 0);
        }

        private void createInt(double x, double y, double xx, double yy, int n)
        {
            if (n >= max) return;
            points[n].Add(new Element(x, y));

            double x2 = x * x - y * y;
            double y2 = 2 * x * y;
            double xroot = 0, yroot = 0;
            CMath.csqrt(x, y, ref xroot, ref yroot);
            double newx = x2 * xroot - y2 * yroot + xx;
            double newy = x2 * yroot + y2 * xroot + yy;
            double newx2 = -x2 * xroot + y2 * yroot + xx;
            double newy2 = -x2 * yroot - y2 * xroot + yy;
            createInt(newx, newy, xx, yy, n + 1);
            createInt(newx2, newy2, xx, yy, n + 1);


        }

        public void findCycle()
        {
        }

    }
}
