using Microsoft.EntityFrameworkCore;
using PetProject.Domain.Interfaces;
using PetProject.Specification.Common;

namespace PetProject.Specification
{
    public class PetProjectUnitOfWork : UnitOfWork
    {
        public PetProjectUnitOfWork(DbContext dbContext) : base(dbContext) { }

        public override IBaseRepository<TEntity> GetRepository<TEntity>()
        {
            var typeEntity = typeof(TEntity);
            var result = GetRepositoryByTypeName<TEntity>(typeEntity.Name);
            if (result !=null)
            {
                return (IBaseRepository<TEntity>)result;
            }
            var typeIRepository = typeof(IBaseRepository<TEntity>);
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .FirstOrDefault(t => typeIRepository.IsAssignableFrom(t)
                );
            if (type != null)
            {
                var value = Activator.CreateInstance(type, _dbContext);
                SetRepository(typeEntity.Name, value);
            }
            return base.GetRepository<TEntity>();
        }
    }
}
