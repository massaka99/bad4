using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using bad4.Data;
using bad4.Models;
using bad4.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ApiUserBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly BakeryDbContext _db;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _signingKey;
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        public AccountController(
                BakeryDbContext db,
                ILogger<AccountController> logger,
                IConfiguration configuration,
                UserManager<ApiUser> userManager,
                SignInManager<ApiUser> signInManager)
        {
            _db = db;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _signingKey = _configuration["JWT:SigningKey"];
        }

        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterDTO input, string registerClaim)
        {
            if (registerClaim != "IsAdmin" && registerClaim != "IsManager" && registerClaim != "IsBaker" && registerClaim != "IsDriver")
            {
                return BadRequest("Invalid claim.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = new ApiUser
                    {
                        UserName = input.Email,
                        Email = input.Email,
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(newUser, input.Password);

                    if (result.Succeeded)
                    {
                        var claim = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, newUser.UserName)
                        };

                        switch (registerClaim)
                        {
                            case "IsAdmin":
                                claim.AddRange(new[]
                                {
                                    new Claim("IsAdmin", "true"),
                                    new Claim("IsManager", "true"),
                                    new Claim("IsDriver", "true"),
                                    new Claim("IsBaker", "true")
                                });
                                break;
                            case "IsManager":
                                claim.AddRange(new[]
                                {
                                    new Claim("IsManager", "true"),
                                    new Claim("IsDriver", "true"),
                                    new Claim("IsBaker", "true")
                                });
                                break;
                            case "IsBaker":
                                claim.Add(new Claim("IsBaker", "true"));
                                break;
                            case "IsDriver":
                                claim.Add(new Claim("IsDriver", "true"));
                                break;
                        }

                        await _userManager.AddClaimsAsync(newUser, claim);
                        _logger.LogInformation("User {userName} ({email}) has been created.", newUser.UserName, newUser.Email);
                        return StatusCode(StatusCodes.Status201Created, $"User '{newUser.UserName}' has been created.");
                    }
                    else
                    {
                        return BadRequest(result.Errors.Select(e => e.Description));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, "Error occurred while saving entity changes.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the entity changes.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An unexpected error occurred.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginDTO input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (input == null || input.UserName == null || input.Password == null)
                    {
                        return BadRequest("Input, username, or password cannot be null.");
                    }
                    var user = await _userManager.FindByNameAsync(input.UserName);
                    if (user == null || !await _userManager.CheckPasswordAsync(user, input.Password))
                        throw new Exception("Invalid login attempt.");

                    var signingCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(_signingKey)),
                            SecurityAlgorithms.HmacSha256);

                    var claims = await _userManager.GetClaimsAsync(user);

                    var jwtObject = new JwtSecurityToken(
                        issuer: _configuration["JWT:Issuer"],
                        audience: _configuration["JWT:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(7),
                            signingCredentials: signingCredentials);

                    var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtObject);

                    return StatusCode(StatusCodes.Status200OK, jwtString);
                }
                else
                {
                    var details = new ValidationProblemDetails(ModelState);
                    details.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    details.Status = StatusCodes.Status400BadRequest;
                    return new BadRequestObjectResult(details);
                }
            }
            catch (Exception e)
            {
                var exceptionDetails = new ProblemDetails();
                exceptionDetails.Detail = e.Message;
                exceptionDetails.Status = StatusCodes.Status401Unauthorized;
                exceptionDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
                return StatusCode(StatusCodes.Status401Unauthorized, exceptionDetails);
            }
        }

    }
}