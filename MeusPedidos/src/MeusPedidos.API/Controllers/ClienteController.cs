using MeusPedidos.Domain.Entities;
using MeusPedidos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeusPedidos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(id, cancellationToken);
        if (cliente == null)
        {
            return NotFound();
        }
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar(
        [FromBody] Cliente cliente,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await _clienteRepository.AdicionarAsync(cliente, cancellationToken);

            return Ok(cliente);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Atualizar(
        Guid id,
        [FromBody] Cliente cliente,
        CancellationToken cancellationToken
    )
    {

        var clienteExistente = await _clienteRepository.ObterPorIdAsync(id, cancellationToken);
        if (clienteExistente == null)
        {
            return NotFound();
        }
        await _clienteRepository.AtualizarAsync(cliente, cancellationToken);
        return NoContent();
    }
}
