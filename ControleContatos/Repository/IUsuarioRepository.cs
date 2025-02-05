using ControleContatos.Models;

namespace ControleContatos.Repository;

public interface IUsuarioRepository {
    
    UsuarioModel ListbyLogin(string login);
    
    UsuarioModel ListById(int id);
    
    List<UsuarioModel> GetAll();
    
    UsuarioModel Create(UsuarioModel usuario);
    
    UsuarioModel Update(UsuarioModel usuario);
    
    bool Delete(int id);
    
}