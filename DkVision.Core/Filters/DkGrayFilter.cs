using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace DkVision.Core.Filters
{
    public class DkGrayFilter : IDkFilter
    {
        public bool IsDebug { get; set; }

        public Bitmap Execute(Bitmap source)
        {
            using (Image<Gray, byte> imgTarget = new Image<Gray, byte>(source))
            {
                if (IsDebug)
                {
                    CvInvoke.cvShowImage($"Gray ({imgTarget.Width}x{imgTarget.Height})", imgTarget.Ptr);
                }
                return imgTarget.Bitmap;
            }
        }
    }
}
