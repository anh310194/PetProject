using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetProject.Business.Interfaces;
using PetProject.Business.Model;
using PetProject.Core;
using PetProject.Core.Exceptions;
using PetProject.Core.Helper;
using PetProject.Core.Interfaces;
using PetProject.Entities;

namespace PetProject.Business.Implements
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, ILogger<CountryService> logger) : base(unitOfWork, logger) { }

        public async Task<SignInModel> Authenticate(string userName, string password)
        {
            var user = await Queryable<User>(p => p.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new PetProjectException("the userName could not be found!");
            }
            if (!SecurityHelper.VerifyHashedPassword(user.Password, password, user.SaltPassword))
            {
                throw new PetProjectException($"login fail!");
            }
            return new SignInModel()
            {
                UserName = userName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Roles = new long[] { 2, 4 },
                UserType = "sysadmin"
            };
        }
    }
}
