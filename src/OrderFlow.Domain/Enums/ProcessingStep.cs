namespace OrderFlow.Domain.Enums
{
    public enum ProcessingStep
    {
        None = 0,               //Nenhuma etapa começou ainda.
        Validation = 1,         //O sistema está validando os dados do pedido.
        Payment = 2,            //O sistema está processando ou simulando o pagamento.
        Invetory = 3,           //O sistema está verificando ou reservando o estoque.
        Shipping = 4,           //O sistema está preparando a etapa de envio.
        Notification = 5,       //O sistema está enviando uma notificação sobre o resultado.
        Completed = 6           //Todas as etapas foram concluídas.
    }
}
