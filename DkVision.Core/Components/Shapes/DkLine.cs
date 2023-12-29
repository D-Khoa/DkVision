using Emgu.CV;
using Emgu.CV.Structure;

namespace DkVision.Core.Components.Shapes
{
    public class DkLine : DkShape
    {
        public DkLine(int frameWidth, int frameHeight)
            : base(frameWidth, frameHeight) { }

        protected override void Draw<TColor, TDepth>(Image<TColor, TDepth> frame
            , Bgr borderColor, int borderThickness)
        {
            if (borderThickness < 1) borderThickness = 1;
            if (borderThickness > 255) borderThickness = 255;
            CvInvoke.cvLine(frame.Ptr, P1, P2
                , borderColor.MCvScalar, borderThickness
                , Emgu.CV.CvEnum.LINE_TYPE.CV_AA, 0);
            if (!string.IsNullOrWhiteSpace(Name))
            {
                CvInvoke.cvPutText(frame, Name, P1, ref _textFont, borderColor.MCvScalar);
            }
        }
    }
}
