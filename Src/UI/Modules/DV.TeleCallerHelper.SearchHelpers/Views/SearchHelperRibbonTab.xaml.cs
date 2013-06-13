using Microsoft.Practices.Prism.Regions;
using Microsoft.Windows.Controls.Ribbon;

namespace DV.TeleCallerHelper.SearchHelpers.Views
{
    /// <summary>
    /// Interaction logic for ModuleARibbonTab.xaml
    /// </summary>
    public partial class SearchHelperRibbonTab : RibbonTab, IRegionMemberLifetime
    {
        #region Constructor

        public SearchHelperRibbonTab()
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
