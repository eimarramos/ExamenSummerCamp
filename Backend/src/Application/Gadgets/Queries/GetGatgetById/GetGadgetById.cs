using Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Application.Gadgets.Queries.GetGatgetById
{
    public record GetGadgetByIdQuery(int Id) : IRequest<GetGadgetByIdDto?>;

    public class GetGadgetByIdHandler : IRequestHandler<GetGadgetByIdQuery, GetGadgetByIdDto?>
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;

        public GetGadgetByIdHandler(IDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetGadgetByIdDto?> Handle(GetGadgetByIdQuery request, CancellationToken cancellationToken)
        {
            var gadget = await _context.Gadgets
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

            Guard.Against.NotFound(request.Id, gadget);

            var gadgetDto = _mapper.Map<GetGadgetByIdDto>(gadget);

            return gadgetDto;
        }
    }
}
