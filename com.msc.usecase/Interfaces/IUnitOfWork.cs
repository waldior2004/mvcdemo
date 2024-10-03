namespace com.msc.usecase.Interfaces
{
    public interface IUnitOfWork
    {
        ITareaRepository TareaRepository { get; }

        void BeginTransaction();

        void CommitTransaction();

        void RollBackTransaction();

        void Dispose();
    }
}
