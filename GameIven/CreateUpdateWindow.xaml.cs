using Repositories.Entities;
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

namespace GameIven
{
    /// <summary>
    /// Interaction logic for CreateUpdateWindow.xaml
    /// </summary>
    public partial class CreateUpdateWindow : Window
    {
        public Product selectedProduct { get; set; } = null;
        public CreateUpdateWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (selectedProduct != null)
            {
                WindowLabel.Content = "Update Product";
                ConfirmButton.Content = "Update";
            }

            if (selectedProduct == null)
            {
                WindowLabel.Content = "Create Product";
                ConfirmButton.Content = "Create";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
