using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace DV.TeleCallerHelper.SearchHelpers.Views
{
    /// <summary>
    /// Interaction logic for ModuleView.xaml
    /// </summary>
    public partial class MainContainer : UserControl, IRegionMemberLifetime
    {
        #region Constructor

        public MainContainer()
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
