using FluentValidation;

namespace OrderFlow.Application.Commands.Orders.UpdateOrder;

public sealed class UpdateOrderCommandValidator
    : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("O identificador do pedido é obrigatório.");
        RuleFor(command => command.OrderNumber)
            .NotEmpty()
            .WithMessage("O número do pedido é obrigatório.")
            .MaximumLength(50)
            .WithMessage("O número do pedido deve possuir no máximo 50 caracteres.");

        RuleFor(command => command.CustomerId).NotEmpty().WithMessage("O identificador do cliente é obrigatório.");
    }
}