﻿using PetProject.Shared;

namespace PetProject.Domain.Entities
{
    public class TimeZone: BaseEntity
    {
        public string? TimeZoneId { get; set; }
        public string? TimeZoneName { get; set; }

    }
}
