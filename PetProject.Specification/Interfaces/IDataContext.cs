using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Specification.Interfaces
{
    public interface IDataContext
    {
        DbContext Context { get; }
    }
}
