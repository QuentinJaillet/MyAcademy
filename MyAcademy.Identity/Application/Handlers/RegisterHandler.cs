using MediatR;
using Microsoft.AspNetCore.Identity;
using MyAcademy.Identity.Application.Commands;
using MyAcademy.Identity.Infrastructure.Entities;

namespace MyAcademy.Identity.Application.Handlers;

public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly UserManager<User> _userManager;

    public RegisterHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.Email, 
            Email = request.Email
        };
        
        var result = await _userManager.CreateAsync(user, request.Password);

        return result.Succeeded;
    }
}