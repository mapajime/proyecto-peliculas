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
    public class GenderControllerTests
    {
        private readonly Mock<IGenderBusiness> _mockGenderBusiness;
        private readonly IMapper _mapper;

        public GenderControllerTests()
        {
            _mockGenderBusiness = new Mock<IGenderBusiness>();
            var mapperConfiguration = new MapperConfiguration(c => c.AddProfile(new MappingProfile()));
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task CreateGenderAsync_WhenGenderIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.CreateGenderAsync(null);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);

            _mockGenderBusiness.Verify(g => g.CreateGenderAsync(It.IsAny<Gender>()), Times.Never());
        }

        [Fact]
        public async Task CreateGenderAsync_WhenGenderIsNotNull_ShouldReturnOK()
        {
            //Arrange
            _mockGenderBusiness.Setup(g => g.CreateGenderAsync(It.IsAny<Gender>())).ReturnsAsync(new Gender { Name = "Masculino" });
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.CreateGenderAsync(new GenderModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var genderModel = result.Value as GenderModel;
            Assert.NotNull(genderModel);
            Assert.Equal("Masculino", genderModel.Name);

            _mockGenderBusiness.Verify(g => g.CreateGenderAsync(It.IsAny<Gender>()), Times.Once());
        }

        [Fact]
        public async Task CreateCountryAsync_WhenErrorOccurs_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockGenderBusiness.Setup(g => g.CreateGenderAsync(It.IsAny<Gender>()))
                .ThrowsAsync(new Exception("Prueba Error"));
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.CreateGenderAsync(new GenderModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Prueba Error", result.Value.ToString());
            _mockGenderBusiness.Verify(g => g.CreateGenderAsync(It.IsAny<Gender>()), Times.Once);
        }

        [Fact]
        public async Task DeleteGenderAsync_WhenIdGenderExist_ShouldReturnOk()
        {
            //Arrange
            _mockGenderBusiness.Setup(g => g.DeleteGenderAsync(It.IsAny<Guid>()));
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.DeleteGenderAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);
            _mockGenderBusiness.Verify(g => g.DeleteGenderAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetGenderByNameAsync_WhenNameGenderIsEmpty_ShouldReturnBadRequest()
        {
            //Arrange
            _mockGenderBusiness.Setup(g => g.GetGenderByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<Gender> { new Gender { Name = "Femenino" } });
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.GetGenderByNameAsync(string.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);

            _mockGenderBusiness.Verify(g => g.GetGenderByNameAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task GetGenderByNameAsync_WhenNameGenderIsNotNullAndTherIsNoGender_ShouldReturnNotFound()
        {
            //Arrange
            _mockGenderBusiness.Setup(g => g.GetGenderByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(Enumerable.Empty<Gender>());
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.GetGenderByNameAsync("Masculino");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);

            _mockGenderBusiness.Verify(g => g.GetGenderByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetGenderByNameAsync_WhenNameGenderIsNotNull_ShouldReturnOKWithGender()
        {
            //Arrange
            _mockGenderBusiness.Setup(g => g.GetGenderByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Gender> { new Gender { Name = "Femenino" }, new Gender { Name = "Masculino" } });
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.GetGenderByNameAsync("Femenino");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var list = (result.Value as IEnumerable<GenderModel>)?.ToList();
            Assert.NotNull(list);
            Assert.Equal(2, list.Count);
            Assert.Equal("Femenino", list.First().Name);
            Assert.Contains(list, g => g.Name == "Masculino");
            _mockGenderBusiness.Verify(g => g.GetGenderByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetGenderByIdAsync_WhenIdGenderExist_ShouldReturnOkWithGender()
        {
            //Arrange
            _mockGenderBusiness.Setup(g => g.GetGenderByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Gender());
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.GetGenderByIdAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            _mockGenderBusiness.Verify(g => g.GetGenderByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetAllGendersAsync_WhenGenderAreCalled_ShouldReturnOkWithGenders()
        {
            //Arrange
            _mockGenderBusiness.Setup(g => g.GetAllGendersAsync())
                .ReturnsAsync(new List<Gender> { new Gender { Name = "Femenino" }, new Gender { Name = "Masculino" } });
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.GetAllGendersAsync();

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var list = (result.Value as IEnumerable<GenderModel>)?.ToList();
            Assert.NotNull(list);
            Assert.Equal("Femenino", list[0].Name);
            Assert.Equal("Masculino", list.Last().Name);
            Assert.Equal(2, list.Count);

            _mockGenderBusiness.Verify(g => g.GetAllGendersAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateGenderByIdAsync_WhenGenderIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            _mockGenderBusiness.Setup(g => g.UpdateGenderByIdAsync(It.IsAny<Gender>()));
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.UpdateGenderByIdAsync(null);

            //Assert
            Assert.NotNull(actionResult);
            var rersult = actionResult as BadRequestResult;
            Assert.NotNull(rersult);
        }

        [Fact]
        public async Task UpdateGenderByIdAsync_WhenCountryIsNotNull_ShouldReturnOk()
        {
            //Arrange
            _mockGenderBusiness.Setup(g => g.UpdateGenderByIdAsync(It.IsAny<Gender>()));
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.UpdateGenderByIdAsync(new GenderModel { Name = "Femenino" });

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);

            _mockGenderBusiness.Verify(g => g.UpdateGenderByIdAsync(It.IsAny<Gender>()), Times.Once());
        }

        [Fact]
        public async Task UpdateGenderByIdAsync_WhenGenderIsEmpty_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockGenderBusiness.Setup(g => g.UpdateGenderByIdAsync(It.IsAny<Gender>()))
                .ThrowsAsync(new Exception("Error Prueba"));
            var genderController = new GenderController(_mockGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await genderController.UpdateGenderByIdAsync(new GenderModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Error Prueba", result.Value.ToString());

            _mockGenderBusiness.Verify(g => g.UpdateGenderByIdAsync(It.IsAny<Gender>()), Times.Once);
        }
    }
}