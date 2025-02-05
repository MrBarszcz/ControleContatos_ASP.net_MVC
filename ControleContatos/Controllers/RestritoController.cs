using ControleContatos.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers;

[PageUserLogged]
public class RestritoController : Controller {
    // GET
    public IActionResult Index() {
        return View();
    }
}