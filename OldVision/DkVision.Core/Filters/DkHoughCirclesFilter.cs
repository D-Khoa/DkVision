using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace DkVision.Core.Filters
{
    public class DkHoughCirclesFilter : IDkFilter
    {
        public bool IsDebug { get; set; }

        /// <summary>
        /// Resolution of the accumulator used to detect centers of the circles
        /// </summary>
        public double Dp { get; set; } = 5;
        public int MinRadius { get; set; } = 5;
        public int MaxRadius { get; set; } = 0;
        public double MinDistance { get; set; } = 10;
        public double CannyTheshold { get; set; } = 180;
        public double AccumulatorTheshold { get; set; } = 120;

        public Bitmap Execute(Bitmap source)
        {
            Bgr borderColor = new Bgr(Color.Green);
            using (Image<Bgr, byte> imgSource = new Image<Bgr, byte>(source))
            using (Image<Gray, byte> imgGray = new Image<Gray, byte>(source))
            {
                CircleF[] circles = imgGray.PyrDown().PyrUp().HoughCircles(
                    new Gray(CannyTheshold), new Gray(AccumulatorTheshold), Dp, MinDistance, MinRadius, MaxRadius)[0];
                if (IsDebug)
                {
                    CvInvoke.cvShowImage($"Circles-Before ({imgSource.Width}x{imgSource.Height})", imgSource.Ptr);
                }
                foreach (CircleF circle in circles)
                {
                    imgSource.Draw(circle, borderColor, 1);
                }
                if (IsDebug)
                {
                    CvInvoke.cvShowImage($"Circles-After ({imgSource.Width}x{imgSource.Height})", imgSource.Ptr);
                }
                return imgSource.Bitmap;
            }
        }
    }
}
