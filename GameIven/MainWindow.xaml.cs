using Microsoft.Win32;
using Repositories.Entities;
using Services;
using System.Diagnostics.Eventing.Reader;
using System.Security.RightsManagement;
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

namespace GameIven
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProductService _productService = new();
        private SupplierService _supplierService = new();
        private string _excelFilePath;
        public UserAccount UserAccount { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadDataGrid()
        {
            ProductDataGrid.ItemsSource = null;
            ProductDataGrid.ItemsSource = _productService.GetAllProducts();
        }
        private void ProductMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
            GreetUserLabel.Content = "Hello " + UserAccount.Name;
            if(UserAccount.Role == 1)
            {
                UserRoleLabel.Content = "Admin";
            }
            else
            {
                UserRoleLabel.Content = "Staff";
            }
            SearchSupplierComboBox.ItemsSource = _supplierService.GetSuppliers();
            SearchSupplierComboBox.DisplayMemberPath = "SupplierName";
            SearchSupplierComboBox.SelectedValuePath = "SupplierId";
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUpdateWindow create = new();
            create.ShowDialog();
            LoadDataGrid();
        }

        private void DeleteButton_Click_1(object sender, RoutedEventArgs e)
        {
            Product selected = ProductDataGrid.SelectedItem as Product;
            if (selected == null)
            {
                MessageBox.Show("Please select product you want to delete", "Select one", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            _productService.DeleteProduct(selected);
            LoadDataGrid();
        }

<<<<<<< HEAD
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Product selected = ProductDataGrid.SelectedValue as Product;
            if (selected == null)
            {
                MessageBox.Show("Please select product you want to update", "Select one", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            CreateUpdateWindow update = new();
            update.selectedProduct = selected;
            update.ShowDialog();
            LoadDataGrid();
=======
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            int? maxPrice = null;
            int? minPrice = null;
            int? supplierId = null;
            if (!string.IsNullOrEmpty(SearchMaxPriceTextBox.Text))
            {
                try
                {
                    maxPrice = int.Parse(SearchMaxPriceTextBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Max price must be a number!", "Format Error", MessageBoxButton.OK);
                }
            }

            if (!string.IsNullOrEmpty(SearchMinPriceTextBox.Text))
            {
                try
                {
                    minPrice = int.Parse(SearchMinPriceTextBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Max price must be a number!", "Format Error", MessageBoxButton.OK);
                }
                
            }
            string productName = SearchNameTextBox.Text;
            if(SearchSupplierComboBox.SelectedItem != null)
            {
                supplierId = int.Parse(SearchSupplierComboBox.SelectedValue.ToString());
            }
            bool checkIsInStock = SearchInStockCheckBox.IsChecked ?? false;
            List<Product> products = _productService.SearchProduct(productName, supplierId, maxPrice, minPrice, checkIsInStock);
            ProductDataGrid.ItemsSource = null;
            ProductDataGrid.ItemsSource = products;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchMaxPriceTextBox.Text = "";
            SearchMinPriceTextBox.Text = "";
            SearchNameTextBox.Text = "";
            SearchSupplierComboBox.SelectedIndex = -1;
            SearchInStockCheckBox.IsChecked = false;
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _excelFilePath = openFileDialog.FileName;
                FileNameTextBox.Text = _excelFilePath;
            }

        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_excelFilePath))
            {
                MessageBox.Show("Please select an Excel file first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _productService.ImportProducts(_excelFilePath);
            MessageBox.Show("Import completed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadDataGrid(); 
>>>>>>> da61ffb21855b4ddd3f0428f79ab716bfc88c6a3
        }
    }
}