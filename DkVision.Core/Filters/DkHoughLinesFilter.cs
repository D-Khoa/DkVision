using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace DkVision.Core.Filters
{
    public class DkHoughLinesFilter : IDkFilter
    {
        /// <summary>
        /// Distance resolution in pixel-related units
        /// </summary>
        public double RhoResolution { get; set; }
        /// <summary>
        /// Angle resolution measured in radians.
        /// </summary>
        public double ThetaResolution { get; set; }
        public int Threshold { get; set; }
        public double MinLineWidth { get; set; }
        public double GapBetweenLines { get; set; }

        public Bitmap Execute(Bitmap source)
        {
            Bgr borderColor = new Bgr(Color.Green);
            using (Image<Bgr, byte> imgSource = new Image<Bgr, byte>(source))
            {
                LineSegment2D[] lines = imgSource.HoughLinesBinary(RhoResolution, Math.PI / ThetaResolution,
                    Threshold, MinLineWidth, GapBetweenLines)[0];
                foreach (LineSegment2D line in lines)
                {
                    imgSource.Draw(line, borderColor, 1);
                }
                return imgSource.Bitmap;
            }
        }
    }
}
