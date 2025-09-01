using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;

namespace Application.Gadgets.Queries.GetGatgetById
{
    public record GetGadgetByIdQuery(int Id) : IRequest<GetGadgetByIdDto?>;

    public class GetGadgetByIdHandler : IRequestHandler<GetGadgetByIdQuery, GetGadgetByIdDto?>
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetGadgetByIdHandler> _logger;

        public GetGadgetByIdHandler(IDatabaseContext context, IMapper mapper, ILogger<GetGadgetByIdHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetGadgetByIdDto?> Handle(GetGadgetByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetGadgetByIdQuery: Id={Id}", request.Id);
            try
            {
                var gadget = await _context.Gadgets
                    .AsNoTracking()
                    .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

                if (gadget == null)
                {
                    _logger.LogWarning("Gadget not found: Id={Id}", request.Id);
                    Guard.Against.NotFound(request.Id, gadget);
                }

                var gadgetDto = _mapper.Map<GetGadgetByIdDto>(gadget);
                _logger.LogInformation("Gadget found: Id={Id}, Name={Name}", gadgetDto.Id, gadgetDto.Name);
                return gadgetDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling GetGadgetByIdQuery: Id={Id}", request.Id);
                throw;
            }
        }
    }
}
