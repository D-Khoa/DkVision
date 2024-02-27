using OpenCvSharp;

namespace DkVision.Core.Filters
{
    public class DkGrayFilter : DkFilter
    {
        public DkGrayFilter(float x, float y, params System.Drawing.PointF[] points) : base(x, y, points) { }

        protected override Mat OnFilter(Mat img)
        {
            return img.CvtColor(ColorConversionCodes.RGB2GRAY);
        }
    }
}
