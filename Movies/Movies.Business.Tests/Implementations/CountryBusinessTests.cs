using Moq;
using Movies.Business.Implementations;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Movies.Business.Tests.Implementations
{
    public class CountryBusinessTests
    {
        private readonly Mock<ICountryRepository> _mockCountryRepository;

        public CountryBusinessTests()
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
        public async Task CreateCountryAsync_WhenCountryIsOk_ShouldCallMethodAddAsyncFromRepository()
        {
            //Arrange
            _mockCountryRepository.Setup(e => e.AddAsync(It.IsAny<Country>())).ReturnsAsync(true);
            var countryBusiness = new CountryBusiness(_mockCountryRepository.Object);

            //Act
            await countryBusiness.CreateCountryAsync(new Country { Name = "Colombia" });

            //Assert
            _mockCountryRepository.Verify(c => c.AddAsync(It.IsAny<Country>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCountryAsync_WhenIdCountryExist_ShouldDeleteCountrySend()
        {
            //Arrange
            var countryBusiness = new CountryBusiness(_mockCountryRepository.Object);

            //Act
            await countryBusiness.DeleteCountryAsync(Guid.Empty);

            //Assert
            _mockCountryRepository.Verify(c => c.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetCountryByName_WhenCountryIsNull_ShouldReturnNull()
        {
            //Arrange
            var countryBusiness = new CountryBusiness(_mockCountryRepository.Object);

            //Act
            var country = await countryBusiness.GetCountryByName(null);

            //Assert
            Assert.Null(country);
        }

        [Fact]
        public async Task GetCountryByName_WhenCountryIsOk_ShouldGetCountry()
        {
            //Arrange
            _mockCountryRepository.Setup(c => c.FilterAsync(It.IsAny<Func<Country, bool>>()))
                .ReturnsAsync(new List<Country>
                {
                   new Country { Name = "Colombia"},
                   new Country { Name = "España"}
                });
            var countryBusiness = new CountryBusiness(_mockCountryRepository.Object);

            //Act
            var country = (await countryBusiness.GetCountryByName("Colombia")).ToList();

            //Assert
            Assert.NotNull(country);
            Assert.Equal(2, country.Count);
            Assert.Contains("Colombia", country.First().Name);
        }

        [Fact]
        public async Task UpdateCountryByIdAsync_WhenCountryIsNull_ShouldRetunNull()
        {
            //Arrange
            var countryBusiness = new CountryBusiness(_mockCountryRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => countryBusiness.UpdateCountryByIdAsync(null));

            Assert.Contains("The country cannont be null or empty", exception.Message);
        }

        [Fact]
        public async Task UpdateCountryByIdAsync_WhenCountryIsOk_ShouldUpdateCountry()
        {
            //Arrange
            var countryBusiness = new CountryBusiness(_mockCountryRepository.Object);

            //Act
            await countryBusiness.UpdateCountryByIdAsync(new Country { Name = "Colombia" });

            //Assert
            _mockCountryRepository.Verify(c => c.UpdateAsync(It.IsAny<Country>()), Times.Once());
        }
    }
}