using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiguresLibrary;

namespace WindowsFormsPaint
{
    internal class Ellipse : Figure
    {
        int axisA;
        int axisB;

        public Ellipse(int A,int B,int x,int y) : base(x,y)
        {
            axisA = A;
            axisB = B;
        }

        public override void Show(Graphics graphics, Pen pen, Brush brush)
        {
            int A = Convert.ToInt32(axisA * scale);
            int B = Convert.ToInt32(axisB * scale);

            int X = center.X - A;
            int Y = center.Y - B;
            Rectangle rect = new Rectangle(X,Y,2*A,2*B);
            graphics.DrawEllipse(pen,rect);
            graphics.FillEllipse(brush, rect);
        }

        public override Rectangle Region_Capture()
        {
            int A = Convert.ToInt32(axisA * scale);
            int B = Convert.ToInt32(axisB * scale);

            int X = center.X - A;
            int Y = center.Y - B;
            return new Rectangle(X, Y, 2 * A, 2 * B);
        }
    }
}
