using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface ILanguageBusiness
    {
        Task CreateLanguageAsync(Language language);

        Task UpdateLanguageByIdAsync(Language language);

        Task DeleteLanguageAsync(Guid id);

        Task<IEnumerable<Language>> GetLanguagesByNameAsync(string name);
    }
}