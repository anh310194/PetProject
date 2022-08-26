using PetProject.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Business.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryModel>> GetCountries();
        Task<CountryModel?> GetCountryById(long id);
        Task<CountryModel?> UpsertCountryById(CountryModel model);
        void DeleteCountryById(long id);
    }
}
