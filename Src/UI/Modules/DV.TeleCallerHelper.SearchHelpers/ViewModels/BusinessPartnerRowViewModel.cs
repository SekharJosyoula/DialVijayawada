using DV.DataProvider;
using DV.TeleCallerHelper.Common;
using DV.TeleCallerHelper.SearchHelpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.SearchHelpers.ViewModels
{
    /// <summary>
    /// TODO: Still need to add fields that are relevant and neds to be displayed in the grid
    /// </summary>
    public class BusinessPartnerRowViewModel : ViewModelBase, IBusinessPartner
    {
        private bool _sendSm;
        private string _businessUnitName;
        private string _address1;
        private string _area;
        private BusinessUnit _associatedBu;
        private string _natureOfBusiness;
        //private string _phoneNumber;
        private string _phoneNumber;

        public BusinessUnit AssociatedBusinessUnit
        {
            get
            {
                // Update object with latest values.
                UpdateBusinessUnit();
                return this._associatedBu;
            }
        }

        public bool SendSms
        {
            get {
                return this._sendSm;
            }
            set
            {
                this._sendSm = value;
                this.RaisePropertyChanged("SendSms");
            }
        }

        public string BusinessUnitName
        {
            get
            {
                return this._businessUnitName;
            }
            set
            {
                this._businessUnitName = value;
                this.RaisePropertyChanged("BusinessUnitName");
            }
        }

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

        public string NatureOfBusiness
        {
            get
            {
                return this._natureOfBusiness;
            }
            set {
                this._natureOfBusiness = value;
                this.RaisePropertyChanged("NatureOfBusiness");
            }
        }

 
        public BusinessPartnerRowViewModel(BusinessUnit associatedBu)
        {
            if (associatedBu != null)
            {
                this._associatedBu = associatedBu;
                this.BusinessUnitName = associatedBu.Name;
                this.NatureOfBusiness = associatedBu.NatureofBusiness;

                // TODO: Why secondary contact numnber? to be decided?
                this.PhoneNumber = associatedBu.ContactNumber; 
                
                if (associatedBu.Address != null)
                {
                    this.Area = associatedBu.Address.Area;
                    this.Address1 = associatedBu.Address.Address1;
                }
            }
            else
            {
                this._associatedBu = new BusinessUnit();
            }
        }

        private void UpdateBusinessUnit()
        {
            if (this._associatedBu.Address == null)
            {
                this._associatedBu.Address = new Address();
            }

            this._associatedBu.Address.Address1 = this.Address1;
            this._associatedBu.Address.Area = this.Area;
            this._associatedBu.Name = this.BusinessUnitName;
            this._associatedBu.NatureofBusiness = this.NatureOfBusiness;
            this._associatedBu.ContactNumber = this.PhoneNumber;

            if (this._associatedBu.BusinessUnitPhoneNumbers == null)
            {
                this._associatedBu.BusinessUnitPhoneNumbers = new List<BusinessUnitPhoneNumber>();
            }

            if (!this._associatedBu.BusinessUnitPhoneNumbers.Where(bu => bu.PhoneNumber.Equals(this.PhoneNumber)).Any())
            {
                //TODO: hardcoded value for phonenumbertype
                this._associatedBu.BusinessUnitPhoneNumbers.Add(new BusinessUnitPhoneNumber { PhoneNumber = this.PhoneNumber, PhoneNumberTypeId = 1, isActive = true });
            }
        }
    }
}