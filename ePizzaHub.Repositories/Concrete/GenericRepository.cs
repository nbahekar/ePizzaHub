using ePizzaHub.Infra.Models;
using ePizzaHub.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ePizzaHubContext _ePizzaHubContext;

        public GenericRepository(ePizzaHubContext ePizzaHubContext)
        {
            _ePizzaHubContext= ePizzaHubContext;
        }

        public async Task<T> AddAsync(T item)
        {
            await _ePizzaHubContext.Set<T>().AddAsync(item);
            return item;
        }

        public async Task<int> commitAsync()
        {
            return await _ePizzaHubContext.SaveChangesAsync();


        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _ePizzaHubContext.Set<T>();

            return query.ToList();
        }

        public void UpdateAsync(T item)
        {
            _ePizzaHubContext.Set<T>().Update(item);
        }

        public async Task<IEnumerable<T>> GetAllAync(Expression<Func<T, bool>> filter=null)
        {
            IQueryable<T> query = _ePizzaHubContext.Set<T>();
            if (filter!=null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();

        }

        //public Task<T> AddAsync(T item)
        //{

        //    await _ePizzaHubContext.Set<T>().AddAsync(item);
        //    return item;
        //}

        //public int commit()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Delete(T item)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(T item)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
