using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;



namespace BakeryCafe
{
    public partial class Bakery : Form
    {
        ImageBlink imageBlink;
        List<PictureBox> pictureBoxes ;
       
        public Bakery()
        {
            InitializeComponent();
            pictureBoxes = new List<PictureBox> { pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7 };
            imageBlink = new ImageBlink(imageList1);
        }

        private void Bakery_Load(object sender, EventArgs e)
        {
            foreach (var pictureBox in pictureBoxes) { 
            ThreadPool.QueueUserWorkItem(BlincIm, pictureBox);
                
            }
        }

        public void BlincIm(object x)
        {
            try
            {
                PictureBox pb = (PictureBox)x;
                while (true) {
                    this.Invoke(new Action(() => pb.Image = ChangeIm()));
                    Thread.Sleep(1000);
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
              

     
    }
}
