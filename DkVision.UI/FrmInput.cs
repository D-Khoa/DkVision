using System;
using System.Windows.Forms;

namespace DkVision.UI
{
    public partial class FrmInput : Form
    {
        public string Input
        {
            get => txtInput?.Text;
            set => txtInput.Text = value;
        }

        public FrmInput()
        {
            InitializeComponent();
            btnConfirm.Click += BtnConfirm_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtInput.SelectAll();
            txtInput.Focus();
        }
        
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
