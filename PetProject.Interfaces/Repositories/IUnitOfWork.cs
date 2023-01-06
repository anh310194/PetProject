namespace PetProject.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable, IRepositoryFactory
{
    Task<int> SaveChangesAsync();
    int SaveChanges();
    TResult ExecuteTransaction<TResult>(Func<TResult> func) where TResult : class;
    Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func) where TResult : class;
    void ExecuteTransaction(Action action);
    Task ExecuteTransactionAsync(Func<Task> action);
}
