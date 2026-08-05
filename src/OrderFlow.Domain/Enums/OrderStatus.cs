namespace OrderFlow.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 1,            //Pedido recebido pela API.  Ainda está aguardando o Worker.
        Processing = 2,         //O Worker recebeu a mensagem e iniciou o fluxo.
        Completed = 3,          //O pedido passou por todas as etapas com sucesso.
        Failed = 4,             //O pedido apresentou algum erro durante o processamento.
        DeadLetter = 5,         //O pedido falhou mesmo depois das tentativas previstas.
        Cancelled = 6,          //O pedido cancelado
    }
}
