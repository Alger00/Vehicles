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
        private ObservableCollection<Vehicle> vehicles = new ObservableCollection<Vehicle>();
        public MainWindow()
        {
            InitializeComponent();
            VehiclesListBox.ItemsSource = vehicles;
        }
        private Vehicle? SelectedVehicle
        {
            get
            {
                return VehiclesListBox.SelectedItem as Vehicle;
            }
        }
        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            vehicles.Add(new Car("Volvo", "V60"));
        }
        private void AddBoat_Click(object sender, RoutedEventArgs e)
        {
            vehicles.Add(new Boat("Bella", "600"));
        }
        private void AddAmphi_Click(object sender, RoutedEventArgs e)
        {
            vehicles.Add(new AmphibiousCar("Amphi", "X"));
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
            if (SelectedVehicle != null)
            {
                vehicles.Remove(SelectedVehicle);
            }
        }
        private void VehiclesListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
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