using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using WebAPI_test_1.Repositories;
using WebAPI_test_1.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// Adding singleton
builder.Services.AddSingleton<IItemsRepository, MongoDBItemsRepository>();    // I added this



// GetSection will get in "appsettings" a section of the json.
// Get<MongoDBSettings> will transform the json in a MongoDBSettings instance
var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();


// Adds the possibility to check if the api and its dependecies are working
builder.Services.AddHealthChecks().
    AddMongoDb(name:"mongodb", timeout:TimeSpan.FromSeconds(3),
    tags: new[] { "ready" });


builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    //Console.WriteLine(settings.ConnectionString);
    //Console.ReadLine();
    return new MongoClient(mongoDbSettings.ConnectionString);
});



// Changing the serialization of not MongoDB-friendly classes
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("ready")
});     // Maps the health checks to a route
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = (_) => false
});     // Maps the health checks to a route

app.Run();
