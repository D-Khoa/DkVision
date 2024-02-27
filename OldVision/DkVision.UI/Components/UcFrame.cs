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
        private readonly IDkFrame _frame = new DkFrame();
        private readonly Pen _borderPen = new Pen(Brushes.Yellow);
        private readonly Dictionary<string, DkMask> _dictMasks = new Dictionary<string, DkMask>();

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
        public event Action<DkMask> MaskAdded;

        public UcFrame()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            Application.Idle += Application_Idle;
            _frame.FrameChanged += Frame_FrameChanged;
        }

        public void AddMask()
        {
            FrmSetMask frmSetMask = new FrmSetMask()
            {
                MaskName = $"Mask_{_dictMasks.Count}",
                PaintTool = ShapeStyle.None,
                Filter = null,
            };
            if (frmSetMask.ShowDialog() == DialogResult.OK)
            {
                if (_dictMasks.ContainsKey(frmSetMask.MaskName))
                {
                    MessageBox.Show($"Mask name: {frmSetMask.MaskName} is exists!\nPlease choose another name!", "Warning");
                    frmSetMask = new FrmSetMask()
                    {
                        Name = frmSetMask.MaskName,
                        Filter = frmSetMask.Filter,
                        PaintTool = frmSetMask.PaintTool
                    };
                    if (frmSetMask.ShowDialog() == DialogResult.OK)
                    {
                        _mask = new DkMask(frmSetMask.MaskName, frmSetMask.PaintTool)
                        {
                            FrameSize = this.Size,
                            Filter = frmSetMask.Filter,
                        };
                        MaskAdded?.Invoke(_mask);
                    }
                    return;
                }
                _mask = new DkMask(frmSetMask.MaskName, frmSetMask.PaintTool)
                {
                    FrameSize = this.Size,
                    Filter = frmSetMask.Filter,
                };
                MaskAdded?.Invoke(_mask);
            }
        }
        public void CameraCapture()
        {
            try
            {
                if (Camera != null)
                {
                    Camera?.Capture();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void SetMask(string key)
        {
            if (_dictMasks.TryGetValue(key, out DkMask mask))
            {
                FrmSetMask frmSetMask = new FrmSetMask()
                {
                    Filter = mask.Filter,
                    MaskName = mask.Name,
                    PaintTool = mask.Shape
                };
                if (frmSetMask.ShowDialog() == DialogResult.OK)
                {
                    _dictMasks[key].FrameSize = this.Size;
                    _dictMasks[key].Filter = frmSetMask.Filter;
                    _dictMasks[key].Shape = frmSetMask.PaintTool;
                }
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
                if (!_isAuto)
                {
                    foreach (DkMask mask in _dictMasks.Values)
                    {
#if DEBUG
                        mask.IsDebug = true;
                        mask.Filter.IsDebug = true;
#endif
                        e = mask.Execute(e);
                    }
                }
                foreach (DkMask mask in _dictMasks.Values)
                {
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
        }
        private void DrawMask(Bitmap source, DkMask mask)
        {
            using (Graphics g = Graphics.FromImage(source))
            {
                int x = Math.Min(mask.BeginPoint.X, mask.EndPoint.X);
                int y = Math.Min(mask.BeginPoint.Y, mask.EndPoint.Y);
                g.DrawString(mask.Name, DefaultFont, Brushes.Yellow, x, y);
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
                foreach (var mask in _dictMasks.Values)
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
                    if (!_dictMasks.ContainsKey(_mask.Name))
                    {
                        _dictMasks.Add(_mask.Name, _mask);
                    }
                    _mask = null;
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
                if (_mask != null && !_isPainting)
                {
                    _isPainting = true;
                    _mask.BeginPoint = e.Location;
                    _mask.EndPoint = e.Location;
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
