using JWTAuthCoreAPIRestful.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthCoreAPIRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var productList = _productRepository.GetAllProducts();
            return Ok(productList);
        }
    }
}
