using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MyAcademy.Identity.Application.Queries;
using MyAcademy.Identity.Domain;
using MyAcademy.Identity.Infrastructure.Entities;

namespace MyAcademy.Identity.Application.Handlers;

public class MeHandler : IRequestHandler<MeQuery, UserInformations>
{
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MeHandler(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public async Task<UserInformations> Handle(MeQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new Exception("Token invalide");

        var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
        if (user == null)
            throw new Exception("Utilisateur non trouvé");

        var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

        return new UserInformations
        {
            Id = user.Id,
            Email = user.Email,
            Roles = roles.ToList()
        };
    }
}