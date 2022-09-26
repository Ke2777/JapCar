using JapCar.CustomModels;
using JapCar.Models;
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
    /// Логика взаимодействия для AdminChangeUserView.xaml
    /// </summary>
    public partial class AdminChangeUserView : Page
    {
        private readonly CusomUser _selectedUser;
        public AdminChangeUserView(CusomUser selectedUser)
        {
            InitializeComponent();

            this._selectedUser = selectedUser;
            roleComboBox.SelectedItem = selectedUser.User.RoleId;
            login.Text = selectedUser.User.Login;
            PasswordBox.Text = selectedUser.User.Password;
            phoneTextBox.Text = selectedUser.User.Phone;

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
                User user = db.Users.Where(x => x.Id == _selectedUser.User.Id).FirstOrDefault();
                user.Login = login.Text;
                user.Password = PasswordBox.Text;
                user.RoleId = SelectedRole.Id;
                user.Phone = phoneTextBox.Text;

                db.SaveChanges();
            }
            MessageBox.Show("Пользователь успешно Изменён!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}