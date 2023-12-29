using DirectShowLib;
using Emgu.CV;
using System.Linq;
using System.Timers;

namespace DkVision.Core.Components
{
    public class DkCaptureFrame : AbstractFrame
    {
        private Capture _camera;
        private readonly Timer _timer;
        private string[] _camlist = new string[0];

        public DkCaptureFrame(int width, int height) : base(width, height)
        {
            _timer = new Timer()
            {
                Enabled = false,
                AutoReset = true,
                Interval = 1000 / 60,
            };
            _timer.Elapsed += Timer_Elapsed;
        }
        public override void OneShot()
        {
            if (_camera == null) return;
            using (Image<Emgu.CV.Structure.Bgr, byte> frame = _camera.QueryFrame())
            {
                OnFrameChanged(frame);
            }
        }
        public override void AutoShot()
        {
            if (!_timer.Enabled)
            {
                _timer.Enabled = true;
                _timer.Start();
            }
        }
        public override void StopAuto()
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
                _timer.Enabled = false;
            }
        }
        public override void DestroyCamera()
        {
            _camera?.Dispose();
            _camera = null;
        }
        public override void UpdateCameralist()
        {
            DsDevice[] devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            _camlist = devices.Select(x => x.Name).ToArray();
            OnUpdateCameralist(_camlist);
        }
        public override void SetCamera(string name)
        {
            DestroyCamera();
            for (int i = 0; i < _camlist.Length; i++)
            {
                if (name == _camlist[i])
                {
                    _camera = new Capture(i);
                    return;
                }
            }
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OneShot();
        }

        public override void Dispose()
        {
            _timer.Dispose();
            base.Dispose();
        }
    }
}
