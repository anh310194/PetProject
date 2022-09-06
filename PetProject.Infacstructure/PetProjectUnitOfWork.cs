using PetProject.Core.Data;
using PetProject.Core.Interfaces;
using PetProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Infacstructure
{
    public class PetProjectUnitOfWork : UnitOfWork
    {
        public PetProjectUnitOfWork(IDbContext dbContext) : base(dbContext) { }

        public override IRepository<TEntity> GetRepository<TEntity>()
        {
            var typeIRepository = typeof(IRepository<TEntity>);
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .FirstOrDefault(t => typeIRepository.IsAssignableFrom(t)
                );
            if (type != null)
            {
                var typeEntity = typeof(TEntity);
                var value = Activator.CreateInstance(type, _dbContext);
                SetRepository(typeEntity.Name, value);
            }
            return base.GetRepository<TEntity>();
        }
    }
}
