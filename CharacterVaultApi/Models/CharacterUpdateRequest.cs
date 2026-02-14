namespace CharacterVaultApi.Models;

public class CharacterUpdateRequest
{
    public string Name { get; set; } = "";
    public string Class { get; set; } = "";
    public int Level { get; set; }
}