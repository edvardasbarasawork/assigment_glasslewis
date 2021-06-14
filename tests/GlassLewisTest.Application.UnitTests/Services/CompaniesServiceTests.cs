using GlassLewisTest.Core.Entities;
using GlassLewisTest.DataAccess.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace GlassLewisTest.Application.UnitTests.Services
{
    [TestClass]
    public class CompaniesServiceTests
    {
        // TODO: add tests

        [TestMethod]
        public async Task CreateAsync_Should_Add_New_Entity_To_Database()
        {
            // Arrange

            var exchange = new Exchange()
            {
                Id = Guid.NewGuid(),
                Name = "NASDAQ"
            };

            var newCompany = new Company()
            {
                Id = Guid.NewGuid(),
                Ticker = "Ticker",
                ISIN = "US0378331005",
                ExchangeName = "NASDAQ",
                Website = string.Empty
            };

            var exchangesRepository = new Mock<IExchangesRepository>();
            exchangesRepository.Setup(p => p.GetExchangeByNameAsync(It.IsAny<string>())).Returns(Task.FromResult<Exchange>(null));
            exchangesRepository.Setup(p => p.CreateAsync(It.IsAny<Exchange>())).Returns(Task.FromResult<Exchange>(exchange));

            var companiesRepository = new Mock<ICompaniesRepository>();
            companiesRepository.Setup(p => p.CreateAsync(It.IsAny<Company>())).Returns(Task.FromResult<Company>(newCompany));

            var companiesService = new Application.Services.CompaniesService(companiesRepository.Object, exchangesRepository.Object);

            // Act
            var result = await companiesService.CreateAsync(newCompany);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newCompany.Id, result.Id);
            Assert.AreEqual(newCompany.ISIN, result.ISIN);
            Assert.AreEqual(newCompany.Ticker, result.Ticker);
            Assert.AreEqual(newCompany.Website, result.Website);
        }
    }
}
