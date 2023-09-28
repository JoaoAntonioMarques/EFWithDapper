using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFWithDapper
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(p => p.Descricao).IsRequired();
            builder.Property(p => p.Preco);
            builder.Property(p => p.Desconto);
        }
    }
}
