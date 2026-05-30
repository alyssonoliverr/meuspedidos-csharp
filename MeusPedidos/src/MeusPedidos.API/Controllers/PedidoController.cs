using MeusPedidos.Application.UseCases.Pedidos.CancelarPedido;
using MeusPedidos.Application.UseCases.Pedidos.CriarPedido;
using MeusPedidos.Application.UseCases.Pedidos.ListarPedido;
using MeusPedidos.Application.UseCases.Pedidos.ObterPedidoPorId;
using MeusPedidos.Application.UseCases.Pedidos.ObterPedidoPorNumero;
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
    private readonly ObterPedidoPorNumeroHandler _obterPorNumeroHandler;

    public PedidosController(CriarPedidoHandler criarHandler, ListarPedidosHandler listarHandler,
        ObterPedidoPorIdHandler obterPorIdHandler, PagarPedidoHandler pagarHandler,
        CancelarPedidoHandler cancelarHandler, ObterPedidoPorNumeroHandler obterPorNumeroHandler)
    {
        _criarHandler = criarHandler;
        _listarHandler = listarHandler;
        _obterPorIdHandler = obterPorIdHandler;
        _pagarHandler = pagarHandler;
        _cancelarHandler = cancelarHandler;
        _obterPorNumeroHandler = obterPorNumeroHandler;
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

    [HttpGet("{id:guid}")]
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

    [HttpGet("numero/{numeroPedido:int}")]
    public async Task<IActionResult> GetByNumero(int numeroPedido, CancellationToken cancellationToken)
    {
        var query = new ObterPedidoPorNumeroQuery()
        {
            NumeroPedido = numeroPedido
        };

        var pedido = await _obterPorNumeroHandler.Handle(query, cancellationToken);

        if (pedido is null)
        {
            return NotFound();
        }

        return Ok(pedido);
    }


    [HttpPost("{id:guid}/pagar")]
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

    [HttpPost("{id:guid}/cancelar")]
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