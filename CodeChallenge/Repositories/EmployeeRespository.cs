using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;

namespace CodeChallenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        //---------------------- Employee ------------------------
        public Employee AddEmployee(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetEmployeeById(String id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public Employee RemoveEmployee(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }

        //---------------------- Reporting ------------------------
        public ReportingStructure GetReportingStructure(String id)
        {
            ReportingStructure reportingStructure = new ReportingStructure
            {
                Employee= GetAllReports(_employeeContext.Employees, id, out int count),
                NumberOfReports = count
            };

            return reportingStructure;
        }

        private Employee GetAllReports(DbSet<Employee> employees, string id, out int count)
        {
            //get direct reports for the employee
            IEnumerable<Employee> directReports = employees.Include(r => r.DirectReports);
            //make sure that the employee matches the target id
            Employee employee = directReports.FirstOrDefault(i => i.EmployeeId == id );
            
            count = 0;

            foreach( Employee e in directReports)
            {
                //iterate through all of the employees in the list
                //this populates the direct reports for each employee
                //that we have collected in the above include statement
            }

            //Now we find the number of reports we have collected
            //Check each direct report under given employee
            for (int i = 0; i < employee.DirectReports.Count; i++)
            {
                //see if there is a direct report under the employee
                if ( employee.DirectReports[i] != null )
                {
                    //if there is, find all the direct reports
                    for (int j = 0; j <= employee.DirectReports[i].DirectReports.Count; j++)
                    {
                        //add one to the count
                        count++;
                    }
                }
            }

            return employee;
        }

        //---------------------- Helpers ------------------------

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}
