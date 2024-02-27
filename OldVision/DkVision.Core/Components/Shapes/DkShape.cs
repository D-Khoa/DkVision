using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace DkVision.Core.Components.Shapes
{
    /// <summary>
    /// Shape draw on FramePanel
    /// </summary>
    public abstract class DkShape : IDkFrame
    {
        private bool _isPainting = false;

        public string Name { get; set; }
        public Color BorderColor { get; set; }
        public int BorderThickness { get; set; }
        public ShapeStyle PaintStyle { get; set; }

        public int FrameWidth { get; }
        public int FrameHeight { get; }
        public Point P1 { get; private set; }
        public Point P2 { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Point CenterPoint { get; private set; }
        public Rectangle FrameRect { get; protected set; }
        public event EventHandler<Bitmap> FrameChanged;

        protected MCvFont _textFont = new MCvFont(FONT.CV_FONT_HERSHEY_PLAIN, 1, 0.5);

        public DkShape(int frameWidth, int frameHeight)
        {
            BorderThickness = 2;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            BorderColor = Color.Yellow;
        }

        public void Painted()
        {
            if (_isPainting)
            {
                _isPainting = false;
            }
        }
        public void Paint(Point p1)
        {
            if (!_isPainting)
            {
                P1 = p1;
                _isPainting = true;
            }
        }
        public void Painting(Point p2)
        {
            if (_isPainting)
            {
                P2 = p2;
                int w = Math.Abs(P1.X - P2.X);
                int h = Math.Abs(P1.Y - P2.Y);
                FrameRect = new Rectangle(P1.X, P1.Y, w, h);
                CenterPoint = new Point(P1.X + w / 2, P1.Y + h / 2);
            }
        }
        public void ChangeSize(Size size)
        {
            this.Width = size.Width;
            this.Height = size.Height;
            UpdateFrame();
        }
        public void UpdateFrame()
        {
            try
            {
                using (Image<Bgr, byte> img = new Image<Bgr, byte>(FrameWidth, FrameHeight))
                {
                    Draw(img, new Bgr(BorderColor), BorderThickness);
                    using (Image<Bgr, byte> img2 = img.Resize(this.Width, this.Height, INTER.CV_INTER_NN))
                    {
                        OnFrameChanged(img2.Bitmap);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        protected virtual void OnFrameChanged(Bitmap e)
        {
            FrameChanged?.Invoke(this, e);
        }
        protected abstract void Draw<TColor, TDepth>(Image<TColor, TDepth> frame
            , Bgr borderColor, int borderThickness)
            where TColor : struct, IColor where TDepth : new();
    }
}
