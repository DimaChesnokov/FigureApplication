﻿using System;
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
        int X_new, Y_new;
        public Form1()
        {
            
            InitializeComponent();
            picture = new Bitmap(1000,1000);
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

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen pen;
            pen = new Pen(button1.BackColor);
            Graphics graph;
            graph = Graphics.FromImage(picture);
            if (e.Button == MouseButtons.Left)
            {
                graph.DrawLine(pen, X_new, Y_new, e.X, e.Y);
                pictureBox1.Image = picture;
            }
            X_new = e.X;
            Y_new = e.Y;
        }
    }
}