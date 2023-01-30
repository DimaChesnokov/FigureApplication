using System;
using System.Drawing;

namespace FiguresApp
{
    public abstract class Figure
    {
        protected Point center;
        protected double scale;
        protected bool dragged;
        protected Color color;

        public Point Center_of_figure
        {
            get { return center; }
            set { center = value; }
        }

        public double Scale_of_figure
        {
            get { return scale; }
            set { scale = value; }
        }

        public bool Dragging
        {
            get { return dragged; }
            set { dragged = value; }
        }

        public Color Color_of_figure
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Базовый конструктор фигуры
        /// </summary>
        /// <param name="x"> Координата на оси X </param>
        /// <param name="y"> Координата на оси Y </param>
        public Figure(int x, int y)
        {
            center = new Point(x, y);
            scale = 1;
            dragged = false;
            color = Color.Black;
        }

        public void Moving(int x, int y)
        {
            center.X += x;
            center.Y += y;
        }

        public void Scaling(double multipler_of_scale)
        {
            scale *= multipler_of_scale;
        }

        public abstract void Show()
    }
}
