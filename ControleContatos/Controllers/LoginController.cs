using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;
using ISession = ControleContatos.Helper.ISession;

namespace ControleContatos.Controllers;

public class LoginController : Controller {
    public readonly IUsuarioRepository _usuarioRepository;
    private readonly ISession _session;
    
    public LoginController(IUsuarioRepository usuarioRepository, ISession session) {
        _usuarioRepository = usuarioRepository;
        _session = session;
    }
    
    public IActionResult Index() {
        // Se o usuário já estiver logado, redireciona para a página inicial
        if ( _session.GetSessionUser() != null ) {
            return RedirectToAction( "Index", "Home" );
        }
        
        return View();
    }
    
    public IActionResult Logout() {
        _session.DestroySession(); // remove a sessão do usuário
        return RedirectToAction( "Index", "Login" );
    }

    [HttpPost]
    public IActionResult Enter(LoginModel loginModel) {
        try {
            if ( ModelState.IsValid ) {
                UsuarioModel usuario = _usuarioRepository.ListbyLogin( loginModel.Login );
                
                if ( usuario != null) {
                    if ( usuario.SenhaValida( loginModel.Senha ) ) {
                        _session.CreateSession( usuario ); // Cria a sessão do usuário
                        
                        return RedirectToAction( "Index", "Home" );
                    }

                    TempData["MessageError"] = "Senha do usuário inválida!";
                }
                
                TempData["MessageError"] = "Usuário e/ou senha inválidos!";

            }
            
            return View("Index");
            
        } catch (Exception e) {
            TempData["MessageError"] = $"Ops! não foi possível realizar o login. Erro: {e.Message}";
            return RedirectToAction( "Index" );
        }
    }
}