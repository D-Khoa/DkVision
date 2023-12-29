using DkVision.Core.Components;
using DkVision.Core.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DkVision.UI.Components
{
    public partial class UcFrame : Panel
    {
        private IDkFrame _frame;
        private bool _isPainting;
        private UcMask _mask;

        public ShapeStyle PaintTool { get; set; }

        public UcFrame()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        public void SetFrame(IDkFrame frame)
        {
            if (_frame != null) _frame.FrameChanged -= Frame_FrameChanged;
            _frame = frame;
            if (_frame != null)
            {
                _frame.ChangeSize(this.Size);
                _frame.FrameChanged += Frame_FrameChanged;
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _mask?.ChangeSize(this.Size);
            _frame?.ChangeSize(this.Size);
        }
        private void Frame_FrameChanged(object sender, Bitmap e)
        {
            this.BackgroundImage = e;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (_isPainting)
            {
                _isPainting = false;
                using (FrmInput frmInput = new FrmInput())
                {
                    if (frmInput.ShowDialog() == DialogResult.OK)
                    {
                        _mask.Header = frmInput.Input;
                        return;
                    }
                    this.Controls.Remove(_mask);
                }
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!_isPainting)
            {
                _isPainting = true;
                _mask = new UcMask()
                {
                    Location = e.Location,
                    PaintStyle = PaintTool,
                    FrameSize = this.Size
                };
                this.Controls.Add(_mask);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_isPainting)
            {
                int w = Math.Abs(e.Location.X - _mask.Location.X);
                int h = Math.Abs(e.Location.Y - _mask.Location.Y);
                _mask.Size = new Size(w, h);
                int x = Math.Min(e.Location.X, _mask.Location.X);
                int y = Math.Min(e.Location.Y, _mask.Location.Y);
                _mask.Location = new Point(x, y);
            }
        }
    }
}
