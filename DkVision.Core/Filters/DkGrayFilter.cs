using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace DkVision.Core.Filters
{
    public class DkGrayFilter : IDkFilter
    {
        public Bitmap Execute(Bitmap source)
        {
            using (Image<Bgr, byte> imgSource = new Image<Bgr, byte>(source))
            {
                return imgSource.Convert<Gray, byte>().Bitmap;
            }
        }
    }
}
