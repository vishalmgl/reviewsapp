﻿
using reviewsapp.Data;
using reviewsapp.models;

namespace reviewsapp
{
   
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.ModelOwners.Any())
            {
                var modelowners = new List<ModelOwner>()
                {
                    new ModelOwner()
                    {
                        Model = new Model()
                        {
                            Name = "Pikachu",
                            WashDate = new DateTime(1903,1,1),
                            ModelCategories = new List<ModelCategory>()
                            {
                                new ModelCategory { Category = new Category() { Name = "Electric"}}
                            },
                            reviews = new List<Review>()
                            {
                                new Review { Title="Pikachu",text = "Pickahu is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Pikachu", text = "Pickachu is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Pikachu",text = "Pickchu, pickachu, pikachu", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new OwnerName ()
                        {
                            Name = "Jack London",
                            Gym = "Brocks Gym",
                            Country = new Country()
                            {
                                Name = "Kanto"
                            }
                        }
                    },
                    new ModelOwner()
                    {
                        Model = new Model()
                        {
                            Name = "Squirtle",
                            WashDate = new DateTime(1903,1,1),
                            ModelCategories = new List<ModelCategory>()
                            {
                                new ModelCategory { Category = new Category() { Name = "Water"}}
                            },
                            reviews = new List<Review>()
                            {
                                new Review { Title= "Squirtle", text = "squirtle is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title= "Squirtle",text = "Squirtle is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title= "Squirtle", text = "squirtle, squirtle, squirtle", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new OwnerName()
                        {
                            Name = "Harry Potter",
                            Gym = "Mistys Gym",
                            Country = new Country()
                            {
                                Name = "Saffron City"
                            }
                        }
                    },
                    new ModelOwner()
                    {
                            Model = new Model()
                        {
                            Name = "Venasuar",
                            WashDate = new DateTime(1903,1,1),
                            ModelCategories = new List<ModelCategory>()
                            {
                                new ModelCategory { Category = new Category() { Name = "Leaf"}}
                            },
                            reviews = new List<Review>()
                            {
                                new Review { Title="Veasaur",text = "Venasuar is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Veasaur",text = "Venasuar is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Veasaur",text = "Venasuar, Venasuar, Venasuar", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new OwnerName()
                        {
                           
                            Name = "Ash Ketchum",
                            Gym = "Ashs Gym",
                            Country = new Country()
                            {
                                Name = "Millet Town"
                            }
                        }
                    }
                };
                dataContext.ModelOwners.AddRange(ModelOwner);
                dataContext.SaveChanges();
            }
        }
    }
}