using System.Collections.Generic;
using Learn.Adventure.Models.DTO;

namespace Learn.Adventure.Services.Abstractions
{
    public interface IUserService
    {
        IEnumerable<UserDTO> Get();
        UserDTO Get(string id);
        void Create(UserDTO document);
        void Update(UserDTO document);
        void Delete(string id);   
    }
}