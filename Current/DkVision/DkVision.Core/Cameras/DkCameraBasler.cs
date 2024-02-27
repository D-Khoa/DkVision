using Basler.Pylon;
using OpenCvSharp;
using System;
using System.Linq;

namespace DkVision.Core.Cameras
{
    public class DkCameraBasler : DkCamera
    {
        private Camera _camera = null;
        private ICameraInfo[] _devices;
        private bool _isGrabbing = false;
        private EnumName regionSelector;
        private string regionSelectorValue1, regionSelectorValue2;
        private readonly Version _sfnc2_0_0 = new Version(2, 0, 0);
        private readonly PixelDataConverter _converter = new PixelDataConverter();
        private BooleanName _autoFunctionAOIROIUseWhiteBalance, _autoFunctionAOIROIUseBrightness;
        private IntegerName _regionSelectorWidth, _regionSelectorHeight, _regionSelectorOffsetX, _regionSelectorOffsetY;

        public DkCameraBasler() : base() { }
        public DkCameraBasler(double frameWidth, double frameHeight) : base(frameWidth, frameHeight) { }

        public override void Capture()
        {
            if (_camera != null && IsConnected && !_isGrabbing)
            {
                _isGrabbing = true;
                //Configuration.AcquireContinuous(_camera, null);
                //_camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);

                Configuration.AcquireSingleFrame(_camera, null);
                _camera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
        }
        public override void Disconnect()
        {
            Stop();
            _camera?.Close();
            _camera?.Dispose();
            _camera = null;
            OnDisconnected(_CurrentDevice);
            _CurrentDevice = string.Empty;
        }
        public override void UpdateListDevices()
        {
            _devices = CameraFinder.Enumerate().Cast<ICameraInfo>().ToArray();
            string[] devices = _devices.Select(x => x[CameraInfoKey.FullName]).ToArray();
            OnDevicesChanged(devices);
        }
        public override void Connect(string deviceName)
        {
            Disconnect();
            foreach (ICameraInfo device in _devices)
            {
                if (deviceName == device[CameraInfoKey.FullName])
                {
                    _camera = new Camera(device);
                    _camera.CameraOpened += Configuration.AcquireContinuous;
                    _camera.StreamGrabber.ImageGrabbed += StreamGrabber_ImageGrabbed;
                    _camera.Open();
                    if (_camera.IsOpen)
                    {
                        OnConnected(deviceName);
                    }
                    break;
                }
            }
        }
        private void StreamGrabber_ImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            try
            {
                using (IGrabResult result = e.GrabResult.Clone())
                {
                    if (result.GrabSucceeded)
                    {
                        _converter.OutputPixelFormat = PixelType.BGR8packed;
                        byte[] buffer = new byte[_converter.GetBufferSizeForConversion(result)];
                        _converter.Convert(buffer, result);
                        _frame = new Mat(result.Height, result.Width, MatType.CV_8UC3, buffer);
                        OnCaptured();
                    }
                }
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
                _isGrabbing = false;
            }
        }

        public void AutoAdjustment()
        {
            try
            {
                GetROINodeIdentifier();
                // Set the pixel format to one from a list of ones compatible with this example
                string[] pixelFormats = new string[]
                {
                    PLCamera.PixelFormat.YUV422_YUYV_Packed,
                    PLCamera.PixelFormat.YCbCr422_8,
                    PLCamera.PixelFormat.BayerBG8,
                    PLCamera.PixelFormat.BayerRG8,
                    PLCamera.PixelFormat.BayerGR8,
                    PLCamera.PixelFormat.BayerGB8,
                    PLCamera.PixelFormat.Mono8
                };
                _camera.Parameters[PLCamera.PixelFormat].SetValue(pixelFormats);
                // Disable test image generator if available
                _camera.Parameters[PLCamera.TestImageSelector].TrySetValue(PLCamera.TestImageSelector.Off);
                _camera.Parameters[PLCamera.TestPattern].TrySetValue(PLCamera.TestPattern.Off);
                // Set the Auto Function ROI for luminance and white balance statistics.
                // We want to use ROI2 for gathering the statistics.
                if (_camera.Parameters[regionSelector].IsWritable)
                {
                    _camera.Parameters[regionSelector].SetValue(regionSelectorValue1);
                    _camera.Parameters[_autoFunctionAOIROIUseBrightness].SetValue(true);// ROI 1 is used for brightness control
                    _camera.Parameters[_autoFunctionAOIROIUseWhiteBalance].SetValue(true);// ROI 1 is used for white balance control
                }
                _camera.Parameters[regionSelector].SetValue(regionSelectorValue1);
                _camera.Parameters[_regionSelectorOffsetX].SetValue(_camera.Parameters[PLCamera.OffsetX].GetMinimum());
                _camera.Parameters[_regionSelectorOffsetY].SetValue(_camera.Parameters[PLCamera.OffsetY].GetMinimum());
                _camera.Parameters[_regionSelectorWidth].SetValue(_camera.Parameters[PLCamera.Width].GetMaximum());
                _camera.Parameters[_regionSelectorHeight].SetValue(_camera.Parameters[PLCamera.Height].GetMaximum());
                //Set the acquisition parameters to automatic mode
                if (_camera.GetSfncVersion() < _sfnc2_0_0) // Handling for older cameras
                {
                    _camera.Parameters[PLCamera.ProcessedRawEnable].TrySetValue(true);
                    _camera.Parameters[PLCamera.GammaEnable].TrySetValue(true);
                    _camera.Parameters[PLCamera.GammaSelector].TrySetValue(PLCamera.GammaSelector.sRGB);
                    _camera.Parameters[PLCamera.AutoTargetValue].TrySetValue(80);
                    _camera.Parameters[PLCamera.AutoFunctionProfile].TrySetValue(PLCamera.AutoFunctionProfile.GainMinimum);
                    _camera.Parameters[PLCamera.AutoGainRawLowerLimit].TrySetToMinimum();
                    _camera.Parameters[PLCamera.AutoGainRawUpperLimit].TrySetToMaximum();
                    _camera.Parameters[PLCamera.AutoExposureTimeAbsLowerLimit].TrySetToMinimum();
                    _camera.Parameters[PLCamera.AutoExposureTimeAbsUpperLimit].TrySetToMaximum();
                }
                else // Handling for newer cameras (using SFNC 2.0, e.g. USB3 Vision cameras)
                {
                    _camera.Parameters[PLCamera.AutoTargetBrightness].TrySetValue(0.3);
                    _camera.Parameters[PLCamera.AutoFunctionProfile].TrySetValue(PLCamera.AutoFunctionProfile.MinimizeGain);
                    _camera.Parameters[PLCamera.AutoGainLowerLimit].TrySetToMinimum();
                    _camera.Parameters[PLCamera.AutoGainUpperLimit].TrySetToMaximum();
                    double maxExposure = _camera.Parameters[PLCamera.AutoExposureTimeUpperLimit].GetMaximum();
                    // Reduce upper limit to one second for this example
                    if (maxExposure > 1000000)
                    {
                        maxExposure = 1000000;
                    }
                    _camera.Parameters[PLCamera.AutoExposureTimeUpperLimit].TrySetValue(maxExposure);
                }
                // Set all auto functions to once in this example
                _camera.Parameters[PLCamera.GainSelector].TrySetValue(PLCamera.GainSelector.All);
                _camera.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Once);
                _camera.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Once);
                _camera.Parameters[PLCamera.BalanceWhiteAuto].TrySetValue(PLCamera.BalanceWhiteAuto.Once);
                if (_camera.GetSfncVersion() < _sfnc2_0_0) // Handling for older cameras
                {
                    _camera.Parameters[PLCamera.LightSourceSelector].TrySetValue(PLCamera.LightSourceSelector.Daylight);
                }
                else // Handling for newer cameras (using SFNC 2.0, e.g. USB3 Vision cameras)
                {
                    _camera.Parameters[PLCamera.LightSourcePreset].TrySetValue(PLCamera.LightSourcePreset.Daylight5000K);
                }
                _camera.StreamGrabber.Start();
                for (int n = 0; n < 20; n++)            // For demonstration purposes, we will grab "only" 20 images.
                {
                    IGrabResult result = _camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                    using (result)
                    {
                        // Image grabbed successfully?
                        if (result.GrabSucceeded)
                        {
                            ImageWindow.DisplayImage(1, result);
                        }
                    }

                    //For demonstration purposes only. Wait until the image is shown.
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                // Error handling.
                Console.Error.WriteLine("Exception: {0}", e.Message);
            }
        }
        public void GetROINodeIdentifier()
        {
            if (_camera != null)
            {
                if (_camera.GetSfncVersion() < _sfnc2_0_0) // Handling for older cameras
                {
                    regionSelector = PLCamera.AutoFunctionAOISelector;
                    _regionSelectorOffsetX = PLCamera.AutoFunctionAOIOffsetX;
                    _regionSelectorOffsetY = PLCamera.AutoFunctionAOIOffsetY;
                    _regionSelectorWidth = PLCamera.AutoFunctionAOIWidth;
                    _regionSelectorHeight = PLCamera.AutoFunctionAOIHeight;
                    regionSelectorValue1 = PLCamera.AutoFunctionAOISelector.AOI1;
                    regionSelectorValue2 = PLCamera.AutoFunctionAOISelector.AOI2;
                    _autoFunctionAOIROIUseBrightness = PLCamera.AutoFunctionAOIUsageIntensity;
                    _autoFunctionAOIROIUseWhiteBalance = PLCamera.AutoFunctionAOIUsageWhiteBalance;
                }
                else // Handling for newer cameras (using SFNC 2.0, e.g. USB3 Vision cameras)
                {
                    regionSelector = PLCamera.AutoFunctionROISelector;
                    _regionSelectorOffsetX = PLCamera.AutoFunctionROIOffsetX;
                    _regionSelectorOffsetY = PLCamera.AutoFunctionROIOffsetY;
                    _regionSelectorWidth = PLCamera.AutoFunctionROIWidth;
                    _regionSelectorHeight = PLCamera.AutoFunctionROIHeight;
                    regionSelectorValue1 = PLCamera.AutoFunctionROISelector.ROI1;
                    regionSelectorValue2 = PLCamera.AutoFunctionROISelector.ROI2;
                    _autoFunctionAOIROIUseBrightness = PLCamera.AutoFunctionROIUseBrightness;
                    _autoFunctionAOIROIUseWhiteBalance = PLCamera.AutoFunctionROIUseWhiteBalance;
                }
            }
        }
    }
}
