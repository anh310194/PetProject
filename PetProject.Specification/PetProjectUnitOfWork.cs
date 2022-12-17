using Microsoft.EntityFrameworkCore;
using PetProject.Specification.Interfaces;
using PetProject.Specification.Common;

namespace PetProject.Specification
{
    public class PetProjectUnitOfWork : UnitOfWork
    {
        public PetProjectUnitOfWork(DbContext dbContext) : base(dbContext) { }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            var typeEntity = typeof(TEntity);
            var result = GetRepositoryByTypeName<TEntity>(typeEntity.Name);
            if (result != null)
            {
                return (IGenericRepository<TEntity>)result;
            }
            var typeIRepository = typeof(IGenericRepository<TEntity>);
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .FirstOrDefault(t => typeIRepository.IsAssignableFrom(t)
                );
            if (type != null)
            {
                var value = Activator.CreateInstance(type, _dbContext);
                SetRepository<TEntity>(typeEntity.Name, value);
                return (IGenericRepository<TEntity>)value;
            }
            return base.GetRepository<TEntity>();
        }
    }
}
