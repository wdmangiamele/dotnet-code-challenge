using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateSalary([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received salary create request for '{compensation.CompensationId}'");

            _compensationService.CreateSalary(compensation);

            return CreatedAtRoute("GetSalaryById", new { id = compensation.CompensationId }, compensation);
        }

        [HttpGet("{id}", Name = "getSalaryById")]
        public IActionResult GetSalaryById(String id)
        {
            _logger.LogDebug($"Received salary get request for '{id}'");

            var salary = _compensationService.GetSalaryById(id);

            if (salary == null)
                return NotFound();

            return Ok(salary);
        }

    }
}
