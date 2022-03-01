using Moq;
using Movies.Business.Implementations;
using Movies.DataAccess.Repositories.Implementation;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Movies.Business.Tests.Implementations
{
    public class CountryBusinessTest
    {
        private readonly Mock<ICountryRepository> _mockCountryRepository;
        public CountryBusinessTest()
        {
            _mockCountryRepository = new Mock<ICountryRepository>();
        }

        [Fact]
        public async Task CreateCountryAsync_WhenCountryIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            var countryBusiness = new CountryBusiness(_mockCountryRepository.Object);

            //Assert 
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => countryBusiness.CreateCountryAsync(null));
            Assert.Contains("The country cannont be null or empty", exception.Message);
            Assert.Equal("country", exception.ParamName);
        }

        [Fact]
        public async Task CreateCountryAsync_WhenInformationOfCountryIsOk_ShouldCallMethodAddAsyncFromRepository()
        {
            //Arrenge
            _mockCountryRepository.Setup(e => e.AddAsync(It.IsAny<Country>())).ReturnsAsync(true);
            var countryBusiness = new CountryBusiness(_mockCountryRepository.Object);
        }
    }
}
