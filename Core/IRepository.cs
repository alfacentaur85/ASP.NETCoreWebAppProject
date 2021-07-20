using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Core
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetByNameAsync(string name);
        Task<IEnumerable<T>> GetWithPaginationAsync(int skip, int take, string search);
        Task AddAsync(IEnumerable<T> item);
        Task UpdateAsync(IEnumerable<T> item);
        Task DeleteAsync(int id);

    }
}
