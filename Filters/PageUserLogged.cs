using ControleContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ControleContatos.Filters;

public class PageUserLogged : ActionFilterAttribute {
    // Método que será executado antes de qualquer ação, para verificar se o usuário está logado e bloquear o acesso as rotas
    public override void OnActionExecuting( ActionExecutingContext context ) {
        string sessionUser = context.HttpContext.Session.GetString( "sessionUserLogged" ); // Recupera o objeto serializado da sessão
        
        if ( string.IsNullOrEmpty( sessionUser ) ) { // Se o objeto for nulo ou vazio, redireciona para a página de login
            context.Result = new RedirectToRouteResult( new RouteValueDictionary { {"controller", "Login"}, {"action", "Index"} } ); // Redireciona para a página de login
        } else {
            UsuarioModel usuario = JsonConvert.DeserializeObject < UsuarioModel >( sessionUser );

            if ( usuario == null ) {
                context.Result = new RedirectToRouteResult( new RouteValueDictionary { {"controler", "Login"}, {"action", "Index"} } );
            }
        }
        
        base.OnActionExecuting( context );
    }
}