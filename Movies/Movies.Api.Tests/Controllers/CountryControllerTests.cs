using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Api.Configuration;
using Movies.Api.Controllers;
using Movies.Models;
using Movies.Business.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Movies.Api.Tests.Controllers
{
    public class CountryControllerTests
    {
        private readonly Mock<ICountryBusiness> _mockCountryBusiness;
        private readonly IMapper _mapper;

        public CountryControllerTests()
        {
            _mockCountryBusiness = new Mock<ICountryBusiness>();
            var mapperConfiguration = new MapperConfiguration(c => c.AddProfile(new MappingProfile()));
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task CreateCountryAsync_WhenCountryIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            var countryController = new CountryController(_mockCountryBusiness.Object, _mapper);

            //Act
            var actionResult = await countryController.CreateCountryAsync(null);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);

            _mockCountryBusiness.Verify(c => c.CreateCountryAsync(It.IsAny<Country>()), Times.Never());
        }

        [Fact]
        public async Task CreateCountryAsync_WhenCountryIsNotNull_ShouldReturnOK()
        {
            //Arrange
            _mockCountryBusiness.Setup(c => c.CreateCountryAsync(It.IsAny<Country>())).ReturnsAsync(new Country { Name = "Colombia" });
            var countryController = new CountryController(_mockCountryBusiness.Object, _mapper);

            //Act
            var actionResult = await countryController.CreateCountryAsync(new CountryModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var countryModel = result.Value as CountryModel;
            Assert.NotNull(countryModel);
            Assert.Equal("Colombia", countryModel.Name);

            _mockCountryBusiness.Verify(c => c.CreateCountryAsync(It.IsAny<Country>()), Times.Once());
        }

        [Fact]
        public async Task CreateCountryAsync_WhenErrorOccurs_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockCountryBusiness.Setup(c => c.CreateCountryAsync(It.IsAny<Country>()))
                .ThrowsAsync(new Exception("Internal Error"));
            var countryController = new CountryController(_mockCountryBusiness.Object, _mapper);

            //Act
            var actionResult = await countryController.CreateCountryAsync(new CountryModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Internal Error", result.Value.ToString());
            _mockCountryBusiness.Verify(c => c.CreateCountryAsync(It.IsAny<Country>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCountryAsync_WhenIdCountryExist_ShouldReturnOk()
        {
            //Arrange
            _mockCountryBusiness.Setup(c => c.DeleteCountryAsync(It.IsAny<Guid>()));
            var countryController = new CountryController(_mockCountryBusiness.Object, _mapper);

            //Act
            var actionResult = await countryController.DeleteCountryAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);
            _mockCountryBusiness.Verify(c => c.DeleteCountryAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetCountriesByNameAsync_WhenNameCountryIsEmpty_ShouldReturnBadRequest()
        {
            //Arrange
            _mockCountryBusiness.Setup(c => c.GetCountriesByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<Country> { new Country { Name = "Europa" } });
            var countryController = new CountryController(_mockCountryBusiness.Object, _mapper);

            //Act
            var actionResult = await countryController.GetCountriesByNameAsync(string.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);

            _mockCountryBusiness.Verify(c => c.GetCountriesByNameAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task GetCountriesByNameAsync_WhenNameCountryIsNotNullAndThereIsNoCountries_ShouldReturnNotFound()
        {
            //Arrange
            _mockCountryBusiness.Setup(c => c.GetCountriesByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((List<Country>)null);
            var countryController = new CountryController(_mockCountryBusiness.Object, _mapper);

            //Act
            var actionResult = await countryController.GetCountriesByNameAsync("Mexico");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);

            _mockCountryBusiness.Verify(c => c.GetCountriesByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetCountriesByNameAsync_WhenNameCountryIsNotNull_ShouldReturnOKWithCountry()
        {
            //Arrange
            _mockCountryBusiness.Setup(c => c.GetCountriesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Country> { new Country { Name = "Mexico" }, new Country { Name = "Europa" } });
            var countryController = new CountryController(_mockCountryBusiness.Object, _mapper);

            //Act
            var actionResult = await countryController.GetCountriesByNameAsync("Mexico");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var list = (result.Value as IEnumerable<CountryModel>)?.ToList();
            Assert.NotNull(list);
            Assert.Equal("Mexico", list[0].Name);
            Assert.Contains(list, c => c.Name == "Europa");
            Assert.Equal(2, list.Count);
            _mockCountryBusiness.Verify(c => c.GetCountriesByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetAllCountrieAsync_WhenCountryAreCalled_ShouldReturnOkWithCounties()
        {
            //Arrange
            _mockCountryBusiness.Setup(c => c.GetAllCountriesAsync())
                .ReturnsAsync(new List<Country> { new Country { Name = "España" }, new Country { Name = "Colombia" } });
            var countryController = new CountryController(_mockCountryBusiness.Object, _mapper);

            //Act
            var actionResult = await countryController.GetCountriesAsync();

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var list = (result.Value as IEnumerable<CountryModel>)?.ToList();
            Assert.NotNull(list);
            Assert.Equal("España", list[0].Name);
            Assert.Equal("Colombia", list.Last().Name);
            Assert.Equal(2, list.Count);

            _mockCountryBusiness.Verify(c => c.GetAllCountriesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateCountryByIdAsync_WhenCountryIsEmpty_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockCountryBusiness.Setup(c => c.UpdateCountryByIdAsync(It.IsAny<Country>()));
            var countryController = new CountryController(_mockCountryBusiness.Object, _mapper);

            //Act
            var actionResult = await countryController.UpdateCountryByIdAsync(new CountryModel());

            //Assert
            Assert.NotNull(actionResult);
            var rersult = actionResult as StatusCodeResult;
            Assert.NotNull(rersult);

            _mockCountryBusiness.Verify(c => c.UpdateCountryByIdAsync(It.IsAny<Country>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCountryByIdAsync_WhenCountryIsNotNull_ShouldReturnOk()
        {
            //Arrange
            _mockCountryBusiness.Setup(c => c.UpdateCountryByIdAsync(It.IsAny<Country>()));
            var countryController = new CountryController(_mockCountryBusiness.Object, _mapper);

            //Act
            var actionResult = await countryController.UpdateCountryByIdAsync(new CountryModel { Name = "Mexico" });

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);

            _mockCountryBusiness.Verify(c => c.UpdateCountryByIdAsync(It.IsAny<Country>()), Times.Once());
        }
    }
}