using MediatR;
using MyAcademy.Identity.Application.DTOs;

namespace MyAcademy.Identity.Application.Commands;

public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;