using Application;
using Application.ClientData.Commands;
using Application.ClientData.Queries;
using Application.Setting.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingController : ControllerBase
    {
        private IMediator _mediator;
        public SettingController(IMediator mediator)
        {
             _mediator = mediator;
        }

        [HttpPut("Edti")]
        public async Task<ActionResult> Edit([FromBody]UpdateSettingCommand request)
        {
            return new JsonResult(await _mediator.Send(request));
        }
        [HttpGet("Find")]
        public async Task<ActionResult> Find()
        {
            return new JsonResult(await _mediator.Send(new GetSettingQuery()));
        }
    }
}
