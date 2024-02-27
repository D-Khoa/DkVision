using Basler.Pylon;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace DkVision.Core.Components
{
    public class DkPylonFrame : AbstractFrame
    {
        private Camera _camera = null;
        private bool _isGrabbing = false;
        private readonly PixelDataConverter _converter = new PixelDataConverter();
        private readonly Dictionary<string, ICameraInfo> _dictDevices = new Dictionary<string, ICameraInfo>();

        public DkPylonFrame(int width, int height) : base(width, height) { }

        public override void OneShot()
        {
            StopAuto();
            if (_camera != null && !_isGrabbing)
            {
                Console.WriteLine("Pylon - One shot");
                _isGrabbing = true;
                Configuration.AcquireSingleFrame(_camera, null);
                _camera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
        }
        public override void AutoShot()
        {
            if (_camera != null && !_isGrabbing)
            {
                _isGrabbing = true;
                Console.WriteLine("Pylon - Auto shot");
                Configuration.AcquireContinuous(_camera, null);
                _camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }

        }
        public override void StopAuto()
        {
            Console.WriteLine("Pylon - Stop auto shot");
            _camera?.StreamGrabber.Stop();
        }
        public override void DestroyCamera()
        {
            if (_camera != null)
            {
                Console.WriteLine($"Pylon - {_camera.CameraInfo[CameraInfoKey.FriendlyName]} is closed");
                _camera.Close();
                _camera.Dispose();
                _camera = null;
            }
        }
        public override void UpdateCameralist()
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
            OnUpdateCameralist(_dictDevices.Keys.ToArray());
        }
        public override void SetCamera(string name)
        {
            DestroyCamera();
            if (_dictDevices.TryGetValue(name, out ICameraInfo info))
            {
                _camera = new Camera(info);
                _camera.CameraOpened += Configuration.AcquireContinuous;
                _camera.StreamGrabber.GrabStarted += StreamGrabber_GrabStarted;
                _camera.StreamGrabber.ImageGrabbed += StreamGrabber_ImageGrabbed;
                _camera.StreamGrabber.GrabStopped += StreamGrabber_GrabStopped;
                _camera.Open();
                Console.WriteLine($"Pylon - {info[CameraInfoKey.FriendlyName]} is opened");
            }
        }
        private void StreamGrabber_GrabStarted(object sender, EventArgs e)
        {
            Console.WriteLine("Pylon - Grab Started");
        }
        private void StreamGrabber_GrabStopped(object sender, GrabStopEventArgs e)
        {
            _isGrabbing = false;
            Console.WriteLine("Pylon - Grab Stopped");
        }
        private void StreamGrabber_ImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            try
            {
                if (e.GrabResult.IsValid)
                {
                    Console.WriteLine("Pylon - Image Grabbed");
                    IGrabResult gResult = e.GrabResult;
                    Bitmap bmp = new Bitmap(gResult.Width, gResult.Height, PixelFormat.Format32bppRgb);
                    // Lock the bits of the bitmap.
                    BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                    //Place the pointer to the buffer of the bitmap.
                    _converter.OutputPixelFormat = PixelType.BGRA8packed;
                    IntPtr ptrBmp = bmpData.Scan0;
                    _converter.Convert(ptrBmp, bmpData.Stride * bmp.Height, gResult);
                    bmp.UnlockBits(bmpData);
                    Image<Bgr, byte> frame = new Image<Bgr, byte>(bmp);
                    OnFrameChanged(frame);
                }
            }
            finally
            {
                e.GrabResult.Dispose();
                e.DisposeGrabResultIfClone();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            _converter.Dispose();
            _dictDevices.Clear();
        }
    }

    public class GrabStopedEventArgs : EventArgs
    {
        public string ErrorMessage { get; }
        public GrabStopedEventArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
