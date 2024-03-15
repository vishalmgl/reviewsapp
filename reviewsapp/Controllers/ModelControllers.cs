using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reviewsapp.dto;
using reviewsapp.Interfaces;
using reviewsapp.models;


namespace reviewsapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelControllers : Controller
    {
        private readonly IModelRepository _ModelRepository;
        private readonly IOwnerNamesRepository _OwnerNamesRepository;
        private readonly IReviewRepository _ReviewRepository;
        private readonly IMapper _Mapper;

        public ModelControllers(IModelRepository ModelRepository,IOwnerNamesRepository OwnerNamesRepository,IReviewRepository ReviewRepository, IMapper Mapper)
        {
            _ModelRepository = ModelRepository;
            _OwnerNamesRepository = OwnerNamesRepository;
            _ReviewRepository = ReviewRepository;
            _Mapper = Mapper;
        }
        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<Model>))]
        public IActionResult GetModels()
        {
            var Models = _Mapper.Map<List<ModelDto>>(_ModelRepository.GetModels());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Models);
        }
        [HttpGet("{modId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Model>))]
        [ProducesResponseType(400)]
        public IActionResult GetModel(int modId)
        {
            if (_ModelRepository.ModelExists(modId))
                return NotFound();

            var Model = _ModelRepository.GetModel(modId);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(Model);
        }
        [HttpGet("{modId}/Rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetModelRating (int modId)
        {
            if (!_ModelRepository.ModelExists(modId))
                return NotFound(); 
            var Rating = _ModelRepository.GetModelRating(modId);
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(Rating);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateModel([FromQuery] int OwnerId, [FromQuery]int catId, ModelDto ModelCreate)
        {
            if (ModelCreate == null)
                return BadRequest(ModelState);
            var Models = _ModelRepository.GetModels()
                .Where(c => c.Name.Trim().ToUpper() == ModelCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Models != null)
            {
                ModelState.AddModelError("", "Owner already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var modelMap = _Mapper.Map<Model>(ModelCreate);

            if (!_ModelRepository.CreateModel(OwnerId, catId, modelMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);



            }
            return Ok("successfully created");
        }
        [HttpPut("{modId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateModel(int modId,  [FromQuery] int OwnerId,[FromQuery] int catId, [FromBody] ModelDto UpdateModel)        {
            if (UpdateModel == null)
                return BadRequest(ModelState);

            if (modId != UpdateModel.Id)
                return BadRequest(ModelState);

            if (!_ModelRepository.ModelExists(modId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var ModelMap = _Mapper.Map<Model>(UpdateModel);
            if (!_ModelRepository.UpdateModel(OwnerId,catId, ModelMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }
        [HttpDelete("{modId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteModel(int modId)
        {
            if (!_ModelRepository.ModelExists(modId))
            {
                return NotFound();

            }
            var ReviewstoDelete=_ReviewRepository.GetReviewsofAModel(modId);
            var ModelToDelete = _ModelRepository.GetModel(modId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_ReviewRepository.DeleteReviews(ReviewstoDelete.ToList()))
            {
                ModelState.AddModelError("", "somthing went wrong when deleting");
            }
            if (!_ModelRepository.DeleteModel(ModelToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting category");

            }
            return NoContent();
        }
    }


}
