using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DkVision.Core.Interfaces
{
    public static class DkDetectionHelper
    {
        public static LineSegment2D[] DetectLines(this Image<Bgr, byte> frame
            , int cannyThreshold = 180, int cannyThresholdLinking = 120
            , double rho = 1, double theta = 45, int threshold = 20
            , double minWidth = 30, double gap = 10)
        {
            //Convert the image to grayscale and filter out the noise
            Image<Gray, byte> gray = frame.Convert<Gray, byte>().PyrDown().PyrUp();
            Image<Gray, byte> cannyEdges = gray.Canny(new Gray(cannyThreshold), new Gray(cannyThresholdLinking));
            return cannyEdges.HoughLinesBinary(
                rho, //Distance resolution in pixel-related units
                Math.PI / theta, //Angle resolution measured in radians.
                threshold, //threshold
                minWidth, //min Line width
                gap //gap between lines
                )[0]; //Get the lines from the first channel
        }

        public static CircleF[] DetectCircles(this Image<Bgr, byte> frame
            , int cannyThreshold = 180, int cannyThresholdLinking = 120
            , double dp = 5, int minDis = 10, int minRadius = 5, int maxRadius = 0)
        {
            //Convert the image to grayscale and filter out the noise
            Image<Gray, byte> gray = frame.Convert<Gray, byte>().PyrDown().PyrUp();
            return gray.HoughCircles(new Gray(cannyThreshold),
                new Gray(cannyThresholdLinking),
                dp, //Resolution of the accumulator used to detect centers of the circles
                minDis, //min distance 
                minRadius, //min radius
                maxRadius //max radius
                )[0]; //Get the lines from the first channel
        }

        public static Triangle2DF[] DetectTriangles(this Image<Bgr, byte> frame
            , int cannyThreshold = 180, int cannyThresholdLinking = 120)
        {
            List<Triangle2DF> triangleList = new List<Triangle2DF>();
            //Convert the image to grayscale and filter out the noise
            Image<Gray, byte> gray = frame.Convert<Gray, byte>().PyrDown().PyrUp();
            Image<Gray, byte> cannyEdges = gray.Canny(new Gray(cannyThreshold), new Gray(cannyThresholdLinking));
            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
                for (Contour<Point> contours = cannyEdges.FindContours(); contours != null; contours = contours.HNext)
                {
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

                    if (contours.Area > 250) //only consider contours with area greater than 250
                    {
                        if (currentContour.Total == 3) //The contour has 3 vertices, it is a triangle
                        {
                            Point[] pts = currentContour.ToArray();
                            triangleList.Add(new Triangle2DF(
                               pts[0],
                               pts[1],
                               pts[2]
                               ));
                        }
                    }
                }
            return triangleList.ToArray();
        }

        public static MCvBox2D[] DetectRectangles(this Image<Bgr, byte> frame
            , int cannyThreshold = 180, int cannyThresholdLinking = 120)
        {
            List<MCvBox2D> boxList = new List<MCvBox2D>();
            //Convert the image to grayscale and filter out the noise
            Image<Gray, byte> gray = frame.Convert<Gray, byte>().PyrDown().PyrUp();
            Image<Gray, byte> cannyEdges = gray.Canny(new Gray(cannyThreshold), new Gray(cannyThresholdLinking));
            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
                for (Contour<Point> contours = cannyEdges.FindContours(); contours != null; contours = contours.HNext)
                {
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

                    if (contours.Area > 250) //only consider contours with area greater than 250
                    {
                        if (currentContour.Total == 4) //The contour has 4 vertices.
                        {
                            #region determine if all the angles in the contour are within the range of [80, 100] degree
                            bool isRectangle = true;
                            Point[] pts = currentContour.ToArray();
                            LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                            for (int i = 0; i < edges.Length; i++)
                            {
                                double angle = Math.Abs(
                                   edges[(i + 1) % edges.Length].GetExteriorAngleDegree(edges[i]));
                                if (angle < 80 || angle > 100)
                                {
                                    isRectangle = false;
                                    break;
                                }
                            }
                            #endregion

                            if (isRectangle) boxList.Add(currentContour.GetMinAreaRect());
                        }
                    }
                }
            return boxList.ToArray();
        }
    }
}
