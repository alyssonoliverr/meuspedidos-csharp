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
        var itens = command.Itens.Select(item => new ItemPedido(item.ProdutoId, item.Quantidade, item.ValorUnitario))
            .ToList();
        var pedido = Pedido.Criar(command.ClienteId, command.FormaDePagamentoId, itens);
        await _pedidoRepository.AdicionarAsync(pedido, cancellationToken);

        return pedido.Id;
    }
}