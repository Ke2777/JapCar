using System;
using System.Collections.Generic;
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

namespace JapCar.View
{
    /// <summary>
    /// Логика взаимодействия для AdminMenuView.xaml
    /// </summary>
    public partial class AdminMenuView : Page
    {
        public AdminMenuView()
        {
            InitializeComponent();
        }

        public void OnAdminUserButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new AdminUsersView();
        }

        public void OnLoginHistoryButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new AdminLoginHistoryView();
        }

        public void OnMainButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new MenuView();
        }
    }
}
