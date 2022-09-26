using JapCar.Models;
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

namespace JapCar.View
{
    /// <summary>
    /// Логика взаимодействия для AddCarView.xaml
    /// </summary>
    public partial class AddCarView : Page
    {

        private void OnBackToKatalogButton(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new KatalogView();
            
            using (DBContext db = new DBContext())
            {
                Car carX = db.Cars.Where(x => x.Id == 2).FirstOrDefault();
                Console.WriteLine(carX.Colors.Name);
            }
        }

        private void OnCreateNewCarButton(object sender, RoutedEventArgs e)
        {
            using (DBContext db = new DBContext())
            {
                try
                {
                    Car car = new Car
                    {
                        Model = ModelTextBox.Text,
                        ColorId = SelectedColor.Id,
                        ComplectationId = SelectedComplectation.Id,
                        Price = decimal.Parse(PriceTextBox.Text),
                        CreateDate = int.Parse(CreateDateTextBox.Text)
                    };

                    db.Cars.Add(car);
                    db.SaveChanges();
                    MessageBox.Show("Автомобиль успешно добавлен!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);


                }
                catch (SqlException exception)
                {
                    MessageBox.Show("Отсутствует подключение к базе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        public List<Models.Color> Colors { get; set; }
        public Models.Color SelectedColor { get; set; }
        public List<Models.Complectation> Complectations { get; set; }
        public Models.Complectation SelectedComplectation { get; set; }

        public AddCarView()
        {
            InitializeComponent();
           
            using (DBContext db = new DBContext())
            {
                Colors = db.Colors.ToList();
                Complectations = db.Complectations.ToList();
            }
            DataContext = this;
        }
    }
}
