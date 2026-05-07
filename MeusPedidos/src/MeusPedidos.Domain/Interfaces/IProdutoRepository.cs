using System.Runtime.CompilerServices;

namespace MeusPedidos.Domain.Interfaces;

public interface IProdutoRepository
{
    Task AdicionarAsync(Produto produto, CancellationToken cancellationToken);
    Task<Produto?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task AtualizarAsync(Produto produto, CancellationToken cancellationToken);
}
