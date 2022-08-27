using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Core.Data
{
    public interface IDbContext : IDisposable
    {
        DbContext Instance { get; }
    }
}
