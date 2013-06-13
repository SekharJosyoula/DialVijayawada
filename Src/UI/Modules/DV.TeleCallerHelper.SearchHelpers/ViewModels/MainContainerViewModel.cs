using System.Windows;
using DV.DataProvider;
using DV.Manager;
using DV.Manager.Request;
using DV.TeleCallerHelper.Common;
using DV.TeleCallerHelper.SearchHelpers.Interfaces;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DV.TeleCallerHelper.SearchHelpers.ViewModels
{
    class MainContainerViewModel : TabbedViewModelBase, ISearchMainContainer
    {
        private CallerDetailsViewModel _callerDetailViewModel;
        private BusinessPartnerSearchViewModel _bPartnerSearcbViewModel;

        public CallerDetailsViewModel CallerDetailsViewModel
        {
            get {
                return this._callerDetailViewModel;
            }
            set {
                this._callerDetailViewModel = value;
                this.RaisePropertyChanged("CallerDetailsViewModel");
            }
        }

        public BusinessPartnerSearchViewModel BusinessPartnerSearchViewModel
        {
            get
            {
                return this._bPartnerSearcbViewModel;
            }
            set
            {
                this._bPartnerSearcbViewModel = value;
                this.RaisePropertyChanged("BusinessPartnerSearchViewModel");
            }
        }

        public MainContainerViewModel()
        {
            this._callerDetailViewModel = new CallerDetailsViewModel();
            this._bPartnerSearcbViewModel = new BusinessPartnerSearchViewModel();

            this._bPartnerSearcbViewModel.SendSmsCommand = new DelegateCommand(ExecuteSaveCallerAndSendSms);

            ViewTitle = "Tele-Caller Helper";
        }

        public void ExecuteSaveCallerAndSendSms()
        {
            // TODO: Here try to make a call to saving caller details and send SMS
            this.SetStatusbarMessage("Send SMS inprogress...", StatusMessageType.Info);
            this.ShowBusyCursor(true, "Sending SMS");

            var rqManager = new CallerRequestCommitManager();
            var callerRequestCommit = new CallerRequestCommit();


            Task t = new Task(() =>
            {
                callerRequestCommit.Caller = _callerDetailViewModel.CurrentCaller;
                callerRequestCommit.Caller.CanSendSMS = this._bPartnerSearcbViewModel.SendSmsToCaller.Value;

                callerRequestCommit.CallerRequest = new CallerRequestHistory();
                callerRequestCommit.CallerRequest.CallDurationinSecs = 60;
                callerRequestCommit.CallerRequest.RequestedDetails = this._bPartnerSearcbViewModel.SearchCriteriaText;

                //TODO: Assume always get something, employee to be retrieved..
                callerRequestCommit.CallerRequest.TeleCallerID = 1;//GetFirstTeleCaller().EmployeeID;

                callerRequestCommit.BusinessUnitsIdentifiedForCallerRequest = this._bPartnerSearcbViewModel.SelectedBusinessUnits;

                var ret = rqManager.Execute(callerRequestCommit);
            });

            t.ContinueWith(_ =>
            {
                SetStatusbarMessage("Saved successfully and SMS sending put into Queue...", StatusMessageType.Info);
                MessageBox.Show("Saved successfully and SMS sending put into Queue...");
                
                ShowBusyCursor(false);

            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            t.ContinueWith(_ =>
            {
                SetStatusbarMessage("Failed to send SMS and saving...", StatusMessageType.Info);
                ShowBusyCursor(false);

            }, TaskContinuationOptions.OnlyOnFaulted);

            t.Start();
        }
    }
}