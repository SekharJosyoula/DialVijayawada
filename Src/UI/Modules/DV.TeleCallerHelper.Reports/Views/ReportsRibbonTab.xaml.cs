using Microsoft.Practices.Prism.Regions;
using Microsoft.Windows.Controls.Ribbon;

namespace DV.TeleCallerHelper.Reports.Views
{
    /// <summary>
    /// Interaction logic for ModuleARibbonTab.xaml
    /// </summary>
    public partial class ReportsRibbonTab : RibbonTab, IRegionMemberLifetime
    {
        #region Constructor

        public ReportsRibbonTab()
        {
            InitializeComponent();
        }

        #endregion

        #region IRegionMemberLifetime Members

        public bool KeepAlive
        {
            get { return false; }
        }

        #endregion
    }
}
