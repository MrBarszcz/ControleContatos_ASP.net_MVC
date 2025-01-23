using ControleContatos.Models;

namespace ControleContatos.Repository;

public interface IContatoRepository {
    ContatoModel ListById(int id);
    
    List<ContatoModel> GetAll();
    
    ContatoModel Create(ContatoModel contato);
    
    ContatoModel Update(ContatoModel contato);
    
    bool Delete(int id);
    
}