using Application.Gadgets.Queries.GetGadgets;
using Application.Gadgets.Queries.GetGatgetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/gadgets")]
    [ApiController]
    public class GadgetController : Controller
    {
        private readonly ISender _sender;

        public GadgetController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetGadgetsDto>>> GetGadgets(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? filterString = null)
        {
            var result = await _sender.Send(new GetGadgetsQuery(pageNumber, pageSize, filterString));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetGadgetByIdDto>> GetGadget(int id)
        {
            var result = await _sender.Send(new GetGadgetByIdQuery(id));

            return Ok(result);
        }
    }
}
