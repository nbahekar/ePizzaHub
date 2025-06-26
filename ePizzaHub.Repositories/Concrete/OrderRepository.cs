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
    public class OrderRepository:GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ePizzaHubContext ePizzaHubContext) : base(ePizzaHubContext)
        {
        }

        public async Task<bool> AddNewOrder(Order order)
        {
            _ePizzaHubContext.Orders.Add(order);
            int rowsAffected = await _ePizzaHubContext.SaveChangesAsync();

            return rowsAffected > 0;

        }
    }
}
