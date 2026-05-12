using MeusPedidos.Domain.Enums;
using MeusPedidos.Domain.Exceptions;
using System.Data;

namespace MeusPedidos.Domain.Entities;

public class Pedido : Entity
{
    public Guid ClienteId { get; protected set; }
    public Guid FormaDePagamentoId { get; protected set; }
    public PedidoStatus Status { get; protected set; }
    private readonly List<ItemPedido> _itens = new();
    public IReadOnlyCollection<ItemPedido> Itens => _itens;
    public decimal ValorTotalProdutos => _itens.Sum(x => x.ValorTotal);
    public decimal ValorDesconto { get; protected set; } = 0;
    public decimal ValorTotal => ValorTotalProdutos - ValorDesconto;
    public DateTime DataHoraCriacao { get; protected set; }

    protected Pedido() { }

    private Pedido(Guid clienteId, Guid formaDePagamentoId)
    {
        ClienteId = clienteId;
        FormaDePagamentoId = formaDePagamentoId;
        Status = PedidoStatus.Aberta;
        DataHoraCriacao = DateTime.UtcNow;
    }

    public static Pedido Criar(Guid clienteId, Guid formaDePagamentoId, List<ItemPedido> itens)
    {
        if (clienteId == Guid.Empty)
            throw new DomainException("O ID do cliente é obrigatório.");
        if (formaDePagamentoId == Guid.Empty)
            throw new DomainException("O ID da forma de pagamento é obrigatório.");
        var pedido = new Pedido(clienteId, formaDePagamentoId);

        if (itens is null ||!itens.Any())
        {
            throw new DomainException("Pedido deve conter pelo menos um item.");
        }

        foreach (var item in itens)
        {
            pedido.AdicionarItem(Guid.NewGuid(), item.Quantidade, item.ValorUnitario);
        }
        return pedido;
    }

    public void AdicionarItem(Guid produtoId, int quantidade, decimal valorUnitario)
    {
        ValidarEstadoParaAlteracao();

        var item = new ItemPedido(produtoId, quantidade, valorUnitario);
        _itens.Add(item);
    }

    public void AplicarDesconto(decimal valorDesconto)
    {
        ValidarEstadoParaAlteracao();
        if (valorDesconto < 0)
            throw new DomainException("O valor do desconto não pode ser negativo.");
        if (valorDesconto > ValorTotalProdutos)
            throw new DomainException("Desconto não pode ser maior que o total dos produtos.");
        ValorDesconto = valorDesconto;
    }

    public void Pagar()
    {
        ValidarEstadoParaAlteracao();
        Status = PedidoStatus.Paga;
    }

    public void Cancelar()
    {
        if (Status == PedidoStatus.Cancelada)
            throw new DomainException("Pedido já está cancelado.");
        Status = PedidoStatus.Cancelada;
    }

    private void ValidarEstadoParaAlteracao()
    {
        if (Status != PedidoStatus.Aberta)
            throw new DomainException("Pedido não está aberto.");
    }
    
}