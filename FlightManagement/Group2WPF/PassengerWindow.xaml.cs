using System;
using System.Collections.Generic;
using System.Windows;
using DataAccess.Models;
using BusinessLogic.Repository;
using System.Data;
using System.Linq;
using System.Windows.Controls;

namespace Group2WPF
{
    public partial class PassengerWindow : Window
    {
        private readonly IPassengerRepository passengerRepository;
        private int currentPage = 1;
        private const int PageSize = 10;
        private int totalRecords = 0;
      

        public string UserRole { get; set; }
        public PassengerWindow(string role)
        {
            InitializeComponent();
            passengerRepository = new PassengerRepository();
            UserRole = role;
            LoadList();
            SetupRoleBasedUI();
            UpdatePagination();
      
        }
  

        private void LoadList()
        {
            IEnumerable<Passenger> passengers = passengerRepository.GetPaged(currentPage, PageSize);
            totalRecords = passengerRepository.GetTotalCount();
            DataGridPassenger.ItemsSource = passengers;
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


        private void UpdateDataGrid()
        {
            IEnumerable<Passenger> passengers = passengerRepository.GetPaged(currentPage, PageSize);
            DataGridPassenger.ItemsSource = passengers;
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

        private Passenger GetObject()
        {
            try
            {
                // Lấy giá trị từ DatePicker
                DateTime? selectedDate = dpDateOfBirth.SelectedDate;

                // Kiểm tra nếu selectedDate có giá trị
                if (!selectedDate.HasValue)
                {
                    MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng chọn ngày hợp lệ.", "Lỗi");
                    return null;
                }

                return new Passenger
                {
                    Id = string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text),
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    DateOfBirth = selectedDate.Value,
                    Country = txtCountry.Text,
                    Email = txtEmail.Text,
                    Gender = txtGender.Text
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
                return null;
            }
        }





        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Passenger passenger = GetObject();
                passengerRepository.insert(passenger);
                LoadList();
                MessageBox.Show($"Passenger ID: {passenger.Id} inserted successfully ", "Insert");
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
                Passenger passenger = GetObject();
                passengerRepository.update(passenger);
                LoadList();
                MessageBox.Show($"Passenger ID: {passenger.Id} updated successfully ", "Updated");
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
                Passenger passenger = GetObject();
                MessageBoxResult result = MessageBox.Show($"Do you want to delete {passenger.Id}?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    passengerRepository.delete(passenger);
                    LoadList();
                    MessageBox.Show($"Passenger ID: {passenger.Id} deleted successfully", "Delete");
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
            txtFirstName.Text = "";
            txtLastName.Text = "";
            dpDateOfBirth.SelectedDate = null;
            txtCountry.Text = "";
            txtEmail.Text = "";
            txtGender.Text = "";
        }

        private void OpenAirlineWindow_Click(object sender, RoutedEventArgs e)
        {
            AirlineWindow airlineWindow = new AirlineWindow(UserRole);
            airlineWindow.ShowDialog();
        }

        private void OpenAirportWindow_Click(object sender, RoutedEventArgs e)
        {
            AirportWindow airportWindow = new AirportWindow(UserRole);
            airportWindow.ShowDialog();
        }

        private void OpenPassengerWindow_Click(object sender, RoutedEventArgs e)
        {
            PassengerWindow passengerWindow = new PassengerWindow(UserRole);
            passengerWindow.ShowDialog();
        }

        private void OpenBookingWindow_Click(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow(UserRole);
            bookingWindow.ShowDialog();
        }

        private void OpenBookingPlatformWindow_Click(object sender, RoutedEventArgs e)
        {
            BookingPLatformWindow bookingPlatformWindow = new BookingPLatformWindow(UserRole);
            bookingPlatformWindow.ShowDialog();
        }

        private void OpenFlightWindow_Click(object sender, RoutedEventArgs e)
        {
            FlightWindow flightWindow = new FlightWindow(UserRole);
            flightWindow.ShowDialog();
        }

        private void OpenBaggageWindow_Click(object sender, RoutedEventArgs e)
        {
            BaggageWindow baggageWindow = new BaggageWindow(UserRole);
            baggageWindow.ShowDialog();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                var searchResult = passengerRepository.SearchByName(searchText);
                DataGridPassenger.ItemsSource = searchResult;
            }
            else
            {
                currentPage = 1;
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

    }
}
