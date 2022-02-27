using System.Collections.Generic;
using System.ComponentModel;
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
    public class AdventureController : ControllerBase
    {
        private readonly IRepository<Option> _optionRepository;
        private readonly IAdventureService _adventureService;

        public AdventureController(IAdventureService adventureService, IRepository<Option> optionRepository)
        {
            _adventureService = adventureService;
            _optionRepository = optionRepository;
        }
        
        
        /// <summary>
        /// Get list of all adventures
        /// </summary>
        /// <returns>List of adventures</returns>
        [HttpGet]
        public ActionResult<IEnumerable<AdventureDTO>> Get()
        {
            var adventures = _adventureService.Get();
            return Ok(adventures);
        }

        /// <summary>
        /// Get adventure by adventure id
        /// </summary>
        /// <param name="id">Adventure Id</param>
        /// <returns>Adventure object based on specified Id</returns>
        [HttpGet("{id}")]
        public ActionResult<AdventureDTO> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            return Ok(_adventureService.Get(id));
        }

        /// <summary>
        /// Create a new adventure
        /// </summary>
        /// <param name="adventure">Adventure object</param>
        /// <remarks>
        /// Object Definition:
        /// 
        ///     {
        ///         id (blank for adventure creation): Id of the adventure
        ///         name: Name of the adventure
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] AdventureDTO adventure)
        {
            _adventureService.Create(adventure);
            
            return NoContent();
        }

        /// <summary>
        /// Update an existing adventure
        /// </summary>
        /// <param name="adventure"></param>
        /// /// <remarks>
        /// Object Definition:
        /// 
        ///     {
        ///         id: Id of the adventure
        ///         name: Name of the adventure
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Put([FromBody] AdventureDTO adventure)
        {
            if (string.IsNullOrEmpty(adventure.Id))
                return NotFound();

            _adventureService.Update(adventure);
            return NoContent();
        }

        /// <summary>
        /// Delete an adventure
        /// </summary>
        /// <param name="id">Adventure Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            
            _adventureService.Delete(id);
            return NoContent();
        }
    }
}