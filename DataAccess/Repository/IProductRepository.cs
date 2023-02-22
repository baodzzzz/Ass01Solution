using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        void addProduct(Product product);
        void editProduct(Product product);
        void deleteProduct(Product product);
        List<Product> searchProduct(string productName);
    }
}
