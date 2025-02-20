using ControleContatos.Models;

namespace ControleContatos.Helper;

public interface IEmail {
    bool Enviar(String email, string subject, UsuarioModel usuario, string senhaUsuario);
    
}