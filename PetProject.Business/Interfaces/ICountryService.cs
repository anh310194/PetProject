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
        IEnumerable<CountryModel> GetCountries();
    }
}
