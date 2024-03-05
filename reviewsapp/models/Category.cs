namespace reviewsapp.models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelCategory> ModelCategories { get; set; }
    }
}