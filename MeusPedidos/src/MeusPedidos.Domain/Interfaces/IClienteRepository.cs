using MeusPedidos.Domain.Entities;

namespace MeusPedidos.Domain.Interfaces;

public interface IClienteRepository
{
    Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken);
    Task<Cliente?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task AtualizarAsync(Cliente cliente, CancellationToken cancellationToken);
}
