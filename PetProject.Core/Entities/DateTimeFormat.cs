using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Core.Entities
{
    public class DateTimeFormat: BaseEntity
    {
        public string? Format { get; set; }
        public byte FormatType { get; set; }

    }
}
