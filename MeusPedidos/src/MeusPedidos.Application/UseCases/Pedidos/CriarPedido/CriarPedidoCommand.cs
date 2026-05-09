namespace MeusPedidos.Application.UseCases.Pedidos.CriarPedido;

public class CriarPedidoCommand
{
    public Guid ClienteId { get; set; }
    public Guid FormaDePagamentoId  { get; set; }
}
