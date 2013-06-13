using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using DV.TeleCallerHelper.SearchHelpers.Views;

namespace DV.TeleCallerHelper.SearchHelpers
{
    /// <summary>
    /// Initializer class for Module A of the Prism 4 Demo.
    /// </summary>
    public class SearchHelperModule : IModule
    {
        #region IModule Members

        /// <summary>
        /// Initializes the module.
        /// </summary>
        public void Initialize()
        {
            // Register task button with Prism Region
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.RegisterViewWithRegion("RibbonRegion", typeof(SearchHelperRibbonTab));
            //regionManager.RegisterViewWithRegion("WorkspaceRegion", typeof(MainContainer));

            // Register other view objects with DI Container (Unity)
            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            var view = container.Resolve<MainContainer>();
            var mainRegion = regionManager.Regions["MainRegion"];
            mainRegion.Add(view);
            mainRegion.Activate(view);
        }

        #endregion
    }
}
