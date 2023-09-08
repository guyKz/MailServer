using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Drawing.Imaging;

namespace Interfaces
{
    public class png : image
    {
        public string type { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public byte[] data { get; set; }
        public string image;


        public png(Image img)
        {
            height = img.Height;
            width = img.Width;
            type = "PNG";

            ImageConverter converter = new ImageConverter();
            data = (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public png(int height, int width, byte[] data, string image)
        {
            this.height = height;
            this.width = width;
            this.data = data;
            this.image = image;
        }

        public void Draw(Form p)
        {
            PictureBox pictureBox = new PictureBox();
            Bitmap image = new Bitmap(new MemoryStream(data));
            pictureBox.Image = image;
            pictureBox.Size = new Size(width, height);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Dock = DockStyle.Top;
            pictureBox.Anchor = AnchorStyles.Bottom;
            //p.Controls.Add(pictureBox);

            foreach (Control control in p.Controls)
            {
                if (control is FlowLayoutPanel)
                {
                    control.Controls.Add(pictureBox);
                }
            }
        }
        

        public byte[] Serialize()
        {
            return Encoding.ASCII.GetBytes(JsonSerializer.Serialize(this));
        }



       //public Image deSerialize(byte[] arr)
       // {
       //     using (MemoryStream stream = new MemoryStream(arr))
       //     {
       //         Bitmap bitmap = new Bitmap(stream);
       //         return (Image)bitmap;
       //     }
       // }

        public int getWidth()
        {
            throw new NotImplementedException();
        }

        public int getHeight()
        {
            throw new NotImplementedException();
        }

        public void setWidth(int width)
        {
            throw new NotImplementedException();
        }

        public void setHeight(int height)
        {
            throw new NotImplementedException();
        }

        string IAttribute.deSerialize(byte[] arr)
        {
            throw new NotImplementedException();
        }
    }
}
