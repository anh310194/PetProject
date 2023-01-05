using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetProject.Domain.Interfaces;
using PetProject.Infacstructure.Context;

namespace PetProject.TestInfrastructure
{
    public class CountryRepository_Test
    {
        private IUnitOfWork _unitOfWork;
        public CountryRepository_Test()
        {
            var _contextOptions = new DbContextOptionsBuilder<PetProjectContext>()
                .UseInMemoryDatabase("PetProjectDB")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            PetProjectContext projectContext = new PetProjectContext(_contextOptions);
        }
    }
}
