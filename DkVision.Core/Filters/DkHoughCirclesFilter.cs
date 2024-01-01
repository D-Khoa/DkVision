using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace DkVision.Core.Filters
{
    public class DkHoughCirclesFilter : IDkFilter
    {
        /// <summary>
        /// Resolution of the accumulator used to detect centers of the circles
        /// </summary>
        public double Dp { get; set; }
        public int MinRadius { get; set; }
        public int MaxRadius { get; set; }
        public double MinDistance { get; set; }
        public double CannyTheshold { get; set; }
        public double AccumulatorTheshold { get; set; }

        public Bitmap Execute(Bitmap source)
        {
            Bgr borderColor = new Bgr(Color.Green);
            using (Image<Bgr, byte> imgSource = new Image<Bgr, byte>(source))
            {
                CircleF[] circles = imgSource.Convert<Gray, byte>().HoughCircles(
                    new Gray(CannyTheshold), new Gray(AccumulatorTheshold), Dp, MinDistance, MinRadius, MaxRadius)[0];
                foreach (CircleF circle in circles)
                {
                    imgSource.Draw(circle, borderColor, 1);
                }
                return imgSource.Bitmap;
            }
        }
    }
}
