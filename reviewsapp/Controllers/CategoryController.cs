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
        private readonly IMapper _Mapper;

        public CategoryController(ICategoryRepository CategoryRepository, IMapper Mapper)
        {
            _CategoryRepository = CategoryRepository;
            _Mapper = Mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var Categories = _Mapper.Map<List<CategoryDto>>(_CategoryRepository.GetCategories());
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
        [HttpGet("category/{CategoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetModelByCategoryId(int CategoryId)
        {
            var category = _Mapper.Map<List<CategoryDto>>(
                _CategoryRepository.GetModelsByCategory(CategoryId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);
        
        
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody]CategoryDto CategoryCreate)
        {
            if(CategoryCreate ==null)
                return BadRequest(ModelState);
            var category =_CategoryRepository.GetCategories()
                .Where(c=>c.Name.Trim().ToUpper() == CategoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "category already exist");
                return StatusCode(422,ModelState);
            }
               
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var categoryMap = _Mapper.Map<Category>(CategoryCreate);
                if (!_CategoryRepository.CreateCategory(categoryMap))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500,ModelState);
                    


                }
            return Ok("successfully created");
            }
        [HttpPut("{CategoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int CategoryId, [FromBody]CategoryDto UpdateCategory)
        { if (UpdateCategory ==null)
                return BadRequest(ModelState);

        if (CategoryId != UpdateCategory.Id)
                return BadRequest(ModelState);

        if (!_CategoryRepository.CategoryExists(CategoryId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var CatogoryMap= _Mapper.Map<Category>(UpdateCategory);
            if(!_CategoryRepository.UpdateCategory(CatogoryMap))
            {
                ModelState.AddModelError("","Something went wrong updating category");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }
        [HttpDelete("{CategoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int CategoryId)
        {
            if (!_CategoryRepository.CategoryExists(CategoryId))
            {
                return NotFound();

            }
            var CategoryToDelete = _CategoryRepository.GetCategory(CategoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_CategoryRepository.DeleteCategory(CategoryToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting category");

            }
            return NoContent(); 
        }



    }
}
