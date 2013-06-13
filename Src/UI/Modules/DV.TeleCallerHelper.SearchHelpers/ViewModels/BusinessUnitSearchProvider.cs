using DV.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DV.Manager;

namespace DV.TeleCallerHelper.SearchHelpers.ViewModels
{
    public class BusinessUnitSearchProvider : IAutoCompleteDataProvider
    {
        public IEnumerable<string> GetItems(string textPattern)
        {
            if (textPattern.Length > 2)
            {
                IEnumerable<string> _source = BusinessUnitManager.GetBusinessUnitNames(textPattern);

                foreach (var item in _source)
                {
                    yield return item;
                }
            }
        }
    }
}
