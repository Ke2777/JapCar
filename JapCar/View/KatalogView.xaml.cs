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
//using JapCar.CustomModels;
using JapCar.Models;
using Color = JapCar.Models.Color;

namespace JapCar.View
{
    /// <summary>
    /// Логика взаимодействия для KatalogView.xaml
    /// </summary>
    public partial class KatalogView : Page
    {
        public KatalogView()
        {
            InitializeComponent();
            DataContext = this;

            if (Helper.LoginnedUserId == 3)
            {
                OnMainButton.Visibility = Visibility.Collapsed;
                OnMainAdminButton.Visibility = Visibility.Visible;
                AddButton.Visibility = Visibility.Visible;
                DelButton.Visibility = Visibility.Visible;
                ChangeButton.Visibility = Visibility.Visible;
            }
        }

        public void OnDelButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите удалить автомобиль?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                using (DBContext db = new DBContext())
                {
                    try
                    {
                        Car car = db.Cars.Where(x => x.Id == _selectedCar.Car.Id).FirstOrDefault();

                        db.Cars.Remove(car);
                        db.SaveChanges();

                        MessageBox.Show("Автомобиль успешно удалён!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Helper.NavigationFrame.Content = new KatalogView();


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
            if (SelectedCar != null)
            {
                Helper.NavigationFrame.Content = new ChangeCarView(SelectedCar);
            }
            else
            {
                MessageBox.Show("Выберите автомобиль для редактирования!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new AddCarView();
        }
        public void OnMainButtonClick(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new MenuView();
        }

        private CustomCar _selectedCar;

        public CustomCar SelectedCar
        {
            get
            {
                return _selectedCar;
            }

            set
            {
                _selectedCar = value;
            }
        }


        private List<CustomCar> _cars;
        public List<CustomCar> Cars
        {
            get
            {
                try
                {
                    if (_cars == null)
                    {
                        _cars = new List<CustomCar>();

                        using (DBContext db = new DBContext())
                        {
                            List<Car> cars = db.Cars.ToList();
                            foreach (Car car in cars)
                            {
                                CustomCar customCar = new CustomCar();
                                customCar.Car = car;
                                customCar.ColorName = car.Colors.Name;
                                //customCar.Id = car.Id;
                                //customCar.Model = car.Model;
                                customCar.ComplectationName = car.Complectation.Name;
                                //customCar.ColorName = car.Colors.Name;
                                //customCar.Price = car.Price;
                                //customCar.CreateDate = car.CreateDate;
                                _cars.Add(customCar);
                            }
                        }
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Отсутствует подключение к базе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return _cars;
            }
            set
            {
                _cars = value;
            }
        }
    }
}
