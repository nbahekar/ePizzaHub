using AutoMapper;
using ePizzaHub.Core.Interface;
using ePizzaHub.Infra.Models;
using ePizzaHub.Models.ApiModels.Request;
using ePizzaHub.Repositories.Concrete;
using ePizzaHub.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public PaymentService(
           IPaymentRepository paymentRepository,
           IOrderRepository orderRepository,
           IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<string> MakePayment(MakePaymentRequest paymentRequest)
        {
            var paymentModel = _mapper.Map<PaymentDetail>(paymentRequest);

            if (paymentRequest.OrderRequest is not null
                    && paymentRequest.OrderRequest.OrderItems.Count > 0)
            {
                var orderDetails = MapOrderDetails(paymentRequest, paymentModel);

                await _paymentRepository.AddAsync(paymentModel);

                await _orderRepository.AddNewOrder(orderDetails);

                int rowsAffected = await _paymentRepository.commitAsync();

                return rowsAffected.ToString();
            }
            return string.Empty;
        }

        private Order MapOrderDetails(
            MakePaymentRequest paymentRequest,
            PaymentDetail paymentModel)
        {
            var orderDetails = _mapper.Map<Order>(paymentRequest.OrderRequest);

            orderDetails.PaymentId = paymentModel.Id;
            orderDetails.UserId = paymentModel.UserId;
            orderDetails.CreatedDate = paymentModel.CreatedDate;

            orderDetails.OrderItems.ToList().ForEach(x => x.OrderId = orderDetails.Id);

            return orderDetails;
        }
    }
}
