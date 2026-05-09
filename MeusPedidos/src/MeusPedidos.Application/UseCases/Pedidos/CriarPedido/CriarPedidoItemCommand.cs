

namespace MeusPedidos.Application.UseCases.Pedidos.CriarPedido;

public class CriarPedidoItemCommand
{
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
}