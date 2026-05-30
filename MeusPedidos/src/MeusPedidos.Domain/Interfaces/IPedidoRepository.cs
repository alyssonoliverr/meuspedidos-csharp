using MeusPedidos.Domain.Entities;

namespace MeusPedidos.Domain.Interfaces;

public interface IPedidoRepository
{
    Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken);
    Task<Pedido?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Pedido?> ObterPorNumeroPedido(int numeroPedido, CancellationToken cancellationToken);
    Task AtualizarAsync(Pedido pedido, CancellationToken cancellationToken);
    Task<List<Pedido>> ListarTodosAsync(CancellationToken cancellationToken);
}