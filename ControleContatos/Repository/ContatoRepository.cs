using ControleContatos.Data;
using ControleContatos.Models;

namespace ControleContatos.Repository;

public class ContatoRepository : IContatoRepository {
    private readonly BancoContext _bancoContext; // variável privada para acessar o banco de dados, mas somente para leitura
    
    public ContatoRepository(BancoContext bancoContext) {
        _bancoContext = bancoContext;
    }

    public List<ContatoModel> GetAll() {
        return _bancoContext.Contatos.ToList(); // Retorna todos os contatos do banco de dados
    }

    public ContatoModel Create(ContatoModel contato) {
        // Gravar no banco de dados
        _bancoContext.Contatos.Add(contato); // Adiciona o contato ao banco de dados
        _bancoContext.SaveChanges(); // Salva as alterações no banco de dados
        
        return contato;
    }
}