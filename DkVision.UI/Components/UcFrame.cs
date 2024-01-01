using DkVision.Core.Components;
using DkVision.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DkVision.UI.Components
{
    public partial class UcFrame : Panel
    {
        private DkMask _mask;
        private bool _isAuto = false;
        private bool _isPainting = false;
        private bool _isCapturing = false;
        private readonly IDkFrame _frame = new DkFrame();
        private readonly Pen _borderPen = new Pen(Brushes.Yellow);
        private readonly List<DkMask> _lstMasks = new List<DkMask>();

        public bool IsAuto
        {
            get => _isAuto;
            set => SetAuto(value);
        }
        public IDkCamera Camera
        {
            get => _frame.Camera;
            set => _frame.Camera = value;
        }
        public ShapeStyle PaintTool
        {
            get;
            set;
        }

        public UcFrame()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            Application.Idle += Application_Idle;
            _frame.FrameChanged += Frame_FrameChanged;
        }

        public void CameraCapture()
        {
            try
            {
                if (!_isCapturing)
                {
                    _isCapturing = true;
                    Camera?.Capture();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void SetAuto(bool value)
        {
            _isAuto = value;
            CameraCapture();
        }
        private void Frame_FrameChanged(Bitmap e)
        {
            try
            {
                foreach (DkMask mask in _lstMasks)
                {
                    if (!_isAuto)
                    {
                        //e = mask.Execute(e);
                    }
                    DrawMask(e, mask);
                }
                if (_isPainting)
                {
                    DrawMask(e, _mask);
                }
                this.BackgroundImage = e;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _isCapturing = false;
            }
        }
        private void DrawMask(Bitmap source, DkMask mask)
        {
            using (Graphics g = Graphics.FromImage(source))
            {
                switch (mask.Shape)
                {
                    case ShapeStyle.Line:
                        g.DrawLine(_borderPen, mask.BeginPoint, mask.EndPoint);
                        break;
                    case ShapeStyle.Rect:
                        g.DrawRectangle(_borderPen, mask.Rect);
                        break;
                    case ShapeStyle.Circle:
                    case ShapeStyle.Ellipse:
                        g.DrawEllipse(_borderPen, mask.Rect);
                        break;
                    case ShapeStyle.None:
                    default:
                        break;
                }
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            try
            {
                base.OnSizeChanged(e);
                _frame?.ChangeSize(this.Size);
                foreach (var mask in _lstMasks)
                {
                    mask.FrameSize = this.Size;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                base.OnMouseUp(e);
                if (_isPainting)
                {
                    _isPainting = false;
                    _lstMasks.Add(_mask);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                base.OnMouseDown(e);
                if (!_isPainting)
                {
                    _isPainting = true;
                    _mask = new DkMask(e.Location, PaintTool, this.Size);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                base.OnMouseMove(e);
                if (_isPainting)
                {
                    _mask.EndPoint = e.Location;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void Application_Idle(object sender, EventArgs e)
        {
            if (_isAuto) CameraCapture();
        }
    }
}
