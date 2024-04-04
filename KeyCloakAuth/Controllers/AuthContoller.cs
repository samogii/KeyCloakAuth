using Keycloak.Net;
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
public class AuthController : ControllerBase
{
    [HttpPost("token")]
    public async Task<IActionResult> GetToken(string username, string password)
    {
        var x = new CustomKeyCloak();
        
        return Ok(await x.GetTokenAsync(username, password));
    }
    [HttpGet("")]
    [Authorize(Roles ="admin")]
    public IActionResult GetUser()
    {
        ClaimsPrincipal user = this.User;

        return Ok(user.Identity.Name);
    }
}
