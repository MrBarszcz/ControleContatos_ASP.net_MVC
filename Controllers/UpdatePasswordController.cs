using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;
using ISession = ControleContatos.Helper.ISession;

namespace ControleContatos.Controllers;

public class UpdatePasswordController : Controller {
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ISession _session;
    
    public UpdatePasswordController(IUsuarioRepository usuarioRepository, ISession session) {
        _usuarioRepository = usuarioRepository;
        _session = session;
    }
    
    // GET
    public IActionResult Index() {
        return View();
    }

    [HttpPost]
    public IActionResult Update(UpdatePasswordModel updatePasswordModel) {
        try {
            UsuarioModel usuarioLogado = _session.GetSessionUser();
            
            updatePasswordModel.Id = usuarioLogado.Id;
            
            if (ModelState.IsValid) {
                _usuarioRepository.UpdadePassword(updatePasswordModel);
                
                TempData["MessageSuccess"] = "Senha atualizada com sucesso!";
                return View("Index", updatePasswordModel);
            }

            return View("Index", updatePasswordModel);
        } catch (Exception e) {
            TempData["MessageError"] = $"Erro ao atualizar senha, erro: {e.Message}";
            return View("Index", updatePasswordModel);
        }
    }
}