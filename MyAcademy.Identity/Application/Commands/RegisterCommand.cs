using MediatR;

namespace MyAcademy.Identity.Application.Commands;

public record RegisterCommand(string Email, string Password) : IRequest<bool>;