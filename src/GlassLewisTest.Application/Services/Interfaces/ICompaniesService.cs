using GlassLewisTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlassLewisTest.Application.Services.Interfaces
{
    public interface ICompaniesService
    {
        Task<Company> GetByIdAsync(Guid id);

        Task<Company> GetByISINAsync(string ISIN);

        Task<IEnumerable<Company>> GetAllAsync();

        Task<Company> CreateAsync(Company company);

        Task<Company> UpdateAsync(Company company);
    }
}
