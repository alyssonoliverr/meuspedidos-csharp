using MeusPedidos.Domain.Entities;
using MeusPedidos.Domain.Interfaces;

namespace MeusPedidos.Application.UseCases.Pedidos.ObterPedidoPorNumero;

public class ObterPedidoPorNumeroHandler
{
    private readonly IPedidoRepository _pedidoRepository;

    public ObterPedidoPorNumeroHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<Pedido?> Handle(ObterPedidoPorNumeroQuery query, CancellationToken cancellationToken)
    {
        return await _pedidoRepository.ObterPorNumeroPedido(query.NumeroPedido, cancellationToken);
    }
}