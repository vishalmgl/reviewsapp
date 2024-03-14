using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using reviewsapp.dto;
using reviewsapp.Interfaces;
using reviewsapp.models;
using reviewsapp.Repository;
using System.Collections.Generic;

namespace reviewsapp.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _ReviewerRepository;
        private readonly IMapper _Mapper;

        public ReviewerController(IReviewerRepository ReviewerRepository, IMapper Mapper)
        {
            _ReviewerRepository = ReviewerRepository;
            _Mapper = Mapper;
        }
        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerRepository>))]
        public IActionResult GetReviewers()
        {
            var Reviewer = _Mapper.Map<List<ReviewerDto>>(_ReviewerRepository.GetReviewers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Reviewer);
        }
        [HttpGet("{ReviewerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerRepository>))]
        [ProducesResponseType(400)]
        public IActionResult GetModel(int ReviewerId)
        {
            if (_ReviewerRepository.ReviewerExixts(ReviewerId))
                return NotFound();

            var Reviewer = _ReviewerRepository.GetReviewers(ReviewerId);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(Reviewer);
        }
        [HttpGet("{ReviewerId}/Reviews")]
        public IActionResult GetReviewsByAReviewer(int ReviewerId)
        {
            if (_ReviewerRepository.ReviewerExixts(ReviewerId))
                return NotFound();

            var Reviews = _Mapper.Map<List<ReviewsDto>>(_ReviewerRepository.GetReviewsByReviewer(ReviewerId));
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(Reviews);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDto ReviewerCreate)
        {
            if (ReviewerCreate == null)
                return BadRequest(ModelState);
            var Reviewer = _ReviewerRepository.GetReviewers()
                .Where(c => c.LastName.Trim().ToUpper() == ReviewerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Reviewer != null)
            {
                ModelState.AddModelError("", "Owner already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ReviewerMap = _Mapper.Map<Reviewer>(ReviewerCreate);

            if (!_ReviewerRepository.CreateReviewer(ReviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);



            }
            return Ok("successfully created");
        }
        [HttpPut("{ReviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewer(int ReviewerId, [FromBody] ReviewerDto UpdateReviewer)
        {
            if (UpdateReviewer == null)
                return BadRequest(ModelState);

            if (ReviewerId != UpdateReviewer.Id)
                return BadRequest(ModelState);

            if (!_ReviewerRepository.ReviewerExixts(ReviewerId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var ReviewerMap = _Mapper.Map<Reviewer>(UpdateReviewer);
            if (!_ReviewerRepository.UpdateReviewer(ReviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }
        [HttpDelete("{ReviwerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Deletereviewer(int ReviwerId)
        {
            if (!_ReviewerRepository.ReviewerExixts(ReviwerId))
            {
                return NotFound();

            }
            var ReviewerToDelete = _ReviewerRepository.GetReviewers(ReviwerId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_ReviewerRepository.DeleteReviewer(ReviewerToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting category");

            }
            return NoContent();
        }
    }
}
