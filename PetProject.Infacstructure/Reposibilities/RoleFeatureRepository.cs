using PetProject.Domain.Entities;
using PetProject.Domain.Interfaces;
using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Infacstructure.Reposibilities
{
    internal class RoleFeatureRepository : GenericRepository<RoleFeature>, IRoleFeatureRepository
    {
        public RoleFeatureRepository(PetProjectContext context) : base(context)
        {

        }
    }
}
