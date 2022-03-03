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
    public class GenderBusinessTests
    {
        private readonly Mock<IGenderRepository> _mockGenderRepository;

        public GenderBusinessTests()
        {
            _mockGenderRepository = new Mock<IGenderRepository>();
        }

        [Fact]
        public async Task GenderCreateGenderAsync_WhenGenderIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            var genderBusiness = new GenderBusiness(_mockGenderRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => genderBusiness.CreateGenderAsync(null));
            Assert.Contains("The gender cannont be null or empty", exception.Message);
            Assert.Equal("gender", exception.ParamName);
        }

        [Fact]
        public async Task GenderCreateGenderAsync_WhenGenderIsOk_ShouldCreateGender()
        {
            //Arrange
            var genderBusiness = new GenderBusiness(_mockGenderRepository.Object);

            //Act
            await genderBusiness.CreateGenderAsync(new Gender { Name = "Femenino" });

            //Assert
            _mockGenderRepository.Verify(g => g.AddAsync(It.IsAny<Gender>()), Times.Once);
        }

        [Fact]
        public async Task DeleteGenderAsync_WhenIdGenderExiste_ShouldDeleterGender()
        {
            //Arrange
            var genderBusiness = new GenderBusiness(_mockGenderRepository.Object);

            //Act
            await genderBusiness.DeleteGenderAsync(Guid.Empty);

            //Assert
            _mockGenderRepository.Verify(g => g.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetGenderByNameAsync_WhenNameGenderIsNull_ShouldReturnGenderNull()
        {
            //Arrange
            var genderBusiness = new GenderBusiness(_mockGenderRepository.Object);

            //Act
            var gender = await genderBusiness.GetGenderByNameAsync(null);

            //Assert
            Assert.Null(gender);
        }

        [Fact]
        public async Task GetGenderByNameAsync_WhenNameGenderExiste_ShouldReturNameGender()
        {
            //Arrange
            _mockGenderRepository.Setup(g => g.FilterAsync(It.IsAny<Func<Gender, bool>>()))
                .ReturnsAsync(new List<Gender>
                {
                    new Gender { Name ="Femenino"},
                    new Gender { Name = "Masculino"}
                });
            var genderBusiness = new GenderBusiness(_mockGenderRepository.Object);

            //Act
            var genders = await genderBusiness.GetGenderByNameAsync("Femenino");

            //Assert
            Assert.NotNull(genders);
            Assert.Equal("Femenino", genders.First().Name);
            Assert.Equal(2, genders.Count());
        }

        [Fact]
        public async Task UpdateGenderByIdAsync_WhenIdGenderIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            var genderBusiness = new GenderBusiness(_mockGenderRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => genderBusiness.CreateGenderAsync(null));

            //Assert
            Assert.Contains("The gender cannont be null or empty", exception.Message);
            Assert.Equal("gender", exception.ParamName);
        }

        [Fact]
        public async Task UpdateGenderByIdAsync_WhenIdGenderExist_ShouldUpdateGender()
        {
            //Arrange
            var genderBusiness = new GenderBusiness(_mockGenderRepository.Object);

            //Act
            await genderBusiness.UpdateGenderByIdAsync(new Gender { Name = "Femenino" });

            //Assert
            _mockGenderRepository.Verify(g => g.UpdateAsync(It.IsAny<Gender>()), Times.Once);
        }
    }
}