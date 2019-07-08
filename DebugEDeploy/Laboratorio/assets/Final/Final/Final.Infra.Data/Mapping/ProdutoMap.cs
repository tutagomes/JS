using Final.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Infra.Data.Mapping
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
	{

    public void Configure(EntityTypeBuilder<Produto> builder)
    {
      builder.ToTable("Produto");

      builder.HasKey(c => c.Id);

      builder.Property(c => c.Codigo)
        .IsRequired()
        .HasColumnName("Codigo");

      builder.Property(c => c.PrecoCompra)
        .HasColumnName("PrecoCompra");

      builder.Property(c => c.PrecoVenda)
        .HasColumnName("PrecoVenda");

      builder.Property(c => c.Sku)
        .IsRequired()
        .HasColumnName("Sku");

      builder.Property(c => c.Name)
        .IsRequired()
        .HasColumnName("Name");
    }
	}
}