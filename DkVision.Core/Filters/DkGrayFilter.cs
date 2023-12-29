using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace DkVision.Core.Filters
{
    public class DkGrayFilter : IDkFilter
    {
        public Bitmap UpdateFrame(ref Image<Bgr, byte> frame)
        {
            using (Image<Gray, byte> grayFrame = frame.Convert<Gray, byte>())
            {
                return grayFrame.Bitmap;
            }
        }
    }
}
