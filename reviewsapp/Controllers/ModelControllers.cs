using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reviewsapp.dto;
using reviewsapp.Interfaces;
using reviewsapp.models;
using System.Reflection.Metadata.Ecma335;


namespace reviewsapp.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ModelControllers : Controller
    {
        private readonly ImodelRepository _modelRepository;
        public ModelControllers(ImodelRepository ModelRepository,IMapper mapper)
        {
            _modelRepository = ModelRepository;
        }
        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<Model>))]
        public IActionResult GetModels()
        {
            var Models =_mapper.Maps<List<Modeldto>>(_modelRepository.GetModels());
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
        

        public IActionResult GetModelrating(int modId)
        {
            if(!_modelRepository.ModelExists(modId))
                return NotFound();
            var rating=_modelRepository.GetModelRating(modId);
            if(!ModelState.IsValid)
                return BadRequest(rating);
        }
    }


}
