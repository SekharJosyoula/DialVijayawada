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

namespace DV.Controls
{
    public class AutoCompleteTextBox : TextBox
    {
        private AutoCompleteManager _acm;

        public AutoCompleteManager AutoCompleteManager
        {
            get { return _acm; }
        }

        public static readonly DependencyProperty AutoCompleteDataProviderProperty =
          DependencyProperty.Register("AutoCompleteDataProvider", typeof(IAutoCompleteDataProvider),
          typeof(AutoCompleteTextBox), null);


        public IAutoCompleteDataProvider AutoCompleteDataProvider
        {
            get
            {
                return (IAutoCompleteDataProvider)GetValue(AutoCompleteDataProviderProperty);
            }
            set
            {
                SetValue(AutoCompleteDataProviderProperty, value);
            }
        }


        public AutoCompleteTextBox()
        {
            this.Loaded += AutoCompleteTextBox_Loaded;
        }

        void AutoCompleteTextBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _acm = new AutoCompleteManager();
            _acm.Asynchronous = true;
            _acm.DataProvider = AutoCompleteDataProvider;
            _acm.AttachTextBox(this);
        }

    }
}
