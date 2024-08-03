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
using DataAccess.Models;
using BusinessLogic.Repository;

namespace Group2WPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        IAccountMemberRepository accountMemberRepository;
        public LoginWindow()
        {
            InitializeComponent();
            accountMemberRepository=new AccountMemberRepository();  
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                msg += "Email is required.\n";
            }
            if (string.IsNullOrEmpty(txtPassword.Password))
            {
                msg += "Password is required.";
            }
            if (msg.Length > 0)
            {
                MessageBox.Show(msg);
                return;
            }

            bool loginSuccess = accountMemberRepository.Login(txtEmail.Text, txtPassword.Password);

            if (!loginSuccess)
            {
                MessageBox.Show("Email or password is incorrect! Please try again.");
                return;
            }

            string userRole = accountMemberRepository.GetUserRole(txtEmail.Text);

            MainWindow main = new MainWindow(userRole);
            main.Show();
            this.Close();
        }

        private void RegisterTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
