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
using JapCar.Models;
using Role = JapCar.Models.Role;

namespace JapCar.View
{
    /// <summary>
    /// Логика взаимодействия для AdminView.xaml
    /// </summary>
    public partial class AdminView : Page
    {
        public AdminView()
        {
            InitializeComponent();

            using (DBContext db = new DBContext())
            {
                Roles = db.Roles.ToList();
            }

            DataContext = this;
        }

        public List<Models.Role> Roles { get; set; }
        public Models.Role SelectedRole { get; set; }

        private void ButtonOnMain_OnClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new AdminUsersView();
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            using (DBContext db = new DBContext())
            {
                User user = new User();
                user.Login = login.Text;
                user.Password = PasswordBox.Password;
                user.RoleId = SelectedRole.Id;
                user.Phone = phoneTextBox.Text;

                db.Users.Add(user);
                db.SaveChanges();
            }
            MessageBox.Show("Пользователь успешно добавлен!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
