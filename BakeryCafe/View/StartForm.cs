using BakeryCafe.Controllers;
using BakeryCafe.Model;
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
        private ProductController dataProduct = ProductController.Instance;
        private DataProductController data = DataProductController.Instance;

        public StartForm()
        {
            InitializeComponent();
            pictureBoxes = new List<PictureBox> { pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9 };
            imageBlink = new ImageBlink(imageList1);
        }

        private void StartForm_Load_1(object sender, EventArgs e)
        {
            foreach (var pictureBox in pictureBoxes)
              {
                imageBlink.BlincIm(pictureBox);
              }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            new AdminForm().ShowDialog();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            new MenuForm().ShowDialog();
        }
    }
}
