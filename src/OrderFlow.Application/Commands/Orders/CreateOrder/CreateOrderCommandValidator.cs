using FluentValidation;

namespace OrderFlow.Application.Commands.Orders.CreateOrder;

public sealed class CreateOrderCommandValidator
    : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.OrderNumber).NotEmpty().WithMessage("O número do pedido é obrigatório.");
        RuleFor(command => command.CustomerId).NotEmpty().WithMessage("O identificador do cliente é obrigatório.");
        RuleFor(command => command.Items).NotNull().WithMessage("A lista de itens é obrigatória.").NotEmpty().WithMessage("O pedido deve possuir pelo menos um item.");
        RuleForEach(command => command.Items).ChildRules(item =>
            {
                item.RuleFor(orderItem => orderItem.ProductId).NotEmpty().WithMessage("O identificador do produto é obrigatório.");
                item.RuleFor(orderItem => orderItem.ProductName).NotEmpty().WithMessage("O nome do produto é obrigatório.");
                item.RuleFor(orderItem => orderItem.Quantity).GreaterThan(0).WithMessage("A quantidade do produto deve ser maior que zero.");
                item.RuleFor(orderItem => orderItem.UnitPrice).GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");
            });
    }
}