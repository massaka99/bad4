using Microsoft.AspNetCore.Mvc;
using bad4.Data;
using bad4.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace bad4.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BakingGoodsController : ControllerBase
    {
        private readonly BakeryDbContext _context;
        private readonly ILogger<BakingGoodsController> _logger;
        private readonly UserManager<ApiUser> _userManager;
        public BakingGoodsController(BakeryDbContext context, ILogger<BakingGoodsController> logger, UserManager<ApiUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // 3. Get the list of baked goods in an order
        [Authorize(Policy = "IsDriver")]
        [HttpGet("GetBakingGoodsInOrderQuery3/{orderId}")]
        public async Task<ActionResult<object>> GetBakingGoodsInOrder(int orderId)
        {
            var bakingGoods = _context.BakingGoods.Where(bg => bg.CompanyOrdersID == orderId).ToList();

            if (bakingGoods == null)
            {
                return NotFound();
            }
            var userName = User.FindFirstValue(ClaimTypes.Name);

            var UserClaim = await _userManager.FindByNameAsync(userName);

            var UClaim = await _userManager.GetClaimsAsync(UserClaim);

            string date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            var logInfo = new { Operation = "Get", Date = date, UserInfo = UserClaim, Claim = UClaim };
            _logger.LogInformation("{@logInfo}", logInfo);

            // Update logging information
            _logger.LogInformation($"User {userName} accessed GetBakingGoodsInOrder endpoint at {date} with claims: {string.Join(", ", UClaim.Select(c => $"{c.Type}: {c.Value}"))}");

            return Ok(bakingGoods);
        }
    }
}
