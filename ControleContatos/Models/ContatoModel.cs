using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models;

public class ContatoModel {
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Digite o nome do contato")] // Define que o campo é obrigatório
    public string Nome { get; set; }
    
    
    [Required(ErrorMessage = "Digite o email do contato")]
    [EmailAddress(ErrorMessage = "Digite um email válido")] // Define que o campo é um email
    public string Email { get; set; }

    [Required(ErrorMessage = "Digite o celular do contato")]
    [Phone(ErrorMessage = "Digite um celular válido")] // Define que o campo é um número de celular
    public string Celular { get; set; }

    public int? UsuarioId { get; set; }
    
    public UsuarioModel? Usuario { get; set; }
}