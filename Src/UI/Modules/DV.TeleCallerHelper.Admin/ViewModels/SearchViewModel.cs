using DV.TeleCallerHelper.Common;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.Admin.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private ObservableCollection<SearchRowViewModel> _searchRowViewModels;

        public ObservableCollection<SearchRowViewModel> SearchRowViewModels
        {
            get { return _searchRowViewModels; }
            set
            {
                _searchRowViewModels = value;
                this.RaisePropertyChanged("SearchRowViewModels");
            }
        }

        private DelegateCommand _addFilterCommand;

        public DelegateCommand AddFilterCommand
        {
            get { return _addFilterCommand; }
            set
            {
                _addFilterCommand = value;
                this.RaisePropertyChanged("AddFilterCommand");
            }
        }

        private DelegateCommand _searchBusinessUnitsCommand;

        public DelegateCommand SearchBusinessUnitsCommand
        {
            get { return _searchBusinessUnitsCommand; }
            set
            {
                _searchBusinessUnitsCommand = value;
                this.RaisePropertyChanged("SearchBusinessUnitsCommand");
            }
        }

        private List<string> _filterFields;

        public SearchViewModel(List<string> filterColumns)
        {
            this._filterFields = filterColumns;
            this.SearchRowViewModels = new ObservableCollection<SearchRowViewModel>();
            this.SearchRowViewModels.Add(new SearchRowViewModel(_filterFields) { RemoveFilterCommand = new DelegateCommand<SearchRowViewModel>(ExecuteRemoveCurrentFilterCommand) });
            AddFilterCommand = new DelegateCommand(ExecuteAddFilterCommand, CanExecuteAddFilterCommand);

            SearchBusinessUnitsCommand = new DelegateCommand(ExecuteSearchBusinessUnitsCommand);
        }

        private void ExecuteSearchBusinessUnitsCommand()
        {
            /// TODO: Add code here for retrieving the business units
        }

        private void ExecuteRemoveCurrentFilterCommand(SearchRowViewModel obj)
        {
            this.SearchRowViewModels.Remove(obj);
        }

        private bool CanExecuteAddFilterCommand()
        {
            return true;
        }

        private void ExecuteAddFilterCommand()
        {
            this.SearchRowViewModels.Add(new SearchRowViewModel(this._filterFields));
        }
    }
}
