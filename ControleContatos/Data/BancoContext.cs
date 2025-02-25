using ControleContatos.Data.Map;
using ControleContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleContatos.Data;

public class BancoContext : DbContext {
    private readonly IConfiguration _configuration; // Injeta IConfiguration
    
    public BancoContext(DbContextOptions<BancoContext> options, IConfiguration configuration ) : base(options) {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Obtém a string de conexão do Secret Manager (desenvolvimento) ou serviço de segredos (produção)
        string connectionString = _configuration.GetConnectionString("Database");

        // Use o provedor de banco de dados apropriado (SQL Server no seu caso)
        optionsBuilder.UseSqlServer(connectionString);
        
    }
    
    public DbSet<ContatoModel> Contatos { get; set; }
    
    public DbSet<UsuarioModel> Usuarios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfiguration(new ContatoMap());
        
        base.OnModelCreating(modelBuilder);
    }
}