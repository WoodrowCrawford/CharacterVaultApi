using CharacterVaultApi.Models;
using CharacterVaultApi.Services;

namespace CharacterVaultApi.Endpoints;

public static class CharacterEndpoints
{
    public static void MapCharacterEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/characters");

        group.MapGet("/", (ICharacterRepository repo) => Results.Ok(repo.GetAll()));

        group.MapGet("/{id:int}", (int id, ICharacterRepository repo) =>
        {
            var character = repo.GetById(id);
            return character is null ? Results.NotFound() : Results.Ok(character);
        });

        group.MapPost("/", (CharacterCreateRequest request, ICharacterRepository repo) =>
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Class))
                return Results.BadRequest("Name and Class are required.");

            if (request.Level < 1)
                return Results.BadRequest("Level must be 1 or higher.");

            var created = repo.Add(request);
            return Results.Created($"/api/characters/{created.Id}", created);
        });

        group.MapPut("/{id:int}", (int id, CharacterUpdateRequest request, ICharacterRepository repo) =>
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Class))
                return Results.BadRequest("Name and Class are required.");

            if (request.Level < 1)
                return Results.BadRequest("Level must be 1 or higher.");

            var updated = repo.Update(id, request);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        });

        group.MapDelete("/{id:int}", (int id, ICharacterRepository repo) =>
        {
            bool deleted = repo.Delete(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}