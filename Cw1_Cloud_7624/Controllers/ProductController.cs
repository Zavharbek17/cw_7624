using Cw1_Cloud_7624.Model;
using Cw1_Cloud_7624.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Cw1_Cloud_7624.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // GET: api/Product
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productRepository.GetProducts();
            return new OkObjectResult(products);
        }   
        // GET: api/Product/{id}
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult Get(int id)
        {
            var product = _productRepository.GetProductById(id);
            return new OkObjectResult(product);
            //return "value";
        }
        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            using (var scope = new TransactionScope())
            {
                _productRepository.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
        }
        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    _productRepository.UpdateProduct(product);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return new OkResult();
        }
    }
}
