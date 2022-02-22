using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnsClient.Internal;
using Learn.Adventure.Models.DTO;
using Learn.Adventure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Learn.Adventure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdventureController : ControllerBase
    {
        private readonly IRepository<Models.Entities.Adventure> _repository;
        private readonly ILogger<AdventureController> _logger;

        public AdventureController(IRepository<Models.Entities.Adventure> repository, ILogger<AdventureController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.DTO.AdventureDTO>> Get()
        {
            var adventures = _repository.FilterBy(
                filter => true).Select(adventure => new AdventureDTO()
            {
                Id = adventure.Id.ToString(),
                Name = adventure.AdventureName
            });
            
            
            return Ok(adventures);
        }

        [HttpGet("{id}", Name = "GetAdventure")]
        public ActionResult<Models.DTO.AdventureDTO> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var adventureEntity = _repository.FindById(id);

            return new AdventureDTO()
            {
                Id = adventureEntity.Id.ToString(),
                Name = adventureEntity.AdventureName
            };
        }

        [HttpPost]
        public ActionResult Post([FromBody] Models.DTO.AdventureDTO adventure)
        {
            var adventureEntity = new Models.Entities.Adventure()
            {
                AdventureName = adventure.Name
            };
            
            _repository.InsertOne(adventureEntity);
            return Ok();
        }
    }
}