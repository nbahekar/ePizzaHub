﻿using ePizzaHub.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Contract
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<bool> AddNewOrder(Order order);
    }
}
