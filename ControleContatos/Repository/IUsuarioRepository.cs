using ControleContatos.Models;

namespace ControleContatos.Repository;

public interface IUsuarioRepository {
    
    UsuarioModel ListbyLogin(string login);
    
    UsuarioModel SearchEmailLogin (string email, string login);
    
    UsuarioModel ListById(int id);
    
    List<UsuarioModel> GetAll();
    
    UsuarioModel Create(UsuarioModel usuario);
    
    UsuarioModel Update(UsuarioModel usuario);
    
    UsuarioModel UpdadePassword(UpdatePasswordModel updatePasswordModel);
    
    bool Delete(int id);
    
}