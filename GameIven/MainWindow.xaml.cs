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
    }
}