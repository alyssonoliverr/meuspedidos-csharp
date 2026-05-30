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
        builder.Property(p => p.NumeroPedido).ValueGeneratedOnAdd();
        builder.HasIndex(x => x.NumeroPedido)
            .IsUnique();
        builder.HasOne<Cliente>().WithMany().HasForeignKey(p => p.ClienteId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<FormaDePagamento>().WithMany().HasForeignKey(p => p.FormaDePagamentoId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.ValorDesconto).HasColumnType("decimal(18,2)").IsRequired();
        builder.Ignore(p => p.ValorTotalProdutos);
        builder.Ignore(p => p.ValorTotal);
        builder
            .Metadata.FindNavigation(nameof(Pedido.Itens))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}