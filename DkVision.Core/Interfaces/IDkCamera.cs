using System;
using System.Drawing;

namespace DkVision.Core.Interfaces
{
    public interface IDkCamera
    {
        event EventHandler<Bitmap> FrameChanged;
        event EventHandler<string> CameraChanged;
        event EventHandler<string[]> CameraListChanged;
        void Capture();
        void GetCameraList();
        void DestroyCamera();
        void OpenCamera(string key);
    }
}
