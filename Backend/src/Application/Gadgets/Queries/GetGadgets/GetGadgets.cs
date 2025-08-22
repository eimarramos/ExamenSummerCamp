using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;

namespace Application.Gadgets.Queries.GetGadgets
{
    public record GetGadgetsQuery(int pageNumber, int pageSize, string? filterString) : IRequest<PaginatedList<GetGadgetsDto>>;

    public class GetGadgetsQueryHandler : IRequestHandler<GetGadgetsQuery, PaginatedList<GetGadgetsDto>>
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;

        public GetGadgetsQueryHandler(IDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetGadgetsDto>> Handle(GetGadgetsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Gadgets.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.filterString))
            {
                var filter = request.filterString.Trim().ToLower();
                query = query.Where(g => 
                    g.Name.ToLower().Contains(filter) ||
                    g.Brand.ToLower().Contains(filter) ||
                    g.Category.ToLower().Contains(filter) ||
                    g.Id.ToString().Contains(filter) ||
                    g.Price.ToString().Contains(filter) ||
                    g.ReleaseDate.ToString().ToLower().Contains(filter) ||
                    g.IsAvailable.ToString().ToLower().Contains(filter));
            }

            var gadgets = await query
                .ProjectTo<GetGadgetsDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.pageNumber, request.pageSize);

            return gadgets;
        }
    }
}

