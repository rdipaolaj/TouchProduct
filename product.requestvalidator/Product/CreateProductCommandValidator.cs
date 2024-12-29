using FluentValidation;
using product.request.Commands.v1.Product;

namespace product.requestvalidator.Product;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre del producto no debe exceder los 100 caracteres.");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción del producto es obligatoria.")
            .MaximumLength(500).WithMessage("La descripción del producto no debe exceder los 500 caracteres.");

        RuleFor(x => x.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser mayor que 0.");

        RuleFor(x => x.Cantidad)
            .GreaterThanOrEqualTo(0).WithMessage("La cantidad debe ser mayor o igual a 0.");

        RuleFor(x => x.Categoria)
            .NotEmpty().WithMessage("La categoría del producto es obligatoria.")
            .MaximumLength(50).WithMessage("La categoría no debe exceder los 50 caracteres.");
    }
}