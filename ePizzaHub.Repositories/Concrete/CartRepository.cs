using ePizzaHub.Infra.Models;
using ePizzaHub.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Concrete
{
    public class CartRepository :GenericRepository<Cart>, ICartRepository
    {
         public CartRepository(ePizzaHubContext _ePizzaHubContext):base(_ePizzaHubContext) { 
        
            
        }

        public async Task<int> GetCartItemCountAsync(Guid guid)
        {
            int itemCount= await _ePizzaHubContext.CartItems.Where(x=>x.CartId== guid).CountAsync();
            return itemCount;
        }
    }
}
