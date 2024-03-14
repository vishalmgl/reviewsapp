using Microsoft.EntityFrameworkCore;
using reviewsapp;
using reviewsapp.Data;
using reviewsapp.Interfaces;
using reviewsapp.Repository;
using System.Reflection;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);//creates a new instance of a class

// Add services to the container.
//you register varoius services and dependencies
builder.Services.AddControllers();//This registers the controllers with the dependency injection container.
builder.Services.AddTransient<Seed>();//This line registers the Seed class with the service container as a transient service
builder.Services.AddControllers().AddJsonOptions(x =>
                            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IModelRepository, ModelRepository>();//This registers the IModelRepository interface with its corresponding implementation ModelRepository as a scoped service.
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IOwnerNamesRepository, OwnerNameRepository>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());    //You configure AutoMapper to handle object-to-object mapping 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//configure the context to sql server to provide data base
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));//data context is entity framework core
});//This configures the application's data context to use SQL Server as the database provider
var app = builder.Build();



void SeedData(IHost app)
{

    using (var scope = app.Services.CreateScope())//This SeedData method initializes the database with initial data by creating a scope
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();//swagger for documenting and testing api
    app.UseSwaggerUI();//visualization
}

app.UseHttpsRedirection();

app.UseAuthorization();

 app.MapControllers();          
SeedData(app);
app.Run();
