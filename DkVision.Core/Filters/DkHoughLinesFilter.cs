using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace DkVision.Core.Filters
{
    public class DkHoughLinesFilter : IDkFilter
    {
        public bool IsDebug { get; set; }
        /// <summary>
        /// Distance resolution in pixel-related units
        /// </summary>
        public double RhoResolution { get; set; } = 1;
        /// <summary>
        /// Angle resolution measured in radians.
        /// </summary>
        public double ThetaResolution { get; set; } = 45;
        public int Threshold { get; set; } = 20;
        public double MinLineWidth { get; set; } = 20;
        public double GapBetweenLines { get; set; } = 10;

        public Bitmap Execute(Bitmap source)
        {
            Bgr borderColor = new Bgr(Color.Green);
            using (Image<Bgr, byte> imgSource = new Image<Bgr, byte>(source))
            using (Image<Gray, byte> imgGray = new Image<Gray, byte>(source))
            {
                if (IsDebug)
                {
                    CvInvoke.cvShowImage($"Lines-Before ({imgSource.Width}x{imgSource.Height})", imgSource.Ptr);
                }
                LineSegment2D[] lines = imgGray.PyrDown().PyrUp().HoughLinesBinary(RhoResolution, Math.PI / ThetaResolution,
                    Threshold, MinLineWidth, GapBetweenLines)[0];
                foreach (LineSegment2D line in lines)
                {
                    imgSource.Draw(line, borderColor, 1);
                }
                if (IsDebug)
                {
                    CvInvoke.cvShowImage($"Lines-After ({imgSource.Width}x{imgSource.Height})", imgSource.Ptr);
                }
                return imgSource.Bitmap;
            }
        }
    }
}
