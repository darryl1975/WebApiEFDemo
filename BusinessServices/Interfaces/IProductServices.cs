using EFDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    /// <summary>  
    /// Product Service Contract  
    /// </summary>  
    public interface IProductServices
    {
        Product GetProductById(int productId);
        IEnumerable<Product> GetAllProducts();
        int CreateProduct(Product productEntity);
        bool UpdateProduct(int productId, Product productEntity);
        bool DeleteProduct(int productId);
    }
}
