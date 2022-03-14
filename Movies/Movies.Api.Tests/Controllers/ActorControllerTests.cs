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
    public class ActorControllerTests
    {
        private readonly Mock<IActorBusiness> _mockActorBusiness;
        private readonly IMapper _mapper;

        public ActorControllerTests()
        {
            _mockActorBusiness = new Mock<IActorBusiness>();
            var mapperConfiguration = new MapperConfiguration(a => a.AddProfile(new MappingProfile()));
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task CreateActorAsync_WhenActorIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.CreateActorAsync(null);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as BadRequestResult;
            Assert.NotNull(result);

            _mockActorBusiness.Verify(a => a.CreateActorAsync(It.IsAny<Actor>()), Times.Never());
        }

        [Fact]
        public async Task CreateActorAsync_WhenActorIsNotNull_ShouldReturnOK()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.CreateActorAsync(It.IsAny<Actor>()));
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.CreateActorAsync(new ActorModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);

            _mockActorBusiness.Verify(a => a.CreateActorAsync(It.IsAny<Actor>()), Times.Once());
        }

        [Fact]
        public async Task CreateActorAsync_WhenErrorOccurs_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.CreateActorAsync(It.IsAny<Actor>()))
                .ThrowsAsync(new Exception("Error Internal"));
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.CreateActorAsync(new ActorModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Error Internal", result.Value.ToString());
            _mockActorBusiness.Verify(a => a.CreateActorAsync(It.IsAny<Actor>()), Times.Once);
        }

        [Fact]
        public async Task GetActorsAsync_WhenActorIsCalled_ShouldReturnOkWithActors()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.GetAllActorsAsync())
                .ReturnsAsync(new List<Actor> { new Actor { FirstName = "Ana" }, new Actor { FirstName = "Jimena" }, new Actor { FirstName = "Pepe" } });
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.GetAllActorsAsync();

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var list = (result.Value as IEnumerable<ActorModel>)?.ToList();
            Assert.NotNull(list);
            Assert.Equal(3, list.Count);

            _mockActorBusiness.Verify(a => a.GetAllActorsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetActorsByLastName_WhenLastNameActorIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionresult = await actorController.GetActorsByLastName(null);

            //Assert
            Assert.NotNull(actionresult);
            var result = actionresult as BadRequestResult;
            Assert.NotNull(result);

            _mockActorBusiness.Verify(a =>a.GetActorByLastNameAsync(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task GetActorsByLastName_WhenLastNameIsNotNullAndThereIsNotActorsWithLastName_ShouldReturnNotFound()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.GetActorByLastNameAsync(It.IsAny<string>()))
            .ReturnsAsync(null as IEnumerable<Actor>);
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);  

            //Act
            var actionResult = await actorController.GetActorsByLastName("Ana");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);

            _mockActorBusiness.Verify(a => a.GetActorByLastNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetActorsByLastName_WhenLastNameActorIsNotNull_ShouldReturnOKWithActor()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.GetActorByLastNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Actor> { new Actor { FirstName = "Ana", LastName = "Perez" } });
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.GetActorsByLastName("Ana");

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var list = (result.Value as IEnumerable<ActorModel>)?.ToList();
            Assert.NotNull(list);
            Assert.Equal(1, list.Count);
            Assert.Contains(list, a => a.FirstName == "Ana");
            _mockActorBusiness.Verify(a => a.GetActorByLastNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetActorById_WhenIdActorIsEmpty_ShouldReturnNotFound()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.GetActorByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Actor)null);
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.GetActorById(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as NotFoundResult;
            Assert.NotNull(result);
            _mockActorBusiness.Verify(a => a.GetActorByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetActorById_WhenIdActorIsNotNull_ShouldReturnOkWithActor()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.GetActorByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Actor { FirstName = "Pepe" });
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.GetActorById(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var actor = result.Value as ActorModel;
            Assert.NotNull(actor);
            Assert.Equal("Pepe", actor.FirstName);
            _mockActorBusiness.Verify(a => a.GetActorByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteActorByIdAsync_WhenIdActorExist_ShouldReturnOk()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.DeleteActorAsync(It.IsAny<Guid>()));
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.DeleteActorByIdAsync(Guid.Empty);

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);
            _mockActorBusiness.Verify(a => a.DeleteActorAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetActorCountAsync_WhenActorsExist_ShouldReturnOkWithNumberActors()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.GetActorCountAsync()).ReturnsAsync(5);
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.GetActorCountAsync();

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(5, result.Value);
            _mockActorBusiness.Verify(a => a.GetActorCountAsync());
        }

        [Fact]
        public async Task GetAllActorsAsync_WhenActorsArecalled_ShouldReturnOkWithActors()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.GetAllActorsAsync())
                .ReturnsAsync(new List<Actor> { new Actor { FirstName = "Luis" }, new Actor { FirstName = "Julio", LastName = "Lopez" } });
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.GetAllActorsAsync();

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);
            var list = (result.Value as IEnumerable<ActorModel>)?.ToList();
            Assert.NotNull(list);
            Assert.Equal("Luis", list[0].FirstName);
            Assert.Equal("Julio", list.Last().FirstName);
            Assert.Equal("Lopez", list.Last().LastName);
            Assert.Equal(2, list.Count);

            _mockActorBusiness.Verify(a => a.GetAllActorsAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateActorByIdAsync_WhenActorIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.UpdateActorByIdAsync(It.IsAny<Actor>()));
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.UpdateActorByIdAsync(null);

            //Assert
            Assert.NotNull(actionResult);
            var rersult = actionResult as BadRequestResult;
            Assert.NotNull(rersult);
        }

        [Fact]
        public async Task UpdateActorByIdAsync_WhenActorIsNotNull_ShouldReturnOk()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.UpdateActorByIdAsync(It.IsAny<Actor>()));
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.UpdateActorByIdAsync(new ActorModel { FirstName = "Ana" });

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as OkResult;
            Assert.NotNull(result);

            _mockActorBusiness.Verify(a => a.UpdateActorByIdAsync(It.IsAny<Actor>()), Times.Once());
        }

        [Fact]
        public async Task UpdateActorByIdAsync_WhenErrorOccurs_ShouldReturnInternalServerError()
        {
            //Arrange
            _mockActorBusiness.Setup(a => a.UpdateActorByIdAsync(It.IsAny<Actor>()))
                .ThrowsAsync(new Exception("Error Prueba"));
            var actorController = new ActorController(_mockActorBusiness.Object, _mapper);

            //Act
            var actionResult = await actorController.UpdateActorByIdAsync(new ActorModel());

            //Assert
            Assert.NotNull(actionResult);
            var result = actionResult as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Error Prueba", result.Value.ToString());
            _mockActorBusiness.Verify(a => a.UpdateActorByIdAsync(It.IsAny<Actor>()), Times.Once);
        }
    }
}