using FluentValidation;
using HealthChecks.API.Entities;

namespace HealthChecks.API.Validators;

public sealed class ClientValidator : AbstractValidator<Client>
{
    public ClientValidator()
    {
        RuleFor(c => c.Name).Length(3, 200);

        RuleFor(c => c.Description).Length(10, 2000);
    }
}
