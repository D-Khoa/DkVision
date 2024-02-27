using DkVision.Core.Components;
using DkVision.Core.Interfaces;
using System;
using System.Windows.Forms;

namespace DkVision.UI
{
    public partial class FrmMain : Form
    {
        private readonly TreeNode _rootUsbCamera;
        private readonly TreeNode _rootBaslerCamera;
        private readonly IDkCamera _usbCamera = new DkEmgucvCamera();
        private readonly IDkCamera _baslerCamera = new DkBaslerCamera();

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
            btnFilter.Click += BtnFilter_Click;
            btnCapture.Click += BtnCapture_Click;
            btnRefreshCameraList.Click += BtnRefreshCameraList_Click;
            #endregion
            ucMainFrame.MaskAdded += UcMainFrame_MaskAdded;
        }

        #region Form events
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                _usbCamera.GetCameraList();
                _baslerCamera.GetCameraList();
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
                _usbCamera?.DestroyCamera();
                _baslerCamera?.DestroyCamera();
                ucMainFrame.Camera?.DestroyCamera();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void UcMainFrame_MaskAdded(DkMask obj)
        {
            Button btnSetMask = new Button()
            {
                Text = obj.Name,
                Dock = DockStyle.Left
            };
            btnSetMask.Click += BtnSetMask_Click;
            pnlMasks.Controls.Add(btnSetMask);
        }
        private void BtnSetMask_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                ucMainFrame.SetMask(btn.Text);
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
                    ucMainFrame.Camera = camera;
                    ucMainFrame.Camera.OpenCamera(e.Node.Text);
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
            ucMainFrame.IsAuto = isAuto;
            if (ucMainFrame.IsAuto)
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
            try
            {
                ToggleAuto(!ucMainFrame.IsAuto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void BtnFilter_Click(object sender, EventArgs e)
        {
            ucMainFrame.AddMask();
        }
        private void BtnCapture_Click(object sender, EventArgs e)
        {
            try
            {
                ToggleAuto(false);
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
        #endregion
    }
}
