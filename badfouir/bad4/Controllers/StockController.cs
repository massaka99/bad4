using Microsoft.AspNetCore.Mvc;
using bad4.Data;
using bad4.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace bad4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class StockController : ControllerBase
    {
        private readonly BakeryDbContext _context;
        private readonly ILogger<StockController> _logger;
        private readonly UserManager<ApiUser> _userManager;
        public StockController(BakeryDbContext context, ILogger<StockController> logger, UserManager<ApiUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // Add new ingredient and quantity to stock
        [Authorize(Policy = "IsAdmin")]
        [HttpPost("add-ingredient-to-stock")]
        public async Task<ActionResult<object>> AddIngredientToStock(int quantity, string name)
        {
            if (quantity < 0 || string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var existingIngredient = _context.Stock.FirstOrDefault(s => s.Name.ToLower() == name.ToLower());
            if (existingIngredient != null)
            {
                existingIngredient.Quantity += quantity;
            }
            else
            {
                var newIngredient = new Stock
                {
                    Quantity = quantity,
                    Name = name
                };

                _context.Stock.Add(newIngredient);
            }

            await _context.SaveChangesAsync();

            var userName = User.FindFirstValue(ClaimTypes.Name);
            var UserClaim = await _userManager.FindByNameAsync(userName);
            var UClaim = await _userManager.GetClaimsAsync(UserClaim);
            string date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            var logInfo = new { Operation = "Post", Date = date, UserInfo = UserClaim, Claim = UClaim };
            _logger.LogInformation("{@logInfo}", logInfo);

            return Ok();
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpDelete("remove-ingredient-from-stock")]
        public async Task<ActionResult<object>> RemoveIngredientFromStock(int id)
        {
            var stock = _context.Stock.FirstOrDefault(s => s.StockID == id);

            if (stock == null)
            {
                return NotFound();
            }

            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();

            var userName = User.FindFirstValue(ClaimTypes.Name);
            var UserClaim = await _userManager.FindByNameAsync(userName);
            var UClaim = await _userManager.GetClaimsAsync(UserClaim);
            string date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            var logInfo = new { Operation = "Delete", Date = date, UserInfo = UserClaim, Claim = UClaim };
            _logger.LogInformation("{@logInfo}", logInfo);

            return Ok();
        }

        // 1. Get current stock
        [Authorize(Policy = "IsBaker")]
        [HttpGet("current-stock1")]
        public async Task<ActionResult<object>> GetCurrentStock()
        {
            var currentStock = await _context.GetCurrentStock().ToListAsync();
            return Ok(currentStock);
        }

        [Authorize(Policy = "IsManager")]
        [HttpPut("update-ingredient-quantity")]
        public async Task<ActionResult<object>> UpdateIngredientQuantity(int Stockid, int quantity)
        {
            var stock = _context.Stock.FirstOrDefault(s => s.StockID == Stockid);

            if (stock == null)
            {
                return NotFound();
            }

            if ((stock.Quantity + quantity) < 0)
            {
                return BadRequest();
            }

            stock.Quantity += quantity;
            await _context.SaveChangesAsync();

            var userName = User.FindFirstValue(ClaimTypes.Name);
            var UserClaim = await _userManager.FindByNameAsync(userName);
            var UClaim = await _userManager.GetClaimsAsync(UserClaim);
            string date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            var logInfo = new { Operation = "Get", Date = date, UserInfo = UserClaim, Claim = UClaim };
            _logger.LogInformation("{@logInfo}", logInfo);

            return Ok(stock);
        }
    }
}
