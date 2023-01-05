using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetProject.Domain.Entities;
using PetProject.Domain.Interfaces;
using PetProject.Infacstructure.Context;

namespace PetProject.TestInfrastructure
{
    public class CountryRepository_Test
    {
        private int userId = 1;
        private Country _seedCountry;
        private IUnitOfWork _unitOfWork;

        public CountryRepository_Test()
        {
            var _contextOptions = new DbContextOptionsBuilder<PetProjectContext>()
                .UseInMemoryDatabase("PetProjectDB")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            PetProjectContext projectContext = new PetProjectContext(_contextOptions);

            _unitOfWork = Common.UnitOfWorkInstance(projectContext);
            _seedCountry = new Country
            {
                CountryCode = "US",
                CountryName = "United States",
                RowVersion = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, }
            };

            if (_unitOfWork.CountryRepository.Queryable().Any())
            {
                return;
            }
            _seedCountry = _unitOfWork.CountryRepository.Insert(
                new Country
                {
                    CountryCode = "US",
                    CountryName = "United States",
                    RowVersion = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, }
                }, userId);
            _unitOfWork.SaveChanges();
        }

        [Test]
        public async Task GetCountries_Ok()
        {
            //Atc
            var result = await _unitOfWork.CountryRepository.Queryable().ToListAsync();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public async Task GetCountryByCountryCode_Ok()
        {
            //Atc
            var result = await _unitOfWork.CountryRepository.GetByCountryCodeAsync(_seedCountry.CountryCode);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.CountryCode == result.CountryCode);
        }

        [Test]
        public async Task InsertCountry_Ok()
        {
            //Atc
            var newCountry = new Country
            {
                CountryCode = "VN",
                CountryName = "Viet Name",
                RowVersion = new byte[] { 1, 2, 3, 4, 5, 6, 7, 6, }
            };
            var result = await _unitOfWork.CountryRepository.InsertAsync(newCountry, userId);
            await _unitOfWork.SaveChangesAsync();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.CountryCode == result.CountryCode);
            Assert.IsTrue(result.CountryName == result.CountryName);
        }

        [Test]
        public void UpdateCountry_RowversionChanged()
        {
            //Atc
            var newCountry = new Country
            {
                Id = _seedCountry.Id,
                CountryCode = _seedCountry.CountryCode,
                CountryName = _seedCountry.CountryName,
                CreatedBy = _seedCountry.CreatedBy,
                CreatedTime = _seedCountry.CreatedTime,
                RowVersion = new byte[] { 1, 2, 3, 4, 5, 6, 4, 6, }
            };
            Assert.Throws<System.InvalidOperationException>(() => _unitOfWork.CountryRepository.Update(newCountry, userId));
        }

        [Test]
        public void UpdateCountry_Successed()
        {
            //Arrange
            var newCountry = _unitOfWork.CountryRepository.Find(_seedCountry.Id);
            newCountry.CountryName = "Changed";

            //Atc
            var result = _unitOfWork.CountryRepository.Update(newCountry, userId);
            _unitOfWork.SaveChanges();

            //Assert
            Assert.IsTrue(newCountry.CountryName == result.CountryName);
        }

        [Test]
        public void DeleteCountry_Successed()
        {
            //Arrange
            var newCountry = new Country
            {
                CountryCode = "Delete",
                CountryName = "Delete Country Name",
                RowVersion = new byte[] { 1, 2, 3, 4, 5, 6, 7, 6, }
            };
            var inserted = _unitOfWork.CountryRepository.Insert(newCountry, userId);
            _unitOfWork.SaveChanges();

            //Atc
            _unitOfWork.CountryRepository.Delete(inserted);
            _unitOfWork.SaveChanges();
            var deleted = _unitOfWork.CountryRepository.Find(inserted.Id);

            //Assert
            Assert.IsNull(deleted);
        }
    }
}
