using MeusPedidos.Domain.Exceptions;


namespace MeusPedidos.Domain.Entities;

public class Produto : ActivableEntity
{
    public string Descricao { get; protected set; } = string.Empty;

    public decimal Preco { get; protected set; }

    public int Estoque { get; protected set; }

    protected Produto()
    {
    }

    public Produto(string descricao, decimal preco, int estoque)
    {
        Validar(descricao, preco, estoque);

        Descricao = descricao;
        Preco = preco;
        Estoque = estoque;
    }

    private static void Validar(string descricao, decimal preco, int estoque)
    {
        ValidarDescricao(descricao);

        ValidarPreco(preco);

        ValidarEstoque(estoque);
    }

    private static void ValidarPreco(decimal preco)
    {
        if (preco <= 0)
            throw new DomainException("O preço do produto deve ser mais que zero.");
    }

    private static void ValidarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("A descrição do produto é obrigatória.");
    }

    private static void ValidarEstoque(int estoque)
    {
        if (estoque < 0)
            throw new DomainException("O estoque do produto não pode ser negativo.");
    }

    public void AdicionarEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new DomainException("A quantidade deve ser maior que zero.");

        Estoque += quantidade;
    }

    public void RemoverEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new DomainException("A quantidade deve ser maior que zero.");

        if (quantidade > Estoque)
            throw new DomainException("Não há estoque suficiente.");

        Estoque -= quantidade;
    }

    public void AlterarPreco(decimal preco)
    {
        ValidarPreco(preco);
        Preco = preco;
    }

    public void AlterarDescricao(string descricao)
    {
        ValidarDescricao(descricao);
        Descricao = descricao;
    }
}