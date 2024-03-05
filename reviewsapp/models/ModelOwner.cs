namespace reviewsapp.models
{
    public class ModelOwner
    {
        public int modelId { get; set; }
        public int OwnerId { get; set; }

        // Navigation properties
        public Model Model { get; set; } // Navigation property for the model
        public OwnerName Owner { get; set; } // Navigation property for the owner
        public ICollection<Review> Reviews { get; set; }
    }
}
