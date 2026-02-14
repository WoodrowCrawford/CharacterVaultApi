namespace CharacterVaultApi.Models;

public class CharacterCreateRequest
{
    public string Name { get; set; } = "";
    public string Class { get; set; } = "";
    public int Level { get; set; }
}