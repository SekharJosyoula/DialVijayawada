using Microsoft.Practices.Prism.Regions;
using Microsoft.Windows.Controls.Ribbon;

namespace DV.TeleCallerHelper.Admin.Views
{
    /// <summary>
    /// Interaction logic for ModuleARibbonTab.xaml
    /// </summary>
    public partial class AdminRibbonTab : RibbonTab, IRegionMemberLifetime
    {
        #region Constructor

        public AdminRibbonTab()
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
