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
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountry()
        {
            var Country = _mapper.Map<List<Countrydto>>(_countryRepository.GetCountry());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Country);
        }
        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (_countryRepository.CountryExists(countryId))
                return NotFound();

            var Country = _countryRepository.GetCountry(countryId);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(Country);
        }
        [HttpGet("/OwnerNames/{OwnerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200,Type =typeof(Country))]
        public IActionResult GetCountryofAnOwner(int OwnerId)
        {
            var Country =_mapper.Map<Countrydto>(_countryRepository.GetCountryByOwner(OwnerId));
            if (!ModelState.IsValid)
                    return BadRequest(ModelState);
            return Ok(Country);

        }
    }
}
