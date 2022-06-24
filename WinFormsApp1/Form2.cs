using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Bitmap logo = new Bitmap(@"C:\Users\Кирилл\Documents\C++\Projects\WinFormsApp1\cards\logo.png");
            pictureBox1.Image = logo;
            pictureBox1.Width = 300;
            pictureBox1.Height = 300;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 newform1 = new Form1();
            Form2 newform2 = new Form2();
            newform2.Close();
            newform1.Show();
        }
    }
}
