using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface ILanguageBusinesscs
    {
        Task CreateLanguageAsync(Language language);

        Task UpdateLanguageAsync(Language language);

        Task DeleteLanguageAsync(Guid id);

        Task<IEnumerable<Language>> GetLanguageByNameAsync(string name);
    }
}