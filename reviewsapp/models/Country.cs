namespace reviewsapp.models
{
    public class Country
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public  ICollection<OwnerName>OwnerNames { get; set; }
    }
}
