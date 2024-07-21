using Repositories.Entities;
using Services;
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
        }
    }
}