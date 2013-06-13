using DV.TeleCallerHelper.Admin.Views;
using DV.TeleCallerHelper.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.Admin.ViewModels
{
    public class AdminRibbonViewModel : ViewModelBase
    {
        private DelegateCommand _curdBUCommand;
        public DelegateCommand CurdBusinessUnitsCommand
        {
            get {
                return _curdBUCommand;
            }
            set
            {
                _curdBUCommand = value;
                this.RaisePropertyChanged("CurdBusinessUnitsCommand");
            }
        }

        public AdminRibbonViewModel()
        {
            CurdBusinessUnitsCommand = new DelegateCommand(ExecuteCurdBusinessUnitsCommand);
        }

        private void ExecuteCurdBusinessUnitsCommand()
        {
            // Register task button with Prism Region
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            // Register other view objects with DI Container (Unity)
            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            var view = container.Resolve<MainContainer>();
            var mainRegion = regionManager.Regions["MainRegion"];
            mainRegion.Add(view);
            mainRegion.Activate(view);
        }
    }
}
