using System.Collections.Generic;
using System.Threading.Tasks;


namespace Core
{
    public interface IRepositorySecurity<T, R> where T : class where R : class
    {
        Task<IReadOnlyList<T>> GetByLoginPasswordAsync(string login, string password);

    }
}
