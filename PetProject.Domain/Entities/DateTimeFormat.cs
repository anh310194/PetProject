using PetProject.Domain.Common;

namespace PetProject.Domain.Entities
{
    public class DateTimeFormat: BaseEntity
    {
        public string? Format { get; set; }
        public byte FormatType { get; set; }

    }
}
