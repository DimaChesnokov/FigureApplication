using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiguresLibrary;
using System.Drawing;

namespace WindowsFormsPaint
{
    internal class Create_Figure : Figure
    {
        Point firts_point;
        Point second_point;
        List<Point> Blueprint;
        public Create_Figure(List<Point> b)
        {
            Blueprint = b;
        }

        public Create_Figure(int x1, int y1, int x2, int y2, int x_center, int y_center) : base(x_center, y_center)
        {            
            firts_point = new Point(x1, y1);
            second_point = new Point(x2, y2);
            if (Blueprint.Count == 0)
                Blueprint.Add(firts_point);
            Blueprint.Add(second_point);
        }

        public override void Show(Graphics graphics, Pen pen, Brush brush)
        {
            for(int i = 1; i < Blueprint.Count; i++)
                graphics.DrawLine(pen, Blueprint[i-1], Blueprint[i]);
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
