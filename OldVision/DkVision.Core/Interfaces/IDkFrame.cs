using System;
using System.Drawing;

namespace DkVision.Core.Interfaces
{
    public interface IDkFrame
    {
        int Width { get; }
        int Height { get; }
        IDkCamera Camera { get; set; }
        event Action<Bitmap> FrameChanged;
        void ChangeSize(Size size);
    }
}
