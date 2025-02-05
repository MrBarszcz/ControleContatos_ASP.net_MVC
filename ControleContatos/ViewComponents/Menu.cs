using ControleContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleContatos.ViewComponents;
 
public class Menu : ViewComponent {
    public async Task <IViewComponentResult> InvokeAsync() { // Método que será chamado para renderizar a view componente
        string sessionUser = HttpContext.Session.GetString( "sessionUserLogged" );
        
        if ( string.IsNullOrEmpty( sessionUser ) ) return null;
        
        UsuarioModel usuario = JsonConvert.DeserializeObject < UsuarioModel >( sessionUser );
        
        return View(usuario);
    }
}