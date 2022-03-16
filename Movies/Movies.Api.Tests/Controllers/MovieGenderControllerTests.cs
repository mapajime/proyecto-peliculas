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
using System.Threading.Tasks;
using Xunit;

namespace Movies.Api.Tests.Controllers
{
    public class MovieGenderControllerTests
    {
        private readonly Mock<IMovieGenderBusiness> _mockMovieGenderBusiness;
        private readonly IMapper _mapper;

        public MovieGenderControllerTests()
        {
            _mockMovieGenderBusiness = new Mock<IMovieGenderBusiness>();
            var mapperConfiguration = new MapperConfiguration(a => a.AddProfile(new MappingProfile()));
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task CreateGenderMovieAsync_WhenMovieGenderIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            var movieGenderController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await movieGenderController.CreateGenderMovieAsync(null);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);

            _mockMovieGenderBusiness.Verify(mg => mg.CreateGenderMovieAsync(It.IsAny<MovieGender>()), Times.Never());
        }

        [Fact]
        public async Task CreateGenderMovieAsync_WhenMovieGenderIsNotNull_ShouldReturnOK()
        {
            //Arrange
            _mockMovieGenderBusiness.Setup(mg => mg.CreateGenderMovieAsync(It.IsAny<MovieGender>())).ReturnsAsync(new MovieGender { Name = "Accion" });
            var movieGenderController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await movieGenderController.CreateGenderMovieAsync(new MovieGenderModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var movieGender = result.Value as MovieGenderModel;
            Assert.NotNull(movieGender);
            Assert.Equal("Accion", movieGender.Name);

            _mockMovieGenderBusiness.Verify(mg => mg.CreateGenderMovieAsync(It.IsAny<MovieGender>()), Times.Once());
        }

        [Fact]
        public async Task CreateGenderMovieAsync_WhenErrorOccurs_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockMovieGenderBusiness.Setup(mg => mg.CreateGenderMovieAsync(It.IsAny<MovieGender>()))
                .ThrowsAsync(new Exception("Error Internal"));
            var moviegenderController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await moviegenderController.CreateGenderMovieAsync(new MovieGenderModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Error Internal", result.Value.ToString());
            _mockMovieGenderBusiness.Verify(mg => mg.CreateGenderMovieAsync(It.IsAny<MovieGender>()), Times.Once);
        }

        [Fact]
        public async Task DeleteGenderMovieAsyncc_WhenIdMovieGenderExist_ShouldReturnOk()
        {
            //Arrange
            _mockMovieGenderBusiness.Setup(mg => mg.DeleteGenderMovieAsync(It.IsAny<Guid>()));
            var movieGenderController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await movieGenderController.DeleteGenderMovieAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);
            _mockMovieGenderBusiness.Verify(mg => mg.DeleteGenderMovieAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetGenderMovieByNameAsync_WhenNameMovieGenderIsEmpty_ShouldReturnBadRequest()
        {
            //Arrange
            _mockMovieGenderBusiness.Setup(mg => mg.GetGenderMovieByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<MovieGender> { new MovieGender { Name = "Accion" }, new MovieGender { Name = "Suspenso" } });
            var movieGenderController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await movieGenderController.GetGenderMovieByNameAsync(string.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);

            _mockMovieGenderBusiness.Verify(mg => mg.GetGenderMovieByNameAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task GetGenderMovieByNameAsync_WhenNameMovieGenderIsNotNullAndThereIsNoGenders_ShouldReturnNotFound()
        {
            //Arrange
            _mockMovieGenderBusiness.Setup(mg => mg.GetGenderMovieByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((IEnumerable<MovieGender>)null);
            var movieGenderController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await movieGenderController.GetGenderMovieByNameAsync("Accion");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetGenderMovieByNameAsync_WhenNameMovieGenderIsNotNull_ShouldReturnOKWithMovieGender()
        {
            //Arrange
            _mockMovieGenderBusiness.Setup(mg => mg.GetGenderMovieByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<MovieGender> { new MovieGender { Name = "Accion" }, new MovieGender { Name = "Romantica" } });
            var movieGenderController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await movieGenderController.GetGenderMovieByNameAsync("Romantica");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            _mockMovieGenderBusiness.Verify(mg => mg.GetGenderMovieByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetGenderMovieByIdAsync_WhenIdMovieGenderIsNull_ShouldReturnNotFound()
        {
            //Arrange
            _mockMovieGenderBusiness.Setup(mg => mg.GetGenderMovieByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((MovieGender)null);
            var movieGenderController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await movieGenderController.GetGenderMovieByIdAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
            _mockMovieGenderBusiness.Verify(mg => mg.GetGenderMovieByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetGenderMovieByIdAsync_WhenIdMovieGenderIsNotNull_ShouldReturnOkWithMovieGender()
        {
            //Arrange
            _mockMovieGenderBusiness.Setup(mg => mg.GetGenderMovieByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new MovieGender { Name = "Accion" });
            var movieGenderController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await movieGenderController.GetGenderMovieByIdAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            _mockMovieGenderBusiness.Verify(mg => mg.GetGenderMovieByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task UpdateGenderMovieByIdAsync_WhenMovieGenderIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            _mockMovieGenderBusiness.Setup(mg => mg.UpdateGenderMovieByIdAsync(It.IsAny<MovieGender>()));
            var movieGendeController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await movieGendeController.UpdateGenderMovieByIdAsync(null);

            //Assert
            Assert.NotNull(actionResult);
            var rersult = actionResult as BadRequestResult;
            Assert.NotNull(rersult);
        }

        [Fact]
        public async Task UpdateGenderMovieByIdAsync_WhenMovieGenderIsNotNull_ShouldReturnOk()
        {
            //Arrange
            _mockMovieGenderBusiness.Setup(mg => mg.UpdateGenderMovieByIdAsync(It.IsAny<MovieGender>()));
            var movieGenderController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await movieGenderController.UpdateGenderMovieByIdAsync(new MovieGenderModel { Name = "Accion" });

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);

            _mockMovieGenderBusiness.Verify(mg => mg.UpdateGenderMovieByIdAsync(It.IsAny<MovieGender>()), Times.Once());
        }

        [Fact]
        public async Task UpdateGenderMovieByIdAsync_WhenErrorOccurs_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockMovieGenderBusiness.Setup(mg => mg.UpdateGenderMovieByIdAsync(It.IsAny<MovieGender>()))
                .ThrowsAsync(new Exception("Error Prueba"));
            var movieGnderController = new MovieGenderController(_mockMovieGenderBusiness.Object, _mapper);

            //Act
            var actionResult = await movieGnderController.UpdateGenderMovieByIdAsync(new MovieGenderModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Error Prueba", result.Value.ToString());
            _mockMovieGenderBusiness.Verify(mg => mg.UpdateGenderMovieByIdAsync(It.IsAny<MovieGender>()), Times.Once);
        }
    }
}