using ePizzaHub.Core.Interface;
using ePizzaHub.Models.ApiModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        IItemService _itemService { get; set; }
        public  ItemController(IItemService itemService) {

            _itemService= itemService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var item= await _itemService.GetItemsAsync();

            ApiResponseModel<IEnumerable<GetItemResponse>> responseFormate = new
                Models.ApiModels.Response.ApiResponseModel<IEnumerable<GetItemResponse>>(true, item, "Record Fetched");
            return Ok(responseFormate.Data);
        }
    }
}
