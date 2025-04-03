using ControleContatos.Models;
using Newtonsoft.Json;

namespace ControleContatos.Helper;

public class Session : ISession {
    private readonly IHttpContextAccessor _httpContext; // Interface para acessar o contexto HTTP
    
    public Session(IHttpContextAccessor httpContext) {
        _httpContext = httpContext; // Injeta o contexto HTTP, que é necessário para acessar a sessão
    }
    
    public UsuarioModel GetSessionUser() {
        string sessionUser =
            _httpContext.HttpContext.Session
                .GetString( "sessionUserLogged" ); // Recupera o objeto serializado da sessão

        if ( string.IsNullOrEmpty( sessionUser ) ) return null; // Se o objeto for nulo ou vazio, retorna nulo

        return JsonConvert.DeserializeObject < UsuarioModel >( sessionUser ); // Deserializa o objeto para o tipo UsuarioModel 
    }
    
    public void CreateSession( UsuarioModel usuario ) {
        string valor = JsonConvert.SerializeObject(usuario); // Serializa o objeto para JSON
        
        _httpContext.HttpContext.Session.SetString("sessionUserLogged", valor); // Salva o objeto serializado na sessão 
    }

    public void DestroySession() {
        _httpContext.HttpContext.Session.Remove( "sessionUserLogged" );
        
    }

    
}