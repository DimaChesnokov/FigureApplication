using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiguresLibrary;

namespace WindowsFormsPaint
{
    internal class MyRectangle : Figure
    {
        double a_side;
        double b_side;

        public MyRectangle(double a, double b, int x, int y) : base(x, y)
        {
            this.a_side = a;
            this.b_side = b;
        }

        public override void Show(Graphics graphics, Pen pen, Brush brush)
        {
            int a = Convert.ToInt32(a_side * scale);
            int b = Convert.ToInt32(b_side * scale);

            int X = center.X - a / 2 ;
            int Y = center.Y - b / 2;
            Rectangle rect = new Rectangle(X, Y, a, b);
            graphics.DrawRectangle(pen, rect);
            graphics.FillRectangle(brush, rect);
        }

        public override Rectangle Region_Capture()
        {
            int a = Convert.ToInt32(a_side * scale);
            int b = Convert.ToInt32(b_side * scale);

            int X = center.X - a / 2;
            int Y = center.Y - b / 2;
            return new Rectangle(X, Y, a, b);
        }
    }
}
