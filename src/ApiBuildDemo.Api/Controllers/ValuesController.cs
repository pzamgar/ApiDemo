﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApiBuildDemo.Core.Interfases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        [ProducesResponseType (typeof (List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType (typeof (List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType (typeof (string), StatusCodes.Status500InternalServerError)]
        public ActionResult<IList<string>> Get () {
            return _valueService.GetValues ();
        }
    }
}