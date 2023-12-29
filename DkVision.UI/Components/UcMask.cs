using DkVision.Core.Components;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DkVision.UI.Components
{
    public partial class UcMask : Panel
    {
        private static readonly object _lock = new object();
        private readonly Pen _borderPen = new Pen(Brushes.Yellow);
        public ShapeStyle PaintStyle { get; set; }
        public string Header { get; set; }
        public Size FrameSize { get; set; }
        public UcMask()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        public void ChangeSize(Size frameSize)
        {
            double xScale = (double)frameSize.Width / FrameSize.Width;
            double yScale = (double)frameSize.Height / FrameSize.Height;
            this.Size = new Size((int)(this.Width * xScale), (int)(this.Height * yScale));
            this.Location = new Point((int)(this.Location.X * xScale), (int)(this.Location.Y * yScale));
            FrameSize = frameSize;
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                lock (_lock)
                {
                    base.OnPaintBackground(e);
                    string text = string.IsNullOrWhiteSpace(this.Header) ? this.Name : this.Header;
                    e.Graphics.DrawString(text, DefaultFont, Brushes.Yellow, e.ClipRectangle.Left, e.ClipRectangle.Top);
                    switch (PaintStyle)
                    {
                        case ShapeStyle.Line:
                            e.Graphics.DrawLine(_borderPen,
                                e.ClipRectangle.Left, e.ClipRectangle.Top,
                                e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
                            break;
                        case ShapeStyle.Rect:
                            e.Graphics.DrawRectangle(_borderPen,
                                e.ClipRectangle.Left, e.ClipRectangle.Top,
                                e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
                            break;
                        case ShapeStyle.Circle:
                        case ShapeStyle.Ellipse:
                            e.Graphics.DrawEllipse(_borderPen,
                                e.ClipRectangle.Left, e.ClipRectangle.Top,
                                e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
                            break;
                        case ShapeStyle.None:
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
