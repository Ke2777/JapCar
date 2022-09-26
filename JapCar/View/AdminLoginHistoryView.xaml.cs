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
    /// Логика взаимодействия для AdminLoginHistoryView.xaml
    /// </summary>
    public partial class AdminLoginHistoryView : Page
    {
        public AdminLoginHistoryView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new AdminMenuView();
        }

        private List<CustomLoginHistory> _loginHistories;

        public List<CustomLoginHistory> LoginHistories
        {
            get
            {
                try
                {
                    if (_loginHistories == null)
                    {
                        _loginHistories = new List<CustomLoginHistory>();

                        using (DBContext db = new DBContext())
                        {
                            var loginHistory = db.LoginHistories.ToList().Join(db.Users,
                                h => h.UserId,
                                u => u.Id,
                                (h, u) => new CustomLoginHistory{ LoginDate = h.LoginDate,  UserLogin = u.Login, RoleType = u.Role.Type});

                            _loginHistories = loginHistory.ToList();
                        }

                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Отсутствует подключение к базе!", "Ошибка!", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                return _loginHistories;
            }
            set { _loginHistories = value; }
        }
    }
}