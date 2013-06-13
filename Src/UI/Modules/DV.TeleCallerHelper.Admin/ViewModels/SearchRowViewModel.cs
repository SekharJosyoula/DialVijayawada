using DV.TeleCallerHelper.Common;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.Admin.ViewModels
{
    public class SearchRowViewModel : ViewModelBase
    {
        private string _field;

        public string Field
        {
            get { return _field; }
            set
            {
                _field = value;
                this.RaisePropertyChanged("Field");
            }
        }

        private string _operand;

        public string Operand
        {
            get { return _operand; }
            set
            {
                _operand = value;
                this.RaisePropertyChanged("Operand");
            }
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                this.RaisePropertyChanged("Value");
            }
        }

        private List<string> _filterFields;

        public List<string> FilterFields
        {
            get { return _filterFields; }
            set
            {
                _filterFields = value;
                this.RaisePropertyChanged("FilterFields");
            }
        }

        private DelegateCommand<SearchRowViewModel> _removeFilterCommand;

        public DelegateCommand<SearchRowViewModel> RemoveFilterCommand
        {
            get { return _removeFilterCommand; }
            set
            {
                _removeFilterCommand = value;
                this.RaisePropertyChanged("RemoveFilterCommand");
            }
        }

        public bool IsAllFieldsFilled
        {
            get {
                return !string.IsNullOrEmpty(Value) && !string.IsNullOrEmpty(Field) && !string.IsNullOrEmpty(Operand);
            }
        }

        public SearchRowViewModel(List<string> filterFields)
        {
            this.FilterFields = filterFields;
        }
    }
}
