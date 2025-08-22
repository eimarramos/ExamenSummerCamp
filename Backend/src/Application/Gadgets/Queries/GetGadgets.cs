using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;

namespace Application.Gadgets.Queries
{
    public record GetGadgetsQuery(int pageNumber, int pageSize) : IRequest<PaginatedList<GetGadgetsDto>>;

    public class GetPaginatedApartmentsQueryHandler : IRequestHandler<GetGadgetsQuery, PaginatedList<GetGadgetsDto>>
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;

        public GetPaginatedApartmentsQueryHandler(IDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetGadgetsDto>> Handle(GetGadgetsQuery request, CancellationToken cancellationToken)
        {
            var apartments = await _context.Gadgets
                .AsNoTracking()
                .ProjectTo<GetGadgetsDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.pageNumber, request.pageSize);

            return apartments;
        }
    }
}

