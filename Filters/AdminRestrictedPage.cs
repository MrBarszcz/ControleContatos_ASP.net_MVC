using ControleContatos.Enums;
using ControleContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ControleContatos.Filters;

public class AdminRestrictedPage : ActionFilterAttribute {
    public override void OnActionExecuting( ActionExecutingContext context ) {
        string sessionUser = context.HttpContext.Session.GetString( "sessionUserLogged" ); 
        
        if ( string.IsNullOrEmpty( sessionUser ) ) { 
            context.Result = new RedirectToRouteResult( new RouteValueDictionary { {"controller", "Login"}, {"action", "Index"} } ); // Redireciona para a página de login
        } else {
            UsuarioModel usuario = JsonConvert.DeserializeObject < UsuarioModel >( sessionUser );

            if ( usuario == null ) {
                context.Result = new RedirectToRouteResult( new RouteValueDictionary { {"controler", "Login"}, {"action", "Index"} } );
            }
            
            if ( usuario.Perfil != PerfilEnum.Admin ) {
                context.Result = new RedirectToRouteResult( new RouteValueDictionary { {"controller", "restrito"}, {"action", "Index"} } );
            }
        }
        
        base.OnActionExecuting( context );
    }
}