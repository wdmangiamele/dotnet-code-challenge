using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        //----------------- Employee ------------------- 
        public Employee CreateEmployee(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.AddEmployee(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }        

        public Employee GetEmployeeById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetEmployeeById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if (originalEmployee != null)
            {
                _employeeRepository.RemoveEmployee(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.AddEmployee(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        //----------------- Reporting ------------------- 
        public ReportingStructure GetStructureById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {                
                return _employeeRepository.GetReportingStructure(id);
            }

            return null;
        }

    }
}
