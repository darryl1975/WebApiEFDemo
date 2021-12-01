using BusinessServices.Interfaces;
using EFDemo.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;
using Xunit;

namespace WebAPITest
{
    public class ProductTest
    {
        #region Property  
        public Mock<IProductServices> mock = new Mock<IProductServices>();
        #endregion

        [Fact]
        public async void GetProductById()
        {
            mock.Setup(p => p.GetProductById(1));
            ProductController prod = new ProductController();
            Product result = (Product)prod.Get(1);
            Assert.Equal("Nov design rear absorber for 3sixty/pikes/brompton", result.ProductName);
        }

        [Fact]
        public async void GetAllProducts()
        {
            mock.Setup(p => p.GetAllProducts());
            ProductController prod = new ProductController();
            List<Product> result = prod.Get().ToList();
            Assert.True(result.Count > 0);
        }
    }
}
