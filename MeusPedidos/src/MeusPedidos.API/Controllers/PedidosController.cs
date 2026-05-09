using MeusPedidos.Application.UseCases.Pedidos.CriarPedido;
using Microsoft.AspNetCore.Mvc;

namespace MeusPedidos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly CriarPedidoHandler _handler;

    public PedidosController(CriarPedidoHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        CriarPedidoCommand command,
        CancellationToken cancellationToken)
    {
        var pedidoId = await _handler.Handle(
            command,
            cancellationToken);

        return Ok(pedidoId);
    }
}