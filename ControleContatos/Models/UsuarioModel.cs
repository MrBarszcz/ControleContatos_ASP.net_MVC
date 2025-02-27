using System.ComponentModel.DataAnnotations;
using ControleContatos.Enums;
using ControleContatos.Helper;

namespace ControleContatos.Models;

public class UsuarioModel {
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Digite o nome do usuário")]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "Digite um login para o usuário")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Digite o email do usuário")]
    [EmailAddress(ErrorMessage = "Digite um email válido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Informe o perfil do usúario")]
    public PerfilEnum? Perfil { get; set; }
    
    [Required(ErrorMessage = "Digite a senha do usuário")]
    public string Senha { get; set; }
    
    public DateTime DataCadastro { get; set; }
    
    public DateTime? DataAlteracao { get; set; } // ? define que o campo pode ser nulo
    
    
    public virtual List<ContatoModel>? Contatos { get; set; }
    
    public bool SenhaValida(string senha) {
        return Senha == senha.GenerateHash();
    }

    public void SetPasswordHash() { // método para criptografar a senha
        Senha = Senha.GenerateHash(); // o this faz com que seja possivel usar o metodo aqui
    }
    
    public void SetNewPassword(string novaSenha) {
        Senha = novaSenha.GenerateHash();
    }

    public string GenerateNewPassword() {
        string novaSenha = Guid.NewGuid().ToString().Substring( 0, 8 ); // gera uma senha aleatória de 8 caracteres
        
        Senha = novaSenha.GenerateHash();
        
        return novaSenha;
    }
}