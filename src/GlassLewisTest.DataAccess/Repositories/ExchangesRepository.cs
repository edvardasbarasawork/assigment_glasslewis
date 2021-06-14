using GlassLewisTest.Core.Entities;
using GlassLewisTest.DataAccess.Infrastructure;
using GlassLewisTest.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace GlassLewisTest.DataAccess.Repositories
{
    public class ExchangesRepository : BaseRepository, IExchangesRepository
    {
        public ExchangesRepository(IOptions<ConnectionOptions> connectionOptions)
           : base(connectionOptions.Value)
        {
        }

        public async Task<Exchange> CreateAsync(Exchange exchange)
        {
            return await QuerySingleAsync<Exchange>(new QueryObject(@"
	            insert into Exchanges (Id, Name)
				output inserted.Id, inserted.Name
	            values (newid(), @Name)
            ", exchange));
        }

        public async Task<Exchange> GetExchangeByNameAsync(string exchangeName)
        {
            return await QueryFirstOrDefaultAsync<Exchange>(new QueryObject(@"
	            select * from Exchanges where Name = @Name
            ", new { Name = exchangeName }));
        }
    }
}
