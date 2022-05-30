using PetProject.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Infacstructure.Database
{
    public class PetProjectUnitOfWork : UnitOfWork
    {
        public PetProjectUnitOfWork(PetProjectContext context) : base(context) { }
    }
}
