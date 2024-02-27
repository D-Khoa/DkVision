using DirectShowLib;
using DkVision.Core.Interfaces;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Linq;

namespace DkVision.Core.Components
{
    public class DkEmgucvCamera : IDkCamera
    {
        private Capture _camera;
        private string _cameraName;
        private DsDevice[] _devices;

        public event EventHandler<string> CameraChanged;
        public event EventHandler<Bitmap> FrameChanged;
        public event EventHandler<string[]> CameraListChanged;

        public void Capture()
        {
            if (_camera != null)
            {
                using (Image<Bgr, byte> frame = _camera.QueryFrame())
                {
                    FrameChanged?.Invoke(this, frame.Bitmap);
                }
            }
        }
        public void GetCameraList()
        {
            _devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            CameraListChanged?.Invoke(this, _devices.Select(x => x.Name).ToArray());
        }
        public void DestroyCamera()
        {
            if (_camera != null)
            {
                _camera.Dispose();
                _camera = null;
                Console.WriteLine($"Emgucv - Camera {_cameraName} is closed");
                _cameraName = string.Empty;
            }
        }
        public void OpenCamera(string key)
        {
            DestroyCamera();
            for (int i = 0; i < _devices.Length; i++)
            {
                if (key == _devices[i].Name)
                {
                    _cameraName = key;
                    _camera = new Capture(i);
                    Console.WriteLine($"Emgucv - Camera {key} is opened");
                    CameraChanged?.Invoke(this, key);
                    return;
                }
            }
        }
    }
}
