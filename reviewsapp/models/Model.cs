namespace reviewsapp.models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime WashDate { get; set; }  
        public ICollection<OwnerName> OwnerNames { get; set; }
        public ICollection<ModelCategory> ModelCategories { get; set; }
        public ICollection<Review> reviews { get; set; }
        public ICollection<ModelOwner> ModelOwners { get; set; }
    }
}
