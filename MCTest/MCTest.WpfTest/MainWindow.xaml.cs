using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MCTest.WinformTest;

namespace MCTest.WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        readonly ObservableCollection<InterestCalc> _memberData = new ObservableCollection<InterestCalc>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = _memberData;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((InterestCalc)(((Button)sender)).DataContext).Calc();
        }
    }
}
