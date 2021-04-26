using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfNoPageNav
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public NavigationService NavigationService { get; private set; } = new(5);

        public MainWindow()
        {
            NavigationService.Navigated += NavigationService_Navigated;

            NavigationService.PropertyChanged += (s, e) => PropertyChanged?.Invoke(s, e);

            // Being lazy, setting the data context to this same class.
            DataContext = this;

            InitializeComponent();

            content.Content = new HomePage();
            NavigationService.SetCurrent((HomePage)content.Content);

            // or could do `NavigationService.Navigate(new HomePage());`
        }

        private void NavigationService_Navigated(object sender, NavigatingEventArgs e)
        {
            content.Content = e.Control;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage());
        }

        private void btnTestClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TestPage());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoForward)
            {
                NavigationService.GoForward();
            }
        }
    }
}
