using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiguresLibrary;
using System.Drawing;

namespace WindowsFormsPaint
{
    internal class Line : Figure
    {
        //ds
        Point firts_point;
        Point second_point;

        public Line(int x1, int y1, int x2, int y2, int x_center, int y_center) : base(x_center,y_center)
        {
            firts_point = new Point(x1,y1);
            second_point = new Point(x2,y2);
        }

        public override void Show(Graphics graphics, Pen pen, Brush brush)
        {
            graphics.DrawLine(pen, firts_point, second_point);
        }

        public override Rectangle Region_Capture()
        {
            int a = Convert.ToInt32(Math.Abs(firts_point.X - second_point.X) * scale);
            int b = Convert.ToInt32(Math.Abs(second_point.Y - firts_point.X) * scale);

            int X = center.X - a / 2;
            int Y = center.Y - b / 2;
            return new Rectangle(X, Y, a, b);
        }
    }
}
