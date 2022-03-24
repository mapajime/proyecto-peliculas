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
    public class MovieControllerTests
    {
        private readonly Mock<IMovieBusiness> _mockMovieBusiness;
        private readonly IMapper _mapper;

        public MovieControllerTests()
        {
            _mockMovieBusiness = new Mock<IMovieBusiness>();
            var mapperConfiguration = new MapperConfiguration(c => c.AddProfile(new MappingProfile()));
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task CreateMovieAsync_WhenMovieIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.CreateMovieAsync(null);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);
            _mockMovieBusiness.Verify(m => m.CreateMovieAsync(It.IsAny<Movie>()), Times.Never());
        }

        [Fact]
        public async Task CreateMovieAsync_WhenMovieIsNotNull_ShouldReturnOK()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.CreateMovieAsync(It.IsAny<Movie>())).ReturnsAsync(new Movie { Name="Ghost"});
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.CreateMovieAsync(new MovieModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var movie = result.Value as MovieModel;
            Assert.NotNull(movie);
            Assert.Equal("Ghost", movie.Name);

            _mockMovieBusiness.Verify(m => m.CreateMovieAsync(It.IsAny<Movie>()), Times.Once());
        }

        [Fact]
        public async Task CreateMovieAsync_WhenErrorOccurs_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.CreateMovieAsync(It.IsAny<Movie>()))
                .ThrowsAsync(new Exception("PruebaError"));
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.CreateMovieAsync(new MovieModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("PruebaError", result.Value.ToString());
            _mockMovieBusiness.Verify(m => m.CreateMovieAsync(It.IsAny<Movie>()), Times.Once);
        }

        [Fact]
        public async Task GetAllMoviesAsync_WhenMoviesAreCalled_ShouldReturnOkWithMovies()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.GetAllMoviesAsync())
                .ReturnsAsync(new List<Movie> { new Movie { Name = "Ghost" }, new Movie { Name = "ForestGump" } });
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.GetAllMoviesAsync();

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var list = (result.Value as IEnumerable<MovieModel>)?.ToList();
            Assert.NotNull(list);
            Assert.Equal("Ghost", list[0].Name);
            Assert.Equal("ForestGump", list.Last().Name);
            Assert.Equal(2, list.Count);

            _mockMovieBusiness.Verify(m => m.GetAllMoviesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteMovieAsync_WhenIdExist_ShouldReturnOk()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.DeleteMovieAsync(It.IsAny<Guid>()));
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.DeleteMovieAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);
            _mockMovieBusiness.Verify(m => m.DeleteMovieAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetMovieByIdAsync_WhenIdMovieExist_ShouldReturnOk()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.GetMovieByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Movie());
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.GetMovieByIdAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            _mockMovieBusiness.Verify(m => m.GetMovieByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]// revisar
        public async Task GetMoviesByNameAsync_WhenNameMovieIsEmpty_ShouldReturnBadRequest()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.GetMoviesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Movie> { new Movie { Name = "Forest Gump" }, new Movie { Name = "Crepusculo" } });
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.GetMoviesByNameAsync(string.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);
            //var list = (result as IEnumerable<Movie>).ToList();
            //Assert.NotNull(list);
            //_mockMovieBusiness.Verify(m=>m.GetMoviesByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetMoviesByNameAsync_WhenNameIsNotNullAndThereIsNoMoviesWithName_ShouldReturnNotFound()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.GetMoviesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(Enumerable.Empty<Movie>());
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.GetMoviesByNameAsync("Crepusculo");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);

            _mockMovieBusiness.Verify(m => m.GetMoviesByNameAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task GetMoviesByNameAsync_WhenMovieNameIsCalled_ShouldRetunrOkWithName()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.GetMoviesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Movie> { new Movie { Name = "Ghost" }, new Movie { Name = "ForestGump" } });
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.GetMoviesByNameAsync("Ghost");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var list = (result.Value as IEnumerable<MovieModel>)?.ToList();
            Assert.NotNull(list);
            Assert.Equal("Ghost", list[0].Name);
            Assert.Equal("ForestGump", list.Last().Name);
            Assert.Equal(2, list.Count);

            _mockMovieBusiness.Verify(m => m.GetMoviesByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetNumberOfMoviesAsync_WhenNumberMovieIsGreaterThan0_ShouldReturnOkWithNumberOfMovies()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.GetNumberOfMoviesAsync()).ReturnsAsync(3);
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.GetNumberOfMoviesAsync();

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(3, result.Value);
            _mockMovieBusiness.Verify(m => m.GetNumberOfMoviesAsync());
        }

        [Fact]
        public async Task UpdateMovieAsync_WhenMovieIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.UpdateMovieAsync(It.IsAny<Movie>()));
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.UpdateMovieAsync(null);

            //Assert
            Assert.NotNull(actionResult);
            var rersult = actionResult as BadRequestResult;
            Assert.NotNull(rersult);
        }

        [Fact]
        public async Task UpdateMovieAsync_WhenMovieIsNotNull_ShouldReturnOk()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.UpdateMovieAsync(It.IsAny<Movie>()));
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.UpdateMovieAsync(new MovieModel { Name = "Ghost" });

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);

            _mockMovieBusiness.Verify(m => m.UpdateMovieAsync(It.IsAny<Movie>()), Times.Once());
        }

        [Fact]
        public async Task UpdateMovieAsync_WhenErrorOccurs_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockMovieBusiness.Setup(m => m.UpdateMovieAsync(It.IsAny<Movie>()))
                .ThrowsAsync(new Exception("Test Error"));
            var movieController = new MovieController(_mockMovieBusiness.Object, _mapper);

            //Act
            var actionResult = await movieController.UpdateMovieAsync(new MovieModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Test Error", result.Value.ToString());
            _mockMovieBusiness.Verify(m => m.UpdateMovieAsync(It.IsAny<Movie>()), Times.Once);
        }
    }
}