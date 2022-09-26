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
using JapCar.CustomModels;
using Color = JapCar.Models.Color;

namespace JapCar.View
{
    /// <summary>
    /// Логика взаимодействия для ChangeCarView.xaml
    /// </summary>
    public partial class ChangeCarView : Page
    {
        private CustomCar selectedCar;


        public ChangeCarView(CustomCar selectedCar)
        {
            InitializeComponent();
            try
            {
                this.selectedCar = selectedCar;
                ChangeModelTextBox.Text = selectedCar.Car.Model;
                ChangeColorIdComboBox.Text = selectedCar.ColorName;
                ChangeComplectationIdComboBox.Text = selectedCar.ComplectationName;
                ChangePriceTextBox.Text = selectedCar.Car.Price.ToString();
                ChangeCreateDateTextBox.Text = selectedCar.Car.CreateDate.ToString();

                SelectedColor = selectedCar.Car.Colors;
                SelectedComplectation = selectedCar.Car.Complectation;
                

                using (DBContext db = new DBContext())
                {
                    Colors = db.Colors.ToList();
                    Complectations = db.Complectations.ToList();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Отсутствует подключение к базе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            DataContext = this;
        }

        public List<Models.Color> Colors { get; set; }
        public Models.Color SelectedColor { get; set; }
        public List<Models.Complectation> Complectations { get; set; }
        public Models.Complectation SelectedComplectation { get; set; }

        private void OnBackToKatalogButton(object sender, RoutedEventArgs e)
        {
            Helper.NavigationFrame.Content = new KatalogView();
        }

        private void OnSaveChangesButton(object sender, RoutedEventArgs e)
        {
            using (DBContext db = new DBContext())
            {
                try
                {
                    Car car = db.Cars.Where(x => x.Id == selectedCar.Car.Id).FirstOrDefault();

                        car.Model = ChangeModelTextBox.Text;
                        car.Price = decimal.Parse(ChangePriceTextBox.Text);
                        car.CreateDate = int.Parse(ChangeCreateDateTextBox.Text);
                        car.ColorId = SelectedColor.Id;
                        car.ComplectationId = SelectedComplectation.Id;
                        db.SaveChanges();

                    MessageBox.Show("Информация успешно отредактирована!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);


                }
                catch (SqlException exception)
                {
                    MessageBox.Show("Отсутствует подключение к базе!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            Helper.NavigationFrame.Content = new KatalogView();
        }
    }
}
