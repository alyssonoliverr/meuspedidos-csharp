using MeusPedidos.Domain.Entities;
using MeusPedidos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeusPedidos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormaDePagamentoController : Controller
{
    private readonly IFormaDePagamentorRepository _formaDePagamentoRepository;
    public FormaDePagamentoController(IFormaDePagamentorRepository formaDePagamentoRepository)
    {
        _formaDePagamentoRepository = formaDePagamentoRepository;
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] FormaDePagamento formaDePagamento, CancellationToken cancellationToken)
    {
        await _formaDePagamentoRepository.AdicionarAsync(formaDePagamento, cancellationToken);
        return CreatedAtAction(nameof(ObterPorId), new { id = formaDePagamento.Id }, formaDePagamento);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var formaDePagamento = await _formaDePagamentoRepository.ObterPorIdAsync(id, cancellationToken);
        if (formaDePagamento == null)
        {
            return NotFound();
        }
        return Ok(formaDePagamento);
    } 
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Atualizar(Guid id, [FromBody] FormaDePagamento formaDePagamento, CancellationToken cancellationToken)
    {
        var formaDePagamentoExistente = await _formaDePagamentoRepository.ObterPorIdAsync(id, cancellationToken);
        if (formaDePagamentoExistente == null)
        {
            return NotFound();
        }
        await _formaDePagamentoRepository.AtualizarAsync(formaDePagamento, cancellationToken);
        return NoContent();
    }

}
