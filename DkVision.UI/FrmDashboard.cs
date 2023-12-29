using DkVision.Core.Components;
using System;
using System.Windows.Forms;

namespace DkVision.UI
{
    public partial class FrmDashboard : Form
    {
        private readonly DkEmgucvCamera _camera = new DkEmgucvCamera();

        public FrmDashboard()
        {
            InitializeComponent();
            ucFrame.SetCamera(_camera);
            _camera.CameraListChanged += Camera_CameraListChanged;
        }

        private void Camera_CameraListChanged(object sender, string[] e)
        {
            if (e.Length > 0)
            {
                _camera.OpenCamera(e[0]);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _camera.GetCameraList();
            Application.Idle += Application_Idle;
        }
        protected override void OnClosed(EventArgs e)
        {
            _camera.DestroyCamera();
            base.OnClosed(e);
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            _camera.Capture();
        }
    }
}
