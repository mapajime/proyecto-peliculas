using Movies.Business.Interfaces;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Implementations
{
    public class LanguageBusiness : ILanguageBusinesscs
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

        public async Task<IEnumerable<Language>> GetLanguageByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return await _languageRepository.FilterAsync(n => n.Name.Contains(name));
        }

        public async Task UpdateLanguageAsync(Language language)
        {
            ValidateLanguage(language);
            await _languageRepository.UpdateAsync(language);
        }

        private static void ValidateLanguage(Language language)
        {
            if (string.IsNullOrEmpty(language?.Name))
            {
                throw new ArgumentNullException("The language cannont be null o empty");
            }
        }
    }
}