using ControleContatos.Data;
using ControleContatos.Models;

namespace ControleContatos.Repository;

public class ContatoRepository : IContatoRepository {
    private readonly BancoContext _bancoContext; // variável privada para acessar o banco de dados, mas somente para leitura
    
    public ContatoRepository(BancoContext bancoContext) {
        _bancoContext = bancoContext;
    }

    public ContatoModel ListById(int id) {
        return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id); // Retorna o contato com o id informado
    }

    public List<ContatoModel> GetAll(int usuarioId) {
        return _bancoContext.Contatos.Where(x => x.UsuarioId == usuarioId).ToList(); // Retorna todos os contatos do banco de dados
    }

    public ContatoModel Create(ContatoModel contato) {
        Console.WriteLine("chegou no create");
        
        // Gravar no banco de dados
        _bancoContext.Contatos.Add(contato); // Adiciona o contato ao banco de dados
        
        Console.WriteLine("chegou a adicionar");

        
        _bancoContext.SaveChanges(); // Salva as alterações no banco de dados
        
        Console.WriteLine("chegou no save changes");

        return contato;
    }

    public ContatoModel Update(ContatoModel contato) {
        ContatoModel contatoDB = ListById(contato.Id);
        
        if (contatoDB == null) throw new Exception("Houve um erro ao atualizar o contato"); // Se o contato não existir, lança uma exceção e encerra a exec
        
        contatoDB.Nome = contato.Nome;
        contatoDB.Email = contato.Email;
        contatoDB.Celular = contato.Celular;

        _bancoContext.Contatos.Update(contatoDB);
        _bancoContext.SaveChanges();
        
        return contatoDB;
    }

    public bool Delete(int id) {
        ContatoModel contatoDB = ListById(id);
        
        if (contatoDB == null) throw new Exception("Houve um erro ao deletar o contato");
        
        _bancoContext.Contatos.Remove(contatoDB);
        _bancoContext.SaveChanges();
        
        return true;
    }
}