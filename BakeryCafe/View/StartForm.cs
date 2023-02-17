using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryCafe.View
{
    public partial class StartForm : Form
    {
        ImageBlink imageBlink;
        List<PictureBox> pictureBoxes;

        public StartForm()
        {
            InitializeComponent();
            pictureBoxes = new List<PictureBox> { pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9 };
            imageBlink = new ImageBlink(imageList1);
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            BlincIm(pictureBox1);
            pictureBox1.Visible = false;
            /* foreach (var pictureBox in pictureBoxes)
             {
                 BlincIm(pictureBox);
                 //ThreadPool.QueueUserWorkItem(BlincIm, pictureBox);

             }*/
        }

        public async void BlincIm(object x)
        {

            try
            {
                PictureBox pb = (PictureBox)x;
                while (true)
                {
                    this.Invoke(new Action(() => pb.Image = ChangeIm()));
                    await Task.Delay(1000);
                }
                /* timerPicture.Tick += timerPicture_Tick;
                 timerPicture.Start();*/
            }
            catch
            {
            }
        }

        public Image ChangeIm()
        {
            Image image;
            Random random = new Random();
            int showIm = random.Next(0, 2);

            if (showIm == 0) { image = null; }
            else
            {
                image = imageList1.Images[random.Next(0, imageList1.Images.Count)];
            }
            Thread.Sleep(10);
            return image;
        }
        private void timerPicture_Tick(object x, EventArgs e)
        {
            //  this.Invoke(new Action(() => label1.Text = i.ToString()));
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            //Form ifrm = new BakeryShop();
            //ifrm.Show(); // отображаем Form2

        }

        private void StartForm_Load_1(object sender, EventArgs e)
        {
            foreach (var pictureBox in pictureBoxes)
              {
                  BlincIm(pictureBox);
                  //ThreadPool.QueueUserWorkItem(BlincIm, pictureBox);

              }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
             Form ifrm = new AdminForm();
             ifrm.Show(); // отображаем Form2
        }
    }
}
