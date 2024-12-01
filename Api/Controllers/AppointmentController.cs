using Application.Appointments.Commands;
using Application.Appointments.Queries;
using Application.ClientData.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private IMediator _mediator;
        public AppointmentController(IMediator mediator)
        {
             _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody]CreateAppointmentCommand request)
        {
            return new JsonResult(await _mediator.Send(request));
        }
        [HttpPut("change-status")]
        public async Task<ActionResult> Edit([FromBody] ChangeAppointmentStatusCommand request)
        {
            return new JsonResult(await _mediator.Send(request));
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete([FromBody] DeleteAppointmentCommand request)
        {
            return new JsonResult(await _mediator.Send(request));
        }
        [HttpGet("Find")]
        public async Task<ActionResult> Find([FromQuery] Guid Id)
        {
            return new JsonResult(await _mediator.Send(new GetAppointmentQuery() { Id = Id }));
        }
        [HttpPost("GetAllData")]
        public async Task<ActionResult> List([FromBody] GetAllAppointmentsQuery request)
        {
            return new JsonResult(await _mediator.Send(request));
        }
    }
}
