using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace DkVision.Core.Components
{
    public class DkMask
    {
        private int _radius;
        private double _angle;
        private Point _center;
        private Size _frameSize;
        private Rectangle _rect;
        private Point _endPoint;
        private Point _beginPoint;
        private IDkFilter _filter;
        private readonly ShapeStyle _shape;

        public Point EndPoint
        {
            get => _endPoint;
            set => SetEndPoint(value);
        }
        public Size FrameSize
        {
            get => _frameSize;
            set => SetFrameSize(value);
        }
        public IDkFilter Filter
        {
            get => _filter;
            set => _filter = value;
        }
        public int Radius => _radius;
        public double Angle => _angle;
        public Rectangle Rect => _rect;
        public Point Center => _center;
        public ShapeStyle Shape => _shape;
        public Point BeginPoint => _beginPoint;

        public DkMask(Point location, ShapeStyle shape, Size frameSize)
        {
            _shape = shape;
            _frameSize = frameSize;
            _beginPoint = location;
            EndPoint = location;
        }

        public Bitmap Execute(Bitmap source)
        {
            Bitmap filterdImage = Crop(source);
            if (Filter != null)
            {
                filterdImage = Filter.Execute(filterdImage);
            }
            return Overlay(source, filterdImage);
        }

        private Bitmap Crop(Bitmap source)
        {
            using (Image<Bgr, byte> imgSource = new Image<Bgr, byte>(source))
            using (Image<Bgr, byte> imgClone = new Image<Bgr, byte>(source))
            using (Image<Gray, byte> imgMask = new Image<Gray, byte>(source.Width, source.Height))
            {
                DrawMask(imgMask);
                using (Image<Bgr, byte> imgOut = imgSource.And(imgClone, imgMask))
                {
#if DEBUG
                    CvInvoke.cvShowImage("CROP-SOURCE", imgSource.Ptr);
                    CvInvoke.cvShowImage("CROP-MASK", imgMask.Ptr);
                    CvInvoke.cvShowImage("CROP-OUTPUT", imgOut.Ptr);
#endif
                    return imgOut.Bitmap;
                }
            }
        }
        private void SetFrameSize(Size size)
        {
            double xScale = (double)size.Width / _frameSize.Width;
            double yScale = (double)size.Height / _frameSize.Height;
            int x1 = (int)(xScale * _beginPoint.X);
            int y1 = (int)(yScale * _beginPoint.Y);
            int x2 = (int)(xScale * _endPoint.X);
            int y2 = (int)(yScale * _endPoint.Y);
            _beginPoint = new Point(x1, y1);
            EndPoint = new Point(x2, y2);
            _frameSize = size;
        }
        private void SetEndPoint(Point point)
        {
            _endPoint = point;
            int x = Math.Min(_beginPoint.X, _endPoint.X);
            int y = Math.Min(_beginPoint.Y, _endPoint.Y);
            int width = Math.Abs(_endPoint.X - _beginPoint.X);
            int height = Math.Abs(_endPoint.Y - _beginPoint.Y);
            _radius = Math.Min(width, height) / 2;
            _rect = new Rectangle(x, y, width, height);
            _center = new Point(x + width / 2, y + height / 2);
            if (height > 0)
            {
                _angle = Math.Atan(Math.Tan(width / height));
            }
        }
        private void DrawMask(Image<Gray, byte> imgMask)
        {
            switch (Shape)
            {
                case ShapeStyle.Line:
                    CvInvoke.cvLine(imgMask.Ptr, BeginPoint, EndPoint
                        , new MCvScalar(255, 255, 255), -1, Emgu.CV.CvEnum.LINE_TYPE.CV_AA, 0);
                    break;
                case ShapeStyle.Rect:
                    CvInvoke.cvRectangle(imgMask.Ptr, BeginPoint, EndPoint
                        , new MCvScalar(255, 255, 255), -1, Emgu.CV.CvEnum.LINE_TYPE.CV_AA, 0);
                    break;
                case ShapeStyle.Circle:
                //CvInvoke.cvCircle(imgMask.Ptr, Center, Radius
                //    , new MCvScalar(255, 255, 255), -1, Emgu.CV.CvEnum.LINE_TYPE.CV_AA, 0);
                //break;
                case ShapeStyle.Ellipse:
                    CvInvoke.cvEllipse(imgMask.Ptr, Center, Rect.Size, Angle, 0, 360
                        , new MCvScalar(255, 255, 255), -1, Emgu.CV.CvEnum.LINE_TYPE.CV_AA, 0);
                    break;
                case ShapeStyle.None:
                default:
                    break;
            }
        }
        private Bitmap Overlay(Bitmap source, Bitmap overlay)
        {
            using (Image<Bgr, byte> imgSource = new Image<Bgr, byte>(source))
            using (Image<Bgr, byte> imgOverlay = new Image<Bgr, byte>(overlay))
            using (Image<Gray, byte> imgMask = new Image<Gray, byte>(source.Width, source.Height))
            {
                DrawMask(imgMask);
                imgSource.SetValue(new Bgr(0, 0, 0), imgMask);
                imgOverlay.SetValue(new Bgr(0, 0, 0), imgMask.Not());
                using (Image<Bgr, byte> imgOut = imgSource.Or(imgOverlay))
                {
#if DEBUG
                    CvInvoke.cvShowImage("OVERLAY-SOURCE", imgSource.Ptr);
                    CvInvoke.cvShowImage("OVERLAY-OVERLAY", imgOverlay.Ptr);
                    CvInvoke.cvShowImage("OVERLAY-MASK", imgMask.Ptr);
                    CvInvoke.cvShowImage("OVERLAY-OUTPUT", imgOut.Ptr);
#endif
                    return imgOut.Bitmap;
                }
            }
        }
    }
}
