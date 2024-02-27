using OpenCvSharp;
using System;
using System.Windows.Forms;

namespace DkVision.GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string imagePath = @"D:\05.Textures\03.Images\JPG\j3750x2500_bubbles_colourful.jpg";
            Mat src = new Mat(imagePath, ImreadModes.Grayscale);
            Mat dst = new Mat();
            // Edge detection by Canny algorithm
            Cv2.Canny(src, dst, 50, 200);
            using (new Window("src image", src))
            using (new Window("dst image", dst))
            {
                Cv2.WaitKey();
            }
        }
    }
}
