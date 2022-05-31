using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Geometr
{
    
    public partial class Form1 : Form
    {
        public static Bitmap image;

        public Form1()
        {
            InitializeComponent();            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {      
            
        }

        public void colorize()
        {
            for (int y = 0; y < pictureBox1.Height; y++)
                for (int x = 0; x < pictureBox1.Width; x++)
                    image.SetPixel(x, y, Color.FromArgb((int)affine_transform(x, y)));
        }

        public UInt32 affine_transform(int x, int y)
        {
            UInt32 pixelValue; 

            double l1, l2, l3;
            pixelValue = 0xFFFFFFFF; 

            l1 = ((data.y2 - data.y3) * ((double)(x) - data.x3) + (data.x3 - data.x2) * ((double)(y) - data.y3)) /
            ((data.y2 - data.y3) * (data.x1 - data.x3) + (data.x3 - data.x2) * (data.y1 - data.y3));
            l2 = ((data.y3 - data.y1) * ((double)(x) - data.x3) + (data.x1 - data.x3) * ((double)(y) - data.y3)) /
                ((data.y2 - data.y3) * (data.x1 - data.x3) + (data.x3 - data.x2) * (data.y1 - data.y3));

            l3 = 1 - l1 - l2;

            if (l1 >= 0 && l1 <= 1 && l2 >= 0 && l2 <= 1 && l3 >= 0 && l3 <= 1)
            {
                pixelValue = (UInt32)0xFF000000 |
                    ((UInt32)(l1 * ((data.colorA & 0x00FF0000) >> 16) + l2 * ((data.colorB & 0x00FF0000) >> 16) + l3 * ((data.colorC & 0x00FF0000) >> 16)) << 16) |
                    ((UInt32)(l1 * ((data.colorA & 0x0000FF00) >> 8) + l2 * ((data.colorB & 0x0000FF00) >> 8) + l3 * ((data.colorC & 0x0000FF00) >> 8)) << 8) |
                    (UInt32)(l1 * (data.colorA & 0x000000FF) + l2 * (data.colorB & 0x000000FF) + l3 * (data.colorC & 0x000000FF));
            }

            return pixelValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            colorize();
            pictureBox1.Image = image;

        }
    }
}
