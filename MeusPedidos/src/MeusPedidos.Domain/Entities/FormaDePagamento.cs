
using MeusPedidos.Domain.Exceptions;

namespace MeusPedidos.Domain.Entities;

public class FormaDePagamento : ActivableEntity
{
    public string Descricao { get; protected set; } = string.Empty;


    protected FormaDePagamento() { }

    public FormaDePagamento(string descricao)
    {
        ValidarDescricao(descricao);
        Descricao = descricao;
    }

    public void ValidarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("A descrição da forma de pagamento é obrigatória.");

        if (descricao.Length > 10)
            throw new DomainException("A descrição da forma de pagamento deve conter no máximo 10 caracteres.");
    }
    public void AtualizarDescricao(string descricao)
    {
        ValidarDescricao(descricao);
        Descricao = descricao;
    }
}
