using DV.TeleCallerHelper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.Admin.ViewModels
{
    public class MainContainerViewModel : TabbedViewModelBase
    {
        private SearchViewModel _searchViewModel;

        public SearchViewModel SearchViewModel
        {
            get { return _searchViewModel; }
            set
            {
                _searchViewModel = value;
                this.RaisePropertyChanged("SearchRowViewModel");
            }
        }

        public MainContainerViewModel()
        {
            ViewTitle = "Admin - Business Units";
            this.SearchViewModel = new SearchViewModel(GetFilterColumns());
        }

        private List<string> GetFilterColumns()
        {
            return new List<string>() { "BusinessUnitName", "PhoneNumber" };
        }
    }
}
