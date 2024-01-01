using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace DkVision.Core.Extensions
{
    public static class DkImageHelper
    {
        public static Bitmap ToGray(this Bitmap source)
        {
            using (Image<Bgr, byte> imgSource = new Image<Bgr, byte>(source))
            {
                return imgSource.Convert<Gray, byte>().Bitmap;
            }
        }

        public static void Show(this Bitmap source, string caption = "SHOW IMAGE")
        {
            using (Image<Bgr, byte> imgSource = new Image<Bgr, byte>(source))
            {
                CvInvoke.cvShowImage(caption, imgSource.Ptr);
            }
        }
    }
}
