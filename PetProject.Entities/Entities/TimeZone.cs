using PetProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Entities
{
    public class TimeZone: BaseEntity
    {
        public string? TimeZoneId { get; set; }
        public string? TimeZoneName { get; set; }

    }
}
