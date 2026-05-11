using MeusPedidos.Domain.Entities;
using MeusPedidos.Domain.Interfaces;
using MeusPedidos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;    

namespace MeusPedidos.Infrastructure.Repositories;

public class FormaDePagamentoRepository : IFormaDePagamentorRepository
{
    private readonly AppDbContext _context;

    public FormaDePagamentoRepository(AppDbContext context)
    {
        _context = context;
    }
public async Task AdicionarAsync(FormaDePagamento formaDePagamento, CancellationToken cancellationToken)
    {
        await _context.FormasDePagamento.AddAsync(formaDePagamento, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(FormaDePagamento formaDePagamento, CancellationToken cancellationToken)
    {
         _context.FormasDePagamento.Update(formaDePagamento);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<FormaDePagamento?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.FormasDePagamento.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }
}
