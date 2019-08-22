using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiBuildDemo.Core.Interfases;
using ApiBuildDemo.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBuildDemo.Api.Controllers {
    [ApiController]
    [ApiVersion ("1")]
    [Route ("api/v{version:apiVersion}/[controller]")]
    [Produces ("application/json")]
    public class ValuesController : ControllerBase {

        private readonly ILoggerAdapter<ValuesController> _logger;
        private readonly IValueService _valueService;

        public ValuesController (ILoggerAdapter<ValuesController> logger, IValueService valueService) {
            _logger = logger;
            _valueService = valueService;
        }

        /// <summary>
        /// Get list of values
        /// </summary>
        /// <returns>List of values</returns>
        /// <remarks>
        /// Get list of values
        /// </remarks>
        /// <response code="200">Returns list of values</response>
        /// <response code="400">List of values is null</response> 
        /// <response code="500">Error of server</response> 
        [HttpGet]
        [ProducesResponseType (typeof (List<Value>), StatusCodes.Status200OK)]
        [ProducesResponseType (typeof (List<Value>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType (typeof (string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Value>>> GetValuesAsync () {
            var values = await _valueService.GetValuesAsync ();
            return Ok (values);
        }

        /// <summary>
        /// Get a Value of list values
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return Value</returns>
        /// <remarks>
        /// Get value
        /// </remarks>
        /// <response code="200">Returns Value</response>
        /// <response code="400">Empty value</response> 
        /// <response code="500">Error of server</response> 
        /// 
        [HttpGet ("{id}")]
        [ProducesResponseType (typeof (Value), StatusCodes.Status200OK)]
        [ProducesResponseType (typeof (Value), StatusCodes.Status400BadRequest)]
        [ProducesResponseType (typeof (string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Value>> GetValueByIdAsync (Guid id) {
            var result = await _valueService.GetValueByIdAsync (id);

            if (result == null) {
                return NotFound ();
            }

            return Ok (result);
        }

        /// <summary>
        /// Create a Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Status code Http create</returns>
        /// <remarks>
        /// Add value 
        /// </remarks>
        /// <response code="201">Returns Value created</response>
        /// <response code="400">Empty value</response> 
        /// <response code="500">Error of server</response> 
        [HttpPost]
        [ProducesResponseType (typeof (Value), StatusCodes.Status201Created)]
        [ProducesResponseType (typeof (Value), StatusCodes.Status400BadRequest)]
        [ProducesResponseType (typeof (string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Value>> CreateValueAsync ([FromBody] Value value) {
            var result = await _valueService.AddValueAsync (value);

            if (result == null) {
                return NotFound ();
            }

            return Ok (result);
        }

        /// <summary>
        /// Delete Value of list
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status Http code</returns>
        /// <remarks>
        /// Delete value 
        /// </remarks>
        /// <response code="400"></response> 
        /// <response code="404"></response> 
        /// <response code="500">Error of server</response> 
        [HttpDelete ("{id}")]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        [ProducesResponseType (typeof (string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteValueByIdAsync (Guid id) {

            await _valueService.DeleteValueByIdAsync (id);
            return NoContent ();
        }
    }
}