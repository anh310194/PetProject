using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Business.Model
{
    public class SignInModel
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public long[]? Roles { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserType { get; set; }

    }
}
