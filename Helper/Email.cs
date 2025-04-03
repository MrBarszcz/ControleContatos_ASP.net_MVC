using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using ControleContatos.Models;
using Microsoft.Extensions.Configuration;

using RazorEngine.Templating;

namespace ControleContatos.Helper;

public class Email : IEmail {
    private readonly IConfiguration _configuration; // Lê as configurações no appsettings.json

    public Email( IConfiguration configuration ) {
        _configuration = configuration;
    }

    public bool Enviar( string email, string subject, UsuarioModel usuario, string senhaUsuario ) { // Envia o email
        try {
            string Host = _configuration [ "SMTP:Host" ];
            string Name = _configuration [ "SMTP:Nome" ];
            string UserName = _configuration [ "SMTP:UserName" ];
            string senhaApp = _configuration [ "SMTP:AppSenha" ]; // Acessa a senha do Secret Manager
            int Porta = _configuration.GetValue < int >( "SMTP:Porta" );

            // Carregar o template do corpo do email
            string bodyPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "EmailHTML", "EmailResetPassword.cshtml");
            string body = File.ReadAllText(bodyPath);

            // Substituir os placeholders pelos valores reais
            string bodyHtml = body.Replace("@Model.Nome", usuario.Nome).Replace("@Model.Senha", senhaUsuario);

            MailMessage mail =
                new MailMessage() { // Cria o objeto de email
                    From = new MailAddress( UserName, Name ), // De quem é o email
                    To = { email }, // Para quem é o email
                    Subject = subject, // Assunto do email
                    Body = bodyHtml, // Mensagem do email
                    IsBodyHtml =
                        true, // Define que a mensagem é HTML, para poder usar tags HTML e deixar o email mais bonito
                    Priority = MailPriority.High // Define a prioridade do email
                };

            using ( SmtpClient smtp = new SmtpClient( Host, Porta ) ) { // Cria o objeto de conexão com o servidor SMTP
                smtp.Credentials = new NetworkCredential( UserName, senhaApp ); // Define as credenciais de acesso
                smtp.EnableSsl = true; // Define que a conexão é segura
                smtp.UseDefaultCredentials = false; // Define que não vai usar as credenciais padrão

                smtp.Send( mail ); // Envia o email

                return true;
            }
        } catch ( Exception e ) {
            Console.WriteLine( e.Message );
            // _logger.LogError(e, "Erro ao enviar email.");  // Em produção, use um logger
            return false;
        }
    }
}