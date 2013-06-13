using DV.Controls;
using DV.TeleCallerHelper.SearchHelpers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DV.TeleCallerHelper.SearchHelpers.Views
{
    /// <summary>
    /// Interaction logic for BusinessPartnerSearch.xaml
    /// </summary>
    public partial class BusinessPartnerSearch : UserControl
    {
        //private AutoCompleteManager _acmBuSuggestion;

        public BusinessPartnerSearch()
        {
            //var testData = GetTestData();

            InitializeComponent();

            //_acmBuSuggestion = new AutoCompleteManager(this.businessSearchCriteria);
            //_acmBuSuggestion.DataProvider = new BusinessUnitSearchProvider(testData);
            //_acmBuSuggestion.Asynchronous = true;
            //businessSearchCriteria.KeyDown += businessSearchCriteria_KeyDown;
        }

        //private IEnumerable<string> GetTestData()
        //{
        //    List<string> testData = new List<string>();
        //    testData.Add(" Volgaarchery academy");
        //    testData.Add("COASTAL PNEMATIC AGENCIES(Authorised Dealer ELGI)");
        //    testData.Add("Colgate Palmolive India Ltd");
        //    testData.Add("Colgate Tooth paste Dealers");
        //    testData.Add("DATTASAI PNEUMATIC & EQUIPMENT  Authorised Dealer For ELGI");
        //    testData.Add("District Fire Officer, Nalgonda.");
        //    testData.Add("Dr. Prasanth kumar Urolgist");
        //    testData.Add("eluru tolgate");
        //    testData.Add("LG Electronics");
        //    return testData;
        //}

        //void businessSearchCriteria_KeyDown(object sender, KeyEventArgs e)
        //{
            
        //}
    }
}
