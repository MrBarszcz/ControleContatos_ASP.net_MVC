using ControleContatos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleContatos.Data.Map;

public class ContatoMap : IEntityTypeConfiguration<ContatoModel> { // IEntityTypeConfiguration é uma interface do EntityFramework, que define que a classe é uma configuração de mapeamento de entidade
    public void Configure(EntityTypeBuilder<ContatoModel> builder) { // Método de configuração de mapeamento de entidade
        builder.HasKey(x => x.Id); // Define a chave primária da entidade, no caso, o campo Id
        
        builder.HasOne(x => x.Usuario)
            .WithMany(u => u.Contatos)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull);
        // Define o relacionamento entre a entidade Contato e a entidade Usuario
    }
}