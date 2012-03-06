using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MathLib;

namespace Microsoft
{
    public class Evaluators
    {
        public static Evaluator evaluatorFactory(MandelProps mandelProps)
        {
            if (mandelProps.algorithm.Equals("Mandel"))
            {
                return new MandelEvaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("Frac25"))
            {
                return new Frac25Evaluator(mandelProps, 0);
            }
            else if (mandelProps.algorithm.Equals("Frac25a"))
            {
                return new Frac25Evaluator(mandelProps, 1);
            }
            else if (mandelProps.algorithm.Equals("Frac25c"))
            {
                return new Frac25CEvaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("Frac25d"))
            {
                return new Frac25DEvaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("Frac25Multi"))
            {
                return new Frac25MultiEvaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("Frac25Planes"))
            {
                return new Frac25PlanesEvaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("Cycles"))
            {
                return new CyclesEvaluator(mandelProps);
            }
             else if (mandelProps.algorithm.Equals("Cycles1"))
            {
                return new Cycles1Evaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("Cycles2"))
            {
                return new Cycles2Evaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("Zeros"))
            {
                return new ZerosEvaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("MandelZeros"))
            {
                return new MandelZerosEvaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("Frac15Multi"))
            {
                return new Frac15MultiEvaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("Frac25Planes2"))
            {
                return new Frac25Planes2Evaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("Escape25"))
            {
                return new Escape25Evaluator(mandelProps);
            }
            else if (mandelProps.algorithm.Equals("Frac375Multi"))
            {
                return new Frac375MultiEvaluator(mandelProps);
            }
            else
            {
                return new MandelEvaluator(mandelProps);
            }
        }

        class Frac25Evaluator : Evaluator
        {
            int max;
            int flag;

            public Frac25Evaluator(MandelProps props, int flag)
            {
                this.max = props.max_iter;
                this.flag = flag;
            }

            public int eval(double xx, double yy)
            {
                double x = 0;
                double y = 0;
                int i;
                for (i = 0; i < max; i++)
                {
                    double x2 = x * x - y * y;
                    double y2 = 2 * x * y;
                    double xroot = 0, yroot = 0;
                    CMath.csqrt(x, y, ref xroot, ref yroot);
                    if (flag == 1 && x < 0)
                    {
                        xroot = -xroot;
                        yroot = -yroot;
                    }
                    double newx = x2 * xroot - y2 * yroot + xx;
                    double newy = x2 * yroot + y2 * xroot + yy;
                    if (newx * newx + newy * newy > 4) break;
                    x = newx;
                    y = newy;
                }
                return i;
            }
        }

        class Frac25CEvaluator : Evaluator
        {
            int max;

            public Frac25CEvaluator(MandelProps props)
            {
                this.max = props.max_iter;          
            }

            public int eval(double xx, double yy)
            {
                double x = 0;
                double y = 0;
                int i;
                for (i = 0; i < max; i++)
                {
                    double x2 = x * x - y * y;
                    double y2 = 2 * x * y;
                    double xroot = 0, yroot = 0;
                    CMath.csqrt(x, y, ref xroot, ref yroot);
                    double x25 = x2 * xroot - y2 * yroot;
                    double y25 = x2 * yroot + y2 * xroot;
                    double newx = x25 + xx;
                    double newy = y25 + yy;
                    double newx2 = -x25 + xx;
                    double newy2 = -y25 + yy;
                    if ((x - newx) * (x - newx) + (y - newy) * (y - newy) <
                        (x - newx2) * (x - newx2) + (y - newy2) * (y - newy2))
                    {
                        x = newx;
                        y = newy;
                    }
                    else
                    {
                        x = newx2;
                        y = newy2;
                    }
                    if (x * x + y * y > 4) break;
                }
                return i;
            }
        }

        class Frac25DEvaluator : Evaluator
        {
            int max;

            public Frac25DEvaluator(MandelProps props)
            {
                this.max = props.max_iter;
            }

            public int eval(double xx, double yy)
            {
                double x = 0;
                double y = 0;
                int i;
                for (i = 0; i < max; i++)
                {
                    double x2 = x * x - y * y;
                    double y2 = 2 * x * y;
                    double xroot = 0, yroot = 0;
                    CMath.csqrt(x, y, ref xroot, ref yroot);
                    double newx = x2 * xroot - y2 * yroot;
                    double newy = x2 * yroot + y2 * xroot;
                    double newx2 = -x2 * xroot + y2 * yroot;
                    double newy2 = -x2 * yroot - y2 * xroot;
                    if ((x - newx) * (x - newx) + (y - newy) * (y - newy) >
                        (x - newx2) * (x - newx2) + (y - newy2) * (y - newy2))
                    {
                        x = newx + xx;
                        y = newy + yy;
                    }
                    else
                    {
                        x = newx2 + xx;
                        y = newy2 + yy;
                    }
                    if (x * x + y * y > 4) break;
                }
                return i;
            }
        }

        class Frac25MultiEvaluator : Evaluator
        {
            int max;
            double bound;
            bool julia;
            double juliax, juliay;

            public Frac25MultiEvaluator(MandelProps props)
            {
                this.max = props.max_iter;
                this.bound = props.tol > 1 ? props.tol : 4;
                this.julia = props.julia;
                this.juliax = props.juliax;
                this.juliay = props.juliay;
            }

            public int eval(double xx, double yy)
            {
                int c;
                if (julia)
                {
                    c = eval(xx, yy, juliax, juliay, 0);
                }
                else
                {
                    c = eval(xx, yy, xx, yy, 0);
                }
                if (c == 1 << max) return 255;
                if (c == 0 || max < 8) return c;
                double d = 1 + Math.Log(c + 1) / Math.Log(Math.Pow(2, max)) * 250;
                return (int) d;
            }

            int eval(double zx, double zy, double cx, double cy, int n)
            {
                if (n == max)
                {
                    return 1;
                }
                // zsquared is z^2, zroot is sqrt(z), z25 is z^2.5
                double zsquaredx = zx * zx - zy * zy;
                double zsquaredy = 2 * zx * zy;
                double zrootx = 0, zrooty = 0;
                CMath.csqrt(zx, zy, ref zrootx, ref zrooty);
                double z25x = zsquaredx * zrootx - zsquaredy * zrooty;
                double z25y = zsquaredx * zrooty + zsquaredy * zrootx;

                int c = 0;
                // Use the first root
                double newx1 =  z25x + cx;
                double newy1 =  z25y + cy;
                if (newx1 * newx1 + newy1 * newy1 < bound)
                {
                    c += eval(newx1, newy1, cx, cy, n + 1);
                }

                // Use the second root
                double newx2 = -z25x + cx;
                double newy2 = -z25y + cy;
                if (newx2 * newx2 + newy2 * newy2 < bound)
                {
                    c += eval(newx2, newy2, cx, cy, n + 1);
                }
                return c;
            }
        }

        class Frac25PlanesEvaluator : Evaluator
        {
            int max;
            int planeProp;

            public Frac25PlanesEvaluator(MandelProps mandelProps)
            {
                this.max = mandelProps.max_iter;
                this.planeProp = mandelProps.plane;
            }

            public int eval(double xx, double yy)
            {
                double res = eval3(xx, yy, xx, yy, 0, planeProp);
                if (res < 4)
                {
                    return (int)(32 / 5.0 * (4 - res));
                }
                else
                {
                    return (int)(128 + Math.Log10(res) * 5);
                }
            }


            double eval3(double x, double y, double xx, double yy, int n, int desiredPlane)
            {
                if (n == max)
                {
                    return x * x + y * y;
                }
                double tol = .02 * (1 << n);
                if (Math.Abs(x) < tol && Math.Abs(y) < tol) {
                    return 999;
                }
                if (Math.Abs(y) < tol && x < 0)
                {
                    return 0;
                }


                double x2 = x * x - y * y;
                double y2 = 2 * x * y;
                double xroot = 0, yroot = 0;
                CMath.csqrt(x, y, ref xroot, ref yroot);
                if ((desiredPlane & 1) == 1)
                {
                    xroot = -xroot;
                    yroot = -yroot;
                }

                double newx = x2 * xroot - y2 * yroot + xx;
                double newy = x2 * yroot + y2 * xroot + yy;
                return eval3(newx, newy, xx, yy, n + 1, desiredPlane >> 1);

            }
        }

        class Frac25Planes2Evaluator : Evaluator
        {
            int max;
            int planeProp;
            int planeCount ;

            public Frac25Planes2Evaluator(MandelProps mandelProps)
            {
                this.max = mandelProps.max_iter;
                this.planeProp = mandelProps.plane;
                this.planeCount = mandelProps.depth;
            }

            public int eval(double xx, double yy)
            {
                return evalPlane(xx, yy, xx, yy, 0, planeProp);
            }

            int eval3(double x, double y, double xx, double yy, int n)
            {
                if (n == max)
                {
                    return 1;
                }
                double x2 = x * x - y * y;
                double y2 = 2 * x * y;
                double xroot = 0, yroot = 0;
                CMath.csqrt(x, y, ref xroot, ref yroot);
                double newx = x2 * xroot - y2 * yroot + xx;
                double newy = x2 * yroot + y2 * xroot + yy;
                double newx2 = -x2 * xroot + y2 * yroot + xx;
                double newy2 = -x2 * yroot - y2 * xroot + yy;
                int c = 0;
                if (newx * newx + newy * newy < 4)
                {
                    c += eval3(newx, newy, xx, yy, n + 1);
                }
                if (newx2 * newx2 + newy2 * newy2 < 4)
                {
                    c += eval3(newx2, newy2, xx, yy, n + 1);
                }
                return c;
            }

            int evalPlane(double x, double y, double xx, double yy, int n, int desiredPlane)
            {
                if (n == planeCount)
                {
                    if (n == max)
                    {
                        return (int) (10 * (x * x + y * y));
                    }
                    else
                    {
                        return eval3(x, y, xx, yy, n);
                    }
                }

                double tol = .02 * (1 << n);
                if (Math.Abs(x) < tol && Math.Abs(y) < tol)
                {
                    return 200 + n;
                }
                if (Math.Abs(y) < tol && x < 0)
                {
                    return n;
                }

                double x2 = x * x - y * y;
                double y2 = 2 * x * y;
                double xroot = 0, yroot = 0;
                CMath.csqrt(x, y, ref xroot, ref yroot);
                if ((desiredPlane & (1 << (planeCount - n - 1))) != 0)
                {
                    xroot = -xroot;
                    yroot = -yroot;
                }

                double newx = x2 * xroot - y2 * yroot + xx;
                double newy = x2 * yroot + y2 * xroot + yy;
                return evalPlane(newx, newy, xx, yy, n + 1, desiredPlane);

            }
        }

        class Frac15MultiEvaluator : Evaluator
        {
            int max;

            public Frac15MultiEvaluator(MandelProps props)
            {
                this.max = props.max_iter;
            }

            public int eval(double xx, double yy)
            {
                return eval3(xx, yy, xx, yy, 0);
            }

            int eval3(double x, double y, double xx, double yy, int n)
            {
                if (n == max)
                {
                    return 1;
                }
                double x2 = x;
                double y2 = y;
                double xroot = 0, yroot = 0;
                CMath.csqrt(x, y, ref xroot, ref yroot);
                double newx = x2 * xroot - y2 * yroot + xx;
                double newy = x2 * yroot + y2 * xroot + yy;
                double newx2 = -x2 * xroot + y2 * yroot + xx;
                double newy2 = -x2 * yroot - y2 * xroot + yy;
                int c = 0;
                if (newx * newx + newy * newy < 16)
                {
                    c += eval3(newx, newy, xx, yy, n + 1);
                }
                if (newx2 * newx2 + newy2 * newy2 < 16)
                {
                    c += eval3(newx2, newy2, xx, yy, n + 1);
                }
                return c;
            }
        }

        class MandelEvaluator : Evaluator
        {
            int max;
            bool julia;
            double juliax, juliay;

            public MandelEvaluator(MandelProps props)
            {
                this.max = props.max_iter;
                this.julia = props.julia;
                this.juliax = props.juliax;
                this.juliay = props.juliay;

            }

            public int eval(double x0, double y0)
            {
                double x = x0;
                double y = y0;
                if (julia)
                {
                    x0 = juliax;
                    y0 = juliay;
                }
                int i;
                int n = max;

                for (i = 0; i < n; i++)
                {
                    double x1 = x * x - y * y + x0;
                    double y1 = 2 * x * y + y0;
                    if (x1 * x1 + y1 * y1 > 4)
                    {
                        break;
                    }
                    x = x1;
                    y = y1;
                }
                return i;
            }
        }
class CyclesEvaluator : Evaluator
        {
            int max;
            double tol;

            public CyclesEvaluator(MandelProps props)
            {
                this.max = props.max_iter;
                this.tol = props.tol;
            }

            bool isClose(double x0, double y0, double x1, double y1)
            {
                return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1) < tol;
            }

            // Return cycle length, or 999 if no cycle detected.
            private int evalcycle(double x0, double y0, double x, double y, double xc, double yc, int n)
            {
                if (n >= 6) return 999;

                double x2 = x * x - y * y;
                double y2 = 2 * x * y;
                double xroot = 0, yroot = 0;
                CMath.csqrt(x, y, ref xroot, ref yroot);
                double newx = x2 * xroot - y2 * yroot + xc;
                double newy = x2 * yroot + y2 * xroot + yc;
                double newx2 = -x2 * xroot + y2 * yroot + xc;
                double newy2 = -x2 * yroot - y2 * xroot + yc;
                if (isClose(x0, y0, newx, newy))
                {
                    return n;
                }
                else if (isClose(x0, y0, newx2, newy2))
                {
                    return n;
                }
                int ret1 = evalcycle(x0, y0, newx, newy, xc, yc, n + 1);
                int ret2 = evalcycle(x0, y0, newx2, newy2, xc, yc, n + 1);
                return Math.Min(ret1, ret2);
            }

            public int eval(double x0, double y0)
            {
                return eval3(x0, y0, x0, y0, 0);
            }

            int eval3(double x, double y, double xx, double yy, int n)
            {
                if (n == max)
                {
                    return evalcycle(x, y, x, y, xx, yy, 1);
                }
                double x2 = x * x - y * y;
                double y2 = 2 * x * y;
                double xroot = 0, yroot = 0;
                CMath.csqrt(x, y, ref xroot, ref yroot);
                double newx = x2 * xroot - y2 * yroot + xx;
                double newy = x2 * yroot + y2 * xroot + yy;
                double newx2 = -x2 * xroot + y2 * yroot + xx;
                double newy2 = -x2 * yroot - y2 * xroot + yy;
                int c1 = -1, c2 = -1;
                if (newx * newx + newy * newy < 4)
                {
                    c1 = eval3(newx, newy, xx, yy, n + 1);
                }
                if (newx2 * newx2 + newy2 * newy2 < 4)
                {
                    c2 = eval3(newx2, newy2, xx, yy, n + 1);
                }
                if (c1 > 0 && c2 > 0)
                {
                    return Math.Min(c1, c2);
                }
                else if (c1 > 0)
                {
                    return c1;
                }
                else if (c2 > 0)
                {
                    return c2;
                }
                else
                {
                    // Diverges
                    return -1;
                }
            }
        }
class Escape25Evaluator : Evaluator
{
    int max;
    double tol;
    bool julia;
    double juliax, juliay;

    public Escape25Evaluator(MandelProps props)
    {
        this.max = props.max_iter;
        this.tol = props.tol;
        this.julia = props.julia;
        this.juliax = props.juliax;
        this.juliay = props.juliay;
    }

    public int eval(double x0, double y0)
    {
        int val;
        if (julia)
        {
            val = eval3(x0, y0, juliax, juliay, 0);
        }
        else
        {
            val = eval3(x0, y0, x0, y0, 0);
        }
        if (val > 0) val = val % (256-32);
        if (val < -31) val = -31;
        return val;
    }

    // -n if escape after n iterations
    // n if n is the number of convergent planes
    int eval3(double x, double y, double xx, double yy, int n)
    {
        if (n == max)
        {
            return 1;
        }
        double x2 = x * x - y * y;
        double y2 = 2 * x * y;
        double xroot = 0, yroot = 0;
        CMath.csqrt(x, y, ref xroot, ref yroot);
        double newx = x2 * xroot - y2 * yroot + xx;
        double newy = x2 * yroot + y2 * xroot + yy;
        double newx2 = -x2 * xroot + y2 * yroot + xx;
        double newy2 = -x2 * yroot - y2 * xroot + yy;
        int c1, c2;
        if (newx * newx + newy * newy < 4)
        {
            c1 = eval3(newx, newy, xx, yy, n + 1);
        }
        else
        {
            c1 = -(n + 1);
        }
        if (newx2 * newx2 + newy2 * newy2 < 4)
        {
            c2 = eval3(newx2, newy2, xx, yy, n + 1);
        }
        else
        {
            c2 = -(n + 1);
        }
        if (c1 == 0 || c2 == 0) return 0;
        if (c1 > 0 && c2 > 0) return c1 + c2;
        if (c1 > 0) return c1;
        if (c2 > 0) return c2;
        return Math.Min(c1, c2);
    }
}
        class Cycles1Evaluator : Evaluator
        {
            int max;
            double tol;

            public Cycles1Evaluator(MandelProps props)
            {
                this.max = props.max_iter;
                this.tol = props.tol;
            }

            bool isClose(double x0, double y0, double x1, double y1)
            {
                return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1) < tol;
            }

            double dist2(double x0, double y0, double x1, double y1)
            {
                return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1);
            }

            public int eval(double x0, double y0)
            {
                double d = eval3(x0, y0, x0, y0, 0) / tol;
                if (d < 0)
                {
                    return 0;
                } else {
                    return Math.Min(255, (int) d);
                }
            }

            double eval3(double x, double y, double xx, double yy, int n)
            {

                double x2 = x * x - y * y;
                double y2 = 2 * x * y;
                double xroot = 0, yroot = 0;
                CMath.csqrt(x, y, ref xroot, ref yroot);
                double newx = x2 * xroot - y2 * yroot + xx;
                double newy = x2 * yroot + y2 * xroot + yy;
                double newx2 = -x2 * xroot + y2 * yroot + xx;
                double newy2 = -x2 * yroot - y2 * xroot + yy;
                if (n == max)
                {
                    return Math.Min(dist2(x, y, newx, newy), dist2(x, y, newx2, newy2));
                }
                double c1 = -1, c2 = -1;
                if (newx * newx + newy * newy < 4)
                {
                    c1 = eval3(newx, newy, xx, yy, n + 1);
                }
                if (newx2 * newx2 + newy2 * newy2 < 4)
                {
                    c2 = eval3(newx2, newy2, xx, yy, n + 1);
                }
                if (c1 > 0 && c2 > 0)
                {
                    return Math.Min(c1, c2);
                }
                else if (c1 > 0)
                {
                    return c1;
                }
                else if (c2 > 0)
                {
                    return c2;
                }
                else
                {
                    // Diverges
                    return -1;
                }
            }
        }

        class Cycles2Evaluator : Evaluator
        {
            int max;
            double tol;

            public Cycles2Evaluator(MandelProps props)
            {
                this.max = props.max_iter;
                this.tol = props.tol;
            }

            bool isClose(double x0, double y0, double x1, double y1)
            {
                return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1) < tol;
            }

            double dist2(double x0, double y0, double x1, double y1)
            {
                return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1);
            }

            public int eval(double x0, double y0)
            {
                double d = eval3(x0, y0, x0, y0, 0) / tol;
                if (d < 0)
                {
                    return 0;
                }
                else
                {
                    return Math.Min(255, (int)d);
                }
            }

            double eval3(double x, double y, double xx, double yy, int n)
            {

                double x2 = x * x - y * y;
                double y2 = 2 * x * y;
                double xroot = 0, yroot = 0;
                CMath.csqrt(x, y, ref xroot, ref yroot);
                double newx = x2 * xroot - y2 * yroot + xx;
                double newy = x2 * yroot + y2 * xroot + yy;
                double newx2 = -x2 * xroot + y2 * yroot + xx;
                double newy2 = -x2 * yroot - y2 * xroot + yy;
                if (n == max)
                {
                    CMath.csqrt(newx, newy, ref xroot, ref yroot);
                    double newAx = newx * xroot - newy * yroot + xx;
                    double newAy = newx * yroot + newy * xroot + yy;
                    double newAx2 = -newx * xroot + newy * yroot + xx;
                    double newAy2 = -newx * yroot - newy * xroot + yy;
                    CMath.csqrt(newx2, newy2, ref xroot, ref yroot);
                    double newBx = newx2 * xroot - newy2 * yroot + xx;
                    double newBy = newx2 * yroot + newy2 * xroot + yy;
                    double newBx2 = -newx2 * xroot + newy2 * yroot + xx;
                    double newBy2 = -newx2 * yroot - newy2 * xroot + yy;
                    return Math.Min(
                        Math.Min(dist2(x, y, newAx, newAy), dist2(x, y, newAx2, newAy2)),
                        Math.Min(dist2(x, y, newBx, newBy), dist2(x, y, newBx2, newBy2)));
                }
                double c1 = -1, c2 = -1;
                if (newx * newx + newy * newy < 4)
                {
                    c1 = eval3(newx, newy, xx, yy, n + 1);
                }
                if (newx2 * newx2 + newy2 * newy2 < 4)
                {
                    c2 = eval3(newx2, newy2, xx, yy, n + 1);
                }
                if (c1 > 0 && c2 > 0)
                {
                    return Math.Min(c1, c2);
                }
                else if (c1 > 0)
                {
                    return c1;
                }
                else if (c2 > 0)
                {
                    return c2;
                }
                else
                {
                    // Diverges
                    return -1;
                }
            }
        }

        class ZerosEvaluator : Evaluator
        {
            int max;
            double tol;

            public ZerosEvaluator(MandelProps props)
            {
                this.max = props.max_iter;
                this.tol = props.tol;
            }

            bool isClose(double x0, double y0, double x1, double y1)
            {
                return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1) < tol;
            }

            double dist2(double x0, double y0, double x1, double y1)
            {
                return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1);
            }

            public int eval(double x0, double y0)
            {
               return eval3(x0, y0, x0, y0, 0);
            }

            // Return 0 to 254 for finite, 255 for divergent
            int eval3(double x, double y, double xx, double yy, int n)
            {
                double mag = x * x + y * y;
                if (n == max)
                {
                    int val = (int) (mag / tol);
                    if (val > 254)
                    {
                        val = 254;
                    }
                    return val;
                    
                }
                else if (mag > 4)
                {
                    return 255;
                }
                double x2 = x * x - y * y;
                double y2 = 2 * x * y;
                double xroot = 0, yroot = 0;
                CMath.csqrt(x, y, ref xroot, ref yroot);
                double newx = x2 * xroot - y2 * yroot + xx;
                double newy = x2 * yroot + y2 * xroot + yy;
                double newx2 = -x2 * xroot + y2 * yroot + xx;
                double newy2 = -x2 * yroot - y2 * xroot + yy;
                int c1 = eval3(newx, newy, xx, yy, n + 1);
                int c2 = eval3(newx2, newy2, xx, yy, n + 1);
                return Math.Min(c1, c2);
            }
        }

        class MandelZerosEvaluator : Evaluator
        {
            int max;
            double tol;

            public MandelZerosEvaluator(MandelProps props)
            {
                this.max = props.max_iter;
                this.tol = props.tol;
            }

            bool isClose(double x0, double y0, double x1, double y1)
            {
                return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1) < tol;
            }

            double dist2(double x0, double y0, double x1, double y1)
            {
                return (x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1);
            }

            public int eval(double x0, double y0)
            {
                return eval3(x0, y0, x0, y0, 0);
            }

            // Return 0 to 254 for finite, 255 for divergent
            int eval3(double x, double y, double xx, double yy, int n)
            {
                double mag = x * x + y * y;
                if (n == max)
                {
                    int val = (int)(mag / tol);
                    if (val > 254)
                    {
                        val = 254;
                    }
                    return val;

                }
                else if (mag > 4)
                {
                    return 255;
                }
                double x2 = x * x - y * y;
                double y2 = 2 * x * y;
                double newx = x2 + xx;
                double newy = y2 + yy;
                return eval3(newx, newy, xx, yy, n + 1);
            }
        }

        class Frac375MultiEvaluator : Evaluator
        {
            int max;
            double bound;
            bool julia;
            double juliax, juliay;

            public Frac375MultiEvaluator(MandelProps props)
            {
                this.max = props.max_iter;
                this.bound = props.tol > 1 ? props.tol : 4;
                this.julia = props.julia;
                this.juliax = props.juliax;
                this.juliay = props.juliay;
            }

            public int eval(double xx, double yy)
            {
                int val;
                if (julia)
                {
                    val = eval(xx, yy, juliax, juliay, 0);
                }
                else
                {
                    val = eval(xx, yy, xx, yy, 0);
                }
                if (val < 0)
                {
                    return 0;
                }
                else
                {
                    double d = 1 + Math.Log(val + 1) / Math.Log(Math.Pow(4, max)) * 250;
                    return (int)d;
                }
            }

            // -n if escape after n iterations
            // n if n is the number of convergent planes
            int eval(double zx, double zy, double cx, double cy, int n)
            {
                if (n == max)
                {
                    return 1;
                }
                // z2 is z^2, zroot is sqrt(z), z25 is z^2.5
                double z2x = zx * zx - zy * zy;
                double z2y = 2 * zx * zy;
                double z3x = zx * z2x - zy * z2y;
                double z3y = zx * z2y + z2x * zy;
                double zrootx = 0, zrooty = 0;
                CMath.csqrt(zx, zy, ref zrootx, ref zrooty);
                double z4rootx = 0, z4rooty = 0;
                CMath.csqrt(zrootx, zrooty, ref z4rootx, ref z4rooty);
                double z35x = z3x * zrootx - z3y * zrooty;
                double z35y = z3x * zrooty + zrootx * z3y;
                double z375x = z35x * z4rootx - z35y * z4rooty;
                double z375y = z35x * z4rooty + z4rootx * z35y;

                int c1, c2, c3, c4;
                // Use the first root
                double newx1 = z375x + cx;
                double newy1 = z375y + cy;
                if (newx1 * newx1 + newy1 * newy1 < bound)
                {
                    c1 = eval(newx1, newy1, cx, cy, n + 1);
                } else {
                    c1 = -(n+1);
                }

                // Use the second root
                double newx2 = -z375x + cx;
                double newy2 = -z375y + cy;
                if (newx2 * newx2 + newy2 * newy2 < bound)
                {
                    c2 = eval(newx2, newy2, cx, cy, n + 1);
                } else {
                    c2 = -(n+1);
                }

                // Use the third root
                double newx3 = -z375y + cx;
                double newy3 = z375x + cy;
                if (newx3 * newx3 + newy3 * newy3 < bound)
                {
                    c3 = eval(newx3, newy3, cx, cy, n + 1);
                } else {
                    c3 = -(n+1);
                }

                // Use the fourth root
                double newx4 = z375y + cx;
                double newy4 = -z375x + cy;
                if (newx4 * newx4 + newy4 * newy4 < bound)
                {
                    c4 = eval(newx4, newy4, cx, cy, n + 1);
                } else {
                    c4 = -(n+1);
                }
                if (c1 == 0 || c2 == 0 || c3 == 0 || c4 == 0) return 0;
                int s = 0;
                if (c1 > 0) s += c1;
                if (c2 > 0) s += c2;
                if (c3 > 0) s += c3;
                if (c4 > 0) s += c4;
                if (s > 0) return s;
                return Math.Min(Math.Min(c1, c2), Math.Min(c3, c4));
            }
        }
    }
}

