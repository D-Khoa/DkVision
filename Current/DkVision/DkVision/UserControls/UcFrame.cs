using DkVision.Core;
using DkVision.Core.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DkVision.UserControls
{
    public partial class UcFrame : UserControl
    {
        private DkCamera _Camera = null;
        private bool _IsDrawing = false;
        private bool _IsPointing = false;
        private bool _IsRotating = false;
        private PointF _RotatePoint = PointF.Empty;
        private readonly Timer _RotateTimer = new Timer();
        private ShapeTypes _DrawType = ShapeTypes.Rectangle;
        private readonly List<PointF> _DrawingPoints = new List<PointF>();

        public DkCamera Camera
        {
            get => _Camera;
            set => SetCamera(value);
        }
        public bool IsDrawing => _IsDrawing;
        public ShapeTypes DrawType => _DrawType;
        public bool IsRotating
        {
            get => _IsRotating;
            set => SetRotate(value);
        }
        public bool TextVisible { get; set; } = false;
        public BindingList<DkShape> Shapes { get; } = new BindingList<DkShape>();

        public UcFrame()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _RotateTimer.Interval = 100;
            _RotateTimer.Enabled = false;
            _RotateTimer.Tick += RotateTimer_Tick;
        }

        public void CancelDraw()
        {
            _DrawingPoints.Clear();
            _Camera?.Record();
            _IsDrawing = false;
        }
        public void ConfirmDraw(string shapeText)
        {
            if (_DrawingPoints.Count > 0)
            {
                DkShape shape = new DkShape(_DrawType, _DrawingPoints.ToArray())
                {
                    Text = shapeText,
                };
                Shapes.Add(shape);
                Shapes.ResetBindings();
                _DrawingPoints.Clear();
                _Camera?.AddFilter(new DkCannyFilter(shape.CenterX, shape.CenterY, shape.Points));
                this.Invalidate();
            }
            _Camera?.Record();
            _IsDrawing = false;
        }
        public void BeginDraw(ShapeTypes type)
        {
            _DrawingPoints.Clear();
            _Camera?.Stop();
            _DrawType = type;
            _IsDrawing = true;
        }

        private void SetRotate(bool value)
        {
            _IsRotating = value;
            if (_IsRotating)
            {
                _Camera?.Stop();
            }
            else
            {
                _Camera?.Record();
            }
        }
        private void SetCamera(DkCamera camera)
        {
            if (_Camera != null)
            {
                _Camera.Captured -= Camera_Captured;
                _Camera.Disconnect();
                _Camera = null;
            }
            _Camera = camera;
            if (_Camera != null)
            {
                _Camera.Captured += Camera_Captured;
                _Camera.Resize(this.Width, this.Height);
                _Camera.Record();
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_Camera != null)
            {
                _Camera.Resize(this.Width, this.Height);
            }
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            // This code defines a graphics shape using a GraphicsPath
            // and draws multiple copies along a grid and with various
            // rotation angle angles.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            for (int i = 0; i < Shapes.Count; i++)
            {
                if (Shapes[i].IsVisible)
                {
                    //Shapes[i].Path.SetMarkers();
                    // Save the default transformation state
                    var state = e.Graphics.Save();
                    // Traslate the origin to (x,y), each grid point
                    e.Graphics.TranslateTransform(Shapes[i].CenterX, Shapes[i].CenterY);
                    // Rotate shape by an angle (negative = CCW)
                    e.Graphics.RotateTransform(-Shapes[i].RotateAngle);
                    var pen = Shapes[i].IsSelected ? Pens.Red : Pens.LightGreen;
                    var brush = Shapes[i].IsSelected ? Brushes.Red : Brushes.LightGreen;
                    if (IsRotating && Shapes[i].IsSelected)
                    {
                        e.Graphics.FillEllipse(brush, new RectangleF(-5, -5, 10, 10));
                        e.Graphics.DrawLine(pen, 0, 0, Shapes[i].AngleX, Shapes[i].AngleY);
                    }
                    switch (Shapes[i].ShapeType)
                    {
                        case ShapeTypes.Circle:
                            e.Graphics.DrawEllipse(pen, Shapes[i].Rect);
                            break;
                        case ShapeTypes.Rectangle:
                            e.Graphics.DrawRectangle(pen, Shapes[i].Rect.X, Shapes[i].Rect.Y, Shapes[i].Rect.Width, Shapes[i].Rect.Height);
                            break;
                        case ShapeTypes.Polygon:
                            e.Graphics.DrawPath(pen, Shapes[i].Path);
                            break;
                        default:
                            break;
                    }
                    if (TextVisible)
                    {
                        e.Graphics.DrawString($"{i}-{Shapes[i].Text}", DefaultFont, brush, Shapes[i].Rect.Location);
                    }
                    // Restore the default transformation state
                    e.Graphics.Restore(state);
                }
            }
            if (_DrawingPoints.Count > 1)
            {
                float xMin = _DrawingPoints.Min(p => p.X);
                float yMin = _DrawingPoints.Min(p => p.Y);
                float xMax = _DrawingPoints.Max(p => p.X);
                float yMax = _DrawingPoints.Max(p => p.Y);
                switch (_DrawType)
                {
                    case ShapeTypes.Circle:
                        e.Graphics.DrawEllipse(Pens.Red, xMin, yMin, xMax - xMin, yMax - yMin);
                        break;
                    case ShapeTypes.Rectangle:
                        e.Graphics.DrawRectangle(Pens.Red, xMin, yMin, xMax - xMin, yMax - yMin);
                        break;
                    case ShapeTypes.Polygon:
                        e.Graphics.DrawPolygon(Pens.Red, _DrawingPoints.ToArray());
                        break;
                    default:
                        break;
                }
            }
            //// Step 1 - Define a rectangle 20 by 12 pixels, center at origin.
            //var gp = new GraphicsPath();
            //gp.AddLines(new PointF[] {
            //        new PointF(-10, -6),
            //        new PointF( 10, -6),
            //        new PointF( 10,  6),
            //        new PointF(-10,  6) });
            //gp.CloseFigure();

            //// Step 2 - Define a 10×9 grid with two loops
            //float angle = 0;
            //for (int i = 0; i < 9; i++)
            //{
            //    // divide the control height into 10 divisions
            //    float y = (i + 1) * this.Height / 10;
            //    for (int j = 0; j < 10; j++)
            //    {
            //        // divide the control width into 11 divisions
            //        float x = (j + 1) * this.Width / 11;
            //        // Save the default transformation state
            //        var state = e.Graphics.Save();

            //        // Traslate the origin to (x,y), each grid point
            //        e.Graphics.TranslateTransform(x, y);
            //        // Rotate shape by an angle (negative = CCW)
            //        e.Graphics.RotateTransform(-angle);

            //        // Draw the shape
            //        e.Graphics.FillPath(Brushes.LightSeaGreen, gp);
            //        e.Graphics.DrawPath(Pens.Black, gp);

            //        // Restore the default transformation state
            //        e.Graphics.Restore(state);
            //        e.Graphics.FillPolygon(Brushes.Red, new PointF[] {
            //            new PointF(x-1, y),
            //            new PointF(x+1, y),
            //            new PointF(x, y-1),
            //            new PointF(x, y+1),
            //        });

            //        // Increment the angle by one degree. 
            //        // The idea is to show all 90 degrees of rotation in a 10×9 grid.
            //        angle++;
            //    }
            //}
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_IsPointing && _DrawingPoints.Count > 0)
            {
                _DrawingPoints[_DrawingPoints.Count - 1] = e.Location;
                _IsPointing = false;
                this.Invalidate();
                return;
            }
            if (IsRotating)
            {
                _RotateTimer.Stop();
                _RotateTimer.Enabled = false;
                return;
            }
            base.OnMouseUp(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (IsDrawing)
            {
                _DrawingPoints.Add(e.Location);
                if (_DrawingPoints.Count == 1)
                {
                    _DrawingPoints.Add(e.Location);
                }
                this.Invalidate();
                _IsPointing = true;
                return;
            }
            if (IsRotating)
            {
                _RotatePoint = e.Location;
                _RotateTimer.Enabled = true;
                _RotateTimer.Start();
                return;
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_IsPointing && _DrawingPoints.Count > 0)
            {
                _DrawingPoints[_DrawingPoints.Count - 1] = e.Location;
                this.Invalidate();
                return;
            }
            base.OnMouseMove(e);
        }
        private void RotateTimer_Tick(object sender, EventArgs e)
        {
            foreach (DkShape shape in Shapes)
            {
                if (_RotatePoint.X > shape.CenterX)
                {
                    shape.RotateAngle--;
                }
                else
                {
                    shape.RotateAngle++;
                }
                this.Invalidate();
            }
        }
        private void Camera_Captured(object sender, MemoryStream e)
        {
            this.BackgroundImage = new Bitmap(e);
        }
    }
}
