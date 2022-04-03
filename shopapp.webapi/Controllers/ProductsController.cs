using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webapi.DTO;

namespace shopapp.webapi.Controllers
{
    // localhost:4001/products
    // localhost:4001/products/2
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetAll();
            var productsDto = new List<ProductDTO>();
            
            foreach (var item in products)
            {
                productsDto.Add(ProductToDto(item)); // Burada oluşturduğumuz metoda parametre göndererek kod tekrarını azalttım
            }

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound(); // 404 Error
            }
            return Ok(product); // 200 Ok Code
        }

        [HttpPost]
        public IActionResult CreateProduct(Product entity)
        {
            _productService.Create(entity);
            return CreatedAtAction(nameof(GetProduct), new {id = entity.ProductId}, entity);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound(); // 404 Error
            }

            _productService.Delete(product);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product entity)
        {
            var product = _productService.GetById(entity.ProductId);
            if (product == null)
            {
                return NotFound(); // 404 Error
            }

            product.Name = entity.Name;
            product.Url = entity.Name;
            product.Price = entity.Price;
            product.Rating = entity.Rating;
            product.Stock = entity.Stock;
            product.Description = entity.Description;
            product.ImageUrl = entity.ImageUrl;
            product.IsHome = entity.IsHome;
            product.IsApproved = entity.IsApproved;

            _productService.Update(product);
            return CreatedAtAction(nameof(GetProduct), new {id = product.ProductId}, entity);
        }

        // Burada ise ilk önce DTO (Data Transfer Object) Klasöründeki ProductDTO'yu oluşturup sadece istediğim bilgileri içine aktararak kullanıcıya gösterdim.
        private ProductDTO ProductToDto(Product product)
        {
            return new ProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Url = product.Url,
                Price = product.Price,
                Rating = product.Rating,
                Stock = product.Stock,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            };
        }
    }
}