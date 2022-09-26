using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
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
using JapCar.Models;

namespace JapCar.View
{
    /// <summary>
    /// Логика взаимодействия для AuthView.xaml
    /// </summary>
    public partial class AuthView : Page
    {
        public AuthView()
        {
            InitializeComponent();
          //  KeyDown +=
        }

        bool Visible = false;
        string inputedPass;
        private void OnViewPasswordClick(object sender, RoutedEventArgs e)
        {
            if (Visible == false)
            {
                inputedPass = PasswordBoxHide.Password;
                PasswordBoxHide.Visibility = Visibility.Collapsed;
                PasswordBoxView.Visibility = Visibility.Visible;
                PasswordBoxView.Text = inputedPass;
                Visible = true;
            }

            else
            {
                PasswordBoxHide.Visibility = Visibility.Visible;
                PasswordBoxView.Visibility = Visibility.Collapsed;
                inputedPass = PasswordBoxView.Text;
                PasswordBoxHide.Password = inputedPass;
                Visible = false;
            }
        }

        private void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            inputedPass = PasswordBoxHide.Password;

            using (DBContext db = new DBContext())
            {
                try
                {
                    var user = db.Users.Where(x => x.Login == LoginBox.Text && x.Password == inputedPass).Select(x => x)
                        .FirstOrDefault();


                    if (user != null)
                    {
                        LoginHistory loginHistory = new LoginHistory
                        {
                            UserId = user.Id,
                            LoginDate = DateTime.Now
                        };

                        db.LoginHistories.Add(loginHistory);
                        db.SaveChanges();

                        if (user.RoleId == 3)
                        {
                            Helper.LoginnedUserId = (int)user.RoleId;
                            Helper.NavigationFrame.Content = new KatalogView();
                            MessageBox.Show($"Вы вошли как {user.Role.Type}!", "Информация!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else if (user.RoleId == 4)
                        {
                            Helper.LoginnedUserId = (int)user.RoleId;
                            Helper.NavigationFrame.Content = new AdminMenuView();
                            MessageBox.Show($"Вы вошли как {user.Role.Type}!", "Информация!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Убедитесь что данные введены верно!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Отсутствует подключение к базе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}