using PetProject.Shared.Common;

namespace PetProject.Domain.Entities
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
