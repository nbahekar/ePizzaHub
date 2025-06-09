using ePizzaHub.UI.Models.ApiModel.Response;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DashboardController(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult>  Index()
        {
            var client=_httpClientFactory.CreateClient("ePizzaAPI");
            var items = await client.GetFromJsonAsync<ApiResponseModel<IEnumerable<ItemResponseModel>>>("Item");

            if (items.Success)
            {
                return View(items.Data);
            }
            else
            return View();
        }
    }
}
