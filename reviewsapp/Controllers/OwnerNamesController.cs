using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using reviewsapp.dto;
using reviewsapp.Interfaces;
using reviewsapp.models;
using reviewsapp.Repository;

namespace reviewsapp.Controllers
{
     [Route("api/[controller]")]
        [ApiController]
    public class OwnerNamesController :Controller

    {
        private readonly IOwnerNamesRepository _OwnerNamesRepository;
        private readonly ICountryRepository _CountryRepository;
        private readonly IMapper _Mapper;
        
        public OwnerNamesController(IOwnerNamesRepository OwnerNamesRepository,ICountryRepository CountryRepository, IMapper Mapper)
        {
            _OwnerNamesRepository = OwnerNamesRepository;
            _CountryRepository = CountryRepository;
            _Mapper = Mapper;
        }

        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerNameRepository>))]
        public IActionResult GetOwnerNames()
        {
            var OwnerName = _Mapper.Map<List<OwnerNamesDto>>(_OwnerNamesRepository.GetOwnerNames());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok (OwnerName);
        }
        [HttpGet("{OwnerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerName>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnerNames(int OwnerId)
        {
            if (_OwnerNamesRepository.OwnersExist(OwnerId))
                return NotFound();

            var OwnerNames = _OwnerNamesRepository.GetOwner(OwnerId);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(OwnerNames);
        }
        [HttpGet("{OwnerId}/Model")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerName>))]
        [ProducesResponseType(400)]
        public IActionResult GetModelByOwnerNames(int OwnerId)
        {
            if (!_OwnerNamesRepository.OwnersExist(OwnerId))  
            {
                return NotFound();
            }
            var OwnerNames =_Mapper.Map<List<OwnerNamesDto>>(_OwnerNamesRepository.GetModelByOwnerNames(OwnerId));
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(OwnerNames);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery]int CountryId,[FromBody] OwnerNamesDto OwnerCreate)
        {
            if (OwnerCreate == null)
                return BadRequest(ModelState);
            var Owner = _OwnerNamesRepository.GetOwnerNames()
                .Where(c => c.LastName.Trim().ToUpper() == OwnerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Owner != null)
            {
                ModelState.AddModelError("", "Owner already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var OwnerMap = _Mapper.Map<OwnerName>(OwnerCreate);
            OwnerMap.Country = _CountryRepository.GetCountry(CountryId); 
            if (!_OwnerNamesRepository.CreateOwner(OwnerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);



            }
            return Ok("successfully created");
        }
        [HttpPut("{OwnerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwnerName(int OwnerId, [FromBody] OwnerNamesDto UpdateOwnerName)
        {
            if (UpdateOwnerName == null)
                return BadRequest(ModelState);

            if (OwnerId != UpdateOwnerName.Id)
                return BadRequest(ModelState);

            if (!_OwnerNamesRepository.OwnersExist(OwnerId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var OwnerMap = _Mapper.Map<OwnerName>(UpdateOwnerName);
            if (!_OwnerNamesRepository.UpdateOwnerNames(OwnerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }
        [HttpDelete("{OwnerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwnerName(int OwnerId)
        {
            if (!_OwnerNamesRepository.OwnersExist(OwnerId))
            {
                return NotFound();

            }
            var OwnerToDelete = _OwnerNamesRepository.GetOwner(OwnerId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_OwnerNamesRepository.DeleteOwner(OwnerToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting category");

            }
            return NoContent();
        }
    }
}
