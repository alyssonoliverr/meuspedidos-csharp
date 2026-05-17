using MeusPedidos.Domain.Interfaces;

namespace MeusPedidos.Application.UseCases.Pedidos.CancelarPedido;

public class CancelarPedidoHandler
{
    private readonly IPedidoRepository _pedidoRepository;

    public CancelarPedidoHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<bool> Handle(CancelarPedidoCommand command, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId, cancellationToken);

        if (pedido is null) return false;

        await _pedidoRepository.AtualizarAsync(pedido, cancellationToken);
        return true;
    }
}