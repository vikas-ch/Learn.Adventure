using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Learn.Adventure.Models.DTO;
using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository.Abstractions;
using Learn.Adventure.Services.Abstractions;
using MongoDB.Bson;

namespace Learn.Adventure.Services.Implementation
{
    public class UserJourneyService : IUserJourneyService
    {
        private readonly IRepository<UserJourney> _userJourneyRepository;

        public UserJourneyService(IRepository<UserJourney> userJourneyRepository)
        {
            _userJourneyRepository = userJourneyRepository;
        }
        public IEnumerable<UserJourneyDTO> Get(string userId)
        {
            if(ObjectId.TryParse(userId, out ObjectId objectId))
            {
                var userJourneys = _userJourneyRepository
                    .FilterBy(doc => doc.UserId == objectId);

                if (!userJourneys.Any())
                    return null;

                return userJourneys.Select(journey => new UserJourneyDTO()
                {
                    AdventureId = journey.AdventureId.ToString(),
                    UserId = journey.UserId.ToString(),
                    SelectedOptions = journey.SelectedOptions
                });
            }

            return null;
        }

        public UserJourneyDTO Get(string userId, string adventureId)
        {
            if(ObjectId.TryParse(userId, out ObjectId uObjectId) 
               && ObjectId.TryParse(adventureId, out ObjectId aObjectId))
            {
                var userJourney = _userJourneyRepository
                    .FindOne(doc => doc.UserId == uObjectId
                                    && doc.AdventureId == aObjectId);

                if (userJourney is null)
                    return new UserJourneyDTO()
                    {
                        AdventureId = adventureId,
                        UserId = userId,
                        SelectedOptions = new List<string>()
                    };

                return new UserJourneyDTO()
                {
                    AdventureId = userJourney.AdventureId.ToString(),
                    UserId = userJourney.UserId.ToString(),
                    SelectedOptions = userJourney.SelectedOptions
                };
            }

            return new UserJourneyDTO()
            {
                AdventureId = adventureId,
                UserId = userId,
                SelectedOptions = new List<string>()
            };
        }

        public MessageDTO Create(UserJourneyDTO document)
        {
            if(ObjectId.TryParse(document.UserId, out ObjectId uObjectId) 
               && ObjectId.TryParse(document.AdventureId, out ObjectId aObjectId))

            {
                var userJourney = _userJourneyRepository.FindOne(journey =>
                    journey.UserId == uObjectId
                    && journey.AdventureId == aObjectId);

                if (userJourney is not null)
                    return (new MessageDTO()
                    {
                        Success = false,
                        Message = "A journey is already underway for this user"
                    });

                userJourney = new UserJourney()
                {
                    UserId = ObjectId.Parse(document.UserId),
                    AdventureId = ObjectId.Parse(document.AdventureId),
                    SelectedOptions = document.SelectedOptions
                };

                _userJourneyRepository.InsertOne(userJourney);
                return new MessageDTO()
                {
                    Success = true,
                    Message = string.Empty
                };
            }

            return new MessageDTO()
            {
                Success = false,
                Message = "Invalid request"
            };
        }

        public MessageDTO Update(UserJourneyDTO document)
        {
            var userJourney = _userJourneyRepository.FindOne(journey =>
                journey.UserId == ObjectId.Parse(document.UserId)
                && journey.AdventureId == ObjectId.Parse(document.AdventureId));

            if (userJourney is null)
                return new MessageDTO()
                {
                    Success = false,
                    Message = "User hasn't started this journey yet!"
                };

            userJourney.SelectedOptions = document.SelectedOptions;
            _userJourneyRepository.ReplaceOne(userJourney);
            return new MessageDTO()
            {
                Success = true,
                Message = string.Empty
            };
        }

        public void Delete(string userId, string adventureId)
        {
            if (ObjectId.TryParse(userId, out ObjectId uObjectId)
                && ObjectId.TryParse(adventureId, out ObjectId aObjectId))
                _userJourneyRepository.DeleteOne(journey => journey.UserId == uObjectId
                                                            && journey.AdventureId == aObjectId);
        }
    }
}