using GlassLewisTest.Core.Entities;
using GlassLewisTest.DataAccess.Infrastructure;
using GlassLewisTest.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlassLewisTest.DataAccess.Repositories
{
    public class CompaniesRepository : BaseRepository, ICompaniesRepository
    {
        public CompaniesRepository(IOptions<ConnectionOptions> connectionOptions)
            : base(connectionOptions.Value)
        {
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await QueryAsync<Company>(new QueryObject(@"
                select 
                    c.Id as Id,
                    c.Name as Name,
                    c.Ticker as Ticker,
                    c.ISIN as ISIN,
                    c.Website as Website,
                    e.Id as ExchangeId,
                    e.Name as ExchangeName
                from Companies c
                    join Exchanges e on c.ExchangeId = e.Id
                where c.Deleted is null"));
        }

        public async Task<Company> CreateAsync(Company company)
        {
            return await QuerySingleAsync<Company>(new QueryObject(@"
                insert into Companies (Id, Name, ExchangeId, Ticker, ISIN, Website, Created)
                output inserted.Id, inserted.Name, inserted.ExchangeId, inserted.Ticker, inserted.ISIN, inserted.Website
                values (newid(), @Name, @ExchangeId, @Ticker, @ISIN, @Website, getdate())
            ", company));
        }

        public async Task<Company> GetByIdAsync(Guid id)
        {
            return await QueryFirstOrDefaultAsync<Company>(new QueryObject(@"
                select 
                    c.Id as Id,
                    c.Name as Name,
                    c.Ticker as Ticker,
                    c.ISIN as ISIN,
                    c.Website as Website,
                    e.Id as ExchangeId,
                    e.Name as ExchangeName
                from Companies c
                    join Exchanges e on c.ExchangeId = e.Id
                where c.Id = @Id
            ", new { Id = id }));
        }

        public async Task<Company> UpdateAsync(Company company)
        {
            await ExecuteAsync(new QueryObject(@"
	            update Companies 
                set Name = @Name,
                    Ticker = @Ticker,
                    ISIN = @ISIN,
                    Website = @Website,
                    ExchangeId = @ExchangeId
                where Id = @Id
            ", company));

            return company;
        }

        public async Task<Company> GetByISINAsync(string ISIN)
        {
            return await QueryFirstOrDefaultAsync<Company>(new QueryObject(@"
                select top 1
                    c.Id as Id,
                    c.Name as Name,
                    c.Ticker as Ticker,
                    c.ISIN as ISIN,
                    c.Website as Website,
                    e.Id as ExchangeId,
                    e.Name as ExchangeName
                from Companies c
                    join Exchanges e on c.ExchangeId = e.Id
                where c.ISIN = @ISIN
            ", new { ISIN = ISIN }));
        }
    }
}
