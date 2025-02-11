using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyAcademy.Identity.Application.Commands;
using MyAcademy.Identity.Infrastructure.Entities;

namespace MyAcademy.Identity.Application.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, string>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public LoginHandler(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _signInManager
                .PasswordSignInAsync(request.Email, request.Password, false, false)
                .ConfigureAwait(false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(request.Email).ConfigureAwait(false);
                var token = GenerateJwtToken(user);
                return token;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        throw new Exception("Invalid login attempt.");
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("70FC177F-3667-453D-9DA1-AF223DF6C014"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken("https://mon-site.com", "https://mon-site.com",
            claims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}