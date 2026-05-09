using MeusPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeusPedidos.Infrastructure.Persistence.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ClienteId).IsRequired();
        builder.Property(p => p.FormaDePagamentoId).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.ValorDesconto).HasColumnType("decimal(18,2)").IsRequired();
        builder.Ignore(p => p.ValorTotalProdutos);
        builder.Ignore(p => p.ValorTotal);
        builder
            .Metadata.FindNavigation(nameof(Pedido.Itens))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
