using System;
using System.Collections.Generic;
using System.Linq;
using Learn.Adventure.Models.Abstractions;
using Learn.Adventure.Models.DTO;
using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository.Abstractions;
using Learn.Adventure.Services.Abstractions;
using MongoDB.Bson;

namespace Learn.Adventure.Services.Implementation
{
   public class AdventureService : IAdventureService
   {
      private readonly IRepository<Models.Entities.Adventure> _adventureRepository;
      private readonly IRepository<Option> _optionRepository;

      public AdventureService(IRepository<Models.Entities.Adventure> adventureRepository,
         IRepository<Option> optionRepository)
      {
         _adventureRepository = adventureRepository;
         _optionRepository = optionRepository;
      }
      public IEnumerable<AdventureDTO> Get()
      {
         var adventures = _adventureRepository.FilterBy(
            filter => true).Select(adventure => new AdventureDTO()
         {
            Id = adventure.Id.ToString(),
            Name = adventure.AdventureName
         });

         return adventures;
      }

      public AdventureDTO Get(string id)
      {
         if(ObjectId.TryParse(id, out _))
         {
            var adventureEntity = _adventureRepository.FindById(id);

            if (adventureEntity is not null)
               return new AdventureDTO()
               {
                  Id = adventureEntity.Id.ToString(),
                  Name = adventureEntity.AdventureName
               };
         }
         return new AdventureDTO();
      }

      public void Create(AdventureDTO document)
      {
         var adventureEntity = new Models.Entities.Adventure()
         {
            AdventureName = document.Name
         };
            
         _adventureRepository.InsertOne(adventureEntity);
      }

      public void Update(AdventureDTO document)
      {
         if (ObjectId.TryParse(document.Id, out ObjectId objectId))
         {
            var adventureEntity = new Models.Entities.Adventure()
            {
               Id = objectId,
               AdventureName = document.Name
            };

            _adventureRepository.ReplaceOne(adventureEntity);
         }
         
      }

      public void Delete(string id)
      {
         if(ObjectId.TryParse(id, out ObjectId objectId))
         {
            _adventureRepository.DeleteById(id);
            _optionRepository.DeleteOne(doc => doc.AdventureId == objectId);
         }
      }
   }
}