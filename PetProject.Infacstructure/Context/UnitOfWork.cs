using Microsoft.EntityFrameworkCore;
using System.Transactions;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using PetProject.Repositories.Common;
using PetProject.Domain.Interfaces;
using PetProject.Domain.Entities;

namespace PetProject.Infacstructure.Context;

public class UnitOfWork : RepositoryFactory, IUnitOfWork
{
    protected DbContext _dbContext { get; private set; }
    private bool _disposed;

    public UnitOfWork(
        PetProjectContext context,
        ICountryRepository countryRepository,
        IDateTimeFormatRepository dateTimeFormatRepository,
        IFeatureRepository featureRepository,
        IRoleFeatureRepository roleFeatureRepository,
        IRoleRepository roleRepository,
        ITimeZoneRepository timeZoneRepository,
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository
    ) : base(
             countryRepository,
             dateTimeFormatRepository,
             featureRepository,
             roleFeatureRepository,
             roleRepository,
             timeZoneRepository,
             userRepository,
             userRoleRepository
         )
    {
        _dbContext = context;
    }

    #region Dispose        
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // The bulk of the clean-up code is implemented in Dispose(bool)
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        _disposed = true;
    }
    #endregion

    public virtual int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }

    public virtual Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    public virtual TResult ExecuteTransaction<TResult>(Func<TResult> func) where TResult : class
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();
        return strategy.Execute(() =>
        {
            using var transaction = new TransactionScope();
            var result = func.Invoke();
            transaction.Complete();
            return result;
        });
    }

    public virtual void ExecuteTransaction(Action action)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();
        strategy.Execute(() =>
        {
            using var transaction = new TransactionScope();
            action.Invoke();
            transaction.Complete();
        });
    }

    public virtual Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func) where TResult : class
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();
        return strategy.ExecuteAsync(async () =>
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await func.Invoke().ConfigureAwait(false);
                transaction.Complete();
                return result;
            }
        });
    }

    public virtual Task ExecuteTransactionAsync(Func<Task> action)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();
        return strategy.ExecuteAsync(async () =>
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await action.Invoke().ConfigureAwait(false);
                transaction.Complete();
            }
        });
    }


}

