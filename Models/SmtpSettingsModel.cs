namespace ControleContatos.Models;

public class SmtpSettingsModel {
    public string UserName { get; set; }
    public string Nome { get; set; }
    public string Host { get; set; }
    public string AppSenha { get; set; } // Não usar em produção
    public int Porta { get; set; }
}