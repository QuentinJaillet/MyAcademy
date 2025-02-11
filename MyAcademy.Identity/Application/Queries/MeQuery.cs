using MediatR;
using MyAcademy.Identity.Domain;

namespace MyAcademy.Identity.Application.Queries;

public record MeQuery : IRequest<UserInformations>;