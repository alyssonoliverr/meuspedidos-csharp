using MeusPedidos.Domain.Entities;
using MeusPedidos.Domain.Interfaces;

namespace MeusPedidos.Application.UseCases.Pedidos.ObterPedidoPorId;

public class ObterPedidoPorIdHandler
{
    private readonly IPedidoRepository _pedidoRepository;

    public ObterPedidoPorIdHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<Pedido?> Handle(ObterPedidoPorIdQuery query, CancellationToken cancellationToken)
    {
        return await _pedidoRepository.ObterPorIdAsync(query.PedidoId, cancellationToken);
    }
}