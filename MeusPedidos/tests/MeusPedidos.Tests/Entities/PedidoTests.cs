

using FluentAssertions;
using MeusPedidos.Domain.Entities;
using MeusPedidos.Domain.Exceptions;

namespace MeusPedidos.Domain.Tests.Entities;

public class PedidoTests
{
    [Fact]
    public void Deve_Adicionar_Item_Ao_Pedido()
    {
        // Arrange 
        var ClienteId = Guid.NewGuid();
        var FormaDePagamentoId = Guid.NewGuid();
        var pedido = Pedido.Criar(ClienteId, FormaDePagamentoId);
        var produto = new Produto("Mouse Gamer", 150, 10);

        // Act 
        pedido.AdicionarItem(produto.Id, 2, produto.Preco);

        // Assert

        pedido.Itens.Should().HaveCount(1);

        pedido.ValorTotalProdutos.Should().Be(300);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_Quantidade_For_Menor_Ou_Igual_Zero()
    {
        // Arrange  
        var ClienteId = Guid.NewGuid();
        var FormaDePagamentoId = Guid.NewGuid();

        var pedido = Pedido.Criar(ClienteId, FormaDePagamentoId);

        var produto = new Produto("Teclado Gamer", 200, 10);


        // Act

        Action act = () => pedido.AdicionarItem(produto.Id, 0, produto.Preco);


        // Assert


        act.Should().Throw<DomainException>().WithMessage("A quantidade do item deve ser maior que zero.");





    }
}
