﻿using System.Collections.Generic;
using System.Threading.Tasks;


namespace Core
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetByNameAsync(string name);
        Task<IReadOnlyList<T>> GetWithPaginationAsync(int skip, int take, string search);
        Task AddAsync(IReadOnlyList<T> item);
        Task UpdateAsync(IReadOnlyList<T> item);
        Task DeleteAsync(int id);

    }
}
