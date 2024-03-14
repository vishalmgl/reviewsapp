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
        private readonly IReviewRepository _ReviewRepository;
        private readonly IModelRepository _ModelRepository;
        private readonly IReviewerRepository _ReviewerRepository;
        private readonly IMapper _Mapper;

        public ReviewController(IReviewRepository ReviewRepository,IModelRepository ModelRepository,IReviewerRepository ReviewerRepository, IMapper Mapper)
        {
            _ReviewRepository = ReviewRepository;
            _ModelRepository = ModelRepository;
            _ReviewerRepository = ReviewerRepository;
            _Mapper = Mapper;
        }
        [HttpGet]

        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewRepository>))]
        public IActionResult GetReviews()
        {
            var Reviews = _Mapper.Map<List<ReviewsDto>>(_ReviewRepository.GetReviews());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Reviews);
        }
        [HttpGet("{ReviewId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewRepository>))]
        [ProducesResponseType(400)]
        public IActionResult GetModel(int ReviewId)
        {
            if (_ReviewRepository.ReviewExists(ReviewId))
                return NotFound();

            var Review = _ReviewRepository.GetReview(ReviewId);
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            return Ok(Review);
        }
        [HttpGet("Model/{modId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewRepository>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsForAModel(int modId)
        {
            var Reviews = _Mapper.Map<List<ReviewsDto>>(_ReviewRepository.GetReviewsofAModel(modId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Reviews);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview( [FromQuery] int ReviewerId,[FromQuery]int modId,[FromBody] ReviewsDto ReviewCreate)
        {
            if (ReviewCreate == null)
                return BadRequest(ModelState);
            var Reviews = _ReviewRepository.GetReviews()
                .Where(c => c.Title.Trim().ToUpper() == ReviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (Reviews != null)
            {
                ModelState.AddModelError("", "Owner already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ReviewsMap = _Mapper.Map<Review>(ReviewCreate);
            ReviewsMap.Model=_ModelRepository.GetModel(modId);
            ReviewsMap.Reviewer=_ReviewerRepository.GetReviewers(ReviewerId);
            if (!_ReviewRepository.CreateReview(ReviewsMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);



            }
            return Ok("successfully created");
        }
        [HttpPut("{ReviewsId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int ReviewsId, [FromBody] ReviewsDto UpdateReview)
        {
            if (UpdateReview == null)
                return BadRequest(ModelState);

            if (ReviewsId != UpdateReview.Id)
                return BadRequest(ModelState);

            if (!_ReviewRepository.ReviewExists(ReviewsId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var ReviewsMap = _Mapper.Map<Review>(UpdateReview);
            if (!_ReviewRepository.UpdateReview(ReviewsMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }
        [HttpDelete("{ReviewsId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int ReviewsId)
        {
            if (!_ReviewRepository.ReviewExists(ReviewsId))
            {
                return NotFound();

            }
            var ReviewToDelete = _ReviewRepository.GetReview(ReviewsId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_ReviewRepository.DeleteReview(ReviewToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting category");

            }
            return NoContent();
        }
    }
}
