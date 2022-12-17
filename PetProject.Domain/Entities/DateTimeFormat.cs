﻿using PetProject.Shared;

namespace PetProject.Domain.Entities
{
    public class DateTimeFormat: BaseEntity
    {
        public string? Format { get; set; }
        public byte FormatType { get; set; }

    }
}
