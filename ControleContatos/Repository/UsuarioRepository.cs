using ControleContatos.Data;
using ControleContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleContatos.Repository;

public class UsuarioRepository : IUsuarioRepository {
    private readonly BancoContext _bancoContext;
    
    public UsuarioRepository(BancoContext bancoContext) {
        _bancoContext = bancoContext;
    }

    public UsuarioModel ListbyLogin( string login) {
        return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
    }

    public UsuarioModel SearchEmailLogin( string email, string login ) {
        return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper() && x.Email.ToUpper() == email.ToUpper());
    }

    public UsuarioModel ListById(int id) {
        return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
    }

    public List<UsuarioModel> GetAll() {
        return _bancoContext.Usuarios
            .Include(x => x.Contatos) // Include para trazer os contatos do usuário
            .ToList();
    }

    public UsuarioModel Create(UsuarioModel usuario) {
        Console.WriteLine("Chegou até aqui");
        
        usuario.DataCadastro = DateTime.Now;
        
        usuario.SetPasswordHash();
        
        _bancoContext.Usuarios.Add(usuario);
        
        _bancoContext.SaveChanges();
        
        Console.WriteLine("Chegou até aqui o SaveChanges");
        
        return usuario;
    }

    public UsuarioModel Update(UsuarioModel usuario) {
        UsuarioModel usuarioDB = ListById(usuario.Id);
        
        if (usuarioDB == null) throw new Exception("Houve um erro ao atualizar o usuário");
        
        usuarioDB.Nome = usuario.Nome;
        usuarioDB.Email = usuario.Email;
        usuarioDB.Login = usuario.Login;
        usuarioDB.Perfil = usuario.Perfil;
        usuarioDB.DataAlteracao = DateTime.Now;

        _bancoContext.Usuarios.Update(usuarioDB);
        _bancoContext.SaveChanges();
        
        return usuarioDB;
    }

    public UsuarioModel UpdadePassword(UpdatePasswordModel updatePasswordModel) {
        UsuarioModel UsuarioDB = ListById(updatePasswordModel.Id);
        
        if (UsuarioDB == null) throw new Exception("Houve um erro ao atualizar a senha, usúario não encontrado");
        
        if (!UsuarioDB.SenhaValida(updatePasswordModel.SenhaAtual)) throw new Exception("Senha atual inválida");
        
        if (UsuarioDB.SenhaValida(updatePasswordModel.NovaSenha)) throw new Exception("A nova senha não pode ser igual a senha atual");

        UsuarioDB.SetNewPassword(updatePasswordModel.NovaSenha);
        UsuarioDB.DataAlteracao = DateTime.Now;
        
        _bancoContext.Usuarios.Update(UsuarioDB);
        _bancoContext.SaveChanges();
        
        return UsuarioDB;
    }

    public bool Delete(int id) {
        UsuarioModel usuarioDB = ListById(id);
        
        if (usuarioDB == null) throw new Exception("Houve um erro ao deletar o usuário");
        
        _bancoContext.Usuarios.Remove(usuarioDB);
        _bancoContext.SaveChanges();
        
        return true;
    }
}