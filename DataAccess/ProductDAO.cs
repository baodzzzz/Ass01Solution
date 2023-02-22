using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataAccess
{
    public class ProductDAO: IProductRepository
    {
        public ASS1_DBContext db = new ASS1_DBContext();
        public void addProduct(Product product)
        {
            db.Add<Product>(product);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Add successfully!");
            }
        }

        public void deleteProduct(Product product)
        {
            db.Remove<Product>(product);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Record removed successfully");
            }
        }

        public void editProduct(Product product)
        {
            db.Update<Product>(product);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Edit success.");
            }
            else
            {
                MessageBox.Show("Edit failed.");
            }
        }

        public List<Product> searchProduct(string productName)
        {
            List<Product> productList = db.Products.Where(p =>
            p.ProductName.ToLower().Contains(productName)).ToList();
            if (productList.Count > 0)
            {
                return productList;
            }
            return null;
        }
    }
}
