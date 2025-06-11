using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        Task<T> AddAsync(T item);

        // Task<T> UpdateAsync(T item);

        //Task<T> DeleteAsync(T item);

        void UpdateAsync(T item);

        Task<int> commitAsync();
    }
}
