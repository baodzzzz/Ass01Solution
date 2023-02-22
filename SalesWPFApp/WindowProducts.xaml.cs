using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using MaterialDesignColors;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for WindowProducts.xaml
    /// </summary>
    public partial class WindowProducts : Window
    {
        ASS1_DBContext db = new ASS1_DBContext();
        private IProductRepository productRepository;
        public WindowProducts()
        {
            InitializeComponent();
            productRepository = new ProductDAO();
        }

        public void LoadData()
        {
            var products = db.Products.Select(p => new
            {
                id = p.ProductId,
                categoryId = p.CategoryId,
                productName = p.ProductName,
                weight = p.Weight,
                unitPrice = p.UnitPrice,
                unitsInStock = p.UnitsInStock,
            }).ToList();

            lvProduct.ItemsSource = products;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtProductId.Text = "";
            txtCategoryId.Text = "";
            txtProductName.Text = "";
            txtWeight.Text = "";
            txtUnitPrice.Text = "";
            txtUnitInStock.Text = "";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Product p = new Product();
            p.CategoryId = int.Parse(txtCategoryId.Text);
            p.ProductName = txtProductName.Text;
            p.Weight = txtWeight.Text;
            p.UnitPrice = decimal.Parse(txtUnitPrice.Text);
            p.UnitsInStock = int.Parse(txtUnitInStock.Text);
            productRepository.addProduct(p);
            LoadData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Product p = new Product
            {
                ProductId = int.Parse(txtProductId.Text),
                CategoryId = int.Parse(txtCategoryId.Text),
                ProductName = txtProductName.Text,
                Weight = txtWeight.Text,
                UnitPrice = decimal.Parse(txtUnitPrice.Text),
                UnitsInStock = int.Parse(txtUnitInStock.Text),
            };
            productRepository.editProduct(p);
            LoadData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Product product = db.Products.FirstOrDefault(x => x.ProductId == int.Parse(txtProductId.Text));
            productRepository.deleteProduct(product);
            LoadData();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchValue = txtSearch.Text.ToLower();

            var productList = db.Products.Select(p => new
            {
                id = p.ProductId,
                categoryId = p.CategoryId,
                productName = p.ProductName,
                weight = p.Weight,
                unitPrice = p.UnitPrice,
                unitsInStock = p.UnitsInStock
            }).Where(p =>
                p.id.ToString().Equals(searchValue) ||
                p.unitPrice.ToString().Equals(searchValue) ||
                p.unitsInStock.ToString().Equals(searchValue) ||
                p.productName.ToLower().Contains(searchValue))
         .ToList();

            lvProduct.ItemsSource = productList;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            WindowMembers windowMembers = new WindowMembers();
            windowMembers.Show();
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            WindowOrders windowOrders = new WindowOrders();
            windowOrders.Show();
            this.Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            WindowProducts windowProducts = new WindowProducts();
            windowProducts.Show();
            this.Close();
        }
    }
}
