using ControleContatos.Models;

namespace ControleContatos.Helper;

public interface ISession {
    void CreateSession(UsuarioModel usuario);
    
    UsuarioModel GetSessionUser();
    
    void DestroySession();

}