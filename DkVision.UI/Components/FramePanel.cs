using DkVision.Core.Components;
using DkVision.Core.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DkVision.UI.Components
{
    /// <summary>
    /// Panel show images from camera
    /// </summary>
    public class FramePanel : Panel, IDkFrame
    {
        private AbstractFrame _frame;

        public event EventHandler<Bitmap> FrameChanged;
        public event EventHandler<DkShape> ShapePainted;
        public event EventHandler<string[]> CameralistChanged;

        public ShapeStyle PaintTool
        {
            get => _frame?.PaintTool ?? ShapeStyle.None;
            set
            {
                if (_frame != null)
                {
                    _frame.PaintTool = value;
                }
            }
        }
        public List<DkShape> Shapes => _frame?.Shapes;

        public FramePanel()
        {
            this.DoubleBuffered = true;
        }

        public void OneShot()
        {
            _frame.OneShot();
        }
        public void AutoShot()
        {
            _frame.AutoShot();
        }
        public void StopAuto()
        {
            _frame.StopAuto();
        }
        public void DestroyCamera()
        {
            _frame.DestroyCamera();
        }
        public void UpdateCameralist()
        {
            _frame.UpdateCameralist();
        }
        public void SetCamera(string name)
        {
            _frame.SetCamera(name);
        }
        public void Init(AbstractFrame frame)
        {
            _frame = frame;
            if (_frame != null)
            {
                _frame.FrameChanged += Frame_Changed;
                _frame.ShapePainted += Frame_ShapeAdded;
                _frame.CameralistChanged += Frame_CameralistChanged;
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            _frame?.Dispose();
            base.Dispose(disposing);
        }
        private void Frame_Changed(object sender, Bitmap e)
        {
            this.BackgroundImage = e;
            FrameChanged?.Invoke(this, e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _frame?.OnShapePainted();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _frame?.OnShapePaint(e.Location);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _frame?.OnShapePainting(e.Location);
        }
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (_frame != null)
            {
                _frame.Width = this.Width;
                _frame.Height = this.Height;
            }
        }
        private void Frame_ShapeAdded(object sender, DkShape e)
        {
            ShapePainted?.Invoke(this, e);
        }
        private void Frame_CameralistChanged(object sender, string[] e)
        {
            CameralistChanged?.Invoke(this, e);
        }
    }
}
