using Microsoft.AspNetCore.Authorization;
using PetProject.Core.Helper;
using PetProject.WebAPI.Enums;

namespace PetProject.WebAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public RoleAuthorizeAttribute(params FeatureEnum[] features)
        {
            if (features == null || features.Length == 0)
            {
                return;
            }
            string[] roles = new string[features.Length];
            for (int i = 0; i < features.Length; i++)
            {
                roles[i] = (int)features[i] + "";
            }

            Roles = string.Join(",", roles);
        }
    }
}
