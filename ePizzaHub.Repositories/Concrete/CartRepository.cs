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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ePizzaHubContext _ePizzaHubContext) : base(_ePizzaHubContext)
        {


        }

        public async Task<bool> DeleteItemAsync(Guid cartId, int itemId)
        {
            var items = await _ePizzaHubContext.CartItems.FirstOrDefaultAsync(x => x.ItemId == itemId && x.CartId == cartId);
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


        public async Task<Cart> GetCartDetailAsync(Guid cartId, bool persistChanges = false)
        {

            if (persistChanges)
            {
                return await _ePizzaHubContext.Carts
                               .Include(x => x.CartItems)
                               .ThenInclude(x => x.Item)
                            .Where(x => x.Id == cartId && x.IsActive == true)
                            .FirstOrDefaultAsync();
            }
            else
            {
                return await _ePizzaHubContext.Carts
                               .Include(x => x.CartItems)
                               .ThenInclude(x => x.Item)
                            .Where(x => x.Id == cartId && x.IsActive == true)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();

            }
        }

        public async Task<int> GetCartItemCountAsync(Guid guid)
        {
            int itemCount = await _ePizzaHubContext.CartItems.Where(x => x.CartId == guid).CountAsync();
            return itemCount;
        }

        public async Task<int> UpdateItemQuantity(Guid cartId, int ItemId, int quantity)
        {
            var currentItems = await _ePizzaHubContext.
                        CartItems.Where(x => x.CartId == cartId && x.ItemId == ItemId)
                        .FirstOrDefaultAsync();

            currentItems.Quantity = quantity;

            _ePizzaHubContext.Entry(currentItems).State = EntityState.Modified;

            return await _ePizzaHubContext.SaveChangesAsync();

        }

    }
}
    
    

