using MeusPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MeusPedidos.Domain.Interfaces;
using MeusPedidos.Infrastructure.Persistence;

namespace MeusPedidos.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Produto produto, CancellationToken cancellationToken)
    {
        await _context.Produtos.AddAsync(produto, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Produto produto, CancellationToken cancellationToken)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Produto?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}