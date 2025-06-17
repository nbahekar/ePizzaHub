﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Models.ApiModels.Request
{
    public class UpdateCartItemRequest
    {
        public Guid CardId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
