using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface IRepositoryPD<T, R> where T : class where R : class
    {
        Task AddAsync(IReadOnlyList<R> item);
        Task DeleteAsync(int PersonId, int DepartmentId);
        Task UpdateAsync(IReadOnlyList<R> item);

    }
}
