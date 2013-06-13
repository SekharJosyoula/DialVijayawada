using DV.TeleCallerHelper.Common.Interfaces;
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
    /// Interaction logic for CallerDetails.xaml
    /// </summary>
    public partial class CallerDetails : UserControl
    {
        public CallerDetails()
        {
            InitializeComponent();
        }

        private void OnCallerDetailsSearch(object sender, RoutedEventArgs e)
        {
            if (DataContext is IPopulate)
            {
                ((IPopulate)DataContext).Populate();
            }
        }
    }
}
