using MeusPedidos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeusPedidos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : Controller
{
    private readonly IProdutoRepository _produtoRepository;
    public ProdutoController(IProdutoRepository produtoRepository)    
    {
        _produtoRepository = produtoRepository ;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id, cancellationToken);
        if (produto == null)
        {
            return NotFound();
        }
        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult> Adicionar([FromBody] Produto produto, CancellationToken cancellationToken)
    {
        await _produtoRepository.AdicionarAsync(produto, cancellationToken);
        return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
    }
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Atualizar(Guid id, [FromBody] Produto produto, CancellationToken cancellationToken)
    {
        var exist = await _produtoRepository.ObterPorIdAsync(id, cancellationToken);
        if (exist == null)
        {
            return NotFound();
        }
        await _produtoRepository.AtualizarAsync(produto, cancellationToken);
        return Ok(produto   );
    }
}
