using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace DkVision.Core.Filters
{
    public class DkCannyFilter : IDkFilter
    {
        public bool IsDebug { get; set; }

        public double Thesh { get; set; } = 180;
        public double TheshLinking { get; set; } = 120;

        public Bitmap Execute(Bitmap source)
        {
            using (Image<Gray, byte> imgSource = new Image<Gray, byte>(source))
            {
                using (Image<Gray, byte> imgTarget = imgSource.PyrDown().PyrUp().Canny(new Gray(Thesh), new Gray(TheshLinking)))
                {
                    if (IsDebug)
                    {
                        CvInvoke.cvShowImage($"Canny-Before ({imgSource.Width}x{imgSource.Height})", imgSource.Ptr);
                        CvInvoke.cvShowImage($"Canny-After ({imgTarget.Width}x{imgTarget.Height})", imgTarget.Ptr);
                    }
                    return imgTarget.Bitmap;
                }
            }
        }
    }
}
