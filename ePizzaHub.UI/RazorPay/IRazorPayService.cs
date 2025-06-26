﻿using Razorpay.Api;

namespace ePizzaHub.UI.RazorPay
{
    public interface IRazorPayService
    {
        string CreateOrder(decimal amount, string currency, string receipt);

        Payment GetPayment(string paymentId);

        bool VerifySignature(string signature, string orderId, string paymentId);
    }
}
