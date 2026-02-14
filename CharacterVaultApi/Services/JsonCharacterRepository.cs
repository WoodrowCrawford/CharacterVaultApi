using System.Text.Json;
using CharacterVaultApi.Models;

namespace CharacterVaultApi.Services;

public class JsonCharacterRepository : ICharacterRepository
{
    private readonly string _filePath;
    private readonly object _lock = new();

    private readonly JsonSerializerOptions _readOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly JsonSerializerOptions _writeOptions = new()
    {
        WriteIndented = true
    };

   public JsonCharacterRepository(IConfiguration config, IHostEnvironment env)
    {
    var relativePath = config["CharacterDataPath"] ?? "Data/characters.json";

    var basePath = env.IsDevelopment()
        ? env.ContentRootPath          // project folder (dev)
        : AppContext.BaseDirectory;    // executable folder (published)

    _filePath = Path.Combine(basePath, relativePath);

    var dir = Path.GetDirectoryName(_filePath);
    if (!string.IsNullOrWhiteSpace(dir))
        Directory.CreateDirectory(dir);

    if (!File.Exists(_filePath))
        File.WriteAllText(_filePath, "[]");
    }
    
    public List<Character> GetAll()
    {
        lock (_lock) { return LoadAllUnsafe(); }
    }

    public Character? GetById(int id)
    {
        lock (_lock) { return LoadAllUnsafe().FirstOrDefault(c => c.Id == id); }
    }

    public Character Add(CharacterCreateRequest request)
    {
        lock (_lock)
        {
            var characters = LoadAllUnsafe();
            int newId = characters.Count == 0 ? 1 : characters.Max(c => c.Id) + 1;

            var newCharacter = new Character
            {
                Id = newId,
                Name = request.Name.Trim(),
                Class = request.Class.Trim(),
                Level = request.Level
            };

            characters.Add(newCharacter);
            SaveAllUnsafe(characters);
            return newCharacter;
        }
    }

    public Character? Update(int id, CharacterUpdateRequest request)
    {
        lock (_lock)
        {
            var characters = LoadAllUnsafe();
            var existing = characters.FirstOrDefault(c => c.Id == id);
            if (existing is null) return null;

            existing.Name = request.Name.Trim();
            existing.Class = request.Class.Trim();
            existing.Level = request.Level;

            SaveAllUnsafe(characters);
            return existing;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var characters = LoadAllUnsafe();
            var existing = characters.FirstOrDefault(c => c.Id == id);
            if (existing is null) return false;

            characters.Remove(existing);
            SaveAllUnsafe(characters);
            return true;
        }
    }

    private List<Character> LoadAllUnsafe()
    {
        try
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Character>>(json, _readOptions) ?? new List<Character>();
        }
        catch
        {
            return new List<Character>();
        }
    }

    private void SaveAllUnsafe(List<Character> characters)
    {
        var json = JsonSerializer.Serialize(characters, _writeOptions);
        File.WriteAllText(_filePath, json);
    }
}