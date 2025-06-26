﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Models.ApiModels.Request
{
    public class MakePaymentRequest
    {
        public string Id { get; set; }

        public string TransactionId { get; set; } // will come from Razor Pay

        public decimal Tax { get; set; }

        public string Currency { get; set; }

        public decimal Total { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public Guid CartId { get; set; }

        public decimal GrandTotal { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public OrderRequest OrderRequest { get; set; }
    }
}
