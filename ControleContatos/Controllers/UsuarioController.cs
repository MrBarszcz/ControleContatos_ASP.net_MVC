using ControleContatos.Filters;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers;

[AdminRestrictedPage]
public class UsuarioController : Controller {
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController( IUsuarioRepository usuarioRepository ) {
        _usuarioRepository = usuarioRepository;
    }

    // GET
    public IActionResult Index() {
        List < UsuarioModel > usuarios = _usuarioRepository.GetAll();

        return View( usuarios );
    }

    public IActionResult Create() {
        return View();
    }

    public IActionResult Update( int id ) {
        UsuarioModel usuario = _usuarioRepository.ListById( id );
        return View( usuario );
    }

    public IActionResult DeleteConfirmation( int id ) {
        UsuarioModel usuario = _usuarioRepository.ListById( id );

        return View( usuario );
    }

    public IActionResult Delete( int id ) {
        try {
            bool deleted = _usuarioRepository.Delete( id );

            if ( deleted ) {
                TempData [ "MessageSucess" ] = "Usuário deletado com sucesso!";
            } else {
                TempData [ "MessageError" ] = $"Ops, Não foi possível deletar o usuário!";
            }

            return RedirectToAction( "Index" );
        } catch ( Exception erro ) {
            TempData [ "MessageError" ] = $"Ops, Não foi possível deletar o usuário! erro: {erro.Message}";

            return RedirectToAction( "Index" );
        }
    }

    [HttpPost]
    public IActionResult Create( UsuarioModel usuario ) {
        try {
            if ( ModelState.IsValid ) {
                _usuarioRepository.Create( usuario );

                TempData [ "MessageSucess" ] = "Usuario cadastrado com sucesso!";

                return RedirectToAction( "Index" );
            }

            return View( usuario );
        } catch ( Exception erro ) {
            TempData [ "MessageError" ] = $"Ops, Não foi cadastrar o usuario! erro: {erro.Message}";

            return RedirectToAction( "Index" );
        }
    }

    [HttpPost]
    public IActionResult Update( UsuarioSemSenhaModel usuarioSemSenhaModel ) {
        try {
            UsuarioModel usuario = null;

            if ( ModelState.IsValid ) {
                usuario =
                    new UsuarioModel() {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil
                    };

                usuario = _usuarioRepository.Update( usuario );

                TempData [ "MessageSucess" ] = "Contato atualizado com sucesso!";

                return RedirectToAction( "Index" );
            }

            return View( usuario );
        } catch ( Exception erro ) {
            TempData [ "MessageError" ] = $"Ops, Não foi possível atualizar o usuário! erro: {erro.Message}";

            return RedirectToAction( "Index" );
        }
    }
}