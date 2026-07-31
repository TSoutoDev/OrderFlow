using MediatR;
using OrderFlow.Application.DTOs;

namespace OrderFlow.Application.Queries.Orders.GetOrderById;

public sealed record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;