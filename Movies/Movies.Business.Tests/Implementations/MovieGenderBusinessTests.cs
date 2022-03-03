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
    public class MovieGenderBusinessTests
    {
        private readonly Mock<IMovieGenderRepository> _mockMovieGenderRepository;

        public MovieGenderBusinessTests()
        {
            _mockMovieGenderRepository = new Mock<IMovieGenderRepository>();
        }

        [Fact]
        public async Task CreateGenderMovieAsync_WhenMovieGenderIsNull_ShouldThrowAnArgumentNullException()
        {
            //Arrange
            var movieGenderBusiness = new MovieGenderBusiness(_mockMovieGenderRepository.Object);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => movieGenderBusiness.CreateGenderMovieAsync(null));
            Assert.Contains("The movie gender cannont be null o empty", exception.Message);
            Assert.Equal("gender", exception.ParamName);
        }

        [Fact]
        public async Task CreateGenderMovieAsync_WhenMovieGenderIsOk_ShouldCreateMovieGender()
        {
            //Arrenge
            _mockMovieGenderRepository.Setup(e => e.AddAsync(It.IsAny<MovieGender>())).ReturnsAsync(true);
            var movieGenderBusiness = new MovieGenderBusiness(_mockMovieGenderRepository.Object);

            //Act
            await movieGenderBusiness.CreateGenderMovieAsync(new MovieGender { Name = "Accion" });
            _mockMovieGenderRepository.Verify(e => e.AddAsync(It.IsAny<MovieGender>()), Times.Once);
        }

        [Fact]
        public async Task DeleteGenderMovieAsync_WhenMovieGenderExist_ShouldDeleteMovieGender()
        {
            //Arrange
            var movieGenderBusiness = new MovieGenderBusiness(_mockMovieGenderRepository.Object);

            //Act
            await movieGenderBusiness.DeleteGenderMovieAsync(Guid.Empty);

            //Assert

            _mockMovieGenderRepository.Verify(d => d.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetGenderMovieByNameAsync_WhenMovieGenderIsNull_ShouldGenderMovieReturnNull()
        {
            //Arrange
            var movieGenderBusiness = new MovieGenderBusiness(_mockMovieGenderRepository.Object);

            //Act
            var movieGenders = await movieGenderBusiness.GetGenderMovieByNameAsync(null);

            //Assert
            Assert.Null(movieGenders);
        }

        [Fact]
        public async Task GetGenderMovieByNameAsync_WhenGenderMovieIsOk_ShoulReturnValues()
        {
            //Arrange
            _mockMovieGenderRepository.Setup(f => f.FilterAsync(It.IsAny<Func<MovieGender, bool>>()))
                .ReturnsAsync(new List<MovieGender>
                {
                    new MovieGender { Name = "Accion"},
                    new MovieGender { Name = "Romantica"},
                    new MovieGender { Name = "Suspenso"}
                });
            var movieGenderBusiness = new MovieGenderBusiness(_mockMovieGenderRepository.Object);

            //Act
            var movieGenders = (await movieGenderBusiness.GetGenderMovieByNameAsync("Accion")).ToList();

            //Assert
            Assert.NotNull(movieGenders);
            Assert.Equal(3, movieGenders.Count);
            Assert.Equal("Romantica", movieGenders[1].Name);
        }

        [Fact]
        public async Task UpdateGenderMovieByIdAsync_WhenIdMovieGenderIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            var movieGenderBusiness = new MovieGenderBusiness(_mockMovieGenderRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => movieGenderBusiness.UpdateGenderMovieByIdAsync(null));

            Assert.Contains("The movie gender cannont be null o empty", exception.Message);
            Assert.Equal("gender", exception.ParamName);
        }

        [Fact]
        public async Task UpdateGenderMovieByIdAsync_WhenIdMovieGenderIsOk_ShouldReturnValues()
        {
            //Arrange
            var movieGenderBusiness = new MovieGenderBusiness(_mockMovieGenderRepository.Object);

            //Act
            await movieGenderBusiness.UpdateGenderMovieByIdAsync(new MovieGender { Name = "Accion" });

            //Assert
            _mockMovieGenderRepository.Verify(a => a.UpdateAsync(It.IsAny<MovieGender>()), Times.Once);
        }
    }
}