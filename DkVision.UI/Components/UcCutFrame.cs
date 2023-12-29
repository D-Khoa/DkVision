using DkVision.Core.Shapes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DkVision.UI.Components
{
    public partial class UcCutFrame : UserControl
    {
        private DkShape _shape;

        public DkShape Shape
        {
            get => _shape;
            set
            {
                if (_shape != null)
                {
                    _shape.FrameChanged -= Shape_FrameChanged;
                    _shape = null;
                }
                _shape = value;
                _shape.FrameChanged += Shape_FrameChanged;
            }
        }

        public UcCutFrame()
        {
            InitializeComponent();
        }

        private void UpdateFrame(Bitmap img)
        {
            if (this.InvokeRequired)
            {
                Action<Bitmap> safeAction = new Action<Bitmap>(UpdateFrame);
                this.Invoke(safeAction, img);
                return;
            }
            lblName.Text = _shape.Name;
            picFrame.Image = img;
        }

        private void Shape_FrameChanged(object sender, Bitmap e)
        {
            UpdateFrame(e);
        }
    }
}
