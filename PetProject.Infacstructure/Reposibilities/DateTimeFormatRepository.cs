﻿using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;

namespace PetProject.Infacstructure.Reposibilities
{
    internal class DateTimeFormatRepository : GenericRepository<DateTimeFormat>, IDateTimeFormatRepository
    {
        public DateTimeFormatRepository(PetProjectContext context) : base(context)
        {

        }
    }
}
