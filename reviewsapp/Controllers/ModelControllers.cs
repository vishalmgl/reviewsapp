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
        private readonly IMapper _mapper;

        public ModelControllers(IModelRepository ModelRepository,IMapper mapper)
        {
            _modelRepository = ModelRepository;
            _mapper = mapper;
        }
        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<Model>))]
        public IActionResult GetModels()
        {
            var Models =_mapper.Map<List<Modeldto>>(_modelRepository.GetModels());
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

                return BadRequest(model);

            return Ok(model);
        }
        [HttpGet("{modId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        

        public IActionResult GetModelRating(int modId)
        {
            if(!_modelRepository.ModelExists(modId))
                return NotFound();
            var rating=_modelRepository.GetModelRating(modId);
            if(!ModelState.IsValid)
                return BadRequest(rating);
        }
    }


}
