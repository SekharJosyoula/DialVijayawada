using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Windows.Controls.Ribbon;
using DV.TeleCallerHelper.SearchHelpers;
using DV.TeleCallerHelper.Shell.Views;
using DV.TeleCallerHelper.Reports;
using DV.TeleCallerHelper.Admin;

namespace DV.TeleCallerHelper.Shell
{
    public class Bootstrapper : UnityBootstrapper
    {
        #region Method Overrides

        /// <summary>
        /// Populates the Module Catalog.
        /// </summary>
        /// <returns>A new Module Catalog.</returns>
        /// <remarks>
        /// This method uses the Module Discovery method of populating the Module Catalog. It requires
        /// a post-build event in each module to place the module assembly in the module catalog
        /// directory.
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = new ModuleCatalog();
            moduleCatalog.AddModule(typeof(SearchHelperModule));
            moduleCatalog.AddModule(typeof(ReportsModule));
            moduleCatalog.AddModule(typeof(AdminModule));
            
            return moduleCatalog;
        }

        /// <summary>
        /// Configures the default region adapter mappings to use in the application, in order 
        /// to adapt UI controls defined in XAML to use a region and register it automatically.
        /// </summary>
        /// <returns>The RegionAdapterMappings instance containing all the mappings.</returns>
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            // Call base method
            var mappings = base.ConfigureRegionAdapterMappings();
            if (mappings == null) return null;

            // Add custom mappings
            var ribbonRegionAdapter = ServiceLocator.Current.GetInstance<RibbonRegionAdapter>();
            mappings.RegisterMapping(typeof(Ribbon), ribbonRegionAdapter);

            // Set return value
            return mappings;
        }

        /// <summary>
        /// Instantiates the Shell window.
        /// </summary>
        /// <returns>A new ShellWindow window.</returns>
        protected override DependencyObject CreateShell()
        {
            /* This method sets the UnityBootstrapper.Shell property to the ShellWindow
             * we declared elsewhere in this project. Note that the UnityBootstrapper base 
             * class will attach an instance of the RegionManager to the new Shell window. */

            // initializing handlers
            GlobalExceptionHandler.AddGlobalHandlers();

            return new ShellWindow();
        }

        /// <summary>
        /// Displays the Shell window to the user.
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        #endregion
    }
}