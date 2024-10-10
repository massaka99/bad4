using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using bad4.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;

namespace bad4.Controllers
{
    [Authorize(Policy = "IsAdmin")]
    [ApiController]
    [Route("logs")]
    public class LogController : ControllerBase
    {
        private readonly LogService _logService;

        public LogController(LogService logService)
        {
            _logService = logService;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<object>>> GetLogs(string? user = null, string? from = null, string? to = null, string? operation = null)
        {
            var logs = await _logService.GetLogs(user, from, to, operation);
            return Ok(logs);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllLogs()
        {
            var logs = await _logService.GetAsync();
            return Ok(logs);
        }
    }
}
