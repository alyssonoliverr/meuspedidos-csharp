using MeusPedidos.Domain.Enums;

namespace MeusPedidos.Application.UseCases.Pedidos.ListarPedido;

public class ListarPedidosQuery
{
    public Guid? PedidoId { get; set; }
    public Guid? ClienteId { get; set; }
    public Guid? FormaDePagamentoId { get; set; }
    public PedidoStatus? Status { get; set; }

}