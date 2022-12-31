using PetProject.Domain.Common;

namespace PetProject.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string? RoleName { get; set; }
        public byte Status { get; set; }
        public byte RoleType { get; set; }
        public string? Description { get; set; }
        public IEnumerable<UserRole>? UserRoles { get; set; }
        public IEnumerable<RoleFeature>? RoleFeatures { get; set; }
    }
}
