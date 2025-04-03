using ControleContatos.Filters;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;
using ISession = ControleContatos.Helper.ISession;

namespace ControleContatos.Controllers;

[PageUserLogged]
public class ContatoController : Controller {
    private readonly IContatoRepository _contatoRepository;
    private readonly Helper.ISession _session;

    public ContatoController( IContatoRepository contatoRepository, ISession session ) {
        _contatoRepository = contatoRepository;
        _session = session;
    }

    // GET
    public IActionResult Index() {
        UsuarioModel usuarioLogado = _session.GetSessionUser();
        List < ContatoModel > contatos = _contatoRepository.GetAll(usuarioLogado.Id);

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
                TempData [ "MessageSuccess" ] = "Contato deletado com sucesso!";
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
                UsuarioModel usuarioLogado = _session.GetSessionUser();
                
                contato.UsuarioId = usuarioLogado.Id;
                _contatoRepository.Create( contato );

                TempData [ "MessageSuccess" ] =
                    "Contato cadastrado com sucesso!"; // Mensagem de sucesso, armazenada em uma variável temporária com nome de MessageSuccess

                return RedirectToAction( "Index", "Contato" ); // Redireciona para a página Index
            } else {
                Console.WriteLine("Erros de validação:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors)) {
                Console.WriteLine(error.ErrorMessage);
                }
            }

            return View( contato );
        } catch ( System.Exception erro ) {
            TempData [ "MessageError" ] = $"Ops, Não foi cadastrar o contato! erro: {erro.Message}";
            
            Console.WriteLine("Erro ao salvar contato: " + erro.Message);

            return RedirectToAction( "Index" );
        }
    }

    [HttpPost]
    public IActionResult Update( ContatoModel contato ) {
        try {
            if ( ModelState.IsValid ) {
                UsuarioModel usuarioLogado = _session.GetSessionUser();
                
                contato.UsuarioId = usuarioLogado.Id;
                _contatoRepository.Update( contato );

                TempData [ "MessageSuccess" ] = "Contato atualizado com sucesso!";

                return RedirectToAction( "Index" );
            }

            return View( "Update", contato ); // Retorna a view Update com o contato
        } catch ( System.Exception erro ) {
            TempData [ "MessageError" ] = $"Ops, Não foi possível atualizar o contato! erro: {erro.Message}";

            return View( "Update", contato );
        }
    }
}