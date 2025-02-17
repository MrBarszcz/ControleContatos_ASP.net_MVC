using ControleContatos.Data;
using ControleContatos.Models;

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
        return _bancoContext.Usuarios.ToList();
    }

    public UsuarioModel Create(UsuarioModel usuario) {
        usuario.DataCadastro = DateTime.Now;
        
        usuario.SetPasswordHash();
        
        _bancoContext.Usuarios.Add(usuario);
        
        _bancoContext.SaveChanges();
        
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

    public bool Delete(int id) {
        UsuarioModel usuarioDB = ListById(id);
        
        if (usuarioDB == null) throw new Exception("Houve um erro ao deletar o usuário");
        
        _bancoContext.Usuarios.Remove(usuarioDB);
        _bancoContext.SaveChanges();
        
        return true;
    }
}