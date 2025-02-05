using System.ComponentModel.DataAnnotations;
using ControleContatos.Enums;

namespace ControleContatos.Models;

public class UsuarioSemSenhaModel {
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Digite o nome do usuário")]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "Digite um login para o usuário")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Digite o email do usuário")]
    [EmailAddress(ErrorMessage = "Digite um email válido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Informe o perfil do usúario")]
    public PerfilEnum Perfil { get; set; }
}