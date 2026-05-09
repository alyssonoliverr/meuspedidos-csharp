namespace MeusPedidos.Domain.Entities;

public abstract class ActivableEntity : Entity
{
    public bool Ativo { get; protected set; } = true;

    public void Ativar() => Ativo = true;

    public void Desativar() => Ativo = false;
}
