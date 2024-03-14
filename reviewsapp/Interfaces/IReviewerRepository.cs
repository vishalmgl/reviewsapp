using reviewsapp.models;

namespace reviewsapp.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewers(int reviewerid);
        ICollection<Review> GetReviewsByReviewer(int reviewerid);
        bool ReviewerExixts(int reviewerid);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();
    }
}
