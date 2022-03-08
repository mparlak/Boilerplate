using Boilerplate.Application.Dtos.Product;
using FluentValidation;

namespace Boilerplate.Application.Validations.Product;

public class CreateProductValidator:AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(v => v.Id)
            //.MaximumLength(200)
            .NotEmpty();
    }
}