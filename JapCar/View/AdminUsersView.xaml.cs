using System;
using System.Collections.Generic;
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
using JapCar.CustomModels;
using JapCar.Models;

namespace JapCar.View
{
    /// <summary>
    /// Логика взаимодействия для AdminUsersView.xaml
    /// </summary>
    public partial class AdminUsersView : Page
    {
        public AdminUsersView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new AdminView();
        }

        public void OnMainButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new AdminMenuView();
        }

        private CusomUser _selectedUser;

        public CusomUser SelectedUser
        {
            get
            {
                return _selectedUser;
            }

            set
            {
                _selectedUser = value;
            }
        }


        public void OnDelButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить пользователя?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                using (DBContext db = new DBContext())
                {
                    try
                    {
                        User user = db.Users.Where(x => x.Id == _selectedUser.User.Id).FirstOrDefault();

                        db.Users.Remove(user);
                        db.SaveChanges();

                        MessageBox.Show("Пользователь успешно удалён!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Helper.NavigationFrame.Content = new AdminUsersView();


                    }
                    catch (SqlException exception)
                    {
                        MessageBox.Show("Отсутствует подключение к базе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        public void OnChangeButtonClick(object sender, RoutedEventArgs e)
        {
            CusomUser selectedUser = SelectedUser;
            Helper.NavigationFrame.Content = new AdminChangeUserView(selectedUser);
        }

        private List<CusomUser> _users;
        public List<CusomUser> Users
        {
            get
            {
                try
                {
                    if (_users == null)
                    {
                        _users = new List<CusomUser>();

                        using (DBContext db = new DBContext())
                        {
                            List<User> users = db.Users.ToList();
                            foreach (User user in users)
                            {
                                CusomUser customUser = new CusomUser();
                                customUser.User = user;
                                customUser.RoleType = user.Role.Type;
                                _users.Add(customUser);
                            }
                        }
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Отсутствует подключение к базе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return _users;
            }
            set
            {
                _users = value;
            }
        }
    }
}
