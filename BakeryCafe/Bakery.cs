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

        }

        private void Bakery_Load(object sender, EventArgs e)
        {
            //foreach (var pictureBox in pictureBoxes) 
            ThreadPool.QueueUserWorkItem(BlincIm,pictureBox2);
        }

        public void BlincIm(object x)
        {
            imageBlink = new ImageBlink((PictureBox)x, imageList1);
                timerPicture.Tick += timerPicture_Tick;
                timerPicture.Start();
        }

        private void timerPicture_Tick(object sender, EventArgs e)
        {
            imageBlink.ChangeIm();
        }
              

     
    }
}
