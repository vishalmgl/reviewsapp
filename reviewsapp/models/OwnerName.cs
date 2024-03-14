using System.Collections.Generic;

namespace reviewsapp.models
{
    public class OwnerName
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }
        public Country Country { get; set; }
        public ICollection<ModelOwner> ModelOwners { get; set; }
        
    }
}
