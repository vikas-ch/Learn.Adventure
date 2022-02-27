using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Learn.Adventure.Models.Abstractions;
using Learn.Adventure.Models.DTO;
using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository;
using Learn.Adventure.Repository.Abstractions;
using Learn.Adventure.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Learn.Adventure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserJourneyController : ControllerBase
    {
        private readonly IUserJourneyService _userJourneyService;

        public UserJourneyController(IUserJourneyService userJourneyService)
        {
            _userJourneyService = userJourneyService;
        }

        /// <summary>
        /// Get list of all journeys/adventures of the user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>List of all journeys/adventures of the user</returns>
        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<UserJourneyDTO>> Get(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return NotFound();

            return Ok(_userJourneyService.Get(userId));
        }

        /// <summary>
        /// Get details of a specific journey/adventure for a user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="adventureId">Adventure Id</param>
        /// <returns>Details of a specific journey/adventure for a user</returns>
        [HttpGet("{userId}/{adventureId}")]
        public ActionResult<UserJourneyDTO> Get(string userId, string adventureId) 
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(adventureId))
                return NotFound();
            
            return Ok(_userJourneyService.Get(userId, adventureId));
        }

        /// <summary>
        /// Create a new user journey
        /// </summary>
        /// <param name="userJourneyDTO"></param>
        /// <remarks>
        /// Object Definition:
        /// 
        ///     {
        ///         userId: User Id
        ///         adventureId: Adventure Id
        ///         selectedOptions: List of Options/Text choices (ids) selected by the user for the adventure
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] UserJourneyDTO userJourneyDTO)
        {
            var result = _userJourneyService.Create(userJourneyDTO);
            
            if (!result.Success)
            {
                ModelState.AddModelError("UserJourney", result.Message);
                return ValidationProblem(ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Update a user journey
        /// </summary>
        /// <param name="userJourneyDTO"></param>
        /// <remarks>
        /// Object Definition:
        /// 
        ///     {
        ///         userId: User Id
        ///         adventureId: Adventure Id
        ///         selectedOptions: List of Options/Text choices (ids) selected by the user for the adventure
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Put([FromBody] UserJourneyDTO userJourneyDTO)
        {
            var result = _userJourneyService.Update(userJourneyDTO);

            if (!result.Success)
            {
                ModelState.AddModelError("UserJourney", result.Message);
                return ValidationProblem(ModelState);
            }

            return Ok();
        }

        /// <summary>
        /// Delete a user journey
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="adventureId">Adventure Id</param>
        /// <returns></returns>
        [HttpDelete("{userId}/{adventureId}")]
        public ActionResult Delete(string userId, string adventureId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(adventureId))
                return NotFound();

            _userJourneyService.Delete(userId, adventureId);
            return Ok();
        }
        
    }
}