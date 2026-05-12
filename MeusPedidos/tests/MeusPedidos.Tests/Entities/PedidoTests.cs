using FluentAssertions;
using MeusPedidos.Domain.Entities;
using MeusPedidos.Domain.Enums;
using MeusPedidos.Domain.Exceptions;

namespace MeusPedidos.Domain.Tests.Entities;

public class PedidoTests
{
    [Fact(DisplayName = "Deve adicionar item ao pedido")]
    public void Deve_Adicionar_Item_Ao_Pedido()
    {
        // Arrange
        var pedido = CriarPedidoValido();

        var produto = new Produto(
            "Mouse Gamer",
            150,
            10);

        // Act
        pedido.AdicionarItem(
            produto.Id,
            2,
            produto.Preco);

        // Assert
        pedido.Itens.Should().HaveCount(2);

        pedido.ValorTotalProdutos.Should().Be(400);
    }

    [Fact(DisplayName = "Deve lançar exceção quando quantidade for menor ou igual a zero")]
    public void Deve_Lancar_Excecao_Quando_Quantidade_For_Menor_Ou_Igual_Zero()
    {
        // Arrange
        var pedido = CriarPedidoValido();

        var produto = new Produto(
            "Teclado Gamer",
            200,
            10);

        // Act
        Action act = () => pedido.AdicionarItem(
            produto.Id,
            0,
            produto.Preco);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Quantidade do item deve ser maior que zero.");
    }

    [Fact(DisplayName = "Deve lançar exceção quando o valor unitário for menor ou igual a zero")]
    public void Deve_Lancar_Excecao_Quando_O_Valor_Unitario_For_Menor_Ou_Igual_Zero()
    {
        // Arrange
        var pedido = CriarPedidoValido();

        var produto = new Produto(
            "Headset Gamer",
            300,
            10);

        // Act
        Action act = () => pedido.AdicionarItem(
            produto.Id,
            2,
            -1);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Valor unitário do item deve ser maior que zero.");
    }

    [Fact(DisplayName = "Deve lançar exceção quando o desconto for menor que zero")]
    public void Deve_Lancar_Excecao_Quando_Desconto_For_Menor_Que_Zero()
    {
        // Arrange
        var pedido = CriarPedidoValido();

        // Act
        Action act = () => pedido.AplicarDesconto(-10);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("O valor do desconto não pode ser negativo.");
    }

    [Fact(DisplayName = "Deve lançar exceção quando o desconto for maior que o total dos produtos")]
    public void Deve_Lancar_Excecao_Quando_Desconto_For_Maior_Que_Total_Dos_Produtos()
    {
        // Arrange
        var pedido = CriarPedidoValido();

        // Act
        Action act = () => pedido.AplicarDesconto(500);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Desconto não pode ser maior que o total dos produtos.");
    }

    [Fact(DisplayName = "Deve aplicar desconto ao pedido")]
    public void Deve_Aplicar_Desconto_Ao_Pedido()
    {
        // Arrange
        var pedido = CriarPedidoValido();

        // Act
        pedido.AplicarDesconto(50);

        // Assert
        pedido.ValorDesconto.Should().Be(50);

        pedido.ValorTotal.Should().Be(50);
    }

    [Fact(DisplayName = "Deve pagar o pedido")]
    public void Deve_Pagar_O_Pedido()
    {
        // Arrange
        var pedido = CriarPedidoValido();

        // Act
        pedido.Pagar();

        // Assert
        pedido.Status.Should().Be(PedidoStatus.Paga);
    }

    [Fact(DisplayName = "Deve lançar exceção ao tentar alterar o pedido após pagamento")]
    public void Deve_Lancar_Excecao_Ao_Tentar_Alterar_O_Pedido_Apos_Pagamento()
    {
        // Arrange
        var pedido = CriarPedidoValido();

        pedido.Pagar();

        // Act
        Action act = () => pedido.AdicionarItem(
            Guid.NewGuid(),
            1,
            100);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Pedido não está aberto.");
    }

    [Fact(DisplayName = "Deve cancelar o pedido")]
    public void Deve_Cancelar_O_Pedido()
    {
        // Arrange
        var pedido = CriarPedidoValido();

        // Act
        pedido.Cancelar();

        // Assert
        pedido.Status.Should().Be(PedidoStatus.Cancelada);
    }

    [Fact(DisplayName = "Deve lançar exceção ao tentar editar um pedido cancelado")]
    public void Deve_Lancar_Excecao_Ao_Tentar_Editar_Um_Pedido_Cancelado()
    {
        // Arrange
        var pedido = CriarPedidoValido();

        pedido.Cancelar();

        // Act
        Action act = () => pedido.AdicionarItem(
            Guid.NewGuid(),
            1,
            100);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Pedido não está aberto.");
    }

    [Fact(DisplayName = "Deve lançar exceção ao tentar cancelar um pedido já cancelado")]
    public void Deve_Lancar_Excecao_Ao_Tentar_Cancelar_Um_Pedido_Ja_Cancelado()
    {
        // Arrange
        var pedido = CriarPedidoValido();

        pedido.Cancelar();

        // Act
        Action act = () => pedido.Cancelar();

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Pedido já está cancelado.");
    }

    [Fact(DisplayName = "Deve lançar exceção ao criar pedido sem itens")]
    public void Deve_Lancar_Excecao_Ao_Criar_Pedido_Sem_Itens()
    {
        // Arrange
        var clienteId = Guid.NewGuid();

        var formaDePagamentoId = Guid.NewGuid();

        var itens = new List<ItemPedido>();

        // Act
        Action act = () => Pedido.Criar(
            clienteId,
            formaDePagamentoId,
            itens);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Pedido deve conter pelo menos um item.");
    }

    [Fact(DisplayName = "Deve lançar exceção quando o Cliente ID é vazio")]
    public void Deve_Lancar_Excecao_Quando_O_Cliente_ID_Eh_Vazio()
    {
        // Arrange
        var clienteId = Guid.Empty;
        var formaDePagamentoId = Guid.NewGuid();
        var itens = CriarItensValidos();

        // Act
        Action act = () => Pedido.Criar(
            clienteId,
            formaDePagamentoId,
            itens);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("O ID do cliente é obrigatório.");
    }
    [Fact(DisplayName = "Deve lançar exceção quando o Forma de Pagamento ID é vazio")]
    public void Deve_Lancar_Excecao_Quando_O_Forma_De_Pagamento_ID_Eh_Vazio()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var formaDePagamentoId = Guid.Empty;
        var itens = CriarItensValidos();
        // Act
        Action act = () => Pedido.Criar(
            clienteId,
            formaDePagamentoId,
            itens);
        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("O ID da forma de pagamento é obrigatório.");
    }

    [Fact(DisplayName = "Deve lançar exceção quando os itens forem nulos")]
    public void Deve_Lancar_Excecao_Quando_Itens_Forem_Nulos()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var formaDePagamentoId = Guid.NewGuid();
        List<ItemPedido>? itens = null;

        // Act
        Action act = () => Pedido.Criar(
            clienteId,
            formaDePagamentoId,
            itens);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Pedido deve conter pelo menos um item.");
    }

    private static Pedido CriarPedidoValido()
    {
        return Pedido.Criar(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CriarItensValidos());
    }

    private static List<ItemPedido> CriarItensValidos()
    {
        return
        [
            new ItemPedido(
                Guid.NewGuid(),
                1,
                100)
        ];
    }
}