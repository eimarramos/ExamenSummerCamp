using Application.Gadgets.Queries.GetGatgetById;

namespace Application.Gadgets.Queries.GetGadgets
{
    public class GetGadgetByIdQueryValidator : AbstractValidator<GetGadgetByIdQuery>
    {
        public GetGadgetByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");
        }
    }
}
