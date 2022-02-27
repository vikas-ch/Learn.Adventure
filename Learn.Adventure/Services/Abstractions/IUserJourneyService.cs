using System;
using System.Collections.Generic;
using Learn.Adventure.Models.DTO;

namespace Learn.Adventure.Services.Abstractions
{
    public interface IUserJourneyService
    {
        IEnumerable<UserJourneyDTO> Get(string userId);
        UserJourneyDTO Get(string userId, string adventureId);
        
        MessageDTO Create(UserJourneyDTO document);
        MessageDTO Update(UserJourneyDTO document);
        void Delete(string userId, string adventureId);
    }
}