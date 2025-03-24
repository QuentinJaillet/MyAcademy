using MediatR;
using Microsoft.AspNetCore.Identity;
using MyAcademy.Identity.Application.Commands;
using MyAcademy.Identity.Infrastructure.Entities;

namespace MyAcademy.Identity.Application.Handlers;

public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly ILogger<RegisterHandler> _logger;
    private readonly UserManager<User> _userManager;

    public RegisterHandler(
        ILogger<RegisterHandler> logger,
        UserManager<User> userManager)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering user with email {Email}", request.Email);

        var user = new User
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        await _userManager.AddToRoleAsync(user, "User");

        _logger.LogInformation("User with email {Email} registered", request.Email);

        return result.Succeeded;
    }
}