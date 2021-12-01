using BusinessServices.Interfaces;
using EFDemo.Model;
using Microsoft.AspNetCore.Mvc;
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

        // Reference
        // https://stackoverflow.com/questions/41292919/unit-testing-controller-methods-which-return-iactionresult

        [Fact]
        public async void GetProductById()
        {
            mock.Setup(p => p.GetProductById(1));
            ProductController prod = new ProductController();
            IActionResult result = prod.Get(1);
            var okResult = result as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);
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
