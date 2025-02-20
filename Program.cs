using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using WebAPI_test_1.Repositories;
using WebAPI_test_1.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Adding singleton
builder.Services.AddSingleton<IItemsRepository, MongoDBItemsRepository>();    // I added this


builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    // GetSection will get in "appsettings" a section of the json.
    // Get<MongoDBSettings> will transform the json in a MongoDBSettings instance
    var settings = builder.Configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
    //Console.WriteLine(settings.ConnectionString);
    //Console.ReadLine();
    return new MongoClient(settings.ConnectionString);
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

app.Run();
