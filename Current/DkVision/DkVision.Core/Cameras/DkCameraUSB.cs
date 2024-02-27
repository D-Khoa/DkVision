using DirectShowLib;
using OpenCvSharp;
using System;
using System.Linq;

namespace DkVision.Core.Cameras
{
    public class DkCameraUSB : DkCamera
    {
        private VideoCapture _capture = null;

        public DkCameraUSB() : base() { }
        public DkCameraUSB(double frameWidth, double frameHeight) : base(frameWidth, frameHeight)
        {
        }

        public override void Capture()
        {
            lock (_frame)
            {
                _capture.Read(_frame);
                OnCaptured();
            }
        }
        public override void Disconnect()
        {
            Stop();
            _capture?.Dispose();
            _capture = null;
            OnDisconnected(_CurrentDevice);
            _CurrentDevice = string.Empty;
        }
        public override void UpdateListDevices()
        {
            string[] devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice).Select(x => x.Name).ToArray();
            OnDevicesChanged(devices);
        }
        public override void Connect(string deviceName)
        {
            Disconnect();
            for (int i = 0; i < _Devices.Length; i++)
            {
                if (_Devices[i] == deviceName)
                {
                    _capture = new VideoCapture();
                    _capture.Open(i);
                    if (_capture.CvPtr != IntPtr.Zero)
                    {
                        OnConnected(deviceName);
                    }
                    break;
                }
            }
        }
    }
}
