using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.entity;

namespace shopapp.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]        
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound(); // 400 Error
            }

            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory(Category entity)
        {
            _categoryService.Create(entity);
            return CreatedAtAction(nameof(GetCategory), new {id = entity.CategoryId}, entity);
        }

        [HttpDelete("{id}")]
        public IActionResult CategoryDelete(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound(); // 404 Error
            }

            _categoryService.Delete(category);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category entity)
        {
            var category = _categoryService.GetById(entity.CategoryId);
            if (category == null)
            {
                return NotFound(); // 404 Error
            }

            category.Name = entity.Name;
            category.Url = entity.Url;

            _categoryService.Update(category);
            return CreatedAtAction(nameof(GetCategory), new {id = category.CategoryId}, entity);
        }
    }
}