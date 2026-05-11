using MeusPedidos.Domain.Entities;

namespace MeusPedidos.Domain.Interfaces;

public interface IFormaDePagamentorRepository
{
    Task AdicionarAsync(FormaDePagamento formaDePagamento, CancellationToken cancellationToken);
    Task<FormaDePagamento?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task AtualizarAsync(FormaDePagamento formaDePagamento, CancellationToken cancellationToken);


}
