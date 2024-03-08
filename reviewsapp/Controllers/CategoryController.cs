using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reviewsapp.dto;
using reviewsapp.Interfaces;
using reviewsapp.models;
using reviewsapp.Repository;

namespace reviewsapp.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var Categories = _mapper.Map<List<Categorydto>>(_categoryRepository.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Categories);
        }
        [HttpGet("{CategoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int CategoryId)
        {
            if (_categoryRepository.CategoryExists(CategoryId))
                return NotFound();

            var category = _categoryRepository.GetCategory(CategoryId);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(category);
        }
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetModelByCategoryId(int CategoryId)
        {
            var category = _mapper.Map<List<Categorydto>>(
                _categoryRepository.GetModelsByCategory(CategoryId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(category);
        
        
        }

    }
}
