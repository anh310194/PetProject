using PetProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Entities
{
    public class RoleFeature : BaseEntity
    {
        public long RoleId { get; set; }
        public long FeatureId { get; set; }
        public byte Status { get; set; }
        public Role? Role { get; set; }
        public Feature? Feature { get; set; }
    }
}
