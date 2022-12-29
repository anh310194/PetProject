using PetProject.Domain.Common;

namespace PetProject.Entities
{
    public class UserRole : BaseEntity
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public byte Status { get; set; }
        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}
