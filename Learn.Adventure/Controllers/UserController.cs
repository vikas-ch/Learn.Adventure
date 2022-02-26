using System.Collections.Generic;
using System.Linq;
using Learn.Adventure.Models.DTO;
using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Learn.Adventure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Get()
        {
            var users = _userRepository.FilterBy(user => true)
                .Select(user=> new UserDTO()
                {
                    Id = user.Id.ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                }).ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            
            var user = _userRepository.FindById(id);
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Post([FromBody] UserDTO user)
        {
            if (user is null)
                return BadRequest("Invalid Request");

            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.Email))
                return BadRequest("First name and email are mandatory");

            var userEntity = new User()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            
            _userRepository.InsertOne(userEntity);

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody] UserDTO user)
        {
            if (user is null)
                return BadRequest("Invalid Request");

            if (string.IsNullOrEmpty(user.Id))
                return NotFound();
            
            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.Email))
                return BadRequest("First name and email are mandatory");

            var userEntity = new User()
            {
                Id = ObjectId.Parse(user.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            _userRepository.ReplaceOne(userEntity);

            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            
            _userRepository.DeleteById(id);

            return Ok();
        }
    }
}