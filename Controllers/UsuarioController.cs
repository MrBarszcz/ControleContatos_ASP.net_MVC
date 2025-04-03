using ControleContatos.Filters;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers;

[AdminRestrictedPage]
public class UsuarioController : Controller {
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IContatoRepository _contatoRepository;

    public UsuarioController( IUsuarioRepository usuarioRepository, IContatoRepository contatoRepository ) {
        _usuarioRepository = usuarioRepository;
        _contatoRepository = contatoRepository;
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
                TempData [ "MessageSuccess" ] = "Usuário deletado com sucesso!";
            } else {
                TempData [ "MessageError" ] = $"Ops, Não foi possível deletar o usuário!";
            }

            return RedirectToAction( "Index" );
        } catch ( Exception erro ) {
            TempData [ "MessageError" ] = $"Ops, Não foi possível deletar o usuário! erro: {erro.Message}";

            return RedirectToAction( "Index" );
        }
    }

    public IActionResult ListContactsById(int Id) {
        List<ContatoModel> contatos = _contatoRepository.GetAll(Id);
        return PartialView("_ContatosUsuario", contatos);
    }

    [HttpPost]
    public IActionResult Create( UsuarioModel usuario ) {
        try {
            if ( ModelState.IsValid ) {
                Console.WriteLine("Chegou até o create");
                
                _usuarioRepository.Create( usuario );

                TempData [ "MessageSuccess" ] = "Usuario cadastrado com sucesso!";

                return RedirectToAction( "Index" );
            } else {
                foreach (var modelState in ModelState.Values) {
                    foreach (var error in modelState.Errors) {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
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

                TempData [ "MessageSuccess" ] = "Contato atualizado com sucesso!";

                return RedirectToAction( "Index" );
            }

            return View( usuario );
        } catch ( Exception erro ) {
            TempData [ "MessageError" ] = $"Ops, Não foi possível atualizar o usuário! erro: {erro.Message}";

            return RedirectToAction( "Index" );
        }
    }
}