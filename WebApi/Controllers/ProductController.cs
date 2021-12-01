using BusinessServices.Interfaces;
using BusinessServices.Services;
using EFDemo.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController()
        {
            _productServices = new ProductServices();
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var products = _productServices.GetAllProducts();
            if (products != null)
            {
                var productEntities = products as List<Product> ?? products.ToList();
                if (productEntities.Any())
                    return productEntities;
            }
            return null;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _productServices.GetProductById(id);
            if (product != null)
                return Ok(product);
            return null;
        }

        // POST api/<ProductController>
        [HttpPost]
        public int Post([FromBody] Product product)
        {
            return _productServices.CreateProduct(product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] Product product)
        {
            if (id > 0)
            {
                return _productServices.UpdateProduct(id, product);
            }
            return false;
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (id > 0)
                return _productServices.DeleteProduct(id);
            return false;
        }
    }
}
