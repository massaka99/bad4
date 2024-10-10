using Microsoft.AspNetCore.Mvc;
using bad4.Data;
using bad4.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace bad4.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly BakeryDbContext _context;
        private readonly ILogger<OrdersController> _logger;
        private readonly UserManager<ApiUser> _userManager;
        public OrdersController(BakeryDbContext context, ILogger<OrdersController> logger, UserManager<ApiUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // 2. Get the address and date for an order
        [Authorize(Policy = "IsDriver")]
        [HttpGet("OrderDetailsQuery2")]
        public async Task<ActionResult<object>> GetOrderDetails()
        {
            var orderDetails = await _context.CompanyOrders.Select(o => new
            {
                o.DeliveryPlace,
                o.DeliveryDate
            }).ToListAsync();

            var userName = User.FindFirstValue(ClaimTypes.Name);

            var UserClaim = await _userManager.FindByNameAsync(userName);

            var UClaim = await _userManager.GetClaimsAsync(UserClaim);

            string date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            var logInfo = new { Operation = "Get", Date = date, UserInfo = UserClaim, Claim = UClaim };
            _logger.LogInformation("{@logInfo}", logInfo);

            return Ok(orderDetails);
        }

        //6. Produce a table containing the quantities of each of the baking goods in all the orders 
        [Authorize(Policy = "IsManager")]
        [HttpGet("BakingGoodsInOrdersQuery6")]
        public async Task<ActionResult<object>> GetBakingGoodsInOrders()
        {
            var bakingGoodsInOrders = await _context.BakingGoods
                .GroupBy(bg => bg.Name)
                .Select(bg => new
                {
                    BakingGoodsName = bg.Key,
                    TotalAmount = bg.Sum(bg => bg.Quantity)
                })
                .OrderBy(bg => bg.TotalAmount)
                .ToListAsync();

            var userName = User.FindFirstValue(ClaimTypes.Name);

            var UserClaim = await _userManager.FindByNameAsync(userName);

            var UClaim = await _userManager.GetClaimsAsync(UserClaim);

            string date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");

            var logInfo = new { Operation = "Get", Date = date, UserInfo = UserClaim, Claim = UClaim };
            _logger.LogInformation("{@logInfo}", logInfo);

            return Ok(bakingGoodsInOrders);
        }
    }
}
