namespace MeusPedidos.Domain.Entities;

public class Cliente : Entity
{
    public string Nome { get; protected set; } = string.Empty;

    // EF Core
    protected Cliente() { }
    public Cliente(string nome)
    {
        AtualizarNome(nome);

    }

    private static void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new DomainException("O nome do cliente é obrigatório.");

        }
        if (nome.Length > 50)
        {
            throw new DomainException("O nome do cliente deve conter no máximo 50 caracteres.");
        }

    }
    public void AtualizarNome(string nome)
    {
        ValidarNome(nome);
        Nome = nome;
    }
}