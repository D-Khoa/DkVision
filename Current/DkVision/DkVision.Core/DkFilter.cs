using OpenCvSharp;
using System.Linq;

namespace DkVision.Core
{
    public abstract class DkFilter
    {
        public Rect ROI { get; }
        public Point Center { get; }
        internal Point[] Points { get; }

        public DkFilter(float x, float y, params System.Drawing.PointF[] points)
        {
            Center = new Point(x, y);
            int xMin = (int)points.Min(p => p.X);
            int yMin = (int)points.Min(p => p.Y);
            int xMax = (int)points.Max(p => p.X);
            int yMax = (int)points.Max(p => p.Y);
            ROI = new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
            Points = points.Select(p => new Point(p.X - xMin, p.Y - yMin)).ToArray();
        }

        protected abstract Mat OnFilter(Mat img);
        internal virtual Mat Filter(Mat img)
        {
            Mat outImg = new Mat();
            Mat roiImg = new Mat(img, ROI);
            Mat filterImg = OnFilter(roiImg);
            Mat mask = new Mat(filterImg.Size(), MatType.CV_8UC3);
            Cv2.FillPoly(roiImg, new Point[][] { Points }, new Scalar(0));
            Cv2.FillPoly(mask, new Point[][] { Points }, new Scalar(255, 255, 255));
            Cv2.SeamlessClone(filterImg, img, mask, Center, outImg, SeamlessCloneMethods.NormalClone);
            //Cv2.ImShow("GRAY", gray);
            //Cv2.ImShow("Mask", mask);
            //Cv2.ImShow("Clone", clone);
            //Cv2.WaitKey(0);
            return outImg;
        }
    }
}
