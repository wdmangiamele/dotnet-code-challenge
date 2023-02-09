using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compesationRepository)
        {
            _compensationRepository = compesationRepository;
            _logger = logger;
        }

        public Compensation CreateSalary(Compensation compensation)
        {
            if (compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetSalaryById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetSalaryById(id);
            }

            return null;
        }
    }
}
