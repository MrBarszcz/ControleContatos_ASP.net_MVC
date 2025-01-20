using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers;

public class ContatoController : Controller {
    // GET
    public IActionResult Index() {
        return View();
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
    
}