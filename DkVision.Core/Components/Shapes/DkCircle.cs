using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace DkVision.Core.Components.Shapes
{
    public class DkCircle : DkShape
    {
        public DkCircle(int frameWidth, int frameHeight)
            : base(frameWidth, frameHeight) { }

        protected override void Draw<TColor, TDepth>(Image<TColor, TDepth> frame
            , Bgr borderColor, int borderThickness)
        {
            int len = Math.Min(Width, Height);
            int radius = len / 2;
            FrameRect = new Rectangle(CenterPoint.X - radius, CenterPoint.Y - radius, len, len);
            CvInvoke.cvCircle(frame.Ptr, CenterPoint, radius
                , borderColor.MCvScalar, borderThickness
                , Emgu.CV.CvEnum.LINE_TYPE.CV_AA, 0);
            if (!string.IsNullOrWhiteSpace(Name))
            {
                CvInvoke.cvPutText(frame, Name, P1, ref _textFont, borderColor.MCvScalar);
            }
        }
    }
}
