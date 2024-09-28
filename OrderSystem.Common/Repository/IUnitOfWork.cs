namespace OrderSystem.Common.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task<int> CompleteAsync();
    }
}
