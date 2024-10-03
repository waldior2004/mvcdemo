using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.msc.usecase.Interfaces
{
    public interface IRepository<S, T>
    {
        Task<IEnumerable<S>> Get();
        Task<S> Get(int id);
        Task<int> Add(T obj);
        Task<int> Edit(T obj);
        Task<int> Delete(int id);
    }

    public interface IRepository<S>
    {
        Task<IEnumerable<S>> Get();
        Task<S> Get(int id);
        Task<int> Add(S obj);
        Task<int> Edit(S obj);
        Task<int> Delete(int id);
    }
}