using Microsoft.AspNetCore.Mvc;
using bad4.Data;
using bad4.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bad4.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DispatchController : ControllerBase
    {
        private readonly BakeryDbContext _context;
        private readonly ILogger<DispatchController> _logger;
        private readonly UserManager<ApiUser> _userManager;
        public DispatchController(BakeryDbContext context, ILogger<DispatchController> logger, UserManager<ApiUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }


        // 5. Get the track ids corresponding to an order
        [Authorize(Policy = "IsDriver")]
        [HttpGet("GetTrackIdsOfOrderQuery5/{id}")]
        public async Task<ActionResult<object>> GetTrackIdsOfOrder(int id)
        {

            var packet = await _context.Dispatch.FirstOrDefaultAsync(i => i.TrackID == id);

            if (packet == null)
                return NotFound();

            var res =
                from orders in _context.CompanyOrders
                join pack in _context.Dispatch
                on orders.CompanyOrdersID equals pack.CompanyOrdersID
                where pack.TrackID == id
                select new
                {
                    TrackId = pack.TrackID,
                    Address = orders.DeliveryPlace,
                    Latitude = pack.Latitude,
                    Longitude = pack.Longitude
                };


            var userName = User.FindFirstValue(ClaimTypes.Name);

            var UserClaim = await _userManager.FindByNameAsync(userName);

            var UClaim = await _userManager.GetClaimsAsync(UserClaim);

            string date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            var logInfo = new { Operation = "Get", Date = date, UserInfo = UserClaim, Claim = UClaim };
            _logger.LogInformation("{@logInfo}", logInfo);

            return Ok(res);
        }
    }
}
