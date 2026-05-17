using MeusPedidos.Domain.Entities;
using MeusPedidos.Domain.Interfaces;

namespace MeusPedidos.Application.UseCases.Pedidos.ListarPedido;

public class ListarPedidosHandler
{
    private readonly IPedidoRepository _pedidoRepository;

    public ListarPedidosHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<List<Pedido>> Handle(CancellationToken cancellationToken)
    {
        return await _pedidoRepository.ListarTodosAsync(cancellationToken);
    }
}   