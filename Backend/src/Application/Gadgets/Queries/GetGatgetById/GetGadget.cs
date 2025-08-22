using Application.Common.Interfaces;

namespace Application.Gadgets.Queries.GetGatgetById
{
    public record GetGadgetQuery(int Id) : IRequest<GetGadgetDto?>;

    public class GetGadgetQueryHandler : IRequestHandler<GetGadgetQuery, GetGadgetDto?>
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;

        public GetGadgetQueryHandler(IDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetGadgetDto?> Handle(GetGadgetQuery request, CancellationToken cancellationToken)
        {
            var gadget = await _context.Gadgets
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

            var gadgetDto = _mapper.Map<GetGadgetDto>(gadget);

            return gadgetDto;
        }
    }
}
