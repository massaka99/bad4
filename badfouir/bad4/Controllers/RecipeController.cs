using Microsoft.AspNetCore.Mvc;
using bad4.Data;
using bad4.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bad4.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RecipeController : ControllerBase
    {
        private readonly BakeryDbContext _context;
        private readonly ILogger<RecipeController> _logger;
        private readonly UserManager<ApiUser> _userManager;
        public RecipeController(BakeryDbContext context, ILogger<RecipeController> logger, UserManager<ApiUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
    }
}
