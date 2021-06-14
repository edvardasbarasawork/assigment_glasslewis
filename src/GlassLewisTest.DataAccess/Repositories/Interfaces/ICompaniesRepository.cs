using GlassLewisTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlassLewisTest.DataAccess.Repositories.Interfaces
{
    public interface ICompaniesRepository
    {
        Task<Company> GetByIdAsync(Guid id);

        Task<Company> GetByISINAsync(string ISIN);

        Task<IEnumerable<Company>> GetAllAsync();

        Task<Company> CreateAsync(Company company);

        Task<Company> UpdateAsync(Company company);
    }
}
