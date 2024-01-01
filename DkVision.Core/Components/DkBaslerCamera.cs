using Basler.Pylon;
using DkVision.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace DkVision.Core.Components
{
    public class DkBaslerCamera : IDkCamera
    {
        private Camera _camera;
        private bool _isRunning;
        private readonly object _lock = new object();
        private readonly PixelDataConverter _converter = new PixelDataConverter();
        private readonly Dictionary<string, ICameraInfo> _dictDevices = new Dictionary<string, ICameraInfo>();

        public event EventHandler<Bitmap> FrameChanged;
        public event EventHandler<string> CameraChanged;
        public event EventHandler<string[]> CameraListChanged;

        public void Capture()
        {
            if (_camera != null && !_isRunning)
            {
                _isRunning = true;
                Configuration.AcquireSingleFrame(_camera, null);
                _camera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                //using (IGrabResult result = _camera.StreamGrabber.GrabOne(1000))
                //{
                //    if (result.GrabSucceeded)
                //    {
                //        Bitmap bmp = new Bitmap(result.Width, result.Height, PixelFormat.Format32bppRgb);
                //        // Lock the bits of the bitmap.
                //        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                //        //Place the pointer to the buffer of the bitmap.
                //        _converter.OutputPixelFormat = PixelType.BGRA8packed;
                //        IntPtr ptrBmp = bmpData.Scan0;
                //        _converter.Convert(ptrBmp, bmpData.Stride * bmp.Height, result);
                //        bmp.UnlockBits(bmpData);

                //        //byte[] buffer = result.PixelData as byte[];
                //        //Bitmap bmp = new Bitmap(result.Width, result.Height, result.ComputeStride() ?? result.Width
                //        //    , PixelFormat.Format24bppRgb
                //        //    , Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0));
                //        FrameChanged?.Invoke(this, bmp);
                //    }
                //}
            }
        }
        public void GetCameraList()
        {
            _dictDevices.Clear();
            foreach (ICameraInfo camera in CameraFinder.Enumerate())
            {
                string fullName = camera[CameraInfoKey.FullName];
                if (!_dictDevices.ContainsKey(fullName))
                {
                    _dictDevices.Add(fullName, camera);
                }
            }
            CameraListChanged?.Invoke(this, _dictDevices.Keys.ToArray());
        }
        public void DestroyCamera()
        {
            if (_camera != null)
            {
                Console.WriteLine($"Pylon - {_camera.CameraInfo[CameraInfoKey.FriendlyName]} is closed");
                _camera.Close();
                _camera.Dispose();
                _camera = null;
            }
        }
        public void OpenCamera(string key)
        {
            DestroyCamera();
            if (_dictDevices.TryGetValue(key, out ICameraInfo info))
            {
                _camera = new Camera(info);
                _camera.CameraOpened += Configuration.AcquireContinuous;
                _camera.StreamGrabber.ImageGrabbed += StreamGrabber_ImageGrabbed;
                _camera.Open();
                Console.WriteLine($"Pylon - {info[CameraInfoKey.FriendlyName]} is opened");
            }
        }

        private void StreamGrabber_ImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            lock (_lock)
            {
                _isRunning = false;
                using (IGrabResult result = e.GrabResult.Clone())
                {
                    if (result.GrabSucceeded)
                    {
                        Bitmap bmp = new Bitmap(result.Width, result.Height, PixelFormat.Format32bppRgb);
                        // Lock the bits of the bitmap.
                        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                        //Place the pointer to the buffer of the bitmap.
                        _converter.OutputPixelFormat = PixelType.BGRA8packed;
                        IntPtr ptrBmp = bmpData.Scan0;
                        _converter.Convert(ptrBmp, bmpData.Stride * bmp.Height, result);
                        bmp.UnlockBits(bmpData);

                        //byte[] buffer = result.PixelData as byte[];
                        //Bitmap bmp = new Bitmap(result.Width, result.Height, result.ComputeStride() ?? result.Width
                        //    , PixelFormat.Format24bppRgb
                        //    , Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0));
                        FrameChanged?.Invoke(this, bmp);
                    }
                }
            }
        }
    }
}
