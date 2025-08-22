using Application.Gadgets.Queries;
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
        public async Task<ActionResult<IEnumerable<GetGadgetsDto>>> GetGadgets([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _sender.Send(new GetGadgetsQuery(pageNumber, pageSize));

            return Ok(result);
        }
    }
}
