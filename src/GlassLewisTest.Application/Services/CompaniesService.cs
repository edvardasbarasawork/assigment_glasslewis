using GlassLewisTest.Application.Services.Interfaces;
using GlassLewisTest.Core.Entities;
using GlassLewisTest.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlassLewisTest.Application.Services
{
    public class CompaniesService : ICompaniesService
    {
        public readonly IExchangesRepository _exchangesRepository;
        public readonly ICompaniesRepository _companiesRepository;

        public CompaniesService(ICompaniesRepository companiesRepository,
            IExchangesRepository exchangesRepository)
        {
            _exchangesRepository = exchangesRepository;
            _companiesRepository = companiesRepository;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companiesRepository.GetAllAsync();
        }

        public async Task<Company> CreateAsync(Company company)
        {
            var exchange = await _exchangesRepository.GetExchangeByNameAsync(company.ExchangeName);

            if (exchange == null)
            {
                exchange = await _exchangesRepository.CreateAsync(new Exchange() { Name = company.ExchangeName });
            }

            company.ExchangeId = exchange.Id;

            return await _companiesRepository.CreateAsync(company);
        }

        public async Task<Company> UpdateAsync(Company company)
        {
            var currentCompany = await _companiesRepository.GetByIdAsync(company.Id);

            if (currentCompany == null)
            {
                return null;
            }

            var exchange = await _exchangesRepository.GetExchangeByNameAsync(company.ExchangeName);

            if (exchange == null)
            {
                exchange = await _exchangesRepository.CreateAsync(new Exchange() { Name = company.ExchangeName });
            }

            company.ExchangeId = exchange.Id;

            return await _companiesRepository.UpdateAsync(company);
        }

        public async Task<Company> GetByIdAsync(Guid id)
        {
            return await _companiesRepository.GetByIdAsync(id);
        }

        public async Task<Company> GetByISINAsync(string ISIN)
        {
            return await _companiesRepository.GetByISINAsync(ISIN);
        }
    }
}
