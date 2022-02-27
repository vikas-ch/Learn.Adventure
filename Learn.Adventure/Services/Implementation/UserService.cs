using System.Collections.Generic;
using System.Linq;
using Learn.Adventure.Models.DTO;
using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository.Abstractions;
using Learn.Adventure.Services.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Learn.Adventure.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        
        public IEnumerable<UserDTO> Get()
        {
            return _userRepository.FilterBy(user => true)
                .Select(user=> new UserDTO()
                {
                    Id = user.Id.ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                }).ToList();
        }

        public UserDTO Get(string id)
        {
            if(ObjectId.TryParse(id, out _))
            {
                var user = _userRepository.FindById(id);

                if (user is not null)
                    return new UserDTO()
                    {
                        Id = user.Id.ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    };
            }
            return new UserDTO();
        }

        public void Create(UserDTO document)
        {
            var userEntity = new User()
            {
                Email = document.Email,
                FirstName = document.FirstName,
                LastName = document.LastName
            };
            
            _userRepository.InsertOne(userEntity);
        }

        public void Update(UserDTO document)
        {
            if(ObjectId.TryParse(document.Id, out ObjectId objectId))
            {
                var userEntity = new User()
                {
                    Id = objectId,
                    FirstName = document.FirstName,
                    LastName = document.LastName,
                    Email = document.Email
                };

                _userRepository.ReplaceOne(userEntity);
            }
        }

        public void Delete(string id)
        {
            if(ObjectId.TryParse(id, out _))
                _userRepository.DeleteById(id);
        }
    }
}