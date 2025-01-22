using ControleContatos.Models;

namespace ControleContatos.Repository;

public interface IContatoRepository {
    List<ContatoModel> GetAll();
    ContatoModel Create(ContatoModel contato);
    
}