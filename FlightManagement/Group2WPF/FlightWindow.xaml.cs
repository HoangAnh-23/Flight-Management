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
using System.Windows.Shapes;
using BusinessLogic.Repository;
using DataAccess.Models;
using BusinessLogic.Dao;

namespace Group2WPF
{
    /// <summary>
    /// Interaction logic for FlightWindow.xaml
    /// </summary>
    public partial class FlightWindow : Window
    {
        private readonly IFlightRepository flightRepository;
        private int currentPage = 1;
        private const int PageSize = 10;
        private int totalRecords = 0;
        public string UserRole { get; set; }
        public FlightWindow(string role)
        {
            InitializeComponent();
            flightRepository = new FlightRepository();
            UserRole = role;
            SetupRoleBasedUI();
   
            UpdatePagination();
            LoadList();
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
        private void LoadList()
        {
            totalRecords = flightRepository.GetTotalCount(); 
            UpdatePagination(); 
            UpdateDataGrid(); 
        }

        private void UpdateDataGrid()
        {
            IEnumerable<Flight> flights = flightRepository.GetPaged(currentPage, PageSize); 
            DataGridFlight.ItemsSource = flights;
        }


        private Flight GetObject()
        {
            Flight flight = null;
            try
            {
                flight = new Flight
                {
                    Id = int.Parse(txtId.Text),
                    AirlineId = int.Parse(txtAirlineId.Text),
                    DepartingAirport = int.Parse(txtDepartingAirport.Text),
                    ArrivingAirport = int.Parse(txtArrivingAirport.Text),
                    DepartingGate = txtDepartingGate.Text,
                    ArrivingGate = txtArrivingGate.Text,
                    DepartureTime = Convert.ToDateTime(txtDepartureTime.Text),
                    ArrivalTime = Convert.ToDateTime(txtArrivalTime.Text)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Flight");
            }
            return flight;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Flight flight = GetObject();
                flightRepository.insert(flight);
                LoadList();
                MessageBox.Show($"{flight.Id} inserted successfully ", "Insert");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Flight flight = GetObject();
                flightRepository.update(flight);
                LoadList();
                MessageBox.Show($"{flight.Id} updated successfully ", "Updated");
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
                Flight flight = GetObject();
                MessageBoxResult result = MessageBox.Show($"Do you want to delete {flight.Id}?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    flightRepository.delete(flight);
                    LoadList();
                    MessageBox.Show($"{flight.Id} deleted successfully", "Delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtId.Text = "";
            txtAirlineId.Text = "";
            txtDepartingAirport.Text = "";
            txtArrivingAirport.Text = "";
            txtDepartingGate.Text = "";
            txtArrivingGate.Text = "";
            txtDepartureTime.Text = "";
            txtArrivalTime.Text = "";
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
            if (!string.IsNullOrEmpty(searchText))
            {
                var searchResult = flightRepository.SearchByName(searchText);
                DataGridFlight.ItemsSource = searchResult;
            }
            else
            {
                LoadList();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App)?.Logout();
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

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var statistics = flightRepository.GetStatistics();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Year\tMonth\tFlight Count");

                foreach (var stat in statistics)
                {
                    sb.AppendLine($"{stat.Year}\t{stat.Month}\t{stat.FlightCount}");
                }

                MessageBox.Show(sb.ToString(), "Flight Statistics");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    
    }
}
