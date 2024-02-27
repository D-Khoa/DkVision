using Emgu.CV;
using Emgu.CV.Structure;
using System;

namespace DkVision.Core.Components.Shapes
{
    public class DkEllipse : DkShape
    {
        public DkEllipse(int frameWidth, int frameHeight)
            : base(frameWidth, frameHeight) { }

        protected override void Draw<TColor, TDepth>(Image<TColor, TDepth> frame,
            Bgr borderColor, int borderThickness)
        {
            double angle = Math.Atan(Math.Tan(Width / Height));
            CvInvoke.cvEllipse(frame.Ptr, CenterPoint, FrameRect.Size, angle, 0, 360
                , borderColor.MCvScalar, borderThickness
                , Emgu.CV.CvEnum.LINE_TYPE.CV_AA, 0);
            if (!string.IsNullOrWhiteSpace(Name))
            {
                CvInvoke.cvPutText(frame, Name, P1, ref _textFont, borderColor.MCvScalar);
            }
        }
    }
}
