using PetProject.Domain.Common;

namespace PetProject.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public IEnumerable<User>? Users { get; set; }
    }
}
