using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DV.TeleCallerHelper.SearchHelpers.Interfaces
{
    public interface ICallerDetails
    {
        ///// <summary>
        ///// Command for capturing save client action.
        ///// </summary>
        //DelegateCommand SaveClientCommand
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// Gets or sets Callers first name
        /// </summary>
        string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Callers Last Name
        /// </summary>
        string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Callers primary contact number
        /// </summary>
        string PhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Callers city
        /// </summary>
        string City
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Callers area within the city
        /// </summary>
        string Area
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Callers alternate contact number
        /// </summary>
        string AltPhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Callers Annual Income
        /// </summary>
        string AnnualIncome
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets callers Address 1
        /// </summary>
        string Address1
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Callers Address 2
        /// </summary>
        string Address2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Callers profession
        /// </summary>
        string Profession
        {
            get;
            set;
        }

        //void SaveCallerDetails();
    }
}
