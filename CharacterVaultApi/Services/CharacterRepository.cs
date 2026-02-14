using CharacterVaultApi.Models;

namespace CharacterVaultApi.Services;

public interface ICharacterRepository
{
    List<Character> GetAll();
    Character? GetById(int id);
    Character Add(CharacterCreateRequest request);
    Character? Update(int id, CharacterUpdateRequest request);
    bool Delete(int id);
}