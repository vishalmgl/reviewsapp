namespace reviewsapp.models
{
    public class Review
    {
        public int Id {  get; set; }    
        public string Title { get; set; }
        public string text { get; set; }
        public int Rating { get; set; } 
        public Reviewer Reviewer { get; set; }  //one to one relation

        public Model Model { get; set; }

    }
}
