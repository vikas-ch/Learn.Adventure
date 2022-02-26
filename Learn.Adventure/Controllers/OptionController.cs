using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Learn.Adventure.Models.DTO;
using Learn.Adventure.Models.Entities;
using Learn.Adventure.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Learn.Adventure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OptionController : ControllerBase
    {
        private readonly IRepository<Option> _repository;

        public OptionController(IRepository<Option> repository)
        {
            _repository = repository;
        }
        
        [HttpGet("{adventureId}")]
        public ActionResult<IEnumerable<OptionDTO>> Get(string adventureId)
        {
            var options = _repository
                .FilterBy(options => options.AdventureId == ObjectId.Parse(adventureId));
            return Ok(options.Select(option => new OptionDTO()
            {
                Id = option.Id.ToString(),
                AdventureId = option.AdventureId.ToString(),
                // Node = option.Node
                ParentId = option.ParentId.ToString(),
                Text = option.Text
            }));
        }


        [HttpPost]
        public ActionResult Post([FromBody]OptionDTO option)
        {
            var optionEntity = new Option()
            {
                AdventureId = ObjectId.Parse(option.AdventureId),
                ParentId = string.IsNullOrEmpty(option.ParentId) ? ObjectId.Empty : ObjectId.Parse(option.ParentId),
                Text = option.Text
            };
            
            _repository.InsertOne(optionEntity);
            
            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody] OptionDTO option)
        {
            if (option is null)
                return BadRequest();

            if (string.IsNullOrEmpty(option.Id))
                return NotFound();

            var optionEntity = new Option()
            {
                Id = ObjectId.Parse(option.Id),
                AdventureId = ObjectId.Parse(option.AdventureId),
                // Node = option.Node
                ParentId = string.IsNullOrEmpty(option.ParentId) ? ObjectId.Empty : ObjectId.Parse(option.ParentId),
                Text = option.Text
            };
            
            _repository.ReplaceOne(optionEntity);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            
            _repository.DeleteById(id);
            return Ok();
        }
    }
}