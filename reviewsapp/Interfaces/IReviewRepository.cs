﻿using reviewsapp.models;

namespace reviewsapp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review>GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsofAModel(int modId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool Save();
        
    }
}
