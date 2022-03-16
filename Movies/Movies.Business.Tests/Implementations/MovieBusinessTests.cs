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
    public class MovieBusinessTests
    {
        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly Mock<IActorRepository> _actorRepository;

        public MovieBusinessTests()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _actorRepository = new Mock<IActorRepository>();
        }

        [Fact]
        public async Task CreateMovieAsync_WhenMovieIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            var movieBusiness = new MovieBusiness(_mockMovieRepository.Object, _actorRepository.Object);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => movieBusiness.CreateMovieAsync(null));
            Assert.Contains("The movie is null", exception.Message);
            Assert.Equal("movie", exception.ParamName);
        }

        [Fact]
        public async Task CreateMovieAsync_WhenMovieNameIsEmpty_ShouldThrowNullReferenceException()
        {
            //Arrage
            var movieBusiness = new MovieBusiness(_mockMovieRepository.Object, _actorRepository.Object);
            //assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => movieBusiness.CreateMovieAsync(new Movie
            {
                Name = null,
                Duration = new TimeSpan(1, 54, 50),
                Director = "Robert Zemeckis",
                Cast = new List<Actor>
                {
                    new Actor { FirstName = "Tom", LastName = "Hanks" },
                    new Actor { FirstName = "Gary", LastName = "Sinise" }
                }
            }));
            Assert.Contains("The name of movie is empty", exception.Message);
        }

        [Fact]
        public async Task CreateMovieAsync_WhenCastMovieIsNull_ShouldThrowArgumentException()
        {
            //Arrage
            var movieBusiness = new MovieBusiness(_mockMovieRepository.Object, _actorRepository.Object);
            //assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => movieBusiness.CreateMovieAsync(new Movie
            {
                Name = "ForestGump",
                Duration = new TimeSpan(1, 54, 50),
                Director = "Robert Zemeckis",
                Cast = null
            }));
            Assert.Contains("The movie has no actors", exception.Message);
            _mockMovieRepository.Verify(e => e.AddAsync(It.IsAny<Movie>()), Times.Never);
        }

        [Fact]
        public async Task CreateMovieAsync_WhenDirectorMovieIsEmpty_ShouldThrowArgumentException()
        {
            //Arrage
            var movieBusiness = new MovieBusiness(_mockMovieRepository.Object, _actorRepository.Object);
            //assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => movieBusiness.CreateMovieAsync(new Movie
            {
                Name = "ForestGump",
                Duration = new TimeSpan(1, 54, 50),
                Director = null,
                Cast = new List<Actor>
                {
                     new Actor { FirstName = "Tom", LastName = "Hanks" },
                    new Actor { FirstName = "Gary", LastName = "Sinise" }
                }
            }));
            Assert.Contains("The movie has no Director", exception.Message);
            _mockMovieRepository.Verify(e => e.AddAsync(It.IsAny<Movie>()), Times.Never);
        }

        [Fact]
        public async Task CreateMovieAsync_WhenMovieIsOk_ShouldCallAddMethod()
        {
            //Arrenge
            _mockMovieRepository.Setup(e => e.AddAsync(It.IsAny<Movie>())).ReturnsAsync(true);
            var movieBusiness = new MovieBusiness(_mockMovieRepository.Object, _actorRepository.Object);

            //Act
            await movieBusiness.CreateMovieAsync(new Movie
            {
                Name = "ForestGump",
                Duration = new TimeSpan(1, 54, 50),
                Director = "Robert Zemeckis",
                Cast = new List<Actor>
                {
                     new Actor { FirstName = "Tom", LastName = "Hanks" },
                    new Actor { FirstName = "Gary", LastName = "Sinise" }
                }
            });
            _mockMovieRepository.Verify(e => e.AddAsync(It.IsAny<Movie>()), Times.Once);
        }

        [Fact]
        public async Task DeleteMovieByIdAsync_WhenIdMovieExist_ShouldDeleteMovie()
        {
            //Arrange
            var movieBusiness = new MovieBusiness(_mockMovieRepository.Object,_actorRepository.Object);

            //Act
            await movieBusiness.DeleteMovieAsync(Guid.Empty);

            //Assert

            _mockMovieRepository.Verify(d => d.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetAllMoviesAsync_WhenMoviesExist_ShoulReturnAllMovies()
        {
            //Arrange
            _mockMovieRepository.Setup(a => a.GetAllAsync())
                .ReturnsAsync(new List<Movie>
                {
                    new Movie { Name ="ForestGump", Duration=new TimeSpan(1, 54, 50), Director = "Robert Zemeckis",
                    Cast = new List<Actor>
                    {
                     new Actor { FirstName = "Tom", LastName = "Hanks" },
                    new Actor { FirstName = "Gary", LastName = "Sinise" }
                    }
                }});
            var movieBusiness = new MovieBusiness(_mockMovieRepository.Object, _actorRepository.Object);

            //Act
            var movies = (await movieBusiness.GetAllMoviesAsync()).ToList();

            //Assert
            Assert.NotNull(movies);
            Assert.Equal(1, movies.Count);
            Assert.Equal(2, movies.First().Cast.Count);
            Assert.Equal("ForestGump", movies.Last().Name);
            Assert.Contains(movies, a => a.Director == "Robert Zemeckis");
        }

        [Fact]
        public async Task GetMovieByIdAsync_WhenIdMovieExist_ShouldGetMovieById()
        {
            //Arrange
            _mockMovieRepository.Setup(g => g.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new Movie
                    {
                        Name = "ForestGump",
                        Duration = new TimeSpan(1, 54, 50),
                        Director = "Robert Zemeckis",
                        Cast = new List<Actor>
                    {
                     new Actor {  FirstName = "Tom", LastName = "Hanks" },
                     new Actor { FirstName = "Gary", LastName = "Sinise" }
                    }
                    });

            var movieBusiness = new MovieBusiness(_mockMovieRepository.Object, _actorRepository.Object);

            // Act
            var movie = await movieBusiness.GetMovieByIdAsync(Guid.Empty);

            //Assert
            Assert.NotNull(movie);
            Assert.Equal("ForestGump", movie.Name);
            Assert.Equal("Robert Zemeckis", movie.Director);
            Assert.Equal(1, movie.Duration.Hours);
        }

        [Fact]
        public async Task GetMoviesByNameAsync_WhenNameMovieIsNull_ShouldReturnNull()
        {
            //Arrange
            var movieBusiness = new MovieBusiness(_mockMovieRepository.Object, _actorRepository.Object);

            //Act
            var movies = await movieBusiness.GetMoviesByNameAsync(null);

            //Assert
            Assert.Null(movies);
        }

        [Fact]
        public async Task GetMoviesByNameAsync_WhenNameExist_ShouldReturnValues()
        {
            //Arrange
            _mockMovieRepository.Setup(f => f.FilterAsync(It.IsAny<Func<Movie, bool>>()))
               .ReturnsAsync(new List<Movie>
                {
                    new Movie { Name ="ForestGump", Duration=new TimeSpan(1, 54, 50), Director = "Robert Zemeckis",
                    Cast = new List<Actor>
                    {
                     new Actor { FirstName = "Tom", LastName = "hanks" },
                    new Actor { FirstName = "gary", LastName = "sinise" }
                    }
                 } });
            var movieBusiness = new MovieBusiness(_mockMovieRepository.Object, _actorRepository.Object);

            //Act
            var movies = (await movieBusiness.GetMoviesByNameAsync("ForestGump")).ToList();

            //Assert
            Assert.NotNull(movies);
            Assert.NotEmpty(movies);
            //Assert.True(movies.Any());
            //Assert.Equal(1, movies.Count);
            //Assert.True(movies.Count > 0);
            Assert.Equal("Robert Zemeckis", movies[0].Director);
        }

        [Fact]
        public async Task GetNumberOfMoviesAsync_WhenGetNumberMovies_ShouldReturnInt()
        {
            //Arrange
            _mockMovieRepository.Setup(f => f.CountAsync())
              .ReturnsAsync(2);
            var movieBusiness = new MovieBusiness(_mockMovieRepository.Object, _actorRepository.Object);

            //Act
            var totalMovies = await movieBusiness.GetNumberOfMoviesAsync();

            //Assert
            Assert.Equal(2, totalMovies);
        }
    }
}