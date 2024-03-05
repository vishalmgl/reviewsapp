using reviewsapp.models;

public class ModelCategory
{
    public int modelId { get; set; }
    public int categoryId { get; set; } // Change the type to int

    // Navigation properties
    public Model Model { get; set; }
    public Category Category { get; set; }
}
