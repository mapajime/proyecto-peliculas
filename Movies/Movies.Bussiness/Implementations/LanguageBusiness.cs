using Movies.Business.Interfaces;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Implementations
{
    public class LanguageBusiness : ILanguageBusiness
    {
        private readonly ILanguageRepository _languageRepository;
        public LanguageBusiness(ILanguageRepository repository)
        {
            _languageRepository = repository;
        }
        public async Task CreateLanguageAsync(Language language)
        {
            ValidateLanguage(language);
            await _languageRepository.AddAsync(language);
        }
        public async Task DeleteLanguageAsync(Guid id) => await _languageRepository.DeleteAsync(id);


        public async Task<Language> GetLanguageByIdAsync(Guid id) => await _languageRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync() => await _languageRepository.GetAllAsync();
        public async Task<IEnumerable<Language>> GetLanguagesByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return await _languageRepository.FilterAsync(n => n.Name.Contains(name));
        }

        public async Task UpdateLanguageByIdAsync(Language language)
        {
            ValidateLanguage(language);
            await _languageRepository.UpdateAsync(language);
        }

        private static void ValidateLanguage(Language language)
        {
            if (string.IsNullOrEmpty(language?.Name))
            {
                throw new ArgumentNullException(nameof(language),"The language cannont be null o empty");
            }
        }
    }
}
