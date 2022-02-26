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
    public class AdventureController : ControllerBase
    {
        private readonly IRepository<Models.Entities.Adventure> _adventureRepository;
        private readonly IRepository<Option> _optionRepository;

        public AdventureController(IRepository<Models.Entities.Adventure> repository, IRepository<Option> optionRepository)
        {
            _adventureRepository = repository;
            _optionRepository = optionRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AdventureDTO>> Get()
        {
            var adventures = _adventureRepository.FilterBy(
                filter => true).Select(adventure => new AdventureDTO()
            {
                Id = adventure.Id.ToString(),
                Name = adventure.AdventureName
            });
            
            return Ok(adventures);
        }

        [HttpGet("{id}")]
        public ActionResult<AdventureDTO> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var adventureEntity = _adventureRepository.FindById(id);

            return new AdventureDTO()
            {
                Id = adventureEntity.Id.ToString(),
                Name = adventureEntity.AdventureName
            };
        }

        [HttpPost]
        public ActionResult Post([FromBody] AdventureDTO adventure)
        {

            var adventureEntity = new Models.Entities.Adventure()
            {
                AdventureName = adventure.Name
            };
            
            _adventureRepository.InsertOne(adventureEntity);
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put([FromBody] AdventureDTO adventure)
        {
            if (string.IsNullOrEmpty(adventure.Id))
                return NotFound();

            var adventureEntity = new Models.Entities.Adventure()
            {
                Id = ObjectId.Parse(adventure.Id),
                AdventureName = adventure.Name
            };
            
            _adventureRepository.ReplaceOne(adventureEntity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            
            _adventureRepository.DeleteById(id);
            _optionRepository.DeleteOne(doc=> doc.AdventureId == ObjectId.Parse(id));
            return NoContent();
        }
    }
}