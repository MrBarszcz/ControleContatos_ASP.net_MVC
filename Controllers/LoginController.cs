using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;
using ISession = ControleContatos.Helper.ISession;

namespace ControleContatos.Controllers;

public class LoginController : Controller {
    public readonly IUsuarioRepository _usuarioRepository;
    private readonly ISession _session;
    private readonly IEmail _email;

    public LoginController(IUsuarioRepository usuarioRepository, ISession session, IEmail email) {
        _usuarioRepository = usuarioRepository;
        _session = session;
        _email = email;
    }

    public IActionResult Index() {
        // Se o usuário já estiver logado, redireciona para a página inicial
        if (_session.GetSessionUser() != null) {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult CadastroLogin() {
        return View();
    }

    public IActionResult ResetPassword() {
        return View();
    }

    public IActionResult Logout() {
        _session.DestroySession(); // remove a sessão do usuário
        return RedirectToAction("Index", "Login");
    }

    [HttpPost]
    public IActionResult Enter(LoginModel loginModel) {
        try {
            if (ModelState.IsValid) {
                UsuarioModel usuario = _usuarioRepository.ListbyLogin(loginModel.Login);

                if (usuario != null) {
                    if (usuario.SenhaValida(loginModel.Senha)) {
                        _session.CreateSession(usuario); // Cria a sessão do usuário

                        return RedirectToAction("Index", "Home");
                    }

                    TempData["MessageError"] = "Senha do usuário inválida!";
                }
                else {
                }

                TempData["MessageError"] = "Usuário e/ou senha inválidos!";
            }

            return View("Index");
        }
        catch (Exception e) {
            TempData["MessageError"] = $"Ops! não foi possível realizar o login. Erro: {e.Message}";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult SendResetPassword(ResetPasswordModel resetPasswordModel) {
        try {
            if (ModelState.IsValid) {
                UsuarioModel usuario =
                    _usuarioRepository.SearchEmailLogin(resetPasswordModel.Email, resetPasswordModel.Login);

                if (usuario != null) {
                    string novaSenha = usuario.GenerateNewPassword(); // gera uma nova senha para o usuário

                    bool emailEnviado =
                        _email.Enviar(usuario.Email, "Redefinição de senha", usuario,
                            novaSenha); // envia a nova senha por email

                    if (emailEnviado == true) {
                        _usuarioRepository.Update(usuario);

                        TempData["MessageSuccess"] = "Enviamos um e-mail com sua nova senha!";
                    }
                    else {
                        TempData["MessageError"] = "Não conseguimos Enviar o Email!";
                    }

                    return RedirectToAction("Index", "Login");
                }

                TempData["MessageError"] =
                    "Não conseguimos redefinir sua senha! Por favor verefique os dados informados.";
            }

            return View("Index");
        }
        catch (Exception e) {
            TempData["MessageError"] = $"Ops! não foi possível redefinir sua senha. Erro: {e.Message}";
            return RedirectToAction("Index");
        }
    }
}