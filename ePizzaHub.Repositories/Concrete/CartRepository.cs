using ePizzaHub.Infra.Models;
using ePizzaHub.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Concrete
{
    public class CartRepository :GenericRepository<Cart>, ICartRepository
    {
         public CartRepository(ePizzaHubContext _ePizzaHubContext):base(_ePizzaHubContext) { 
        
            
        }

        public async Task<bool> DeleteItemAsync(Guid cartId, int itemId)
        {
            var items = await _ePizzaHubContext.CartItems.FirstOrDefaultAsync(x=>x.ItemId==itemId && x.CartId == cartId);
            if (items != null)
            {
                _ePizzaHubContext.CartItems.Remove(items);
                int recordsDeleted = await commitAsync();
                return recordsDeleted > 0;
            }
            return false;
        }

        public async Task<Cart> GetCartDetailsAsync(Guid cartid)
        {
            return await _ePizzaHubContext.Carts
                           .Include(x => x.CartItems)
                           .ThenInclude(x => x.Item)
                        .Where(x => x.Id == cartid && x.IsActive == true)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

            //FirstAsync(c => c.Id == id);
        }

        public async Task<int> GetCartItemCountAsync(Guid guid)
        {
            int itemCount= await _ePizzaHubContext.CartItems.Where(x=>x.CartId== guid).CountAsync();
            return itemCount;
        }
    }
}
