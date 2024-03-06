using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reviewsapp.dto;
using reviewsapp.Interfaces;
using reviewsapp.Repository;

namespace reviewsapp.Controllers
{
     [Route("api/controller")]
        [ApiController]
    public class OwnerNamesController :Controller

    {
        private readonly IOwnerNamesRepository _ownerNamesRepository;
        private readonly IMapper _mapper;

        public OwnerNamesController(IOwnerNamesRepository ownerNamesRepository,IMapper mapper)
        {
            _ownerNamesRepository = ownerNamesRepository;
            _mapper = mapper;
        }

        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerNameRepository>))]
        public IActionResult GetModels()
        {
            var Owner = _mapper.Map<List<Modeldto>>(_modelRepository.GetModels());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Models);
        }
    }
}
