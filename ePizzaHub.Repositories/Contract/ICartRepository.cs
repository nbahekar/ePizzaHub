﻿using ePizzaHub.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Contract
{
    public interface ICartRepository:IGenericRepository<Cart>
    {
        public Task<int> GetCartItemCountAsync(Guid guid);

        public Task<Cart> GetCartDetailsAsync(Guid cartid);

        Task<bool> DeleteItemAsync(Guid cartId, int itemId);
    }
}
