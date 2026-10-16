using System.Collections.ObjectModel;
using Vehicles.Core;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vehicles.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Vehicle> cars = new ObservableCollection<Vehicle>();
        private ObservableCollection<Vehicle> boats = new ObservableCollection<Vehicle>();
        private Garage carGarage = new Garage("Car Garage");
        private Garage boatGarage = new Garage("Boat Garage");
        private Vehicle? selectedVehicle;
        public MainWindow()
        {
            InitializeComponent();
            CarsListBox.ItemsSource = cars;
            BoatsListBox.ItemsSource = boats;
        }
        private Vehicle? SelectedVehicle
        {
            get
            {
                return selectedVehicle;
            }
        }
        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            foreach (Vehicle vehicle in carGarage.Vehicles)
            {
                LogListBox.Items.Add(vehicle.Honk());
            }

            Car car = new Car("Volvo", "V60");

            carGarage.Vehicles.Add(car);
            cars.Add(car);
        }
        private void AddBoat_Click(object sender, RoutedEventArgs e)
        {
            foreach (Vehicle vehicle in boatGarage.Vehicles)
            {
                LogListBox.Items.Add(vehicle.Honk());
            }

            Boat boat = new Boat("Bella", "600");

            boatGarage.Vehicles.Add(boat);
            boats.Add(boat);
        }
        private void AddAmphi_Click(object sender, RoutedEventArgs e)
        {
            foreach (Vehicle vehicle in carGarage.Vehicles)
            {
                LogListBox.Items.Add(vehicle.Honk());
            }

            foreach (Vehicle vehicle in boatGarage.Vehicles)
            {
                LogListBox.Items.Add(vehicle.Honk());
            }

            AmphibiousCar amphi = new AmphibiousCar("Amphi", "X");

            carGarage.Vehicles.Add(amphi);
            boatGarage.Vehicles.Add(amphi);
            cars.Add(amphi);
            boats.Add(amphi);
        }
        private void Drive_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedVehicle is not IDriveable driveable)
            {
                return;
            }
            try
            {
                double km = double.Parse(DistanceTextBox.Text);
                string result = driveable.Drive(km);
                LogListBox.Items.Add(result);
                RefreshInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Swim_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedVehicle is not ISwimmable swimmable)
            {
                return;
            }
            try
            {
                double km = double.Parse(DistanceTextBox.Text);
                string result = swimmable.Swim(km);
                LogListBox.Items.Add(result);
                RefreshInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedVehicle == null)
            {
                return;
            }

            bool inCarGarage = carGarage.Vehicles.Contains(SelectedVehicle);
            bool inBoatGarage = boatGarage.Vehicles.Contains(SelectedVehicle);

            carGarage.Vehicles.Remove(SelectedVehicle);
            boatGarage.Vehicles.Remove(SelectedVehicle);

            cars.Remove(SelectedVehicle);
            boats.Remove(SelectedVehicle);

            selectedVehicle = null;
            CarsListBox.SelectedItem = null;
            BoatsListBox.SelectedItem = null;

            if (inCarGarage)
            {
                foreach (Vehicle vehicle in carGarage.Vehicles)
                {
                    LogListBox.Items.Add(vehicle.FlashLights());
                }
            }

            if (inBoatGarage)
            {
                foreach (Vehicle vehicle in boatGarage.Vehicles)
                {
                    LogListBox.Items.Add(vehicle.FlashLights());
                }
            }
        }

        private void CarsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarsListBox.SelectedItem != null)
            {
                BoatsListBox.SelectedItem = null;
                selectedVehicle = CarsListBox.SelectedItem as Vehicle;
            }

            DriveButton.IsEnabled = SelectedVehicle is IDriveable;
            SwimButton.IsEnabled = SelectedVehicle is ISwimmable;

            RefreshInfo();
        }

        private void BoatsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BoatsListBox.SelectedItem != null)
            {
                CarsListBox.SelectedItem = null;
                selectedVehicle = BoatsListBox.SelectedItem as Vehicle;
            }

            DriveButton.IsEnabled = SelectedVehicle is IDriveable;
            SwimButton.IsEnabled = SelectedVehicle is ISwimmable;

            RefreshInfo();
        }

        private void RefreshInfo()
        {
            if (SelectedVehicle == null)
            {
                InfoTextBlock.Text = "";
                return;
            }
            InfoTextBlock.Text = $"{SelectedVehicle}\nOdometer: {SelectedVehicle.Odometer} km.";
        }
    }
}