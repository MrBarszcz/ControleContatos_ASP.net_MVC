using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers;

public class ContatoController : Controller {
    private readonly IContatoRepository _contatoRepository;
    public ContatoController(IContatoRepository contatoRepository) {
        _contatoRepository = contatoRepository;
    }
    
    // GET
    public IActionResult Index() {
        List<ContatoModel> contatos = _contatoRepository.GetAll();
        
        return View(contatos);
    }
    
    public IActionResult Create() {
        return View();
    }
    
    public IActionResult Update() {
        return View();
    }
    
    public IActionResult DeleteConfirmation() {
        return View();
    }

    [HttpPost]
    public IActionResult Create(ContatoModel contato) {
        _contatoRepository.Create(contato);

        return RedirectToAction("Index"); // Redireciona para a página Index
    }
    
}