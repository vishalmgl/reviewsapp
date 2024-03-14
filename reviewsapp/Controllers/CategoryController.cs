using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reviewsapp.dto;
using reviewsapp.Interfaces;
using reviewsapp.models;
using reviewsapp.Repository;
//controller handels the request
namespace reviewsapp.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _CategoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var Categories = _mapper.Map<List<CategoryDto>>(_CategoryRepository.GetCategories());
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
            if (_CategoryRepository.CategoryExists(CategoryId))
                return NotFound();

            var category = _CategoryRepository.GetCategory(CategoryId);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(category);
        }
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetModelByCategoryId(int CategoryId)
        {
            var category = _mapper.Map<List<CategoryDto>>(
                _CategoryRepository.GetModelsByCategory(CategoryId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);
        
        
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody]CategoryDto categoryCreate)
        {
            if(categoryCreate ==null)
                return BadRequest(ModelState);
            var category =_CategoryRepository.GetCategories()
                .Where(c=>c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "category already exist");
                return StatusCode(422,ModelState);
            }
               
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var categoryMap = _mapper.Map<Category>(categoryCreate);
                if (!_CategoryRepository.CreateCategory(categoryMap))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500,ModelState);
                    


                }
            return Ok("successfully created");
            }
        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody]CategoryDto updateCategory)
        { if (updateCategory ==null)
                return BadRequest(ModelState);

        if (categoryId != updateCategory.Id)
                return BadRequest(ModelState);

        if (!_CategoryRepository.CategoryExists(categoryId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var catogoryMap= _mapper.Map<Category>(updateCategory);
            if(!_CategoryRepository.UpdateCategory(catogoryMap))
            {
                ModelState.AddModelError("","Something went wrong updating category");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_CategoryRepository.CategoryExists(categoryId))
            {
                return NotFound();

            }
            var categoryToDelete = _CategoryRepository.GetCategory(categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_CategoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting category");

            }
            return NoContent(); 
        }



    }
}
