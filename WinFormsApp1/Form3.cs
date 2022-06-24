using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Bitmap end = new Bitmap(@"C:\Users\Кирилл\Documents\C++\Projects\WinFormsApp1\cards\end.jpg");
            pictureBox1.Image = end;
            pictureBox1.Width = 300;
            pictureBox1.Height = 400;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 newform1 = new Form1();
            Form3 newform3 = new Form3();
            newform3.Close();
            newform1.Show();
        }
    }
}
