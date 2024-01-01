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
        private IDkCamera _camera = null;
        private static readonly object _lock = new object();

        public IDkCamera Camera
        {
            get => _camera;
            set => SetCamera(value);
        }
        public bool IsAuto { get; set; } = false;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public event Action<Bitmap> FrameChanged;

        public DkFrame() { }

        public void ChangeSize(Size size)
        {
            this.Width = size.Width;
            this.Height = size.Height;
        }
        protected virtual void OnFrameChanged(Bitmap e)
        {
            FrameChanged?.Invoke(e);
        }
        private void SetCamera(IDkCamera camera)
        {
            if (_camera != null)
            {
                _camera.DestroyCamera();
                _camera.FrameChanged -= Camera_FrameChanged;
            }
            _camera = camera;
            if (_camera != null)
            {
                _camera.FrameChanged += Camera_FrameChanged;
            }
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
