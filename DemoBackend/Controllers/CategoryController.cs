using AutoMapper;
using DemoBackend.Dtos;
using DemoBackend.Interfaces;
using DemoBackend.Models;
using DemoBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoBackend.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : Controller
    {
        private ICategoryInterface _categoryRepository;
        private IMapper _mapper;

        public CategoryController(ICategoryInterface categoryRepository, IMapper mapper) { 
            _categoryRepository = categoryRepository;
            _mapper=mapper;
        }
        [HttpGet("getAllCategories")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllCategories()
        {
            var categories =  _mapper.Map<List<CategoryDto>>(_categoryRepository.GetAllCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpPost("addCategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult addCategory([FromBody] CategoryDto request) { 

            if(request == null)
                return BadRequest(ModelState);

            if(_categoryRepository.CategoryExist(request.categoryName))
                return BadRequest("Category Already Exists");

            Category category = new Category();
            category.categoryId = _categoryRepository.LastCategory() + 1;
            category.categoryName = request.categoryName.Trim();

            bool categoryAdded = _categoryRepository.AddCategory(category);

            if (!categoryAdded)
                return BadRequest("Something went wrong");

            return Ok("Category Successfully Added");
        }
    }
}
