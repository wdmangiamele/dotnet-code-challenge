using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface IEmployeeRepository
    {
        //---------Employee-----------

        Employee GetEmployeeById(String id);
        Employee AddEmployee(Employee employee);
        Employee RemoveEmployee(Employee employee);

        //---------Reporting-----------
        ReportingStructure GetReportingStructure(String id);

        //---------Tasks-----------
        Task SaveAsync();
    }
}