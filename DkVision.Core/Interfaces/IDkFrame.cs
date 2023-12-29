using System;
using System.Drawing;

namespace DkVision.Core.Interfaces
{
    public interface IDkFrame
    {
        int Width { get; }
        int Height { get; }
        event EventHandler<Bitmap> FrameChanged;
        void ChangeSize(Size size);
    }
}
