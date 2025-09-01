using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Microsoft.Extensions.Logging;

namespace Application.Gadgets.Queries.GetGadgets
{
    public record GetGadgetsQuery(int pageNumber, int pageSize, string? filterString) : IRequest<PaginatedList<GetGadgetsDto>>;

    public class GetGadgetsQueryHandler : IRequestHandler<GetGadgetsQuery, PaginatedList<GetGadgetsDto>>
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetGadgetsQueryHandler> _logger;

        public GetGadgetsQueryHandler(IDatabaseContext context, IMapper mapper, ILogger<GetGadgetsQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedList<GetGadgetsDto>> Handle(GetGadgetsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetGadgetsQuery: pageNumber={PageNumber}, pageSize={PageSize}, filterString={FilterString}", request.pageNumber, request.pageSize, request.filterString);
            try
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
                    .OrderBy(g => g.Id)
                    .ProjectTo<GetGadgetsDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.pageNumber, request.pageSize);

                if (gadgets.Items == null || !gadgets.Items.Any())
                {
                    _logger.LogWarning("No gadgets found for the given query parameters: pageNumber={PageNumber}, pageSize={PageSize}, filterString={FilterString}", request.pageNumber, request.pageSize, request.filterString);
                }
                else
                {
                    _logger.LogInformation("{Count} gadgets found for the given query parameters.", gadgets.Items.Count);
                }

                return gadgets;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling GetGadgetsQuery: pageNumber={PageNumber}, pageSize={PageSize}, filterString={FilterString}", request.pageNumber, request.pageSize, request.filterString);
                throw;
            }
        }
    }
}

