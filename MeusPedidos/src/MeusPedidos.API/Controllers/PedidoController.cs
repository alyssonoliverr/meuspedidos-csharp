using System.Runtime.CompilerServices;
using MeusPedidos.Application.UseCases.Pedidos.CancelarPedido;
using MeusPedidos.Application.UseCases.Pedidos.CriarPedido;
using MeusPedidos.Application.UseCases.Pedidos.ListarPedido;
using MeusPedidos.Application.UseCases.Pedidos.ObterPedidoPorId;
using MeusPedidos.Application.UseCases.Pedidos.PagarPedido;
using Microsoft.AspNetCore.Mvc;

namespace MeusPedidos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly CriarPedidoHandler _criarHandler;
    private readonly ListarPedidosHandler _listarHandler;
    private readonly ObterPedidoPorIdHandler _obterPorIdHandler;
    private readonly PagarPedidoHandler _pagarHandler;
    private readonly CancelarPedidoHandler _cancelarHandler;

    public PedidosController(CriarPedidoHandler criarHandler, ListarPedidosHandler listarHandler,
        ObterPedidoPorIdHandler obterPorIdHandler, PagarPedidoHandler pagarHandler,
        CancelarPedidoHandler cancelarHandler)
    {
        _criarHandler = criarHandler;
        _listarHandler = listarHandler;
        _obterPorIdHandler = obterPorIdHandler;
        _pagarHandler = pagarHandler;
        _cancelarHandler = cancelarHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        CriarPedidoCommand command,
        CancellationToken cancellationToken
    )
    {
        var pedidoId = await _criarHandler.Handle(command, cancellationToken);

        return Ok(pedidoId);
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var pedidos = await _listarHandler.Handle(cancellationToken);
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new ObterPedidoPorIdQuery
        {
            PedidoId = id
        };

        var pedido = await _obterPorIdHandler.Handle(query, cancellationToken);

        if (pedido is null)
        {
            return NotFound();
        }

        return Ok(pedido);
    }

    [HttpPost("{id}/pagar")]
    public async Task<IActionResult> Pagar(Guid id, CancellationToken cancellationToken)
    {
        var command = new PagarPedidoCommand
        {
            PedidoId = id
        };
        var sucesso = await _pagarHandler.Handle(command, cancellationToken);
        if (!sucesso) return NotFound();

        return NoContent();
    }

    [HttpPost("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(Guid id, CancellationToken cancellationToken)
    {
        var command = new CancelarPedidoCommand
        {
            PedidoId = id
        };

        var sucesso = await _cancelarHandler.Handle(command, cancellationToken);
        if (!sucesso) return NotFound();

        return NoContent();
    }
}