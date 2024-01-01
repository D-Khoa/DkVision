using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace DkVision.Core.Filters
{
    public class DkCannyFilter : IDkFilter
    {
        public double Thesh { get; set; }
        public double TheshLinking { get; set; }

        public Bitmap Execute(Bitmap source)
        {
            using (Image<Gray, byte> imgSource = new Image<Gray, byte>(source))
            {
                return imgSource.PyrDown().PyrUp().Canny(new Gray(Thesh), new Gray(TheshLinking)).Bitmap;
            }
        }
    }
}
