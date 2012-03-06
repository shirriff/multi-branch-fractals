using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathLib;

namespace Microsoft
{
    class Orbits
    {
        Dials dials;
        int max;

        public Orbits(Dials dials, int max)
        {
            this.dials = dials;
            this.max = max;
            
        }

        public void show(double x, double y)
        {
            dials.reset();
            eval3(x, y, x, y, 0);
            dials.counts();
        }

        private void eval3(double x, double y, double xc, double yc, int n)
        {
            dials.set(n, x, y, null /* brush */);
            if (n == max)
            {
                return;
            }
            double x2 = x * x - y * y;
            double y2 = 2 * x * y;
            double xroot = 0, yroot = 0;
            CMath.csqrt(x, y, ref xroot, ref yroot);
            double newx = x2 * xroot - y2 * yroot + xc;
            double newy = x2 * yroot + y2 * xroot + yc;
            double newx2 = -x2 * xroot + y2 * yroot + xc;
            double newy2 = -x2 * yroot - y2 * xroot + yc;
            if (newx * newx + newy * newy < 4)
            {
                eval3(newx, newy, xc, yc, n + 1);
            }
            if (newx2 * newx2 + newy2 * newy2 < 4)
            {
                eval3(newx2, newy2, xc, yc, n + 1);
            }
        }
    }
}
