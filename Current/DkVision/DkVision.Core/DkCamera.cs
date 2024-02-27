using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace DkVision.Core
{
    public abstract class DkCamera
    {
        public const int FPS_60 = 1000 / 60;

        protected Mat _frame = new Mat();
        protected bool _IsConnected = false;
        protected bool _IsCapturing = false;
        protected Size _FrameSize = new Size();
        protected string[] _Devices = new string[0];
        protected string _CurrentDevice = string.Empty;
        protected readonly List<DkFilter> _Filters = new List<DkFilter>();
        protected readonly Timer _Timer = new Timer(FPS_60) { Enabled = false };

        public event EventHandler RecordStopped;
        public event EventHandler RecordStarted;
        public event EventHandler<string> Connected;
        public event EventHandler<string> Disconnected;
        public event EventHandler<MemoryStream> Captured;
        public event EventHandler<string[]> DevicesChanged;

        public bool IsCapturing => _IsCapturing;
        public bool IsConnected => _IsConnected;
        public Size FrameSize => _FrameSize;

        public DkCamera() : this(100, 100) { }
        public DkCamera(double frameWidth, double frameHeight)
        {
            Resize(frameWidth, frameHeight);
            _Timer.Elapsed += Timer_Elapsed;
        }

        public void Stop()
        {
            _IsCapturing = false;
            _Timer.Stop();
            _Timer.Enabled = false;
            OnRecordStopped();
        }
        public void Record()
        {
            if (IsConnected)
            {
                _IsCapturing = true;
                _Timer.Enabled = true;
                _Timer.Start();
                OnRecordStarted();
            }
        }
        public abstract void Capture();
        public abstract void Disconnect();
        public abstract void UpdateListDevices();
        public void AddFilter(DkFilter filter)
        {
            _Filters.Add(filter);
        }
        public abstract void Connect(string deviceName);
        public void Resize(double frameWidth, double frameHeight)
        {
            _FrameSize = new OpenCvSharp.Size(frameWidth, frameHeight);
        }

        protected virtual void OnCaptured()
        {
            if (_frame != null && _frame.Data != IntPtr.Zero)
            {
                Mat img = new Mat();
                Cv2.Resize(_frame, img, _FrameSize);
                //if (!_IsCapturing)
                {
                    foreach (var filter in _Filters)
                    {
                        img = filter.Filter(img);
                    }
                }
                MemoryStream stream = img.ToMemoryStream();
                Captured?.Invoke(this, stream);
            }
        }
        protected virtual void OnRecordStarted()
        {
            RecordStarted?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnRecordStopped()
        {
            RecordStopped?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnConnected(string deviceName)
        {
            _IsConnected = true;
            _CurrentDevice = deviceName;
            Connected?.Invoke(this, deviceName);
        }
        protected virtual void OnDisconnected(string deviceName)
        {
            _IsConnected = false;
            Disconnected?.Invoke(this, deviceName);
        }
        protected virtual void OnDevicesChanged(string[] devices)
        {
            _Devices = devices;
            DevicesChanged?.Invoke(this, devices);
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_IsCapturing)
            {
                Capture();
            }
        }
    }
}
