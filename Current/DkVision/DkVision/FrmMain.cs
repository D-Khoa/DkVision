using DkVision.Core;
using DkVision.Core.Cameras;
using DkVision.UserControls;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DkVision
{
    public partial class FrmMain : Form
    {
        private DkCamera _Camera = null;
        private readonly TreeNode _rootUsbDevices;
        private readonly TreeNode _rootBaslerDevices;
        private readonly DkCameraUSB _USBCamera = new DkCameraUSB();
        private readonly DkCameraBasler _BaslerCamera = new DkCameraBasler();

        public DkCamera Camera => _Camera;

        public FrmMain()
        {
            InitializeComponent();
            dgvShapeViews.DataSource = new BindingSource(frameMain.Shapes, null);
            DefineDatagridView();
            btnRecord.Click += BtnRecord_Click;
            btnDrawCirlce.Click += BtnDraw_Click;
            btnDrawPolygon.Click += BtnDraw_Click;
            btnDrawRectangle.Click += BtnDraw_Click;
            btnDrawCancel.Click += BtnDrawCancel_Click;
            btnDrawConfirm.Click += BtnDrawConfirm_Click;
            btnAutoAdjustment.Click += BtnAutoAdjustment_Click;
            _USBCamera.Connected += Camera_Connected;
            _USBCamera.RecordStarted += Camera_RecordStarted;
            _USBCamera.RecordStopped += Camera_RecordStopped;
            _USBCamera.DevicesChanged += USBCamera_DevicesChanged;
            _BaslerCamera.Connected += Camera_Connected;
            _BaslerCamera.RecordStopped += Camera_RecordStopped;
            _BaslerCamera.RecordStarted += Camera_RecordStarted;
            _BaslerCamera.DevicesChanged += BaslerCamera_DevicesChanged;
            trvCameras.NodeMouseDoubleClick += TrvCameras_NodeMouseDoubleClick;
            _rootUsbDevices = trvCameras.Nodes.Add("rootUSBDevices", "USB Devices");
            _rootBaslerDevices = trvCameras.Nodes.Add("rootBaslerDevices", "Basler Devices");
        }

        private void ResetRecord()
        {
            if (_Camera.IsCapturing)
            {
                btnRecord.Text = "Stop";
                btnRecord.Image = Properties.Resources.p32_stop;
            }
            else
            {
                btnRecord.Text = "Record";
                btnRecord.Image = Properties.Resources.p32_camera_video;
            }
        }
        private void ResetDrawChecked()
        {
            btnDrawCirlce.Checked = false;
            btnDrawPolygon.Checked = false;
            btnDrawRectangle.Checked = false;
            btnDrawConfirm.Visible = false;
            btnDrawCancel.Visible = false;
            txtShapeText.Visible = false;
        }
        private void DefineDatagridView()
        {
            if (dgvShapeViews.Columns.Contains("Text"))
                dgvShapeViews.Columns["Text"].HeaderText = "Shape Name";
            if (dgvShapeViews.Columns.Contains("Rect"))
                dgvShapeViews.Columns["Rect"].Visible = false;
            if (dgvShapeViews.Columns.Contains("Path"))
                dgvShapeViews.Columns["Path"].Visible = false;
            if (dgvShapeViews.Columns.Contains("Points"))
                dgvShapeViews.Columns["Points"].Visible = false;
            if (dgvShapeViews.Columns.Contains("Filters"))
                dgvShapeViews.Columns["Filters"].Visible = false;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _USBCamera.UpdateListDevices();
            _BaslerCamera.UpdateListDevices();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            btnRecord.Click -= BtnRecord_Click;
            btnDrawCirlce.Click -= BtnDraw_Click;
            btnDrawPolygon.Click -= BtnDraw_Click;
            btnDrawRectangle.Click -= BtnDraw_Click;
            btnDrawCancel.Click -= BtnDrawCancel_Click;
            btnDrawConfirm.Click -= BtnDrawConfirm_Click;
            _USBCamera.Connected -= Camera_Connected;
            _USBCamera.RecordStarted -= Camera_RecordStarted;
            _USBCamera.RecordStopped -= Camera_RecordStopped;
            _USBCamera.DevicesChanged -= USBCamera_DevicesChanged;
            _BaslerCamera.Connected -= Camera_Connected;
            _BaslerCamera.RecordStopped -= Camera_RecordStopped;
            _BaslerCamera.RecordStarted -= Camera_RecordStarted;
            _BaslerCamera.DevicesChanged -= BaslerCamera_DevicesChanged;
            trvCameras.NodeMouseDoubleClick -= TrvCameras_NodeMouseDoubleClick;
            _BaslerCamera.Disconnect();
            _USBCamera.Disconnect();
            _Camera?.Disconnect();
            _Camera = null;
            base.OnClosing(e);
        }
        private void Camera_Connected(object sender, string e)
        {
            if (sender is DkCamera camera)
            {
                frameMain.Camera = camera;
            }
        }
        private void BtnDraw_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripButton btn && int.TryParse(btn.Tag?.ToString(), out int sType))
            {
                ResetDrawChecked();
                btn.Checked = true;
                frameMain.BeginDraw((ShapeTypes)sType);
                btnDrawConfirm.Visible = true;
                btnDrawCancel.Visible = true;
                txtShapeText.Visible = true;
                txtShapeText.Text = $"Shape{frameMain.Shapes.Count + 1:00}";
            }
        }
        private void BtnRecord_Click(object sender, EventArgs e)
        {
            if (_Camera.IsCapturing)
                _Camera?.Stop();
            else
                _Camera?.Record();
        }
        private void BtnDrawCancel_Click(object sender, EventArgs e)
        {
            frameMain.CancelDraw();
            ResetDrawChecked();
        }
        private void BtnDrawConfirm_Click(object sender, EventArgs e)
        {
            frameMain.ConfirmDraw(txtShapeText.Text);
            ResetDrawChecked();
        }
        private void Camera_RecordStarted(object sender, EventArgs e)
        {
            ResetRecord();
        }
        private void Camera_RecordStopped(object sender, EventArgs e)
        {
            ResetRecord();
        }
        private void BtnAutoAdjustment_Click(object sender, EventArgs e)
        {
            _BaslerCamera?.AutoAdjustment();
        }
        private void USBCamera_DevicesChanged(object sender, string[] e)
        {
            _rootUsbDevices.Nodes.Clear();
            foreach (string camera in e)
            {
                _rootUsbDevices.Nodes.Add(new TreeNode(camera)
                {
                    Name = camera,
                    Tag = "USB"
                });
            }
            _rootUsbDevices.ExpandAll();
        }
        private void BtnRotate_CheckedChanged(object sender, EventArgs e)
        {
            frameMain.IsRotating = btnRotate.Checked;
        }
        private void BaslerCamera_DevicesChanged(object sender, string[] e)
        {
            _rootBaslerDevices.Nodes.Clear();
            foreach (string camera in e)
            {
                _rootBaslerDevices.Nodes.Add(new TreeNode(camera)
                {
                    Name = camera,
                    Tag = "Basler"
                });
            }
            _rootBaslerDevices.ExpandAll();
        }
        private void BtnShowText_CheckedChanged(object sender, EventArgs e)
        {
            frameMain.TextVisible = btnShowText.Checked;
        }
        private void TrvCameras_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (_Camera != null)
            {
                _Camera.Disconnect();
            }
            btnAutoAdjustment.Visible = false;
            string tag = e.Node.Tag?.ToString();
            switch (tag)
            {
                case "USB":
                    _Camera = _USBCamera;
                    break;
                case "Basler":
                    _Camera = _BaslerCamera;
                    btnAutoAdjustment.Visible = true;
                    break;
                default:
                    _Camera = null;
                    break;
            }
            _Camera?.Connect(e.Node.Text);
            _Camera?.Record();
        }

    }
}
