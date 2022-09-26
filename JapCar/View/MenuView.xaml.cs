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
    public partial class MenuView : Page
    {
        public MenuView()
        {
            InitializeComponent();

            if (Helper.LoginnedUserId > 0 & Helper.LoginnedUserId != 4)
            {
                AuthButton.Visibility = Visibility.Collapsed;
                UnAuthButton.Visibility = Visibility.Visible;
            }
            else if (Helper.LoginnedUserId == 4)
            {
                AuthButton.Visibility = Visibility.Collapsed;
                UnAuthAdminButton.Visibility = Visibility.Visible;
                ToSuperAdminButton.Visibility = Visibility.Visible;
            }
        }

        public void OnAuthButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new AuthView();
        }
        public void OnUnAuthButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.LoginnedUserId = 0;
            Helper.NavigationFrame.Content = new MenuView();
        }

        public void OnUnAuthAdminButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.LoginnedUserId = 0;
            Helper.NavigationFrame.Content = new MenuView();
        }
        public void ToSuperAdminButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new AdminMenuView();
        }
        public void OnInformationButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new InformationView();
        }

        public void OnKatalogButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new KatalogView();
        }

        public void OnApplicationExitButton(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            
        }
    }
}

