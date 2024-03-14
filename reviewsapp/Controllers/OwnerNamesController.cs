using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using reviewsapp.dto;
using reviewsapp.Interfaces;
using reviewsapp.models;
using reviewsapp.Repository;

namespace reviewsapp.Controllers
{
     [Route("api/controller")]
        [ApiController]
    public class OwnerNamesController :Controller

    {
        private readonly IOwnerNamesRepository _ownerNamesRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        
        public OwnerNamesController(IOwnerNamesRepository ownerNamesRepository,ICountryRepository countryRepository, IMapper mapper)
        {
            _ownerNamesRepository = ownerNamesRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerNameRepository>))]
        public IActionResult GetOwnerNames()
        {
            var OwnerName = _mapper.Map<List<OwnerNamesDto>>(_ownerNamesRepository.GetOwnerNames());
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
            if (_ownerNamesRepository.OwnersExist(OwnerId))
                return NotFound();

            var OwnerNames = _ownerNamesRepository.GetOwner(OwnerId);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(OwnerNames);
        }
        [HttpGet("{OwnerId/Model}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerName>))]
        [ProducesResponseType(400)]
        public IActionResult GetModelByOwnerNames(int OwnerId)
        {
            if (!_ownerNamesRepository.OwnersExist(OwnerId))
            {
                return NotFound();
            }
            var ownernames =_mapper.Map<List<OwnerNamesDto>>(_ownerNamesRepository.GetModelByOwnernames(OwnerId));
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(ownernames);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery]int CountryId,[FromBody] OwnerNamesDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);
            var owner = _ownerNamesRepository.GetOwnerNames()
                .Where(c => c.LastName.Trim().ToUpper() == ownerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Owner already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ownerMap = _mapper.Map<OwnerName>(ownerCreate);
            ownerMap.Country = _countryRepository.GetCountry(CountryId); 
            if (!_ownerNamesRepository.CreateOwner(ownerMap))
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

            if (!_ownerNamesRepository.OwnersExist(OwnerId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var OwnerMap = _mapper.Map<OwnerName>(UpdateOwnerName);
            if (!_ownerNamesRepository.UpdateOwnerNames(OwnerMap))
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
            if (!_ownerNamesRepository.OwnersExist(OwnerId))
            {
                return NotFound();

            }
            var OwnerToDelete = _ownerNamesRepository.GetOwner(OwnerId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_ownerNamesRepository.DeleteOwner(OwnerToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting category");

            }
            return NoContent();
        }
    }
}
