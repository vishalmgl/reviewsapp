using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reviewsapp.dto;
using reviewsapp.Interfaces;
using reviewsapp.models;


namespace reviewsapp.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ModelControllers : Controller
    {
        private readonly IModelRepository _modelRepository;
        private readonly IOwnerNamesRepository _ownerNamesRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ModelControllers(IModelRepository ModelRepository, IOwnerNamesRepository ownerNamesRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _modelRepository = ModelRepository;
            _ownerNamesRepository = ownerNamesRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<Model>))]
        public IActionResult GetModels()
        {
            var Models = _mapper.Map<List<ModelDto>>(_modelRepository.GetModels());
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
            if (_modelRepository.ModelExists(modId))
                return NotFound();

            var model = _modelRepository.GetModel(modId);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(model);
        }
        [HttpGet("{modId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetModelRating(int modId)
        {
            if (!_modelRepository.ModelExists(modId))
                return NotFound();
            var rating = _modelRepository.GetModelRating(modId);
            if (!ModelState.IsValid)
                return BadRequest(rating);
            return Ok(rating);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateModel([FromQuery] int ownerId, [FromQuery][FromBody] int catId, ModelDto modelCreate)
        {
            if (modelCreate == null)
                return BadRequest(ModelState);
            var models = _modelRepository.GetModels()
                .Where(c => c.Name.Trim().ToUpper() == modelCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (models != null)
            {
                ModelState.AddModelError("", "Owner already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var modelMap = _mapper.Map<Model>(modelCreate);

            if (!_modelRepository.Createmodel(ownerId, catId, modelMap))
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
        public IActionResult updatemodel(int modId, [FromQuery] int ownerId, [FromQuery] int catId, [FromBody] ModelDto updatemodel)
        {
            if (updatemodel == null)
                return BadRequest(ModelState);

            if (modId != updatemodel.Id)
                return BadRequest(ModelState);

            if (!_modelRepository.ModelExists(modId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var modelMap = _mapper.Map<Model>(updatemodel);
            if (!_modelRepository.updatemodel(ownerId, catId, modelMap))
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
            if (!_modelRepository.ModelExists(modId))
            {
                return NotFound();

            }
            var reviewstoDelete = _reviewRepository.GetReviewsofAModel(modId);
            var ModelToDelete = _modelRepository.GetModel(modId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_reviewRepository.DeleteReviews(reviewstoDelete.ToList()))
            {
                ModelState.AddModelError("", "somthing went wrong when deleting");
            }
            if (!_modelRepository.deleteModel(ModelToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting category");

            }
            return NoContent();
        }
    }


}
