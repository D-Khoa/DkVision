using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace DkVision.Core.Components
{
    public class DkFrame : IDkFrame
    {
        private readonly IDkCamera _camera;
        private static readonly object _lock = new object();

        public int Width { get; private set; }
        public int Height { get; private set; }

        public event EventHandler<Bitmap> FrameChanged;

        public DkFrame(IDkCamera camera)
        {
            _camera = camera;
            _camera.FrameChanged += Camera_FrameChanged;
        }

        public void ChangeSize(Size size)
        {
            this.Width = size.Width;
            this.Height = size.Height;
        }
        protected virtual void OnFrameChanged(Bitmap e)
        {
            FrameChanged?.Invoke(this, e);
        }
        private void Camera_FrameChanged(object sender, Bitmap e)
        {
            lock (_lock)
            {
                using (Image<Bgr, byte> img = new Image<Bgr, byte>(e))
                {
                    using (Image<Bgr, byte> img2 = img.Resize(Width, Height, INTER.CV_INTER_NN))
                    {
                        OnFrameChanged(img2.Bitmap);
                    }
                }
            }
        }
    }
}
