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
        Bitmap picture2;
        int X_new, Y_new;
        public Form1()
        {
            
            InitializeComponent();
            picture = new Bitmap(1500,1500);
            picture2 = new Bitmap(1500, 1500);
            X_new = Y_new = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
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
        private void button6_Click(object sender, EventArgs e)
        {
            mode = "Прямая";//построить логический элемент Y.(два входа один выход) 

        }

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
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen pen;
            pen = new Pen(button1.BackColor);
            
            graph = Graphics.FromImage(picture);
            if (e.Button == MouseButtons.Left)
            {
                if (mode == "Прямая")
                {
                    graph.DrawLine(pen, 50, 50, e.X, e.Y);

                }
                else 
                if (mode == "Карандаш")
                     graph.DrawLine(pen, X_new, Y_new, e.X, e.Y);
                pictureBox1.Image = picture;
            }
            X_new = e.X;
            Y_new = e.Y;
            //graph.DrawEllipse(pen)

        }
    }
}
