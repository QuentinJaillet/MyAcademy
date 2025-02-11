using MediatR;
using Microsoft.AspNetCore.Identity;
using MyAcademy.Identity.Application.Commands;
using MyAcademy.Identity.Infrastructure.Entities;

namespace MyAcademy.Identity.Application.Handlers;

public class LogoutHandler : IRequestHandler<LogoutCommand>
{
    private readonly SignInManager<User> _signInManager;

    public LogoutHandler(SignInManager<User> signInManager)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync().ConfigureAwait(false);
    }
}