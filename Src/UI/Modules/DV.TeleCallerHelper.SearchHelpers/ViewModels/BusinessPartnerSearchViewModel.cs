using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DV.TeleCallerHelper.Common;
using Microsoft.Practices.Prism.Commands;
using DV.TeleCallerHelper.SearchHelpers.Interfaces;
using DV.Manager;
using DV.DataProvider;
using DV.Manager.Request;
using System.Threading.Tasks;
using DV.Controls;

namespace DV.TeleCallerHelper.SearchHelpers.ViewModels
{
    public class BusinessPartnerSearchViewModel : ViewModelBase, IBusinessPartnerSearch
    {
        private ObservableCollection<BusinessPartnerRowViewModel> _buSearchResults;
        private DelegateCommand _searchBusinessUnitsCommand;
        private DelegateCommand _sendSmsCommand;
        private string _searchCriteriaText;
        private bool? _sendSmsToCaller;

        private IAutoCompleteDataProvider _businessunitSearchDataProvider;

        public IList<BusinessUnit> SelectedBusinessUnits
        {
            get
            {
                return (from a in BusinessUnitSearchResults
                        where a.SendSms
                        select a.AssociatedBusinessUnit).ToList();
            }
        }

        public IAutoCompleteDataProvider BusinessunitSearchDataProvider
        {
            get {
                return _businessunitSearchDataProvider;
            }
            set
            {
                _businessunitSearchDataProvider = value;
                RaisePropertyChanged("BusinessunitSearchDataProvider");
            }
        }

        /// <summary>
        /// Gets or sets Business Unit search results.  Binding to the grid
        /// </summary>
        public ObservableCollection<BusinessPartnerRowViewModel> BusinessUnitSearchResults
        {
            get
            {
                return this._buSearchResults;
            }
            set
            {
                this._buSearchResults = value;
                this.RaisePropertyChanged("BusinessUnitSearchResults");
            }
        }

        public DelegateCommand SearchBusinessUnitsCommand
        {
            get
            {
                return this._searchBusinessUnitsCommand;
            }
            set
            {
                this._searchBusinessUnitsCommand = value;
                this.RaisePropertyChanged("SearchBusinessUnitsCommand");
            }
        }

        public bool? SendSmsToCaller
        {
            get {
                return _sendSmsToCaller;
            }
            set {
                _sendSmsToCaller = value;
                this.RaisePropertyChanged("SendSmsToCaller");
            }
        }

        bool _isSendingSmsCompleted;

        public bool IsSendingSmsCompleted
        {
            get {
                return _isSendingSmsCompleted;
            }
            set
            {
                _isSendingSmsCompleted = value;
            }
        }

        public DelegateCommand SendSmsCommand
        {
            get
            {
                return this._sendSmsCommand;
            }
            set
            {
                this._sendSmsCommand = value;
                this.RaisePropertyChanged("SendSmsCommand");
            }
        }

        public string SearchCriteriaText
        {
            get
            {
                return this._searchCriteriaText;
            }
            set
            {
                this._searchCriteriaText = value;
                this.RaisePropertyChanged("SearchCriteriaText");
            }
        }

        public BusinessPartnerSearchViewModel()
        {
            this.SearchBusinessUnitsCommand = new DelegateCommand(ExecuteSearchBusinessUnits);
            this.BusinessUnitSearchResults = new ObservableCollection<BusinessPartnerRowViewModel>();
            this.SendSmsToCaller = true;

            BusinessunitSearchDataProvider = new BusinessUnitSearchProvider();
        }


        /// <summary>
        /// Makes a server call to retrieve the business units
        /// </summary>
        public void ExecuteSearchBusinessUnits()
        {
            //TODO : need to make a server call and populate BusinessPartnerSearchResults collection
            // NOte: Don't create a new instance of BusinessPartnerSearchResults collection for every search.
            //clear the existing collection and add the results to it.
            BusinessPartnerRowViewModel buRowViewModel;

            // This requires a label titled "label1" on the form...
            // Get the UI thread's context
            var context = TaskScheduler.FromCurrentSynchronizationContext();

            this.SetStatusbarMessage("Business units search inprogress...", StatusMessageType.Info);
            this.ShowBusyCursor(true, "Searching business units");

            BusinessUnitSearchResults.Clear();

            var request = new BusinessUnitSearchRequest { SearchString = this.SearchCriteriaText };
            IEnumerable<BusinessUnit> bizUnits = null;

            Task t = new Task(() =>
            {
                bizUnits = ManagerFactory.Create<BusinessUnitSearchRequest, IEnumerable<BusinessUnit>>().Execute(request);
            });

            t.ContinueWith(_ =>
            {
                if (bizUnits != null && bizUnits.Count() > 0)
                {
                    foreach (var bizUnit in bizUnits)
                    {
                        buRowViewModel = new BusinessPartnerRowViewModel(bizUnit);
                        BusinessUnitSearchResults.Add(buRowViewModel);
                    }

                    // TODO: removed for now will add it later when required
                    //BusinessUnitSearchResults.Take(3).ToList().ForEach(item => item.SendSms = true);

                    SetStatusbarMessage("Business unit details retrieved successfully...", StatusMessageType.Info);
                }
                else
                {
                    var bizUnit = new BusinessUnit();
                    buRowViewModel = new BusinessPartnerRowViewModel(bizUnit);
                    BusinessUnitSearchResults.Add(buRowViewModel);
                    SetStatusbarMessage("No business units found..Try in google maps...", StatusMessageType.Warning);
                }

                ShowBusyCursor(false);
            }, context);

            t.Start();
        }
    }
}