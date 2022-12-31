using Microsoft.EntityFrameworkCore;
using PetProject.Utilities.Exceptions;
using PetProject.Utilities.Helper;
using PetProject.Business.Common;
using PetProject.Business.Interfaces;
using PetProject.Business.Models;
using PetProject.Domain.Entities;
using PetProject.Domain.Interfaces;

namespace PetProject.Business.Implements
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<SignInModel> Authenticate(string userName, string password)
        {
            var user = await _unitOfWork.GenericRepository<User>().Queryable(p => p.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new PetProjectException("the userName could not be found!");
            }
            if (!SecurityHelper.VerifyHashedPassword(user.Password, password, user.SaltPassword))
            {
                throw new PetProjectException($"login fail!");
            }
            var result = new SignInModel()
            {
                UserName = userName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                UserType = "sysadmin"
            };
            var paremeter = new Microsoft.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.BigInt) { Value = result.Id };
            var roleIds = (await _unitOfWork.ExecStoreProcedureAsync("GetFeaturesByUser", paremeter));
            if (roleIds != null)
            {
                result.Roles = roleIds.Select(s => (long)s.FirstOrDefault().Value).ToArray();
            }

            var roleFeature = await _unitOfWork.GenericRepository<RoleFeature>().Queryable().ToListAsync();

            return result;
        }
    }
}
