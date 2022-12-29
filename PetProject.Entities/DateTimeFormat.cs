using PetProject.Domain.Common;

namespace PetProject.Entities
{
    public class DateTimeFormat: BaseEntity
    {
        public string? Format { get; set; }
        public byte FormatType { get; set; }

    }
}
