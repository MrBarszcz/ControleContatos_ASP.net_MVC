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
    
    public IActionResult Update(int id) {
        ContatoModel contato = _contatoRepository.ListById(id);
        return View(contato);
    }
    
    public IActionResult DeleteConfirmation(int id) {
        ContatoModel contato = _contatoRepository.ListById(id);
        
        return View(contato);
    }

    public IActionResult Delete(int id) {
        _contatoRepository.Delete(id);
        
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Create(ContatoModel contato) {
        _contatoRepository.Create(contato);

        return RedirectToAction("Index"); // Redireciona para a página Index
    }
    
    [HttpPost]
    public IActionResult Update(ContatoModel contato) {
        _contatoRepository.Update(contato);

        return RedirectToAction("Index");
    }
    
    
    
}