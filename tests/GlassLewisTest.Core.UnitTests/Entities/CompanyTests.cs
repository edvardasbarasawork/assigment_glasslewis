using GlassLewisTest.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GlassLewisTest.Core.UnitTests.Entities
{
    [TestClass]
    public class CompanyTests
    {
        // TODO: add tests

        [TestMethod]
        public void Correct_Format()
        {
            // Arrange
            var model = new Company()
            {
                Ticker = "Ticker",
                ISIN = "US0378331005",
                ExchangeName = "NASDAQ",
                Website = "http://google.com"
            };

            var validationContext = new ValidationContext(model);

            // Act
            var results = model.Validate(validationContext);

            // Assert
            Assert.AreEqual(results.Count(), 0);
        }

        [TestMethod]
        public void Bad_Website_URI_Format()
        {
            // Arrange
            var model = new Company()
            {
                Ticker = "Ticker",
                ISIN = "US0378331005",
                ExchangeName = "NASDAQ",
                Website = "test"
            };

            var validationContext = new ValidationContext(model);

            // Act
            var results = model.Validate(validationContext);

            // Assert
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().ErrorMessage, "Website is not well formed uri string");
        }

        [TestMethod]
        public void Bad_ISIN_Format()
        {
            // Arrange
            var model = new Company()
            {
                Ticker = "Ticker",
                ISIN = "550378331005",
                ExchangeName = "NASDAQ",
            };

            var validationContext = new ValidationContext(model);

            // Act
            var results = model.Validate(validationContext);

            // Assert
            Assert.AreEqual(results.Count(), 1);
            Assert.AreEqual(results.First().ErrorMessage, "ISIN is not correct");
        }
    }
}
