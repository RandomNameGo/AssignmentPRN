using Microsoft.IdentityModel.Tokens;
using Repositories.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                ProductIDTextBox.Text = selectedProduct.ProductId.ToString();
                ProductIDTextBox.IsEnabled = false;
                ProductNameTextBox.Text = selectedProduct.ProductName.ToString();
                ProductPriceTextBox.Text = selectedProduct.Price.ToString();
                ProductQuantityTextBox.Text = selectedProduct.Quantity.ToString();
                ProductWarrantyTextBox.Text = selectedProduct.Warranty.ToString();
                ProductYOMTextBox.Text = selectedProduct.YearOfManufacture.ToString();
                ProductSupplierComboBox.SelectedValue = selectedProduct.SupplierId.ToString();
                ProductCategoryComboBox.SelectedValue = selectedProduct.CategoryId.ToString();
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
            if (string.IsNullOrEmpty(ProductNameTextBox.Text) ||
                string.IsNullOrEmpty(ProductPriceTextBox.Text) ||
                string.IsNullOrEmpty(ProductQuantityTextBox.Text) ||
                string.IsNullOrEmpty(ProductWarrantyTextBox.Text) ||
                string.IsNullOrEmpty(ProductYOMTextBox.Text) ||
                string.IsNullOrEmpty(ProductSupplierComboBox.Text) ||
                string.IsNullOrEmpty(ProductCategoryComboBox.Text))
            {
                MessageBox.Show("All fields are required! ", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!double.TryParse(ProductPriceTextBox.Text, out _))
            {
                MessageBox.Show("Price must be a number! ", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(ProductQuantityTextBox.Text, out _))
            {
                MessageBox.Show("Quantity must be an integer! ", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (int.Parse(ProductYOMTextBox.Text) > DateTime.Now.Year)
            {
                MessageBox.Show("Invalid year! ", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Product p;
            if (selectedProduct != null)
            {
                p = selectedProduct;
            }
            else
            {
                p = new Product();
            }
            p.ProductName = GetName(ProductNameTextBox.Text);
            p.Price = int.Parse(ProductPriceTextBox.Text);
            p.Quantity = int.Parse(ProductQuantityTextBox.Text);
            p.Warranty = ProductWarrantyTextBox.Text;
            p.YearOfManufacture = int.Parse(ProductYOMTextBox.Text);
            p.SupplierId = int.Parse(ProductSupplierComboBox.SelectedValue.ToString());
            //p.CategoryId = int.Parse((ProductCategoryComboBox.SelectedValue.ToString());
            p.CategoryId = int.Parse(ProductCategoryComboBox.SelectedValue.ToString());

            //MessageBoxResult confirm = MessageBox.Show("Do you really want to add this product");

            if (selectedProduct == null)
            {
                _service.CreateProduct(p);
                MessageBox.Show("Create");
            }
            else
            {
                _service.UpdateProduct(p);
                MessageBox.Show("Update");
            }
            this.Close();
        }


        //VALIDATORS
        public string GetName(string input)
        {
            //Console.WriteLine("Enter name: "); string input = Console.ReadLine();
            //split bằng khoảng trắng sẽ chia string thành mảng gồm nhiều giá  trị, bao nhiêu khoảng trắng thì sẽ có n+1 giá trị
            string[] format = input.Split(' ');
            //vd: đầu vào là dang pham xuan loc, có 3 khoảng trắng thì chia ra thành array có 4 giá trị
            for (int i = 0; i < format.Length; i++)
            {
                if (format[i].Length > 0)
                    format[i] = char.ToUpper(format[i][0]) + format[i].Substring(1).ToLower();
                //sau khi split sẽ thành mảng có 4 giá trị là dang, pham, xuan, loc. ngoặc vuông đầu chỉ vị trí của mảng, ở đây format[0] sẽ có giá trị là dang. ngoặc vuông thứ hai chỉ vị trí index => format[0][0] sẽ trả về ký tự d
            }


            return string.Join(" ", format);
            //gộp mảng lại thành string, ngăn cách bởi khoảng  trắng
        }
    }
}
