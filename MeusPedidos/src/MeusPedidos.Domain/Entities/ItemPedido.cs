using MeusPedidos.Domain.Exceptions;

namespace MeusPedidos.Domain.Entities;

public class ItemPedido : Entity
{
    public Guid ProdutoId { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }

    public decimal ValorTotal => Quantidade * ValorUnitario;

    protected ItemPedido()
    {
    }

    public ItemPedido(Guid produtoId, int quantidade, decimal valorUnitario)
    {
        ValidarQuantidade(quantidade);
        ValidarValorUnitario(valorUnitario);

        ProdutoId = produtoId;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    public void AlterarQuantidade(int quantidade)
    {
        ValidarQuantidade(quantidade);
        Quantidade = quantidade;
    }

    public void AlterarValorUnitario(decimal valorUnitario)
    {
        ValidarValorUnitario(valorUnitario);
        ValorUnitario = valorUnitario;
    }

    private static void ValidarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new DomainException("Quantidade do item deve ser maior que zero.");
    }

    private static void ValidarValorUnitario(decimal valorUnitario)
    {
        if (valorUnitario <= 0)
            throw new DomainException("Valor unitário do item deve ser maior que zero.");
    }
}