using FluentValidation;

namespace Application.Gadgets.Queries.GetGadgets
{
    public class GetGadgetsQueryValidator : AbstractValidator<GetGadgetsQuery>
    {
        public GetGadgetsQueryValidator()
        {
            RuleFor(x => x.pageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("El número de página debe ser mayor o igual a 1.");

            RuleFor(x => x.pageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("El tamaño de página debe ser mayor o igual a 1.")
                .LessThan(100)
                .WithMessage("El tamaño de página no debe ser mayor a 100.");
        }
    }
}
