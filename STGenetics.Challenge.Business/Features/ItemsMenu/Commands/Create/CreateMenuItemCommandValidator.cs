using FluentValidation;

namespace STGenetics.Challenge.Business.Features.ItemsMenu.Commands.Create
{
    public class CreateMenuItemCommandValidator : AbstractValidator<CreateMenuItemCommand>
    {
        public CreateMenuItemCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome do item é obrigatório")
                .MaximumLength(255)
                .WithMessage("O nome do item não pode exceder 255 caracteres");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("O preço deve ser maior que 0");
        }
    }
}
