using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApprovalCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<List<LeaveRequestListDto>> GetAll()
        {
            var leaveRequests = await _mediator.Send(new GetAllLeaveRequestsQuery());
            return leaveRequests;
        }

        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDetailDto>> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailQuery { id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveRquest)
        {
            var id = await _mediator.Send(leaveRquest);
            return CreatedAtAction(nameof(Get), new { id = id });
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveRequestCommand updateCommand)
        {
            await _mediator.Send(updateCommand);
            return NoContent();
        }
        [HttpPut]
        [Route("CancelRequest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand CancelRequest)
        {
            await _mediator.Send(CancelRequest);
            return NoContent();
        }
        [HttpPut]
        [Route("UpdateApproval")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand UpdatedApprovalRequest)
        {
            await _mediator.Send(UpdatedApprovalRequest);
            return NoContent();
        }


        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveRequestCommand { Id = id });
            return NoContent();
        }
    }
}
