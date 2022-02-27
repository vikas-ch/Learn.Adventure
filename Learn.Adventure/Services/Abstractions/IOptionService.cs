using System.Collections.Generic;
using Learn.Adventure.Models.DTO;

namespace Learn.Adventure.Services.Abstractions
{
    public interface IOptionService
    {
        IEnumerable<OptionDTO> Get(string adventureId);
        OptionDTO Get(string adventureId, string id);
        void Create(OptionDTO document);
        void Update(OptionDTO document);
        void Delete(string adventureId, string id);    
    }
}