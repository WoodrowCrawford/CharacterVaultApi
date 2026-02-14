using CharacterVaultApi.Endpoints;
using CharacterVaultApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddSingleton<ICharacterRepository, JsonCharacterRepository>();

var app = builder.Build();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Map endpoints
app.MapCharacterEndpoints();

app.Run();