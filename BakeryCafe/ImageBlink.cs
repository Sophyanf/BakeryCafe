using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryCafe
{
    internal class ImageBlink
    {
        public PictureBox PictureBox;
        public ImageList ImageList;


        public ImageBlink(PictureBox pictureBox, ImageList imageList)
        {
            PictureBox = pictureBox;
            ImageList = imageList;
        }

        public void ChangeIm()
        {
            Random random = new Random();
            int showIm = random.Next(0, 2);
            if (showIm == 0) { PictureBox.Image = null; }
            else
            {
                PictureBox.Image = ImageList.Images[random.Next(0, ImageList.Images.Count)];
            }
        }

        
    }
}
