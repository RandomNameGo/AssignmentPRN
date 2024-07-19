using Repositories.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private ProductService _service = new();
        private CategoryService _categoryservice = new();
        private SupplierService _supplierservice = new();
        public Product selectedProduct { get; set; } = null;
        public CreateUpdateWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProductCategoryComboBox.ItemsSource = _categoryservice.GetCategories();

            ProductCategoryComboBox.DisplayMemberPath = "CategoryName";
            ProductCategoryComboBox.SelectedValuePath = "CategoryId";

            ProductSupplierComboBox.ItemsSource = _supplierservice.GetSuppliers();

            ProductSupplierComboBox.DisplayMemberPath = "SupplierName";
            ProductSupplierComboBox.SelectedValuePath = "SupplierId";

            ProductIDTextBox.IsEnabled = false;

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

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProduct == null)
            {
                Product p = new Product();

                p.ProductName = ProductNameTextBox.Text;
                p.Price = int.Parse(ProductPriceTextBox.Text);
                p.Quantity = int.Parse(ProductQuantityTextBox.Text);
                p.Warranty = ProductWarrantyTextBox.Text;
                p.YearOfManufacture = int.Parse(ProductYOMTextBox.Text);
                p.SupplierId = int.Parse(ProductSupplierComboBox.SelectedValue.ToString());
                //p.CategoryId = int.Parse((ProductCategoryComboBox.SelectedValue.ToString());
                p.CategoryId = int.Parse(ProductCategoryComboBox.SelectedValue.ToString());

                //MessageBoxResult confirm = MessageBox.Show("Do you really want to add this product");

                _service.CreateProduct(p);
                this.Close();
            }
        }
    }
}
