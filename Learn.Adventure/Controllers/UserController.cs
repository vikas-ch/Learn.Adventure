using System.Collections.Generic;
using System.Linq;
using Learn.Adventure.Models.DTO;
using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository;
using Learn.Adventure.Repository.Abstractions;
using Learn.Adventure.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Learn.Adventure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get list of all users
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Get()
        {
            return Ok(_userService.Get());
        }

        /// <summary>
        /// Get a user based on Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User based on Id</returns>
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            
            return Ok(_userService.Get(id));
        }

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <param name="user"></param>
        /// <remarks>
        /// Object Definition:
        /// 
        ///     {
        ///         id (blank for adventure creation): Id of the user
        ///         firstName: First name of the user
        ///         lastName: Last name of the user
        ///         Email: Email of the user
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] UserDTO user)
        {
            _userService.Create(user);
            return NoContent();
        }

        
        /// <summary>
        /// Update user details
        /// </summary>
        /// <param name="user"></param>
        /// <remarks>
        /// Object Definition:
        /// 
        ///     {
        ///         id: Id of the user
        ///         firstName: First name of the user
        ///         lastName: Last name of the user
        ///         Email: Email of the user
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Put([FromBody] UserDTO user)
        {
            if (string.IsNullOrEmpty(user.Id))
                return NotFound();

            _userService.Update(user);

            return NoContent();
        }

        /// <summary>
        /// Delete a specific user
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            _userService.Delete(id);

            return NoContent();
        }
    }
}