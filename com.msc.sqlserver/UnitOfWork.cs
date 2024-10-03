using com.msc.usecase.Interfaces;
using System;
using System.Data;
using System.Transactions;

namespace com.msc.sqlserver
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbContext context;
        private readonly ITareaRepository tareaRepository;
        private TransactionScope transaction;
        private bool disposed = false;

        public UnitOfWork(IDbContext _context, ITareaRepository _tareaRepository)
        {
            context = _context;
            tareaRepository = _tareaRepository;
        }

        public ITareaRepository TareaRepository => tareaRepository;

        public void BeginTransaction()
        {
            transaction = new TransactionScope();
        }

        public void CommitTransaction()
        {
            transaction.Complete();
            Dispose();
        }

        public void RollBackTransaction()
        {
            foreach (var entry in (context as SqlContext).ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (transaction != null) transaction.Dispose();
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
