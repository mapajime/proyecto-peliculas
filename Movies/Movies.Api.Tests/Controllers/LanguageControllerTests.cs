using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Api.Configuration;
using Movies.Api.Controllers;
using Movies.Api.Models;
using Movies.Business.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Movies.Api.Tests.Controllers
{
    public class LanguageControllerTests
    {
        private readonly Mock<ILanguageBusiness> _mockLanguageBusiness;
        private readonly IMapper _mapper;

        public LanguageControllerTests()
        {
            _mockLanguageBusiness = new Mock<ILanguageBusiness>();
            var mapperConfiguration = new MapperConfiguration(a => a.AddProfile(new MappingProfile()));
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task CreateLanguageAsync_WhenLanguageIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.CreateLanguageAsync(null);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);

            _mockLanguageBusiness.Verify(l => l.CreateLanguageAsync(It.IsAny<Language>()), Times.Never());
        }

        [Fact]
        public async Task CreateLanguageAsync_WhenLanguageIsNotNull_ShouldReturnOK()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.CreateLanguageAsync(It.IsAny<Language>()));
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.CreateLanguageAsync(new LanguageModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);

            _mockLanguageBusiness.Verify(l => l.CreateLanguageAsync(It.IsAny<Language>()), Times.Once());
        }

        [Fact]
        public async Task CreateLanguageAsync_WhenErrorOccurs_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.CreateLanguageAsync(It.IsAny<Language>()))
                .ThrowsAsync(new Exception("Error Internal"));
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.CreateLanguageAsync(new LanguageModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Error Internal", result.Value.ToString());
            _mockLanguageBusiness.Verify(l => l.CreateLanguageAsync(It.IsAny<Language>()), Times.Once);
        }

        [Fact]
        public async Task DeleteLanguageAsync_WhenIdGenderExist_ShouldReturnOk()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.DeleteLanguageAsync(It.IsAny<Guid>()));
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.DeleteLanguageAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);
            _mockLanguageBusiness.Verify(l => l.DeleteLanguageAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetLanguagesByNameAsync_WhenNameLanguageIsEmpty_ShouldReturnBadRequest()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.GetLanguagesByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<Language> { new Language () });
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.GetLanguagesByNameAsync(string.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetLanguagesByNameAsync_WhenNameLanguageIsCalled_ShouldReturnOkWithLanguages()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.GetLanguagesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Language> { new Language { Name = "Español" }, new Language { Name = "Ingles" } });
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.GetLanguagesByNameAsync("Español");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var list = (result.Value as IEnumerable<LanguageModel>)?.ToList();
            Assert.NotNull(list);
            Assert.Equal(2, list.Count);

            _mockLanguageBusiness.Verify(l => l.GetLanguagesByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetLanguagesByNameAsync_WhenNameLanguageIsNotNull_ShouldReturnOKWithLanguage()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.GetLanguagesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Language> { new Language { Name = "Español" } });
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.GetLanguagesByNameAsync("Español");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            _mockLanguageBusiness.Verify(l => l.GetLanguagesByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetLanguagesByIdAsync_WhenIdLanguageIsNotNullAndThereIsNoLanguage_ShouldReturnNotFound()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.GetLanguageByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Language)null);
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.GetLanguagesByIdAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
            _mockLanguageBusiness.Verify(l => l.GetLanguageByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetLanguagesByIdAsync_WhenIdLanguageIsNotNull_ShouldReturnOkWithLanguage()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.GetLanguageByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Language { Name = "Ingles" });
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.GetLanguagesByIdAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            _mockLanguageBusiness.Verify(l => l.GetLanguageByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetAllLanguagesAsync_WhenLanguagesExist_ShouldReturnOkWithNumberlanguages()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.GetAllLanguagesAsync())
                .ReturnsAsync(new List<Language> { new Language { Name = "Español" }, new Language { Name = "Ingles" } });
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.GetAllLanguagesAsync();

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var list = (result.Value as IEnumerable<LanguageModel>)?.ToList();
            Assert.NotNull(list);
            Assert.Equal("Español", list[0].Name);
            Assert.Equal("Ingles", list.Last().Name);
            Assert.Equal(2, list.Count);

            _mockLanguageBusiness.Verify(l => l.GetAllLanguagesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateLanguageByIdAsync_WhenLanguageIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.UpdateLanguageByIdAsync(It.IsAny<Language>()));
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.UpdateLanguageByIdAsync(null);

            //Assert
            Assert.NotNull(actionResult);
            var rersult = actionResult as BadRequestResult;
            Assert.NotNull(rersult);

            _mockLanguageBusiness.Verify(l => l.GetAllLanguagesAsync(), Times.Never);
        }

        [Fact]
        public async Task UpdateLanguageByIdAsync_WhenLanguageIsNotNull_ShouldReturnOk()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.UpdateLanguageByIdAsync(It.IsAny<Language>()));
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.UpdateLanguageByIdAsync(new LanguageModel { Name = "Ingles" });

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);

            _mockLanguageBusiness.Verify(l => l.UpdateLanguageByIdAsync(It.IsAny<Language>()), Times.Once());
        }

        [Fact]
        public async Task UpdateLanguageByIdAsync_WhenErrorOccurs_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockLanguageBusiness.Setup(l => l.UpdateLanguageByIdAsync(It.IsAny<Language>()))
                .ThrowsAsync(new Exception("Error Prueba"));
            var languageController = new LanguageController(_mockLanguageBusiness.Object, _mapper);

            //Act
            var actionResult = await languageController.UpdateLanguageByIdAsync(new LanguageModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Error Prueba", result.Value.ToString());
            _mockLanguageBusiness.Verify(l => l.UpdateLanguageByIdAsync(It.IsAny<Language>()), Times.Once);
        }
    }
}