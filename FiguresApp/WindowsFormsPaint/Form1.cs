using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsPaint
{
    public partial class Form1 : Form
    {
        Bitmap picture;
        bool modeMove = false;
        int X_new, Y_new;
        public Form1()
        {
            
            InitializeComponent();
            picture = new Bitmap(2000,2000);
            this.MouseMove += new MouseEventHandler(mouseEvent);
            this.MouseClick += new MouseEventHandler(mouseClick);
            X_new = Y_new = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label5.Visible = false;
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
            pen = new Pen(button1.BackColor,5);
            graph = Graphics.FromImage(picture);
            Brush brush = new SolidBrush(button1.BackColor);
            if (mode == "Прямая")
            {
                int CursorX = MousePosition.X + 5;
                int CursorY = MousePosition.Y - /*this.Height */ 110; ;
                X_new = CursorX - 100;
                Y_new = CursorY - 100;
             
             
                graph.DrawLine(pen, X_new, Y_new, CursorX, CursorY);
                
               
            }
            else if(mode == "Квадрат")
            {
          
                
                int CursorX = MousePosition.X + 4/*- this.Height - 8*/;
                int CursorY = MousePosition.Y - /*this.Height */ 120;
                X_new = /*CursorX -*/ 15;
                Y_new = /*CursorY -*/ 15;
                Square square = new Square(trackBar1.Value + 50, CursorX, CursorY);
                square.Show(graph, pen, brush);
            }
            else if(mode == "Круг")
            {
                int CursorX = MousePosition.X + 3 ;
                int CursorY = MousePosition.Y -  127;

                Circle circle = new Circle(trackBar1.Value + 50, CursorX, CursorY);
                circle.Show(graph,pen,brush);
            }
            else if(mode == "Эллипс")
            {
                int CursorX = MousePosition.X + 4;
                int CursorY = MousePosition.Y - 127;
                Ellipse ellipse = new Ellipse(trackBar1.Value + 90, trackBar1.Value + 40, CursorX, CursorY);
                ellipse.Show(graph, pen, brush);
            }
            else if(mode == "Прямоугольник")
            {
                int CursorX = MousePosition.X + 4;
                int CursorY = MousePosition.Y - 127;
                MyRectangle rectangle = new MyRectangle(trackBar1.Value + 100, trackBar1.Value + 30, CursorX, CursorY);
                
                rectangle.Show(graph,pen,brush);
            }
            pictureBox1.Image = picture;
        }


        private void размерФигурToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }
        
        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
             //ds
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string figure = comboBox2.SelectedItem.ToString();
            switch(figure)
            {
                case "Прямая":
                    mode = "Прямая";
                    label5.Visible = true;
                    break;
                case "Квадрат":
                    mode = "Квадрат";
                    label5.Visible = true;
                    break;
                case "Прямоугольник":
                    mode = "Прямоугольник";
                    label5.Visible = true;
                    break;
                case "Круг":
                    mode = "Круг";
                    label5.Visible = true;
                    break;
                case "Эллипс":
                    mode = "Эллипс";
                    label5.Visible = true;
                    break;
                
            }
                
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mode = "Ластик";
            TurnOffPropertys();
        }
        void TurnOffPropertys()
        {
            label5.Visible = false;
            comboBox2.Text = "";
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
                    graph.DrawLine(pen, X_new, Y_new, e.X, e.Y);
                else if(mode == "Ластик")
                {
                    pen = new Pen(Color.White, trackBar1.Value);

                    pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    graph.DrawLine(pen, X_new, Y_new, e.X, e.Y);
                }
                pictureBox1.Image = picture;
            }
            X_new = e.X;
            Y_new = e.Y;
            

        }
        
        object currObject = null;
        void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == "Right")
            {
                currObject = null;
            }
        }
        void mouseEvent(object sender, MouseEventArgs e)
        {
            if (currObject != null && modeMove)
            {
                currObject.GetType().GetProperty("Location").SetValue(currObject, new Point(Cursor.Position.X, Cursor.Position.Y - 50));
                
            }
        }

        private void режимПеремещенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modeMove = true;
            mode = "";
            TurnOffPropertys();
        }

        void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (modeMove == false)
                return;
            currObject = sender;
        }
    }
}
