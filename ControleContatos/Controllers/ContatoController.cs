using ControleContatos.Filters;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers;

[PageUserLogged]
public class ContatoController : Controller {
    private readonly IContatoRepository _contatoRepository;

    public ContatoController( IContatoRepository contatoRepository ) {
        _contatoRepository = contatoRepository;
    }

    // GET
    public IActionResult Index() {
        List < ContatoModel > contatos = _contatoRepository.GetAll();

        return View( contatos );
    }

    public IActionResult Create() {
        return View();
    }

    public IActionResult Update( int id ) {
        ContatoModel contato = _contatoRepository.ListById( id );
        return View( contato );
    }

    public IActionResult DeleteConfirmation( int id ) {
        ContatoModel contato = _contatoRepository.ListById( id );

        return View( contato );
    }

    public IActionResult Delete( int id ) {
        try {
            bool deleted = _contatoRepository.Delete( id );

            if ( deleted ) {
                TempData [ "MessageSucess" ] = "Contato deletado com sucesso!";
            } else {
                TempData [ "MessageError" ] = $"Ops, Não foi possível deletar o contato!";
            }

            return RedirectToAction( "Index" );
        } catch ( Exception erro ) {
            TempData [ "MessageError" ] = $"Ops, Não foi possível deletar o contato! erro: {erro.Message}";

            return RedirectToAction( "Index" );
        }
    }

    [HttpPost]
    public IActionResult Create( ContatoModel contato ) {
        try {
            if ( ModelState.IsValid ) {
                _contatoRepository.Create( contato );

                TempData [ "MessageSucess" ] =
                    "Contato cadastrado com sucesso!"; // Mensagem de sucesso, armazenada em uma variável temporária com nome de MessageSucess

                return RedirectToAction( "Index" ); // Redireciona para a página Index
            }

            return View( contato );
        } catch ( System.Exception erro ) {
            TempData [ "MessageError" ] = $"Ops, Não foi cadastrar o contato! erro: {erro.Message}";

            return RedirectToAction( "Index" );
        }
    }

    [HttpPost]
    public IActionResult Update( ContatoModel contato ) {
        try {
            if ( ModelState.IsValid ) {
                _contatoRepository.Update( contato );

                TempData [ "MessageSucess" ] = "Contato atualizado com sucesso!";

                return RedirectToAction( "Index" );
            }

            return View( "Update", contato ); // Retorna a view Update com o contato
        } catch ( System.Exception erro ) {
            TempData [ "MessageError" ] = $"Ops, Não foi possível atualizar o contato! erro: {erro.Message}";

            return View( "Update", contato );
        }
    }
}