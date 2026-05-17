using MeusPedidos.Domain.Interfaces;

namespace MeusPedidos.Application.UseCases.Pedidos.PagarPedido;

public class PagarPedidoHandler
{
    private readonly IPedidoRepository _pedidoRepository;

    public PagarPedidoHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<bool> Handle(PagarPedidoCommand command, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId, cancellationToken);

        if (pedido is null) return false;

        pedido.Pagar();

        await _pedidoRepository.AtualizarAsync(pedido, cancellationToken);
        return true;
    }
}