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
    public class ActorBusinessTests
    {
        private readonly Mock<IActorRepository> _mockActorRepository;

        public ActorBusinessTests()
        {
            _mockActorRepository = new Mock<IActorRepository>();
        }

        [Fact]
        public async Task CreateActorAsync_WhenActorIsNull_ShouldThrowAnArgumentNullException()
        {
            //Arrange
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => actorBusiness.CreateActorAsync(null));
            Assert.Contains("The actor is null", exception.Message);
            Assert.Equal("actor", exception.ParamName);
        }

        [Fact]
        public async Task CreateActorAsync_WhenFirstNameIsNull_ShouldThrowNullReferenceException()
        {
            //Arrage
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);
            //assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => actorBusiness.CreateActorAsync(new Actor { FirstName = null }));
            Assert.Contains("The name and lastname of actor is empty", exception.Message);
        }

        [Fact]
        public async Task CreateActorAsync_WhenLastNameIsNull_ShouldThrowNullReferenceException()
        {
            //Arrage
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);
            //assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => actorBusiness.CreateActorAsync(new Actor { LastName = null }));
            Assert.Contains("The name and lastname of actor is empty", exception.Message);
        }

        [Fact]
        public async Task CreateActorAsync_WhenDateOfBirthIsNull_ShouldThrowArgumentException()
        {
            //Arrage
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);
            //assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => actorBusiness.CreateActorAsync(new Actor { DateOfBirth = null }));
            Assert.Contains("The name and lastname of actor is empty", exception.Message);
            _mockActorRepository.Verify(e => e.AddAsync(It.IsAny<Actor>()), Times.Never);
        }

        [Fact]
        public async Task CreateActorAsync_WhenActorInformationIsOk_ShouldCallAddMethodFromRepository()
        {
            //Arrenge
            _mockActorRepository.Setup(e => e.AddAsync(It.IsAny<Actor>())).ReturnsAsync(true);
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Act
            await actorBusiness.CreateActorAsync(new Actor { FirstName = "Ana", LastName = "Parra", DateOfBirth = new DateTime(2000, 12, 12) });
            _mockActorRepository.Verify(e => e.AddAsync(It.IsAny<Actor>()), Times.Once);
        }

        [Fact]
        public async Task DeleteActorAsync_WhenIdActorExist_ShouldDeleteIdActorSend()
        {
            //Arrange
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Act
            await actorBusiness.DeleteActorAsync(Guid.Empty);

            //Assert

            _mockActorRepository.Verify(d => d.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetActorByIdAsync_WhenIdActorExist_ShouldGetActorById()
        {
            //Arrange
            _mockActorRepository.Setup(g => g.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Actor { FirstName = "Ana", LastName = "Parra", DateOfBirth = new DateTime(1978, 1, 1) });
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Act
            var actor = await actorBusiness.GetActorByIdAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actor);
            Assert.Equal("Ana", actor.FirstName);
            Assert.Equal("Parra", actor.LastName);
            Assert.Equal(1978, actor.DateOfBirth.Value.Year);
        }

        [Fact]
        public async Task GetActorByLastNameAsync_WhenLastNameIsNull_ShouldReturnNull()
        {
            //Arrange
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Act
            var actors = await actorBusiness.GetActorByLastNameAsync(null);

            //Assert
            Assert.Null(actors);
        }

        [Fact]
        public async Task GetActorByLastNameAsync_WhenLastNameHasValue_ShouldReturnValues()
        {
            //Arrange
            _mockActorRepository.Setup(f => f.FilterAsync(It.IsAny<Func<Actor, bool>>()))
                .ReturnsAsync(new List<Actor>
                {
                    new Actor { FirstName = "Ana"},
                    new Actor { FirstName= "Jimena"},
                    new Actor{FirstName="Maria"}
                });
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Act
            var actors = (await actorBusiness.GetActorByLastNameAsync("Parra")).ToList();

            //Assert
            Assert.NotNull(actors);
            Assert.Equal(3, actors.Count);
            Assert.Equal("Jimena", actors[1].FirstName);
        }

        [Fact]
        public async Task GetAllActorsAsync_WhenIsCalled_ShouldReturnAllActors()
        {
            //Arrange
            _mockActorRepository.Setup(a => a.GetAllAsync())
                .ReturnsAsync(new List<Actor>
                {
                    new Actor { FirstName="Ana", LastName="Parra"},
                    new Actor {FirstName="Jimena", LastName="Martinez"}
                });
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Act
            var actors = (await actorBusiness.GetAllActorsAsync()).ToList();

            //Assert
            Assert.NotNull(actors);
            Assert.Equal(2, actors.Count);
            Assert.Equal("Jimena", actors.Last().FirstName);
            Assert.Contains(actors, a => a.FirstName == "Ana");
        }

        [Fact]
        public async Task UpdateActorByIdAsync_WhenActorIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => actorBusiness.UpdateActorByIdAsync(null));

            Assert.Contains("The actor is null", exception.Message);
            Assert.Equal("actor", exception.ParamName);
        }

        [Fact]
        public async Task UpdateActorByIdAsync_WhenLastNameIsNull_ShouldThrowNullReferenceException()
        {
            //Arrange
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<NullReferenceException>(() => actorBusiness.UpdateActorByIdAsync(new Actor { LastName = null }));
            Assert.Contains("The name or lastname of actor is empty", exception.Message);
        }

        [Fact]
        public async Task UpdateActorByIdAsync_WhenDateOfBirthIsNull_ShouldThrowArgumentException()
        {
            //Arrange
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => actorBusiness.UpdateActorByIdAsync(new Actor { LastName = "Parra", FirstName = "Ana", DateOfBirth = null }));
            Assert.Contains("The DateofBirth is null", exception.Message);
        }
        [Fact]
        public async Task UpdateActorByIdAsync_WhenActorIsOk_ShouldUpdateActor()
        {
            //Arrange
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Act
            await actorBusiness.UpdateActorByIdAsync(new Actor { FirstName = "Ana", LastName = "Parra", DateOfBirth = new DateTime(2000, 12, 12) });

            //Assert
            _mockActorRepository.Verify(a => a.UpdateAsync(It.IsAny<Actor>()), Times.Once);

        }
        [Fact]
        public async Task GetActorCountAsync_WhenIsCalled_ShoulReturnNumberOfActors()
        {
            //Arrage
            _mockActorRepository.Setup(a => a.CountAsync()).ReturnsAsync(9);
            var actorBusiness = new ActorBusiness(_mockActorRepository.Object);

            //Act
            var numberOfActors = await actorBusiness.GetActorCountAsync();

            //Assert
            Assert.Equal(9, numberOfActors);
        }
    }

}