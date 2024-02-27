using OpenCvSharp;

namespace DkVision.Core.Extensions
{
    internal static class DkImageExtensions
    {
        public static Mat CreateMask(this Mat img, params Point[] points)
        {
            //MatExpr mask = Mat.Zeros(img.Rows, img.Cols, (MatType)MatType.CV_8U);
            var mask = new Mat(img.Size(), MatType.CV_8UC3, new Scalar(255, 255, 255));
            Cv2.FillConvexPoly(mask, points, new Scalar(0, 0, 0));
            return mask;
        }
    }
}
