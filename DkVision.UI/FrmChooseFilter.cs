using DkVision.Core.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DkVision.UI
{
    public partial class FrmChooseFilter : Form
    {
        private IDkFilter _filter = null;

        public FrmChooseFilter()
        {
            InitializeComponent();
            btnNextStep.Click += BtnNextStep_Click;
            btnPrevStep.Click += BtnPrevStep_Click;
            lsbFilters.SelectedIndexChanged += LsbFilters_SelectedIndexChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var filterTypes = typeof(IDkFilter).Assembly.GetTypes().Where(t => t.GetInterface(nameof(IDkFilter)) != null);
            if (filterTypes?.Any() == true)
            {
                lsbFilters.DataSource = filterTypes;
                lsbFilters.DisplayMember = "Name";
            }
        }
        private void BtnPrevStep_Click(object sender, EventArgs e)
        {
            if (tcSteps.SelectedIndex > 0)
            {
                tcSteps.SelectedIndex--;
            }
        }
        private void BtnNextStep_Click(object sender, EventArgs e)
        {
            if (tcSteps.SelectedIndex < tcSteps.TabPages.Count - 1)
            {
                tcSteps.SelectedIndex++;
            }
        }
        private void LsbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _filter = (IDkFilter)Activator.CreateInstance(lsbFilters.SelectedItem as Type);
            if (_filter != null)
            {
                propGridFilter.SelectedObject = _filter;
            }
        }
    }
}
