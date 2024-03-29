﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Microsoft.VisualBasic;
using FiguresLibrary;
//using Microsoft.VisualBasic;



namespace WindowsFormsPaint
{
    public partial class Form1 : Form
    {
        Bitmap picture;
        bool modeMove = false;
        int X_new, Y_new;
        List<Figure> listObjects = new List<Figure>();//список всех фигур(кроме карандаша)
        
        Figure SelectedFig;//фигура на которую нажали(дял перемещения)
        Point oldPoint;
        bool create_fig = false;
        //Create_Figure create;
        List<Ppoint> Blueprint = new List<Ppoint>();//для ручного создания фигуры
        public Form1()
        {
            
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            picture = new Bitmap(2000,2000);
            this.DoubleBuffered = true;
            //this.MouseDown += Form1_MouseDown;
            //this.MouseUp += Form1_MouseUp;
            //this.MouseMove += Form1_MouseMove;
            this.Paint += Form1_Paint; 
            X_new = Y_new = 0;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label5.Visible = false;
            trackBar2.Visible = false;
            trackBar3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            button1.BackColor = but.BackColor;
         }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
                picture.Save(saveFileDialog1.FileName);
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                picture = (Bitmap)Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = picture;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string mode;
        string Mov;
        private void очисткаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            graph.Clear(Color.White);
        }

        Graphics graph;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            mode = "Карандаш";
            TurnOffPropertys();
        }
       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Pen pen;
            pen = new Pen(button1.BackColor, trackBar1.Value - 10);
            graph = Graphics.FromImage(picture);
            Brush brush = new SolidBrush(button1.BackColor);
            if (mode == "Прямая")
            {

                int CursorX = MousePosition.X + 23;
                int CursorY = MousePosition.Y - 113; 
                X_new = CursorX    - trackBar2.Value;
                Y_new = CursorY  - trackBar3.Value;
                //graph.DrawLine(pen, X_new, Y_new, CursorX, CursorY);

                Line line = new Line(X_new,Y_new,CursorX,CursorY,5,5);
                line.Show(graph, pen, brush);
                listObjects.Add(line);
            }
            else if(mode == "Квадрат")
            {

                int CursorX = MousePosition.X + 4;
                int CursorY = MousePosition.Y - 120;
                X_new = /*CursorX -*/ 15;
                Y_new = /*CursorY -*/ 15;
                Square square = new Square(trackBar1.Value + 50, CursorX, CursorY);
                
                square.Show(graph, pen, brush);
                listObjects.Add(square);
                //if (Mov == "Left")
                //{
                //    square.Moving(CursorX - 100, CursorY);
                //}
            }
            else if(mode == "Круг")
            {
                int CursorX = MousePosition.X + 3 ;
                int CursorY = MousePosition.Y -  127;

                Circle circle = new Circle(trackBar1.Value + 50, CursorX, CursorY);
                circle.Show(graph,pen,brush);
                listObjects.Add(circle);
            }
            else if(mode == "Эллипс")
            {
                int CursorX = MousePosition.X + 4;
                int CursorY = MousePosition.Y - 127;
                Ellipse ellipse = new Ellipse(trackBar1.Value + 70, trackBar1.Value + 30, CursorX, CursorY);
                ellipse.Show(graph, pen, brush);
                listObjects.Add(ellipse);
            }
            else if(mode == "Прямоугольник")
            {
                int CursorX = MousePosition.X + 4;
                int CursorY = MousePosition.Y - 127;
                MyRectangle rectangle = new MyRectangle(trackBar1.Value + 100, trackBar1.Value + 30, CursorX, CursorY);
                
                rectangle.Show(graph,pen,brush);
                listObjects.Add(rectangle);
            }
            else if(modeMove)
            {
                int CursorX = MousePosition.X + 4;
                int CursorY = MousePosition.Y - 127;
                Figure v;
                for(int i = listObjects.Count - 1; i >0; i--)//идем с конца тк если фигура последняя она сверху, и чтобы не получилось, что взяли фигуру под фигурой
                {
                    v = listObjects[i];
                    Rectangle r = v.Region_Capture();
                    Point left_up = r.Location;

                    if(CursorX>left_up.X&&CursorX<left_up.X+r.Width&& CursorY > left_up.Y && CursorY < left_up.Y + r.Height)
                    {
                        SelectedFig = v;
                        //listObjects.Remove(v);
                    }
                }
            }
            pictureBox1.Image = picture;
        }


        private void размерФигурToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }
        
        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
             
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string figure = comboBox2.SelectedItem.ToString();
            //switch(figure)
            //{
            //    case "Прямая":
            //        mode = "Прямая";
            //        label5.Visible = true;
            //        break;
            //    case "Квадрат":
            //        mode = "Квадрат";
            //        //label5.Visible = true;
            //        break;
            //    case "Прямоугольник":
            //        mode = "Прямоугольник";
            //        //label5.Visible = true;
            //        break;
            //    case "Круг":
            //        mode = "Круг";
            //        //label5.Visible = true;
            //        break;
            //    case "Эллипс":
            //        mode = "Эллипс";
            //        //label5.Visible = true;
            //        break;
                
            //}
                
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mode = "Ластик";
            TurnOffPropertys();
        }
        void TurnOffPropertys()
        {
            //label5.Visible = false;
            //comboBox2.Text = "";
            //comboBox2.SelectedItem = null;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen pen;
            pen = new Pen(button1.BackColor,trackBar1.Value);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;

            graph = Graphics.FromImage(picture);
            if (e.Button == MouseButtons.Left)
            {

                if (mode == "Карандаш")
                {
                    graph.DrawLine(pen, X_new, Y_new, e.X, e.Y);
                }
                else if (mode == "Ластик")
                {
                    pen = new Pen(Color.White, trackBar1.Value);

                    pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    graph.DrawLine(pen, X_new, Y_new, e.X, e.Y);
                }
                else if(modeMove)//перемещение фигуры... в теории
                {
                    int x = X_new - e.X;
                    int y = Y_new - e.Y;
                    SelectedFig.Moving(x, y);
                   // SelectedFig.Show();

                }
                if (create_fig == true)
                {
                    Point p;
                    Ppoint pp = new Ppoint();
                    pp.pen = pen;
                    if (Blueprint.Count == 0)
                    {
                        p = new Point(X_new, Y_new);
                        pp.p = p;
                        
                        Blueprint.Add(pp);
                    }
                    pp.p = new Point(e.X, e.Y);
                    Blueprint.Add(pp);
                }
                pictureBox1.Image = picture;
            }
            X_new = e.X;
            Y_new = e.Y;
            

        }

        #region SelectFigure
        private void режимПеремещенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modeMove = true;
            mode = "";
            TurnOffPropertys();
            Destroy_button();
        }

        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modeMove = false;
            mode = "Квадрат";
            label5.Visible = false;
            trackBar2.Visible = false;
            trackBar3.Visible = false;
            Destroy_button();
        }

        private void прямоугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modeMove = false;
            mode = "Прямоугольник";
            label5.Visible = false;
            trackBar2.Visible = false;
            trackBar3.Visible = false;
            Destroy_button();
        }

        private void эллипсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modeMove = false;
            mode = "Эллипс";
            label5.Visible = false;
            trackBar2.Visible = false;
            trackBar3.Visible = false;
            Destroy_button();
        }

        private void кругToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modeMove = false;
            mode = "Круг";
            label5.Visible = false;
            trackBar2.Visible = false;
            trackBar3.Visible = false;
            Destroy_button();
        }

        private void прямаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modeMove = false;
            mode = "Прямая";
            label5.Visible = true;
            trackBar2.Visible = true;
            trackBar3.Visible = true;
            Destroy_button();
        }
        #endregion

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mov = "Left";
            
        }
        #region Create_Figure
        Button save_fig;
        List<Create_Figure> list_cf = new List<Create_Figure>();
        private void создатьНовуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Destroy_button();
            save_fig = new Button();
            save_fig.Text = "Сохранить фигуру!";
            save_fig.Location = new Point(650, 12);
            save_fig.Width = 91;
            save_fig.Height = 35;
            save_fig.Click += new EventHandler(save_fig_Click);
            panel1.Controls.Add(save_fig);
            create_fig = true;
        }
        private void save_fig_Click(object sender, EventArgs e)
        {
            
            string res = Interaction.InputBox("Внимание!", "Введите название новой фигуры:");
            Create_Figure cf = new Create_Figure();
            cf.name_fig = res;
            cf.Blueprint = Blueprint;
            list_cf.Add(cf);
            ToolStripMenuItem new_figure = new ToolStripMenuItem();
            new_figure.Name = res + "ToolStripMenuItem";
            new_figure.Text = res;
            фигурыToolStripMenuItem.DropDownItems.Add(new_figure);
            new_figure.Click += (s, ea) => {
                foreach(var f in list_cf)
                {
                    if(f.name_fig == s.ToString())
                    {
                        Pen pen = new Pen(button1.BackColor, trackBar1.Value);
                        pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                        graph = Graphics.FromImage(picture);
                        Brush brush = new SolidBrush(pictureBox1.BackColor);
                        f.Show(graph, pen, brush);
                        pictureBox1.Image = picture;
                    }
                }
            };
            
        }
        void Destroy_button()
        {
            panel1.Controls.Remove(save_fig);
            if(save_fig != null)
                save_fig.Dispose();
            create_fig = false;
            //create = null;
        }
        #endregion

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (picture == null) return;
            //RefreshBitmap();
            e.Graphics.DrawImage(picture, 0, 0);
        }

        //void Form1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    switch (e.Button)
        //    {
        //        case MouseButtons.Left:
        //            //Считаем смещение курсора
        //            int deltaX, deltaY;
        //            deltaX = e.Location.X - oldPoint.X;
        //            deltaY = e.Location.Y - oldPoint.Y;
        //            //Смещаем нарисованный объект
        //            currObj.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));
        //            //Запоминаем новое положение курсора
        //            oldPoint = e.Location;
        //            break;
        //        default:
        //            break;
        //    }
        //    //Обновляем форму
        //    this.Refresh();
        //}

        //void Form1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    currObj.Pen.Width -= 1;//Возвращаем ширину пера
        //    currObj = null;//Убираем ссылку на объект
        //}

        //void Form1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    //Запоминаем положение курсора
        //    oldPoint = e.Location;
        //    //Ищем объект, в который попала точка. Если таких несколько, то найден будет первый по списку
        //    foreach (object po in listObjects)
        //    {
        //        if (po.Path.GetBounds().Contains(e.Location))
        //        {
        //            currObj = po;//Запоминаем найденный объект
        //            currObj.Pen.Width += 1;//Делаем перо жирнее
        //            return;
        //        }
        //    }
        //}
        //void RefreshBitmap()
        //{
        //    if (picture != null) picture.Dispose();
        //    picture = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        //    //Прорисовка всех объектов из списка
        //    using (Graphics g = Graphics.FromImage(picture))
        //    {
        //        foreach (object po in listObjects)
        //        {
        //            g.DrawPath(po.Pen, po.Path);
        //        }
        //    }
        //}
    }
}
