using System.Diagnostics;
using ControleContatos.Filters;
using Microsoft.AspNetCore.Mvc;
using ControleContatos.Models;

namespace ControleContatos.Controllers;

[PageUserLogged] // Aplica o filtro de verificação de usuário logado
public class HomeController : Controller {
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}