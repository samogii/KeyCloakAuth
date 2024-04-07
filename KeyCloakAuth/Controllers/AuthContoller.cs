using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KeyCloakAuthenticate;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
namespace KeyCloakAuth.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController(IKeycloakUserManagement cloak) : ControllerBase
{

    [HttpPost("token")]
    public async Task<IActionResult> GetToken(string username = "sam", string password = "123456")
    {
        var x = new CustomKeyCloak();
        var token = await x.GetTokenAsync(username, password);
        return Ok(token);
    }

    
    [HttpGet("user")]
    public async Task<IActionResult> GetUser(string username)
    {
        var x = new CustomKeyCloak();
        var z = await cloak.GetUserProfileAsync(username);
        return Ok(z);
    }
    [HttpGet("")]
    [Authorize]
    public IActionResult GetUser()
    {
        List<Claim> roleClaims = HttpContext.User.FindAll(ClaimTypes.Role).ToList();

        ClaimsPrincipal user = this.User;
        var roles = new List<string>();

        foreach (var role in roleClaims)
        {
            roles.Add(role.Value);
        }
        return Ok(user.Identity.Name);
    }
    [HttpGet("policy")]
    [Authorize(Roles = "manage-account")]
    public IActionResult Test()
    {
        return Ok("Has Role");
    }
    [HttpGet("admin")]
    public IActionResult Test3()
    {
        return Ok("Has Role");
    }
}
