using PetProject.Core.Data;
using PetProject.Core.Entities;
using PetProject.Infacstructure.Database;

namespace PetProject.Infacstructure.Repositories
{
    public class CountryRepository: Repository<Country>
    {
        public CountryRepository(PetProjectContext petProject) : base(petProject)
        { }
    }
}
