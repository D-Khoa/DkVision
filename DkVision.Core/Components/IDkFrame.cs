using DkVision.Core.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DkVision.Core.Components
{
    public interface IDkFrame
    {
        event EventHandler<Bitmap> FrameChanged;
        event EventHandler<DkShape> ShapePainted;
        event EventHandler<string[]> CameralistChanged;

        List<DkShape> Shapes { get; }
        ShapeStyle PaintTool { get; set; }

        void OneShot();
        void AutoShot();
        void StopAuto();
        void UpdateCameralist();
        void DestroyCamera();
        void SetCamera(string name);
        //void DetectLines(DkShape shape);
    }
}
