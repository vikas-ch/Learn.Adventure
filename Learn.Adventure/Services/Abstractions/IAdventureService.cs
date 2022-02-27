using System.Collections.Generic;
using Learn.Adventure.Models.DTO;

namespace Learn.Adventure.Services.Abstractions
{
    public interface IAdventureService
    {
        IEnumerable<AdventureDTO> Get();
        AdventureDTO Get(string id);
        void Create(AdventureDTO document);
        void Update(AdventureDTO document);
        void Delete(string id);
    }
}