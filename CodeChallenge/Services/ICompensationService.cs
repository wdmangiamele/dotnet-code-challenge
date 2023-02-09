using CodeChallenge.Models;
using System;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Compensation GetSalaryById(String id);
        Compensation CreateSalary(Compensation compensation);
    }
}