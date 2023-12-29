using Emgu.CV;
using Emgu.CV.Structure;

namespace DkVision.Core.Components.Shapes
{
    public class DkRect : DkShape
    {
        public DkRect(int frameWidth, int frameHeight)
            : base(frameWidth, frameHeight) { }

        protected override void Draw<TColor, TDepth>(Image<TColor, TDepth> frame
            , Bgr borderColor, int borderThickness)
        {
            CvInvoke.cvRectangle(frame.Ptr, P1, P2
                , borderColor.MCvScalar, borderThickness
                , Emgu.CV.CvEnum.LINE_TYPE.CV_AA, 0);
            if (!string.IsNullOrWhiteSpace(Name))
            {
                CvInvoke.cvPutText(frame, Name, P1, ref _textFont, borderColor.MCvScalar);
            }
        }
    }
}
