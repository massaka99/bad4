using Microsoft.AspNetCore.Mvc;
using bad4.Data;
using bad4.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace bad4.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BatchController : ControllerBase
    {
        private readonly BakeryDbContext _context;
        private readonly ILogger<BatchController> _logger;
        private readonly UserManager<ApiUser> _userManager;
        public BatchController(BakeryDbContext context, ILogger<BatchController> logger, UserManager<ApiUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // 4. Get the ingredients in batch
        [Authorize(Policy = "IsBaker")]
        [HttpGet("IngredientsInBatchQuery4/{id}")]
        public async Task<ActionResult<object>> GetIngredientsAndAllergensFromBatch(int id)
        {
            var BATCH = await _context.Batch.FirstOrDefaultAsync(i => i.BatchID == id);

            if (BATCH == null)
                return NotFound();

            var BAKINGGOODS =
                from bakingGoods in _context.BakingGoods
                join batch in _context.Batch
                on bakingGoods.BakingGoodsID equals batch.BakingGoodsID
                where batch.BatchID == id
                select new
                {
                    bakingGoodsId = bakingGoods.BakingGoodsID,
                    recipeId = bakingGoods.RecipeID
                };

            var RECIPE =
                from recipe in _context.Recipe
                join bg in BAKINGGOODS
                on recipe.RecipeID equals bg.recipeId
                select new
                {
                    recipeId = recipe.RecipeID
                };
            var INGREDIENT =
                from ingredient in _context.Ingredients
                join re in RECIPE
                on ingredient.RecipeID equals re.recipeId
                select new
                {
                    stockId = ingredient.StockID,
                    Quantity = ingredient.Quantity
                };
            var STOCK =
                from stock in _context.Stock
                join ing in INGREDIENT
                on stock.StockID equals ing.stockId
                select new
                {
                    ingredientName = stock.Name,
                    stockId = ing.stockId,
                    Quantity = ing.Quantity
                };
            var ALLERGEN =
                from allergen in _context.Allergen
                join stk in STOCK
                on allergen.StockID equals stk.stockId
                select new
                {
                    ingredientName = stk.ingredientName,
                    Quantity = stk.Quantity,
                    allergens = allergen
                };

            var userName = User.FindFirstValue(ClaimTypes.Name);

            var UserClaim = await _userManager.FindByNameAsync(userName);

            var UClaim = await _userManager.GetClaimsAsync(UserClaim);

            string date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            var logInfo = new { Operation = "Get", Date = date, UserInfo = UserClaim, Claim = UClaim };
            _logger.LogInformation("{@logInfo}", logInfo);

            return Ok(ALLERGEN);
        }

        // 7. Get the average delay for all the batches
        [Authorize(Policy = "IsBaker")]
        [HttpGet("AverageDelayQuery7")]
        public async Task<ActionResult<object>> GetAverageDelay()
        {
            var averageDelay = await _context.Batch.AverageAsync(b => b.Delay);

            var userName = User.FindFirstValue(ClaimTypes.Name);

            var UserClaim = await _userManager.FindByNameAsync(userName);

            var UClaim = await _userManager.GetClaimsAsync(UserClaim);

            string date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");

            var logInfo = new { Operation = "Get", Date = date, UserInfo = UserClaim, Claim = UClaim };
            _logger.LogInformation("{@logInfo}", logInfo);

            return Ok(averageDelay);
        }

    }
}
