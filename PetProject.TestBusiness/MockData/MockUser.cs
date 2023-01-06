using PetProject.Domain.Entities;

namespace PetProject.TestBusiness.Mock
{
    public static class MockUser
    {
        private static User? _mockUser;
        public static User GetUser()
        {
            if (_mockUser != null)
            {
                return _mockUser;
            }

            _mockUser = new User()
            {
                Address1 = "Test_Address1",
                Address2 = "Test_Address2",
                City = "Test_city",
                CountFailSignIn = 0,
                CountryId = 1,
                CreatedBy = 1,
                CreatedTime = DateTime.UtcNow,
                FirstName = "Test_FirstName",
                LastName = "Test_lastName",
                Password = "G+0b/tRkD6OTosxXsV1ed9mSntYcuEB0SDfM1JpO4mg=",
                Status = 1,
                UserName = "Test_username",
                Id = 1,
                SaltPassword = "Dhos2mi7ZDxg3xluwxLZdQ==",
                
            };
            return _mockUser;
        }
        public static string PlainedPassword = "sysadmin";
    }
}
