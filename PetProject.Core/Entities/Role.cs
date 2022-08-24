using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Core.Entities
{
    public class Role: BaseEntity
    {
        public string? RoleName { get; set; }
        public byte Status { get; set; }
        public byte RoleType { get; set; }
        public string? Description { get; set; }
    }
}
