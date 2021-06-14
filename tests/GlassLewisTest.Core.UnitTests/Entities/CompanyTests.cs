using GlassLewisTest.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlassLewisTest.Core.UnitTests.Entities
{
    [TestClass]
    public class CompanyTests
    {
        // TODO: add tests

        [TestMethod]
        public async Task Correct_Format()
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
        public async Task Bad_Website_URI_Format()
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
        public async Task Bad_ISIN_Format()
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
