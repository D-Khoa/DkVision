using DkVision.Core.Shapes;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DkVision.Core.Components
{
    public abstract class AbstractFrame : IDkFrame, IDisposable
    {
        public bool IsAutoShot { get; set; }
        public AbstractFrame(int width, int height)
        {
            Width = width;
            Height = height;
            PaintTool = ShapeStyle.None;
            Shapes = new List<DkShape>();
        }

        #region XỬ LÝ CAMERA
        public event EventHandler<string[]> CameralistChanged;

        public abstract void OneShot();
        public abstract void AutoShot();
        public abstract void StopAuto();
        public abstract void DestroyCamera();
        public abstract void UpdateCameralist();
        public abstract void SetCamera(string name);

        protected virtual void OnUpdateCameralist(string[] camList)
        {
            Console.WriteLine("Camera list updated");
            CameralistChanged?.Invoke(this, camList);
        }
        #endregion
        #region XỬ LÝ FRAME
        public int Width { get; set; }
        public int Height { get; set; }
        public event EventHandler<Bitmap> FrameChanged;
        protected virtual void OnFrameChanged(Image<Bgr, byte> frame)
        {
            frame = frame.Resize(this.Width, this.Height, INTER.CV_INTER_LINEAR);
            try
            {
                foreach (DkShape shape in Shapes)
                {
                    shape.UpdateFrame(frame);
                }
                if (_isPainting)
                {
                    _shape?.UpdateFrame(frame);
                }
                FrameChanged?.Invoke(this, frame.Bitmap);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                frame.Dispose();
            }
        }
        #endregion
        #region XỬ LÝ SHAPE
        private DkShape _shape;
        private Point _firstPoint;
        private bool _isPainting = false;

        public List<DkShape> Shapes { get; }
        public ShapeStyle PaintTool { get; set; }
        public event EventHandler<DkShape> ShapePainted;
        public virtual void OnShapePainted()
        {
            if (_isPainting)
            {
                Shapes.Add(_shape);
                _isPainting = false;
                ShapePainted?.Invoke(this, _shape);
                Console.WriteLine($"{PaintTool} is painted");
            }
        }
        public virtual void OnShapePaint(Point location)
        {
            if (PaintTool != ShapeStyle.None)
            {
                _isPainting = true;
                _firstPoint = location;
                switch (PaintTool)
                {
                    case ShapeStyle.None:
                        break;
                    case ShapeStyle.Line:
                        _shape = new DkLine(Width, Height);
                        break;
                    case ShapeStyle.Rect:
                        _shape = new DkRect(Width, Height);
                        break;
                    case ShapeStyle.Circle:
                        _shape = new DkCircle(Width, Height);
                        break;
                    case ShapeStyle.Ellipse:
                        _shape = new DkEllipse(Width, Height);
                        break;
                    default:
                        break;
                }
                Console.WriteLine($"{PaintTool} start at {location.X},{location.Y}");
            }
        }
        public virtual void OnShapePainting(Point location)
        {
            if (_isPainting)
            {
                _shape.Paint(_firstPoint, location);
                Console.WriteLine($"{PaintTool} is painting at {location.X},{location.Y}");
            }
        }
        #endregion

        //protected virtual void DetectLines(Image<Bgr, byte> frame)
        //{
        //    foreach (DkShape shape in Shapes)
        //    {
        //        shape.DetectLines(ref frame);
        //    }
        //    OnFrameChanged(frame.Bitmap);
        //}

        public virtual void Dispose()
        {
            DestroyCamera();
            Shapes.Clear();
        }
    }
}
