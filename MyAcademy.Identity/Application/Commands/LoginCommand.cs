using MediatR;

namespace MyAcademy.Identity.Application.Commands;

public record LoginCommand(string Email, string Password) : IRequest<string>;