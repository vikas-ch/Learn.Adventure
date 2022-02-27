using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
    public class OptionController : ControllerBase
    {
        private readonly IOptionService _optionService;


        public OptionController(IOptionService optionService)
        {
            _optionService = optionService;
        }
        
        /// <summary>
        /// Get list of all options/text choices for an adventure
        /// </summary>
        /// <param name="adventureId">Adventure Id</param>
        /// <returns>List of all options/text choices for an adventure</returns>
        [HttpGet("{adventureId}")]
        public ActionResult<IEnumerable<OptionDTO>> Get(string adventureId)
        {
            if (string.IsNullOrEmpty(adventureId))
                return NotFound();
            
            return Ok(_optionService.Get(adventureId));
        }
        
        /// <summary>
        /// Get a specific option/text choice for an adventure
        /// </summary>
        /// <param name="adventureId">Adventure Id</param>
        /// <param name="id">Option/Text Choice Id</param>
        /// <returns>A specific option/text choice for an adventure</returns>
        [HttpGet("{adventureId}/{id}")]
        public ActionResult<IEnumerable<OptionDTO>> Get(string adventureId, string id)
        {
            if (string.IsNullOrEmpty(adventureId) || string.IsNullOrEmpty(id))
                return NotFound();
            
            return Ok(_optionService.Get(adventureId, id));
        }


        /// <summary>
        /// Create an Option/Text Choice
        /// </summary>
        /// <param name="option"></param>
        /// <remarks>
        /// Object Definition:
        /// 
        ///     {
        ///         id (blank for adventure creation): Id of the option/text choice being created
        ///         adventureId: Adventure Id
        ///         parentId: Parent element Id (blank for root elements)
        ///         text: Actual text to be is displayed to the user
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody]OptionDTO option)
        {
            _optionService.Create(option);
            return NoContent();
        }

        /// <summary>
        /// Update an Option/Text choice
        /// </summary>
        /// <param name="option"></param>
        /// /// <remarks>
        /// Object Definition:
        /// 
        ///     {
        ///         id: Id of the option/text choice being created
        ///         adventureId: Adventure Id
        ///         parentId: Parent element Id (blank for root elements)
        ///         text: Actual text to be is displayed to the user
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Put([FromBody] OptionDTO option)
        {
            if (string.IsNullOrEmpty(option.Id))
                return NotFound();

            _optionService.Update(option);
            return NoContent();
        }

        /// <summary>
        /// Delete an Option/Text choice 
        /// </summary>
        /// <param name="adventureId">Adventure Id</param>
        /// <param name="id">Option/Text Id</param>
        /// <returns></returns>
        [HttpDelete("{adventureId}/{id}")]
        public ActionResult Delete(string adventureId, string id)
        {
            if (string.IsNullOrEmpty(adventureId) || string.IsNullOrEmpty(id))
                return NotFound();
            
            _optionService.Delete(adventureId, id);
            
            return NoContent();
        }
    }
}