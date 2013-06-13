using System.Windows;
using DV.TeleCallerHelper.Common;
using DV.TeleCallerHelper.SearchHelpers.Interfaces;
using DV.TeleCallerHelper.Common.Interfaces;

using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DV.Manager;
using DV.DataProvider;
using DV.Manager.Request;
using System.Threading.Tasks;

namespace DV.TeleCallerHelper.SearchHelpers.ViewModels
{
    public class CallerDetailsViewModel : ViewModelBase, ICallerDetails, IPopulate
    {
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private string _altPhoneNumber;
        private string _annualIncome;
        private string _address1;
        private string _address2;
        private string _city;
        private string _area;
        private string _profession;
        private string _callerSearchResult;
        private string _emailId;
        private string _searchPhoneNumber;

        private DelegateCommand _callerSearchCommand;
        
        private Caller _currentCaller;

        public DelegateCommand ClearCallerDetailsCommand
        {
            get
            {
                return this._callerSearchCommand;
            }
            set
            {
                this._callerSearchCommand = value;
                this.RaisePropertyChanged("CallerSearchCommand");
            }
        }
        
        public DelegateCommand CallerSearchCommand
        {
            get
            {
                return this._callerSearchCommand;
            }
            set
            {
                this._callerSearchCommand = value;
                this.RaisePropertyChanged("CallerSearchCommand");
            }
        }

        public Caller CurrentCaller
        {
            get
            {
                //TODO: update caller object here before returning it.
                if (_currentCaller == null)
                {
                    this._currentCaller = new Caller();
                    this._currentCaller.Address = new Address();
                }

                this._currentCaller.FirstName = this.FirstName;
                this._currentCaller.LastName = this.LastName;
                this._currentCaller.PhoneNumber = string.IsNullOrEmpty(this.PhoneNumber) ? this.SearchPhoneNumber : this.PhoneNumber;
                this._currentCaller.AlternatePhoneNumber = this.AltPhoneNumber;
                
                if (this._currentCaller.Profession==null)
                {
                    this._currentCaller.Profession = new Profession();    
                }
                this._currentCaller.Profession.Name = this.Profession;

                if (this._currentCaller.Address == null)
                {
                    this._currentCaller.Address = new Address();
                }

                this._currentCaller.Address.EmailID = this.EmailID;
                this._currentCaller.Address.Area = this.Area;
                this._currentCaller.Address.City = this.City;

                return this._currentCaller;
            }
        }

        /// <summary>
        /// Gets or sets Callers search result
        /// </summary>
        public string SearchPhoneNumber
        {
            get
            {
                return this._searchPhoneNumber;
            }
            set
            {
                this._searchPhoneNumber = value;
                this.RaisePropertyChanged("SearchPhoneNumber");
            }
        }

        /// <summary>
        /// Gets or sets Callers search result
        /// </summary>
        public string ClientSearchResult
        {
            get
            {
                return this._callerSearchResult;
            }
            set
            {
                this._callerSearchResult = value;
                this.RaisePropertyChanged("ClientSearchResult");
            }
        }

        public string EmailID
        {
            get
            {
                return this._emailId;
            }
            set
            {
                this._emailId = value;
                this.RaisePropertyChanged("EmailID");
            }
        }

        /// <summary>
        /// Gets or sets Callers first name
        /// </summary>
        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                this._firstName = value;
                this.RaisePropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Gets or sets Callers Last Name
        /// </summary>
        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                this._lastName = value;
                this.RaisePropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Gets or sets Callers primary contact number
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return this._phoneNumber;
            }
            set
            {
                this._phoneNumber = value;
                this.RaisePropertyChanged("PhoneNumber");
            }
        }

        /// <summary>
        /// Gets or sets Callers city
        /// </summary>
        public string City
        {
            get
            {
                return this._city;
            }
            set
            {
                this._city = value;
                this.RaisePropertyChanged("City");
            }
        }

        /// <summary>
        /// Gets or sets Callers area within the city
        /// </summary>
        public string Area
        {
            get
            {
                return this._area;
            }
            set
            {
                this._area = value;
                this.RaisePropertyChanged("Area");
            }
        }

        /// <summary>
        /// Gets or sets Callers alternate contact number
        /// </summary>
        public string AltPhoneNumber
        {
            get
            {
                return this._altPhoneNumber;
            }
            set
            {
                this._altPhoneNumber = value;
                this.RaisePropertyChanged("AltPhoneNumber");
            }
        }

        /// <summary>
        /// Gets or sets Callers Annual Income
        /// </summary>
        public string AnnualIncome
        {
            get
            {
                return this._annualIncome;
            }
            set
            {
                this._annualIncome = value;
                this.RaisePropertyChanged("AnnualIncome");
            }
        }

        /// <summary>
        /// Gets or sets callers Address 1
        /// </summary>
        public string Address1
        {
            get
            {
                return this._address1;
            }
            set
            {
                this._address1 = value;
                this.RaisePropertyChanged("Address1");
            }
        }

        /// <summary>
        /// Gets or sets the Callers Address 2
        /// </summary>
        public string Address2
        {
            get
            {
                return this._address2;
            }
            set
            {
                this._address2 = value;
                this.RaisePropertyChanged("Address2");
            }
        }

        /// <summary>
        /// Gets or sets the Callers profession
        /// </summary>
        public string Profession
        {
            get
            {
                return this._profession;
            }
            set
            {
                this._profession = value;
                this.RaisePropertyChanged("Profession");
            }
        }

        /// <summary>
        /// Instantiates new instance of Caller Details for given caller phone number
        /// </summary>
        /// <param name="clientPhoneNumber"></param>
        public CallerDetailsViewModel()
        {
            this.CallerSearchCommand = new DelegateCommand(ExecuteCallerSearch);
            this.ClearCallerDetailsCommand = new DelegateCommand(ExeucteClearCallerDetails);
        }

        public void ExeucteClearCallerDetails()
        {
            this._currentCaller = null;
            this.Address1 = string.Empty;
            this.Address2 = string.Empty;
            this.PhoneNumber = string.Empty;
            this.AltPhoneNumber = string.Empty;
            this.EmailID = string.Empty;
            this.Area = string.Empty;
            this.City = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.AnnualIncome = null;
            this.Profession = string.Empty;
        }

        public bool CanExecuteClearCallerDetails()
        {
            return true;
            // hardcoded for time being
            //return this._currentCaller != null;
        }

        public void ExecuteCallerSearch()
        {
            Populate();
        }

        public bool CanExecuteCallerSearch()
        {
            return true;
            //return this.PhoneNumber.HasValue;
        }

        /// <summary>
        /// Makes a call to the db to retrieve the data and populate here.
        /// </summary>
        public void Populate()
        {
            // This requires a label titled "label1" on the form...
            // Get the UI thread's context
            var context = TaskScheduler.FromCurrentSynchronizationContext();

            ShowBusyCursor(true, "Fetching Caller Details");

            // TODO: retrieve object and set the member properties. here
            var callerSearchRequest = new CallerSearchRequest { PhoneNumber = this.SearchPhoneNumber };

            Caller caller = null;

            Task t = new Task(() =>
                {
                    caller = ManagerFactory.Create<CallerSearchRequest, Caller>().Execute(callerSearchRequest);
                });

            t.ContinueWith(_ =>
                {
                    if (caller != null)
                    {
                        this._currentCaller = caller;
                        FirstName = caller.FirstName;
                        LastName = caller.LastName;
                        PhoneNumber = caller.PhoneNumber;
                        // AnnualIncome = caller.a

                        if (!string.IsNullOrEmpty(caller.AlternatePhoneNumber))
                        {
                            AltPhoneNumber = caller.AlternatePhoneNumber;
                        }
                        if (caller.Address != null)
                        {
                            //TODO: What about Address1 and Address2
                            Address1 = caller.Address.Address1;
                            Area = caller.Address.Area;
                            City = caller.Address.City;
                            EmailID = caller.Address.EmailID;

                        }

                        if (caller.Profession != null)
                        {
                            //TODO: What about Address1 and Address2
                            Profession = caller.Profession.Name;
                        }

                        SetStatusbarMessage("Caller details retrieved successfully...", StatusMessageType.Info);
                    }
                    else
                    {
                        this.PhoneNumber = this.SearchPhoneNumber;
                        SetStatusbarMessage("Caller not found.. Please capture details...", StatusMessageType.Warning);
                    }

                    ShowBusyCursor(false);
                }, context);

            t.Start();
        }

        
    }
}
