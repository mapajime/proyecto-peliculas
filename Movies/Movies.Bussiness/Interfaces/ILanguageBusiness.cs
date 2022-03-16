using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface ILanguageBusiness
    {
        Task<Language> CreateLanguageAsync(Language language);

        Task UpdateLanguageByIdAsync(Language language);

        Task DeleteLanguageAsync(Guid id);

        Task<IEnumerable<Language>> GetLanguagesByNameAsync(string name);
        Task<Language> GetLanguageByIdAsync(Guid id);
        Task<IEnumerable<Language>> GetAllLanguagesAsync();
    }
}