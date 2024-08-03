using BusinessLogic.Repository;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Group2WPF
{
    /// <summary>
    /// Interaction logic for BaggageWindow.xaml
    /// </summary>
    public partial class BaggageWindow : Window
    {
        IBaggageRepository baggageRepository;
        private int currentPage = 1;
        private const int PageSize = 10;
        private int totalRecords = 0;
        public string UserRole { get; set; }
        public BaggageWindow(string role)
        {
            InitializeComponent();
            baggageRepository = new BaggageRepository();
            UserRole = role;
            UpdatePagination();
            SetupRoleBasedUI();
            LoadList();
        }
        private void LoadList()
        {
            totalRecords = baggageRepository.GetTotalCount();
            UpdatePagination();
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            IEnumerable<Baggage> baggages = baggageRepository.GetPaged(currentPage, PageSize);
            DataGridBaggage.ItemsSource = baggages;
        }
        private void UpdatePagination()
        {
            int totalPages = (int)Math.Ceiling((double)totalRecords / PageSize);
            List<object> pageNumbers = new List<object>();

            if (totalPages <= 3)
            {
                for (int i = 1; i <= totalPages; i++)
                {
                    pageNumbers.Add(i);
                }
            }
            else
            {
                pageNumbers.Add(1);
                pageNumbers.Add(2);
                pageNumbers.Add("...");
                pageNumbers.Add(totalPages);
            }

            PaginationItemsControl.ItemsSource = pageNumbers;
        }
        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadList();
                UpdatePagination();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)totalRecords / PageSize);
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadList();
                UpdatePagination();
            }
        }
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int page = Convert.ToInt32(button.Content);
                currentPage = page;
                UpdateDataGrid();
                UpdatePagination();
            }
        }
        private void SetupRoleBasedUI()
        {
            btnOpenAirlineWindow.Visibility = Visibility.Collapsed;
            btnOpenAirportWindow.Visibility = Visibility.Collapsed;
            btnOpenBookingWindow.Visibility = Visibility.Collapsed;
            btnOpenBookingPlatformWindow.Visibility = Visibility.Collapsed;
            btnOpenFlightWindow.Visibility = Visibility.Collapsed;
            btnOpenBaggageWindow.Visibility = Visibility.Collapsed;
            btnOpenPassengerWindow.Visibility = Visibility.Collapsed;

            if (UserRole == "Admin")
            {
                btnOpenAirlineWindow.Visibility = Visibility.Visible;
                btnOpenAirportWindow.Visibility = Visibility.Visible;
                btnOpenBookingWindow.Visibility = Visibility.Visible;
                btnOpenBookingPlatformWindow.Visibility = Visibility.Visible;
                btnOpenFlightWindow.Visibility = Visibility.Visible;
                btnOpenBaggageWindow.Visibility = Visibility.Visible;
                btnOpenPassengerWindow.Visibility = Visibility.Visible;
            }
            else if (UserRole == "Staff")
            {
                btnOpenPassengerWindow.Visibility = Visibility.Visible;
                btnOpenBookingWindow.Visibility = Visibility.Visible;
                btnOpenBaggageWindow.Visibility = Visibility.Visible;
            }

        }
    
        private Baggage GetObject()
        {
            return new Baggage
            {
                Id = int.TryParse(txtId.Text, out int id) ? id : 0,
                BookingId = int.TryParse(txtBookingId.Text, out int bookingId) ? (int?)bookingId : null,
                WeightInKg = decimal.TryParse(txtWeightInKg.Text, out decimal weight) ? (decimal?)weight : null
            };
        }

        private void ClearForm()
        {
            txtId.Text = string.Empty;
            txtBookingId.Text = string.Empty;
            txtWeightInKg.Text = string.Empty;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Baggage baggage = GetObject();
                baggageRepository.insert(baggage);
                LoadList();
                MessageBox.Show($"Baggage with ID {baggage.Id} added successfully", "Added");
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Baggage baggage = GetObject();
                baggageRepository.update(baggage);
                LoadList();
                MessageBox.Show($"Baggage with ID {baggage.Id} updated successfully", "Updated");
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Baggage baggage = GetObject();
                MessageBoxResult result = MessageBox.Show($"Do you want to delete {baggage.Id}?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    baggageRepository.delete(baggage);
                    LoadList();
                    MessageBox.Show($"{baggage.Id} deleted successfully", "Delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete");
            }
        }

        private void OpenAirlineWindow_Click(object sender, RoutedEventArgs e)
        {
            AirlineWindow airlineWindow = new AirlineWindow(UserRole);
            airlineWindow.ShowDialog();
            this.Close();
        }

        private void OpenAirportWindow_Click(object sender, RoutedEventArgs e)
        {
            AirportWindow airportWindow = new AirportWindow(UserRole);
            airportWindow.ShowDialog();
            this.Close();
        }

        private void OpenPassengerWindow_Click(object sender, RoutedEventArgs e)
        {
            PassengerWindow passengerWindow = new PassengerWindow(UserRole);
            passengerWindow.ShowDialog();
            this.Close();
        }

        private void OpenBookingWindow_Click(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow(UserRole);
            bookingWindow.ShowDialog();
            this.Close();
        }

        private void OpenBookingPlatformWindow_Click(object sender, RoutedEventArgs e)
        {
            BookingPLatformWindow bookingPlatformWindow = new BookingPLatformWindow(UserRole);
            bookingPlatformWindow.ShowDialog();
            this.Close();
        }

        private void OpenFlightWindow_Click(object sender, RoutedEventArgs e)
        {
            FlightWindow flightWindow = new FlightWindow(UserRole);
            flightWindow.ShowDialog();
            this.Close();
        }

        private void OpenBaggageWindow_Click(object sender, RoutedEventArgs e)
        {
            BaggageWindow baggageWindow = new BaggageWindow(UserRole);
            baggageWindow.ShowDialog();
            this.Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText) && decimal.TryParse(searchText, out decimal weight))
            {
                var searchResult = baggageRepository.SearchByWeight(weight);
                DataGridBaggage.ItemsSource = searchResult;
            }
            else
            {
                LoadList();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App)?.Logout();
        }

        
    }
}
