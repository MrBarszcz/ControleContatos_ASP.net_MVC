namespace ControleContatos.Helper;

public interface IEmail {
    bool Enviar(String email, string subject, string message);
    
}