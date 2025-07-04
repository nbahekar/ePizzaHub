﻿using AutoMapper;
using ePizzaHub.Infra.Models;
using ePizzaHub.Models.ApiModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Mapper
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            CreateMap<MakePaymentRequest, PaymentDetail>();
            CreateMap<OrderRequest, Order>();
            CreateMap<OrderItems, OrderItem>();

        }
    }
}
