﻿using PetProject.Core.Data;

namespace PetProject.Entities
{
    public class TimeZone: BaseEntity
    {
        public string? TimeZoneId { get; set; }
        public string? TimeZoneName { get; set; }

    }
}