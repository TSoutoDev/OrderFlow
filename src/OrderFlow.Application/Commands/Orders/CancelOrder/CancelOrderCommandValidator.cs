using FluentValidation;

namespace OrderFlow.Application.Commands.Orders.CancelOrder;

public sealed class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("O identificador do pedido é obrigatório.");
    }
}