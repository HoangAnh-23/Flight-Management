using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Group2WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void Logout()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            foreach (Window window in Current.Windows)
            {
                if (window is not LoginWindow)
                {
                    window.Close();
                }
            }

          
        }
    }
}
