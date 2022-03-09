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
    public class LanguageBusinessTests
    {
        private readonly Mock<ILanguageRepository> _mockLanguageRepository;

        public LanguageBusinessTests()
        {
            _mockLanguageRepository = new Mock<ILanguageRepository>();
        }

        [Fact]
        public async Task CreateLanguageAsync_WhenLanguageIsNull_ShouldThrowAnArgumentNullException()
        {
            //Arrange
            var languageBusiness = new LanguageBusiness(_mockLanguageRepository.Object);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => languageBusiness.CreateLanguageAsync(null));
            Assert.Contains("The language cannont be null o empty", exception.Message);
            Assert.Equal("language", exception.ParamName);
        }

        [Fact]
        public async Task CreateLanguageAsync_WhenLanguageIsOk_ShouldCallMethodAddFromRepository()
        {
            //Arrenge
            _mockLanguageRepository.Setup(e => e.AddAsync(It.IsAny<Language>())).ReturnsAsync(true);
            var languageBusiness = new LanguageBusiness(_mockLanguageRepository.Object);

            //Act
            await languageBusiness.CreateLanguageAsync(new Language { Name = "Ingles" });
            _mockLanguageRepository.Verify(e => e.AddAsync(It.IsAny<Language>()), Times.Once);
        }

        [Fact]
        public async Task DeleteLanguageAsync_WhenIdLanguageExist_ShouldDeleteIdLanguageSend()
        {
            //Arrange
            var languageBusiness = new LanguageBusiness(_mockLanguageRepository.Object);

            //Act
            await languageBusiness.DeleteLanguageAsync(Guid.Empty);

            //Assert

            _mockLanguageRepository.Verify(d => d.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task GetLanguagesByNameAsync_WhenLanguageIsNull_ShouldReturnNull()
        {
            //Arrange
            var languageBusiness = new LanguageBusiness(_mockLanguageRepository.Object);

            //Act
            var languages = await languageBusiness.GetLanguagesByNameAsync(null);

            //Assert
            Assert.Null(languages);
        }

        [Fact]
        public async Task GetLanguagesByNameAsync_WhenLanguageIsOk_ShouldReturnValues()
        {
            //Arrange
            _mockLanguageRepository.Setup(f => f.FilterAsync(It.IsAny<Func<Language, bool>>()))
                .ReturnsAsync(new List<Language>
                {
                    new Language { Name = "Ingles"},
                    new Language { Name= "Español"},
                    new Language { Name = "Frances" }
                });
            var languageBusiness = new LanguageBusiness(_mockLanguageRepository.Object);

            //Act
            var languages = (await languageBusiness.GetLanguagesByNameAsync("Ingles")).ToList();

            //Assert
            Assert.NotNull(languages);
            Assert.Equal(3, languages.Count);
            Assert.Equal("Español", languages[1].Name);
        }

        [Fact]
        public async Task GetLanguageByIdAsync_WhenIdLanguageExist_ShouldReturnLanguges()
        {
            //Arrange
            _mockLanguageRepository.Setup(l => l.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Language { Name = "Ingles" }
                );
            var languageBusiness = new LanguageBusiness(_mockLanguageRepository.Object);

            //Act
            var language = await languageBusiness.GetLanguageByIdAsync(Guid.Empty);

            //Assert
            Assert.NotNull(language);
            Assert.Equal("Ingles", language.Name);
        }

        [Fact]
        public async Task GetAllLanguagesAsync_WhenLanguagesExist_ShouldReturnLanguages()
        {
            //Arrange
            _mockLanguageRepository.Setup(l => l.GetAllAsync())
                .ReturnsAsync(new List<Language> { new Language { Name = "Spanish" }, new Language { Name = "English" }, new Language { Name = "French" } });
            var languageBusiness = new LanguageBusiness(_mockLanguageRepository.Object);

            //Act
            var languages = (await languageBusiness.GetAllLanguagesAsync()).ToList();

            //Assert
            Assert.NotNull(languages);
            Assert.Equal(3, languages.Count);
            Assert.Equal("English", languages[1].Name);
            Assert.Equal("French", languages.Last().Name);
        }

        [Fact]
        public async Task UpdateLanguageByIdAsync_WhenLanguageIsNull_ShouldThrowArgumentNullExceptionl()
        {
            //Arrange
            var languageBusiness = new LanguageBusiness(_mockLanguageRepository.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => languageBusiness.UpdateLanguageByIdAsync(null));

            Assert.Contains("The language cannont be null o empty", exception.Message);
            Assert.Equal("language", exception.ParamName);
        }

        [Fact]
        public async Task UpdateLanguageByIdAsync_WhenIdLanguageExist_ShouldReturnValues()
        {
            //Arrange
            var languageBusiness = new LanguageBusiness(_mockLanguageRepository.Object);

            //Act
            await languageBusiness.UpdateLanguageByIdAsync(new Language { Name = "Ingles" });

            //Assert
            _mockLanguageRepository.Verify(a => a.UpdateAsync(It.IsAny<Language>()), Times.Once);
        }
    }
}