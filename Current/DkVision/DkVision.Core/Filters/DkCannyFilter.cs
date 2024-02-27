using OpenCvSharp;
using System.Drawing;

namespace DkVision.Core.Filters
{
    public class DkCannyFilter : DkFilter
    {
        public DkCannyFilter(float x, float y, params PointF[] points) : base(x, y, points)
        {
        }

        protected override Mat OnFilter(Mat img)
        {
            return img.Canny(100, 200);
        }
    }
}
