using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryCafe
{
    internal class ImageBlink
    {
       
        public ImageList ImageList;
        public Image image;


        public ImageBlink(ImageList imageList)
        {
            ImageList = imageList;
        }

        public Image ChangeIm()
        {
            Image image;
            Random random = new Random();
            int showIm = random.Next(0, 2);

            if (showIm == 0) { image = null; }
            else
            {
                image = ImageList.Images[random.Next(0, ImageList.Images.Count)];
            }
            Thread.Sleep(10);
            return image;
        }

        public async void BlincIm(object x)
        {
            try
            {
                PictureBox pb = (PictureBox)x;
                while (true)
                {
                   pb.Image = ChangeIm();
                    await Task.Delay(1000);
                }
            }
            catch { }
        }
    }
}
