using Microsoft.AspNetCore.Mvc;
using reviewsapp.Interfaces;
using reviewsapp.models;


namespace reviewsapp.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ModelControllers : Controller 
    {
        private readonly ImodelRepository _modelRepository;
        public ModelControllers(ImodelRepository ModelRepository)
        {
            _modelRepository = ModelRepository;
        }
        [HttpGet]

        [ProducesResponseType(200,Type=typeof(IEnumerable<Model>))]
        public IActionResult GetModels()
        {
            var Models = _modelRepository.GetModels();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }
            return Ok(Models);
        }
    }
    
}
