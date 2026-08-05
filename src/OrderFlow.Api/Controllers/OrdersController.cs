using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using OrderFlow.Application.Commands.Orders.CancelOrder;
using OrderFlow.Application.Commands.Orders.CreateOrder;
using OrderFlow.Application.Commands.Orders.UpdateOrder;
using OrderFlow.Application.Queries.Orders.GetOrderById;

namespace OrderFlow.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public sealed class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(Create), new { id = orderId }, orderId); // HTTP 201 (Created), o retorno é o id.
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id), cancellationToken);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            command = command with { Id = id };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
        {
            var command = new CancelOrderCommand(id);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

    }
}
