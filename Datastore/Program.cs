using Datastore.Datastorage;
using Datastore.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IStorage, Storage>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
