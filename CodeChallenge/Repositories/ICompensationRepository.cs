using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation Add(Compensation salary);
        Compensation GetSalaryById(String id);
        Task SaveAsync();
    }
}