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
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }
        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerRepository>))]
        public IActionResult GetReviewers()
        {
            var reviewer = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewer);
        }
        [HttpGet("{reviewerid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerRepository>))]
        [ProducesResponseType(400)]
        public IActionResult GetModel(int reviewerid)
        {
            if (_reviewerRepository.reviewerExixts(reviewerid))
                return NotFound();

            var reviewer = _reviewerRepository.GetReviewers(reviewerid);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(reviewer);
        }
        [HttpGet("{reviewerid}/reviews")]
        public IActionResult GetReviewsByAReviewer(int reviewerid)
        {
            if (_reviewerRepository.reviewerExixts(reviewerid))
                return NotFound();

            var reviews = _mapper.Map<List<ReviewsDto>>(_reviewerRepository.GetReviewsByReviewer(reviewerid));
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(reviews);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null)
                return BadRequest(ModelState);
            var reviewer = _reviewerRepository.GetReviewers()
                .Where(c => c.LastName.Trim().ToUpper() == reviewerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reviewer != null)
            {
                ModelState.AddModelError("", "Owner already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);



            }
            return Ok("successfully created");
        }
        [HttpPut("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerDto Updatereviewer)
        {
            if (Updatereviewer == null)
                return BadRequest(ModelState);

            if (reviewerId != Updatereviewer.Id)
                return BadRequest(ModelState);

            if (!_reviewerRepository.reviewerExixts(reviewerId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var reviewerMap = _mapper.Map<Reviewer>(UpdateReviewer);
            if (!_reviewerRepository.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }
        [HttpDelete("{reviwerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Deletereviewer(int reviwerId)
        {
            if (!_reviewerRepository.reviewerExixts(reviwerId))
            {
                return NotFound();

            }
            var reviewerToDelete = _reviewerRepository.GetReviewers(reviwerId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_reviewerRepository.DeleteReviewer(reviewerToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting category");

            }
            return NoContent();
        }
    }
}
