using Microsoft.IdentityModel.Tokens;
using Repositories.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace GameIven
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserAccountService _userAccountService = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = GetPassword(PasswordPasswordBox);

            UserAccount user = _userAccountService.Authenticate(email, password);
            if (user == null)
            {
                MessageBox.Show("Nani? Who tf are you??", "Invalid credential", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private string GetPassword(PasswordBox passwordBox)
        {
            SecureString securePassword = passwordBox.SecurePassword;

            IntPtr bstr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(securePassword);
            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(bstr);
            }
        }

    }
}
