using MeusPedidos.Domain.Entities;
using MeusPedidos.Domain.Interfaces;

namespace MeusPedidos.Application.UseCases.Pedidos.CriarPedido;

public class CriarPedidoHandler
{
    private readonly IPedidoRepository _pedidoRepository;

    public CriarPedidoHandler(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<Guid> Handle(CriarPedidoCommand command, CancellationToken cancellationToken)
    {
        var pedido = Pedido.Criar(command.ClienteId, command.FormaDePagamentoId);
        await _pedidoRepository.AdicionarAsync(pedido, cancellationToken);

        return pedido.Id;
    }
}
