using ControleContatos.Data;
using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.EntityFrameworkCore;
using ISession = ControleContatos.Helper.ISession;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registro do DbContext - REMOVA A CONFIGURAÇÃO DA STRING DE CONEXÃO AQUI
builder.Services.AddDbContext<BancoContext>(); // O OnConfiguring do BancoContext fará o resto

builder.Services.AddSingleton < IHttpContextAccessor, HttpContextAccessor >(); // Registra o serviço de acesso ao contexto HTTP


builder.Services.Configure < SmtpSettingsModel >( builder.Configuration.GetSection( "SMTP" ) );

builder.Configuration.AddUserSecrets<Program>(); // Importante! Adiciona o secret manager


builder.Services.AddScoped < IContatoRepository, ContatoRepository >();
builder.Services.AddScoped < IUsuarioRepository, UsuarioRepository >();

builder.Services.AddScoped <ISession, Session>(); // Registra o serviço de sessão
builder.Services.AddScoped <IEmail, Email>(); // Registra o serviço de email

builder.Services.AddSession( o => {
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
} ); // Adiciona o serviço de sessão

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( !app.Environment.IsDevelopment() ) {
    app.UseExceptionHandler( "/Home/Error" );
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseSession(); // Habilita o uso de sessão

app.MapStaticAssets();

app.MapControllerRoute( name: "default", pattern: "{controller=Login}/{action=Index}/{id?}" ).WithStaticAssets();


app.Run();