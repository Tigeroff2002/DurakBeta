using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        bool end;
        int winner;
        int n_pictures;
        int n_koloda;
        bool koloda_empty;
        Bitmap[] img = new Bitmap[37];
        picture[] pictures = new picture[40];
        bool[] img_pos = new bool[36];
        Random rnd = new Random();
        Queue queue = new Queue();
        Bitmap[] arrow = new Bitmap[2];
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            winner = 0;
            end = false;
            n_koloda = 24;
            koloda_empty = false;
            label2.Text = "Осталось 24 карты";
            n_pictures = 12;
            queue.sbros = 1;
            queue.hod = 1;
            pictureBox13.Enabled = false;
            label1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            int i;
            for (i = 0; i < 37; i++)
            {
                img[i] = new Bitmap(@"C:\Users\Кирилл\Documents\C++\Projects\WinFormsApp1\cards\" + i.ToString() + ".jpg");
                if (i < 36) img_pos[i] = true;
            }
            arrow[0] = new Bitmap(@"C:\Users\Кирилл\Documents\C++\Projects\WinFormsApp1\cards\up.png");
            arrow[1] = new Bitmap(@"C:\Users\Кирилл\Documents\C++\Projects\WinFormsApp1\cards\down.png");
            pictureBox40.Width = 100;
            pictureBox40.Height = 100;
            pictureBox40.Image = arrow[0];
            using (var soundPlayer = new SoundPlayer(@"c:\Windows\Media\throw.wav"))
            {
                soundPlayer.Play();
            }
            Timer timer = new Timer();
            timer.Interval = 100;
            i = 1;
            timer.Tick += new EventHandler((_s, _e) =>
            {
                if (i < 14)
                {
                    int k;
                    do
                    {
                        k = rnd.Next(36);
                        if (img_pos[k])
                        {
                            (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image = img[k];
                            pictures[i] = new picture();
                            pictures[i].Enabled = true;
                            pictures[i].M = k % 4;
                            pictures[i].V = k / 4;
                            (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Click += new EventHandler(p_Click);
                            Index index = new Index();
                            index.I = i;
                            (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Tag = index;
                        }

                    }
                    while (!img_pos[k]);
                    img_pos[k] = false;
                    i++;
                }
                else timer.Dispose();
            });
            timer.Start();
            pictureBox14.Image = img[36];
        }

        void p_Click(object sender, EventArgs e)
        {
            using (var soundPlayer = new SoundPlayer(@"c:\Windows\Media\desk.wav"))
            {
                PictureBox p = sender as PictureBox;
                int index = ((Index)(p.Tag)).I;
                int k = 15;
                bool go = true;
                while ((((this.Controls.Find("pictureBox" + k, true).First() as PictureBox).Image != null) && (index > 6)) || (((this.Controls.Find("pictureBox" + (k + 1), true).First() as PictureBox).Image != null) && (index < 7)))
                {
                    k = k + 2;
                    go = false;
                }
                for (int i = 0; i < 6; i++)
                    if ((pictures[index].V == queue.myvalues[i]) || (pictures[index].V == queue.envalues[i]))
                    {
                        go = true;
                        break;
                    }
                if (go) queue.sbros *= -1;
                else if (queue.hod == queue.sbros) queue.sbros *= -1;
                if (((index > 6) && (index < 13)) || ((index > 33) && (index < 40)))
                {
                    if (queue.hod == 1)
                    {
                        if (go)
                        {
                            soundPlayer.PlaySync();
                            (this.Controls.Find("pictureBox" + k, true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + index, true).First() as PictureBox).Image;
                            if (pictures[k] == null) pictures[k] = new picture();
                            n_pictures += 1;
                            pictures[k].Enabled = false;
                            pictures[k].M = pictures[index].M;
                            pictures[k].V = pictures[index].V;
                            queue.myvalues[(k - 15) / 2] = pictures[k].V;
                            (this.Controls.Find("pictureBox" + index, true).First() as PictureBox).Visible = false;
                            queue.sbros *= -1;
                            button2.Visible = true;
                            button1.Visible = false;
                        }
                    }
                    else
                    {
                        if ((queue.sbros == 1) || go)
                            if (((pictures[k + 1].M == pictures[index].M) && (pictures[index].V > pictures[k + 1].V)) || ((pictures[index].M == pictures[13].M) && (pictures[k + 1].M != pictures[13].M)))
                            {
                                soundPlayer.PlaySync();
                                (this.Controls.Find("pictureBox" + k, true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + index, true).First() as PictureBox).Image;
                                if (pictures[k] == null) pictures[k] = new picture();
                                n_pictures += 1;
                                pictures[k].Enabled = false;
                                pictures[k].M = pictures[index].M;
                                pictures[k].V = pictures[index].V;
                                queue.myvalues[(k - 15) / 2] = pictures[k].V;
                                (this.Controls.Find("pictureBox" + index, true).First() as PictureBox).Visible = false;
                                queue.sbros *= -1;
                                for (int i = 0; i < 6; i++)
                                    if ((queue.myvalues[i] > -1) && (queue.envalues[i] == -1))
                                    {
                                        queue.sbros *= -1;
                                        break;
                                    }
                                button1.Visible = true;
                                button2.Visible = false;
                            }

                    }
                }
                else if ((index < 7) || ((index > 27) && (index < 34)))
                {
                    if (queue.hod == -1)
                    {
                        if (go)
                        {
                            soundPlayer.PlaySync();
                            (this.Controls.Find("pictureBox" + (k + 1), true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + index, true).First() as PictureBox).Image;
                            if (pictures[k + 1] == null) pictures[k + 1] = new picture();
                            n_pictures += 1;
                            pictures[k + 1].Enabled = false;
                            pictures[k + 1].M = pictures[index].M;
                            pictures[k + 1].V = pictures[index].V;
                            queue.myvalues[(k - 15) / 2] = pictures[k + 1].V;
                            (this.Controls.Find("pictureBox" + index, true).First() as PictureBox).Visible = false;
                            queue.sbros *= -1;
                            button2.Visible = true;
                            button1.Visible = false;
                        }
                    }
                    else
                    {
                        if ((queue.sbros == -1) || go)
                            if (((pictures[k].M == pictures[index].M) && (pictures[index].V > pictures[k].V)) || ((pictures[index].M == pictures[13].M) && (pictures[k].M != pictures[13].M)))
                            {
                                soundPlayer.PlaySync();
                                (this.Controls.Find("pictureBox" + (k + 1), true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + index, true).First() as PictureBox).Image;
                                if (pictures[k + 1] == null) pictures[k + 1] = new picture();
                                n_pictures += 1;
                                pictures[k + 1].Enabled = false;
                                pictures[k + 1].M = pictures[index].M;
                                pictures[k + 1].V = pictures[index].V;
                                queue.envalues[(k - 15) / 2] = pictures[k + 1].V;
                                (this.Controls.Find("pictureBox" + index, true).First() as PictureBox).Visible = false;
                                queue.sbros *= -1;
                                for (int i = 0; i < 6; i++)
                                    if ((queue.myvalues[i] > -1) && (queue.envalues[i] == -1))
                                    {
                                        queue.sbros *= -1;
                                        break;
                                    }
                                button1.Visible = true;
                                button2.Visible = false;
                            }
                    }
                }
                else
                {
                    for (int i = 1; i < n_pictures; i++)
                    {
                        if (pictures[i] == null) pictures[i] = new picture();
                        pictures[i].Enabled = false;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var soundPlayer = new SoundPlayer(@"c:\Windows\Media\bito.wav"))
            {
                bool sound = false;
                int i;
                button1.Visible = false;
                button2.Visible = false;
                int n_b1 = 0, n_b2 = 0, n_c1 = 0, n_c2 = 0;
                for (i = 34; i < 40; i++)
                {
                    if ((this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image != null) n_b1 += 1;
                    if ((this.Controls.Find("pictureBox" + (i - 6), true).First() as PictureBox).Image != null) n_b2 += 1;
                }

                for (i = 15; i < 27; i++)
                {
                    (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + 27, true).First() as PictureBox).Image;
                    if (pictures[i] == null) pictures[i] = new picture();
                    pictures[i].M = -1;
                    pictures[i].V = -1;
                }
                for (i = 1; i < 13; i++)
                {
                    if (((this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible == false) && (((i > 6) && (n_b1 == 0)) || ((i < 7) && (n_b2 == 0))))
                    {
                        if (!koloda_empty)
                        {
                            if (n_koloda > 1)
                            {
                                int k;
                                do
                                {
                                    k = rnd.Next(36);
                                    if (img_pos[k])
                                    {
                                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image = img[k];
                                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible = true;
                                        pictures[i].M = k % 4;
                                        pictures[i].V = k / 4;
                                        label2.Text = "Осталось " + (--n_koloda).ToString() + " карт(ы)";
                                    }
                                }
                                while (!img_pos[k]);
                                img_pos[k] = false;
                                sound = true;
                            }
                            else if (n_koloda == 1)
                            {
                                (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + 13, true).First() as PictureBox).Image;
                                (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible = true;
                                pictures[i].M = pictures[13].M;
                                pictures[i].V = pictures[13].V;
                                label2.Text = "Карты в колоде кончились";
                                koloda_empty = true;
                                (this.Controls.Find("pictureBox" + 13, true).First() as PictureBox).Visible = false;
                                (this.Controls.Find("pictureBox" + 14, true).First() as PictureBox).Visible = false;
                                sound = true;
                            }
                        }
                    }
                    else if (((this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible == false) && (n_b1 > 0) && (i > 6))
                    {
                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + (34 + n_c1), true).First() as PictureBox).Image;
                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible = true;
                        pictures[i].M = pictures[34 + n_c1].M;
                        pictures[i].V = pictures[34 + n_c1].V;
                        (this.Controls.Find("pictureBox" + (34 + n_c1), true).First() as PictureBox).Image = null;
                        pictures[34 + n_c1].M = -1;
                        pictures[34 + n_c1].V = -1;
                        n_b1 -= 1;
                        n_c1 += 1;
                        sound = true;
                    }
                    else if (((this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible == false) && (n_b2 > 0) && (i < 7))
                    {
                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + (28 + n_c2), true).First() as PictureBox).Image;
                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible = true;
                        pictures[i].M = pictures[28 + n_c2].M;
                        pictures[i].V = pictures[28 + n_c2].V;
                        (this.Controls.Find("pictureBox" + (28 + n_c2), true).First() as PictureBox).Image = null;
                        pictures[28 + n_c2].M = -1;
                        pictures[28 + n_c2].V = -1;
                        n_b2 -= 1;
                        n_c2 += 1;
                        sound = true;
                    }
                    if (sound) soundPlayer.PlaySync();
                    sound = false;
                }
                for (i = 33; i > 28; i--)
                {
                    if (((this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image != null) && ((this.Controls.Find("pictureBox" + (i - 1), true).First() as PictureBox).Image == null))
                    {
                        (this.Controls.Find("pictureBox" + (i - 1), true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image;
                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image = null;
                        pictures[i - 1].M = pictures[i].M;
                        pictures[i - 1].V = pictures[i].V;
                        pictures[i].M = -1;
                        pictures[i].V = -1;
                    }
                    if (((this.Controls.Find("pictureBox" + (i + 6), true).First() as PictureBox).Image != null) && ((this.Controls.Find("pictureBox" + (i + 5), true).First() as PictureBox).Image == null))
                    {
                        (this.Controls.Find("pictureBox" + (i + 5), true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + (i + 6), true).First() as PictureBox).Image;
                        (this.Controls.Find("pictureBox" + (i + 6), true).First() as PictureBox).Image = null;
                        pictures[i + 5].M = pictures[i + 6].M;
                        pictures[i + 5].V = pictures[i + 6].V;
                        pictures[i + 6].M = -1;
                        pictures[i + 6].V = -1;
                    }
                }
                for (i = 0; i < 6; i++)
                {
                    queue.myvalues[i] = -1;
                    queue.envalues[i] = -1;
                }
                queue.hod *= -1;
                if (pictureBox40.Image == arrow[0]) pictureBox40.Image = arrow[1];
                else pictureBox40.Image = arrow[0];
                queue.sbros *= -1;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var soundPlayer = new SoundPlayer(@"c:\Windows\Media\bito.wav"))
            {
                bool sound = false;
                button1.Visible = false;
                button2.Visible = false;
                int h;
                int n_b = 0, n_c = 0, n_b1;
                if (queue.hod == -1) h = -6;
                else h = 0;
                for (int i = 34 + h; i < 40 + h; i++)
                    if ((this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image != null) n_b += 1;
                n_b1 = n_b;
                for (int i = 7 + h; i < 13 + h; i++)
                {
                    if (((this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible == false) && (n_b == 0))
                    {
                        if (!koloda_empty)
                        {
                            if (n_koloda > 1)
                            {
                                int k;
                                do
                                {
                                    k = rnd.Next(36);
                                    if (img_pos[k])
                                    {
                                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image = img[k];
                                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible = true;
                                        pictures[i].M = k % 4;
                                        pictures[i].V = k / 4;
                                        label2.Text = "Осталось " + (--n_koloda).ToString() + " карт(ы)";
                                    }

                                }
                                while (!img_pos[k]);
                                img_pos[k] = false;
                                sound = true;
                            }
                            else if (n_koloda == 1)
                            {
                                (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + 13, true).First() as PictureBox).Image;
                                (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible = true;
                                pictures[i].M = pictures[13].M;
                                pictures[i].V = pictures[13].V;
                                label2.Text = "Карты в колоде кончились";
                                koloda_empty = true;
                                (this.Controls.Find("pictureBox" + 13, true).First() as PictureBox).Visible = false;
                                (this.Controls.Find("pictureBox" + 14, true).First() as PictureBox).Visible = false;
                                sound = true;
                            }
                        }
                    }
                    else if (((this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible == false) && (n_b > 0))
                    {
                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + (34 + h + n_c), true).First() as PictureBox).Image;
                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible = true;
                        pictures[i].M = pictures[34 + h + n_c].M;
                        pictures[i].V = pictures[34 + h + n_c].V;
                        (this.Controls.Find("pictureBox" + (34 + h + n_c), true).First() as PictureBox).Image = null;
                        pictures[34 + h + n_c].M = -1;
                        pictures[34 + h + n_c].V = -1;
                        n_b -= 1;
                        n_c += 1;
                    }
                    if (sound) soundPlayer.PlaySync();
                    sound = false;
                }
                for (int i = 1 - h; i < 7 - h; i++)
                {
                    if ((this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible == false)
                        (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible = true;
                }
                for (int i = 15; i < 27; i++)
                {
                    if (pictures[i] == null)
                    {
                        pictures[i] = new picture();
                        n_pictures += 1;
                    }
                    if ((i % 2 == 1) && (h == 0))
                    {
                        (this.Controls.Find("pictureBox" + ((i + 41) / 2 + n_b1), true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image;
                        (this.Controls.Find("pictureBox" + ((i + 41) / 2 + n_b1), true).First() as PictureBox).Click += new EventHandler(p_Click);
                        Index index = new Index();
                        index.I = ((i + 41) / 2 + n_b1);
                        (this.Controls.Find("pictureBox" + ((i + 41) / 2 + n_b1), true).First() as PictureBox).Tag = index;
                        if (pictures[(i + 41) / 2 + n_b1] == null) pictures[(i + 41) / 2 + n_b1] = new picture();
                        n_pictures += 1;
                        pictures[(i + 41) / 2 + n_b1].M = pictures[i].M;
                        pictures[(i + 41) / 2 + n_b1].V = pictures[i].V;
                    }
                    else if ((i % 2 == 0) && (h == -6))
                    {
                        (this.Controls.Find("pictureBox" + ((i + 52) / 2 + n_b1), true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image;
                        (this.Controls.Find("pictureBox" + ((i + 52) / 2 + n_b1), true).First() as PictureBox).Click += new EventHandler(p_Click);
                        Index index = new Index();
                        index.I = ((i + 52) / 2 + n_b1);
                        (this.Controls.Find("pictureBox" + ((i + 52) / 2 + n_b1), true).First() as PictureBox).Tag = index;
                        if (pictures[(i + 52) / 2 + n_b1] == null) pictures[(i + 52) / 2 + n_b1] = new picture();
                        n_pictures += 1;
                        pictures[(i + 52) / 2 + n_b1].M = pictures[i].M;
                        pictures[(i + 52) / 2 + n_b1].V = pictures[i].V;
                    }
                    pictures[i].M = -1;
                    pictures[i].V = -1;
                    (this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Image = (this.Controls.Find("pictureBox" + 27, true).First() as PictureBox).Image;
                }
                for (int i = 0; i < 6; i++)
                {
                    queue.myvalues[i] = -1;
                    queue.envalues[i] = -1;
                }
                for (int i = 1; i < pictures.Length; i++)
                {
                    if (pictures[i] == null) pictures[i] = new picture();
                    if ((i < 15) && (i > 26))
                        pictures[i].Enabled = true;
                }
                if (queue.sbros != queue.hod) queue.sbros = queue.hod;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int k1 = 0, k2 = 0;
            for (int i = 1; i < 7; i++)
            {
                if ((this.Controls.Find("pictureBox" + i + 6, true).First() as PictureBox).Visible == true) k1++;
                if ((this.Controls.Find("pictureBox" + i, true).First() as PictureBox).Visible == true) k2++;
            }
            if ((k1 == 0) || (k2 == 0))
            {
                Form3 newform3 = new Form3();
                newform3.Show();
            }
        }
    }
    public class Queue
    {
        public int sbros { get; set; }
        public int hod { get; set; }
        public int[] myvalues = new int[6] { -1, -1, -1, -1, -1, -1 };
        public int[] envalues = new int[6] { -1, -1, -1, -1, -1, -1 };
    }
    public class picture
    {
        public int M { get; set; }
        public int V { get; set; }
        public bool Enabled { get; set; }
    }
    public class Index
    {
        public int I { get; set; }
    }
}