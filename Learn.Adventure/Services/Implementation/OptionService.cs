using System;
using System.Collections.Generic;
using System.Linq;
using Learn.Adventure.Models.DTO;
using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository.Abstractions;
using Learn.Adventure.Services.Abstractions;
using MongoDB.Bson;

namespace Learn.Adventure.Services.Implementation
{
    public class OptionService : IOptionService
    {
        private readonly IRepository<Option> _optionRepository;

        public OptionService(IRepository<Option> optionRepository)
        {
            _optionRepository = optionRepository;
        }
        public IEnumerable<OptionDTO> Get(string adventureId)
        {
            if (ObjectId.TryParse(adventureId, out ObjectId aObjectId))
            {
                var options = _optionRepository
                    .FilterBy(options => options.AdventureId == aObjectId);

                return options.Select(option => new OptionDTO()
                {
                    Id = option.Id.ToString(),
                    AdventureId = option.AdventureId.ToString(),
                    ParentId = option.ParentId.ToString(),
                    Text = option.Text
                });
            }

            return null;
        }

        public OptionDTO Get(string adventureId, string id)
        {
            if (ObjectId.TryParse(adventureId, out ObjectId aObjectId) && ObjectId.TryParse(id, out ObjectId oObjectId))
            {
                var option = _optionRepository
                    .FindOne(options => options.AdventureId == aObjectId
                                        && options.Id == oObjectId);

                if (option is not null)
                    return new OptionDTO()
                    {
                        Id = option.Id.ToString(),
                        AdventureId = option.AdventureId.ToString(),
                        ParentId = option.ParentId.ToString(),
                        Text = option.Text
                    };
            }

            return new OptionDTO();
        }

        public void Create(OptionDTO document)
        { 
            if(ObjectId.TryParse(document.AdventureId, out ObjectId aObjectId))
            {
                ObjectId pObjectId;
                if(string.IsNullOrEmpty(document.ParentId))
                    pObjectId = ObjectId.Empty;

                if (!ObjectId.TryParse(document.ParentId, out pObjectId))
                    return;
                
                var optionEntity = new Option()
                {
                    AdventureId = aObjectId,
                    ParentId = pObjectId,
                    Text = document.Text
                };

                _optionRepository.InsertOne(optionEntity);
            }
        }

        public void Update(OptionDTO document)
        {
            if(ObjectId.TryParse(document.Id, out ObjectId objectId))
            {
                var optionEntity = new Option()
                {
                    Id = objectId,
                    AdventureId = ObjectId.Parse(document.AdventureId),
                    ParentId = string.IsNullOrEmpty(document.ParentId)
                        ? ObjectId.Empty
                        : ObjectId.Parse(document.ParentId),
                    Text = document.Text
                };

                _optionRepository.ReplaceOne(optionEntity);
            }
        }

        public void Delete(string adventureId, string id)
        {
            if(ObjectId.TryParse(adventureId, out ObjectId aObjectId) && ObjectId.TryParse(id, out ObjectId oObjectId))
            {
                _optionRepository.DeleteOne(option => option.AdventureId == aObjectId
                                                      && option.Id == oObjectId);
            }
            
        }
    }
}