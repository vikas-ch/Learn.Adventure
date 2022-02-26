using System;
using System.Linq;
using System.Net.Mime;
using Learn.Adventure.Models.Abstractions;
using Learn.Adventure.Models.DTO;
using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Learn.Adventure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserJourneyController : ControllerBase
    {
        private readonly IRepository<UserJourney> _userJourneyRepository;

        public UserJourneyController(IRepository<UserJourney> userJourneyRepository)
        {
            _userJourneyRepository = userJourneyRepository;
        }

        [HttpGet("{userId}/{adventureId}")]
        public ActionResult<UserJourneyDTO> Get(string userId, string adventureId) 
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(adventureId))
                return NotFound();

            var userJourney = _userJourneyRepository.
                FindOne(doc => doc.UserId == ObjectId.Parse(userId) 
                               && doc.AdventureId == ObjectId.Parse(adventureId));

            if (userJourney is null)
                return NoContent();
            
            var userJourneyDTO = new UserJourneyDTO()
            {
                AdventureId = userJourney.AdventureId.ToString(),
                UserId = userJourney.UserId.ToString(),
                SelectedOptions = userJourney.SelectedOptions
            };
            
            return Ok(userJourneyDTO);
        }

        [HttpPost]
        public ActionResult Post([FromBody] UserJourneyDTO userJourneyDTO)
        {
            if (userJourneyDTO is null)
                return BadRequest("Invalid Request");

            if (string.IsNullOrEmpty(userJourneyDTO.UserId) || string.IsNullOrEmpty(userJourneyDTO.AdventureId))
                return BadRequest("User Id and Adventure Id are mandatory");

            var userJourney = _userJourneyRepository.FindOne(journey =>
                journey.UserId == ObjectId.Parse(userJourneyDTO.UserId)
                && journey.AdventureId == ObjectId.Parse(userJourneyDTO.AdventureId));

            if (userJourney is not null)
                return BadRequest("This adventure is already underway for this user");

            userJourney = new UserJourney()
            {
                UserId = ObjectId.Parse(userJourneyDTO.UserId),
                AdventureId = ObjectId.Parse(userJourneyDTO.AdventureId),
                SelectedOptions = userJourneyDTO.SelectedOptions
            };

            _userJourneyRepository.InsertOne(userJourney);

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody] UserJourneyDTO userJourneyDTO)
        {
            if (userJourneyDTO is null)
                return BadRequest("Invalid Request");

            if (string.IsNullOrEmpty(userJourneyDTO.UserId) || string.IsNullOrEmpty(userJourneyDTO.AdventureId))
                return BadRequest("User Id and Adventure Id are mandatory");
            
            var userJourney = _userJourneyRepository.FindOne(journey =>
                journey.UserId == ObjectId.Parse(userJourneyDTO.UserId)
                && journey.AdventureId == ObjectId.Parse(userJourneyDTO.AdventureId));

            if (userJourney is null)
                return NotFound();

            userJourney.SelectedOptions = userJourneyDTO.SelectedOptions;
            _userJourneyRepository.ReplaceOne(userJourney);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid request");

            var userJourney = _userJourneyRepository.FindById(id);

            if (userJourney is null)
                return NotFound();
            
            _userJourneyRepository.DeleteById(id);
            return Ok();
        }
        
    }
}