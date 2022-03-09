using Movies.Business.Interfaces;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Business.Implementations
{
    public class GenderBusiness : IGenderBusiness
    {
        private readonly IGenderRepository _genderRepository;

        public GenderBusiness(IGenderRepository repository)
        {
            _genderRepository = repository;
        }

        public async Task CreateGenderAsync(Gender gender)
        {
            ValidateGender(gender);
            await _genderRepository.AddAsync(gender);
        }

        public async Task<Gender> GetGenderByIdAsync(Guid id) => await _genderRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Gender>> GetAllGendersAsync() => await _genderRepository.GetAllAsync();

        public async Task DeleteGenderAsync(Guid id) => await _genderRepository.DeleteAsync(id);

        public async Task<IEnumerable<Gender>> GetGenderByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return await _genderRepository.FilterAsync(n => n.Name.Contains(name));
        }

        public async Task UpdateGenderByIdAsync(Gender gender)
        {
            ValidateGender(gender);
            await _genderRepository.UpdateAsync(gender);
        }

        private static void ValidateGender(Gender gender)
        {
            if (string.IsNullOrEmpty(gender?.Name))
            {
                throw new ArgumentNullException(nameof(gender), "The gender cannont be null or empty");
            }
        }
    }
}