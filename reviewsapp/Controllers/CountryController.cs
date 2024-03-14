using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reviewsapp.dto;
using reviewsapp.Interfaces;
using reviewsapp.models;
using reviewsapp.Repository;

namespace reviewsapp.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _CountryRepository;
        private readonly IMapper _Mapper;

        public CountryController(ICountryRepository CountryRepository, IMapper Mapper)
        {
            _CountryRepository = CountryRepository;
            _Mapper = Mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountry()
        {
            var Country = _Mapper.Map<List<CountryDto>>(_CountryRepository.GetCountry());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Country);
        }
        [HttpGet("{CountryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int CountryId)
        {
            if (_CountryRepository.CountryExists(CountryId))
                return NotFound();

            var Country = _CountryRepository.GetCountry(CountryId);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(Country);
        }
        [HttpGet("/OwnerNames/{OwnerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryofAnOwner(int OwnerId)
        {
            var Country = _Mapper.Map<CountryDto>(_CountryRepository.GetCountryByOwner(OwnerId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Country);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto CountryCreate)
        {
            if (CountryCreate == null)
                return BadRequest(ModelState);
            var Country = _CountryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == CountryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Country!= null)
            {
                ModelState.AddModelError("","Country already exist");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var CountryMap = _Mapper.Map<Country>(CountryCreate);
            if (!_CountryRepository.CreateCountry(CountryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);



            }
            return Ok("successfully created");
        }
        [HttpPut("{CountryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int CountryId, [FromBody] CountryDto UpdateCountry)
        {
            if (UpdateCountry == null)
                return BadRequest(ModelState);

            if (CountryId != UpdateCountry.Id)
                return BadRequest(ModelState);

            if (!_CountryRepository.CountryExists(CountryId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var CountryMap = _Mapper.Map<Country>(UpdateCountry);
            if (!_CountryRepository.UpdateCountry(CountryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }
        [HttpDelete("{CountryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_CountryRepository.CountryExists(countryId))
            {
                return NotFound();

            }
            var CountryToDelete = _CountryRepository.GetCountry(countryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_CountryRepository.DeleteCountry(CountryToDelete)) 
            {
                ModelState.AddModelError("", "something went wrong deleting category");

            }
            return NoContent();
        }

    }
}