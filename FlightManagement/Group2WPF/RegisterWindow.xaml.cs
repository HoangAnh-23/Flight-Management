using BusinessLogic.Dao;
using DataAccess.Models;
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

namespace Group2WPF
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            string role = txtRole.Text;
            string name = txtName.Text;
            DateTime? dob = dpDOb.SelectedDate;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role) ||
                string.IsNullOrEmpty(name) || !dob.HasValue || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address))
            {
                txtErrorMessage.Text = "Please fill in all fields.";
                return;
            }

            AccountMember newAccount = new AccountMember
            {
                Email = email,
                Password = password,
                Role = role,
                Name = name,
                DOb = dob,
                Phone = phone,
                Address = address
            };

            try
            {
                AccountDAO.Instance.Register(newAccount);
                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                txtErrorMessage.Text = ex.Message;
            }
        }

        private void LoginTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();    
            loginWindow.ShowDialog();
            this.Close();
        }
    }
}

