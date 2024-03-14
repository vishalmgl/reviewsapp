using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using reviewsapp.dto;
using reviewsapp.Interfaces;
using reviewsapp.models;
using reviewsapp.Repository;

namespace reviewsapp.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository,IModelRepository modelRepository,IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _modelRepository = modelRepository;
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }
        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewRepository>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewsDto>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }
        [HttpGet("{ReviewId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewRepository>))]
        [ProducesResponseType(400)]
        public IActionResult GetModel(int ReviewId)
        {
            if (_reviewRepository.ReviewExists(ReviewId))
                return NotFound();

            var review = _reviewRepository.GetReview(ReviewId);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(review);
        }
        [HttpGet("Model/{modId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewRepository>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsForAModel(int modId)
        {
            var reviews = _mapper.Map<List<ReviewsDto>>(_reviewRepository.GetReviewsofAModel(modId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviews);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview( [FromQuery] int reviewerId,[FromQuery]int modId,[FromBody] ReviewsDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);
            var reviews = _reviewRepository.GetReviews()
                .Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reviews != null)
            {
                ModelState.AddModelError("", "Owner already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var reviewsMap = _mapper.Map<Review>(reviewCreate);
            reviewsMap.Model=_modelRepository.GetModel(modId);
            reviewsMap.Reviewer=_reviewerRepository.GetReviewers(reviewerId);
            if (!_reviewRepository.CreateReview(reviewsMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);



            }
            return Ok("successfully created");
        }
        [HttpPut("{reviewsId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewsId, [FromBody] ReviewsDto Updatereview)
        {
            if (Updatereview == null)
                return BadRequest(ModelState);

            if (reviewsId != Updatereview.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewsId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var reviewsMap = _mapper.Map<Review>(UpdateReview);
            if (!_reviewRepository.UpdateReview(reviewsMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }
        [HttpDelete("{reviewsId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewsId)
        {
            if (!_reviewRepository.ReviewExists(reviewsId))
            {
                return NotFound();

            }
            var reviewToDelete = _reviewRepository.GetReview(reviewsId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting category");

            }
            return NoContent();
        }
    }
}
