using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models;

public class ResetPasswordModel {
    [Required(ErrorMessage = "Digite o login do usuário")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Digite a email do usuário")]
    public string Email { get; set; }
}