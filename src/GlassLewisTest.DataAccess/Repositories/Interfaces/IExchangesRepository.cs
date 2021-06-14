using GlassLewisTest.Core.Entities;
using System.Threading.Tasks;

namespace GlassLewisTest.DataAccess.Repositories.Interfaces
{
    public interface IExchangesRepository
    {
        public Task<Exchange> CreateAsync(Exchange exchange);

        Task<Exchange> GetExchangeByNameAsync(string exchangeName);
    }
}
