using ePizzaHub.Models.ApiModels.Response;
using ePizzaHub.UI.Helpers;
using ePizzaHub.UI.Helpers.ExtensionMethod;
using ePizzaHub.UI.Models.ApiModel.Response;
using ePizzaHub.UI.Models.ViewModels;
using ePizzaHub.UI.RazorPay;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.UI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IRazorPayService _razorpayService;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService tokenService;
        public PaymentController(
             IRazorPayService razorPayService,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ITokenService tokenService) {

            this._razorpayService = razorPayService;
            this._configuration = configuration;
            this._httpClientFactory = httpClientFactory;
            this.tokenService = tokenService;

        }
        public IActionResult Index()
        {
            PaymentModel payment =new PaymentModel ();

            GetCartResponseModel cart = TempData.Peek<GetCartResponseModel>("CartDetails");


            if (cart != null)
            {
                payment.RazorPayKey = _configuration["RazorPay:Key"];
                payment.Cart = cart;
                payment.GrandTotal = Math.Round(cart.GrandTotal);
                payment.Currency = "INR";
                payment.Description = "A PIZZA PAYMENT";
                payment.Receipt=Guid.NewGuid().ToString();

                payment.OrderId = _razorpayService.CreateOrder(payment.GrandTotal, payment.Currency, payment.Receipt);

                return View(payment);
            }
            return View();
        }
        public async Task<IActionResult> Status(IFormCollection form)
        {
            if (form.Keys.Count > 0)
            {
                string paymentId = form["rzp_paymentid"]!;
                string orderId = form["rzp_orderid"]!;
                string signature = form["rzp_signature"]!;
                string transactionId = form["Receipt"]!;
                string currency = form["Currency"]!;

                bool isSignatureValid = _razorpayService.VerifySignature(signature, orderId, paymentId);
                if (isSignatureValid)
                {
                    var payment = _razorpayService.GetPayment(paymentId);
                    string status = payment["status"];


                    // make payment api

                    var paymentModel = GetPaymentRequest(paymentId, orderId, transactionId, currency, status);
                    var client = _httpClientFactory.CreateClient("ePizzaAPI");
                    var paymentRequest = await client.PostAsJsonAsync("Payment", paymentModel);

                    Response.Cookies.Delete("CartId");
                    TempData.Remove("CartDetail");
                    TempData.Remove("Address");

                    TempData.Set("Paymentdetails", paymentModel);

                    return RedirectToAction("Receipt");
                }
               
            }
            ViewBag.Message = "PaymentFailed";
            return View();
        }
        private MakePaymentRequestModelDetails GetPaymentRequest(string paymentId, string orderId, string transactionId, string currency, string status)
        {
            GetCartResponseModel cart = TempData.Peek<GetCartResponseModel>("CartDetail");
            AddressViewModel address = TempData.Peek<AddressViewModel>("Address");

            return new MakePaymentRequestModelDetails()
            {
                CartId = cart.Id,
                Total = cart.Total,
                Tax = cart.Tax,
                GrandTotal = cart.GrandTotal,
                Currency = currency,
                CreatedDate = DateTime.UtcNow,
                Status = status,
                Email = CurrentUser.Email,
                UserId = CurrentUser.UserId,
                Id = paymentId,
                TransactionId = transactionId,
                OrderRequest = new OrderRequest()
                {
                    Id = orderId,
                    Street = address.Street,
                    City = address.City,
                    Locality = address.Locality,
                    PhoneNumber = address.PhoneNumber,
                    UserId = CurrentUser.UserId,
                    ZipCode = address.ZipCode,
                    OrderItems = GetOrderItems(cart.Items)
                }
            };
        }
        private List<OrderItems> GetOrderItems(List<CartItemResponse> items)
        {
            List<OrderItems> orderItems = [];


            foreach (CartItemResponse item in items)
            {
                OrderItems orderItem = new OrderItems()
                {
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    Total = item.ItemTotal,
                    UnitPrice = item.UnitPrice,
                };


                orderItems.Add(orderItem);
            }
            return orderItems;
        }
    }
}
