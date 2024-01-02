using DkVision.Core.Components;
using DkVision.Core.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DkVision.UI
{
    public partial class FrmSetMask : Form
    {
        private IDkFilter _filter = null;
        private ShapeStyle _paintTool = ShapeStyle.None;

        public string MaskName
        {
            get => txtFilterName.Text;
            set => txtFilterName.Text = value;
        }
        public IDkFilter Filter
        {
            get => _filter;
            set => SetFilter(value);
        }
        public ShapeStyle PaintTool
        {
            get => _paintTool;
            set => SetPaintTool(value);
        }

        public FrmSetMask()
        {
            InitializeComponent();
            btnNone.Click += BtnNone_Click;
            btnCircle.Click += BtnCircle_Click;
            btnRectangle.Click += BtnRectangle_Click;
            lsbFilters.SelectedIndexChanged += LsbFilters_SelectedIndexChanged;
        }

        private void SetFilter(IDkFilter filter)
        {
            lsbFilters.ClearSelected();
            _filter = filter;
            if (_filter != null)
            {
                propGridFilter.SelectedObject = _filter;
            }
            lblFilterName.Text = _filter == null ? "None" : _filter.GetType().Name;
        }
        private void SetPaintTool(ShapeStyle tool)
        {
            _paintTool = tool;
            switch (_paintTool)
            {
                case ShapeStyle.Rect:
                    btnRectangle.BackColor = System.Drawing.Color.PaleGreen;
                    break;
                case ShapeStyle.Circle:
                case ShapeStyle.Ellipse:
                    btnCircle.BackColor = System.Drawing.Color.PaleGreen;
                    break;
                case ShapeStyle.None:
                case ShapeStyle.Line:
                default:
                    btnNone.BackColor = System.Drawing.Color.PaleGreen;
                    break;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                Type[] filterTypes = typeof(IDkFilter).Assembly.GetTypes().Where(t => t.GetInterface(nameof(IDkFilter)) != null).ToArray();
                if (filterTypes?.Any() == true)
                {
                    lsbFilters.DataSource = filterTypes;
                    lsbFilters.DisplayMember = "Name";
                }
            }
            finally
            {
                txtFilterName.SelectAll();
                txtFilterName.Focus();
            }
        }
        private void BtnNone_Click(object sender, EventArgs e)
        {
            PaintTool = ShapeStyle.None;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void BtnCircle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaskName))
            {
                MessageBox.Show("Please enter filter name!", "Error");
                return;
            }
            PaintTool = ShapeStyle.Circle;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void BtnRectangle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaskName))
            {
                MessageBox.Show("Please enter filter name!", "Error");
                return;
            }
            PaintTool = ShapeStyle.Rect;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void LsbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbFilters.SelectedItem is Type type)
            {
                Filter = (IDkFilter)Activator.CreateInstance(type);
            }
        }
    }
}
