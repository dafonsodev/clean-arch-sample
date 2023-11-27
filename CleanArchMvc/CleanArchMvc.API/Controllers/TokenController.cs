using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IAuthenticate authenticate;
    private readonly IConfiguration configuration;

    public TokenController(IAuthenticate authenticate, IConfiguration configuration)
    {
        this.authenticate = authenticate ??
            throw new ArgumentNullException(nameof(authenticate));
        this.configuration = configuration ??
            throw new ArgumentNullException(nameof(configuration));
    }

    [HttpPost("CreateUser")]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<UserToken>> Create([FromBody] RegisterModel registerModel)
    {
        var result = await authenticate.RegisterUserAsync(registerModel.Email, registerModel.Password);
        if (result)
        {
            return Ok($"User {registerModel.Email} was create successfully!");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid create attempt.");
            return BadRequest(ModelState);
        }
    }

    [AllowAnonymous]
    [HttpPost("LoginUser")]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel loginModel)
    {
        var result = await authenticate.AuthenticateAsync(loginModel.Email, loginModel.Password);
        if (result)
        {
            return GenerateToken(loginModel);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }
    }

    private UserToken GenerateToken(LoginModel loginModel)
    {
        //declarações do usuário
        var claims = new[]
        {
            new Claim("email", loginModel.Email),
            new Claim("meuvalor", "oque voce quiser"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //gerar chave privada para assinar o token
        var privateKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));

        //gerar a assinatura digital
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        //definir o tempo de expiração
        var expiration = DateTime.UtcNow.AddMinutes(10);

        //gerar o token
        JwtSecurityToken token = new(
            //emissor
            issuer: configuration["Jwt:Issuer"],
            //audiencia
            audience: configuration["Jwt:Audience"],
            //claims
            claims: claims,
            //data de expiracao
            expires: expiration,
            //assinatura digital
            signingCredentials: credentials
        );

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
