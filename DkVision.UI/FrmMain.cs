using DkVision.Core.Components;
using DkVision.Core.Interfaces;
using DkVision.UI.Components;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DkVision.UI
{
    public partial class FrmMain : Form
    {
        private IDkFrame _frame;
        private IDkCamera _camera;
        private bool _isAuto = false;
        private readonly TreeNode _rootUsbCamera;
        private readonly TreeNode _rootBaslerCamera;
        private readonly IDkCamera _usbCamera = new DkEmgucvCamera();
        private readonly IDkCamera _baslerCamera = new DkBaslerCamera();
        private readonly Dictionary<string, UcFrame> _cutFrames = new Dictionary<string, UcFrame>();

        public FrmMain()
        {
            InitializeComponent();
            #region Camera
            _rootUsbCamera = trvCamera.Nodes.Add("USB Camera");
            _rootBaslerCamera = trvCamera.Nodes.Add("Basler Camera");

            trvCamera.AfterSelect += TrvCamera_AfterSelect;
            _usbCamera.CameraListChanged += UsbCamera_CameraListChanged;
            _baslerCamera.CameraListChanged += BaslerCamera_CameraListChanged;
            #endregion
            #region Tool Buttons
            btnLive.Click += BtnLive_Click;
            btnCapture.Click += BtnCapture_Click;
            btnRefreshCameraList.Click += BtnRefreshCameraList_Click;
            #endregion
        }

        #region Form events
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                _usbCamera.GetCameraList();
                _baslerCamera.GetCameraList();
                Application.Idle += Application_Idle;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            try
            {
                base.OnClosed(e);
                _camera?.DestroyCamera();
                _usbCamera?.DestroyCamera();
                _baslerCamera?.DestroyCamera();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void MnuiBtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
        #region Camera
        private void Application_Idle(object sender, EventArgs e)
        {
            try
            {
                if (_isAuto) _camera?.Capture();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void UsbCamera_CameraListChanged(object sender, string[] e)
        {
            try
            {
                foreach (string camera in e)
                {
                    if (!_rootUsbCamera.Nodes.ContainsKey(camera))
                    {
                        TreeNode node = _rootUsbCamera.Nodes.Add(camera, camera);
                        node.Tag = sender;
                    }
                }
                trvCamera.ExpandAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void BaslerCamera_CameraListChanged(object sender, string[] e)
        {
            try
            {
                foreach (string camera in e)
                {
                    if (!_rootBaslerCamera.Nodes.ContainsKey(camera))
                    {
                        TreeNode node = _rootBaslerCamera.Nodes.Add(camera, camera);
                        node.Tag = sender;
                    }
                }
                trvCamera.ExpandAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void TrvCamera_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Tag is IDkCamera camera)
                {
                    _camera?.DestroyCamera();
                    _camera = camera;
                    if (_frame != null)
                    {
                        //_frame.FrameAdded -= Frame_FrameAdded;
                        _frame = null;
                    }
                    _frame = new DkFrame(_camera);
                    //_frame.FrameAdded += Frame_FrameAdded;
                    ucMainFrame.SetFrame(_frame);
                    _camera.OpenCamera(e.Node.Text);
                    ToggleAuto(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
        #region Tool buttons
        private void ToggleAuto(bool isAuto)
        {
            _isAuto = isAuto;
            if (_isAuto)
            {
                btnLive.Text = "Stop";
                btnLive.Image = Properties.Resources.p32_stop;
            }
            else
            {
                btnLive.Text = "Live";
                btnLive.Image = Properties.Resources.p32_camera_video;
            }
        }
        private void BtnLive_Click(object sender, EventArgs e)
        {
            ToggleAuto(!_isAuto);
        }
        private void BtnCapture_Click(object sender, EventArgs e)
        {
            try
            {
                ToggleAuto(false);
                _camera?.Capture();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void BtnRefreshCameraList_Click(object sender, EventArgs e)
        {
            try
            {
                _usbCamera.GetCameraList();
                _baslerCamera.GetCameraList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void SbtnDrawTool_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (sender is ToolStripSplitButton sbtn)
                {
                    sbtn.Text = e.ClickedItem.Text;
                    sbtn.Image = e.ClickedItem.Image;
                    if (!int.TryParse(e.ClickedItem.Tag?.ToString(), out int toolId))
                    {
                        toolId = 0;
                    }
                    ucMainFrame.PaintTool = (ShapeStyle)toolId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
