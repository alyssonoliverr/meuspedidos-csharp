

using MeusPedidos.Domain.Entities;
using MeusPedidos.Domain.Interfaces;
using MeusPedidos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeusPedidos.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken)
    {
        await _context.Clientes.AddAsync(cliente, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Cliente cliente, CancellationToken cancellationToken)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Cliente?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
