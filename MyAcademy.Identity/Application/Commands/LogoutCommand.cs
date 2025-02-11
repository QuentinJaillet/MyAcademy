using MediatR;

namespace MyAcademy.Identity.Application.Commands;

public record LogoutCommand() : IRequest;