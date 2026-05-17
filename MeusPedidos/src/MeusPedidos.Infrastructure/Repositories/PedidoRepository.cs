using MeusPedidos.Domain.Entities;
using MeusPedidos.Domain.Interfaces;
using MeusPedidos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeusPedidos.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pedido?> ObterPorIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Pedidos.Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken = default)
    {
        await _context.Pedidos.AddAsync(pedido, cancellationToken);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Pedido pedido, CancellationToken cancellationToken = default)
    {
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Pedido>> ListarTodosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Pedidos.Include(p => p.Itens).ToListAsync(cancellationToken);
    }
}