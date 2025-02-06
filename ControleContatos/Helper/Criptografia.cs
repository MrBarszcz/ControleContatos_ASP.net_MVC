using System.Security.Cryptography;
using System.Text;

namespace ControleContatos.Helper;

public static class Criptografia {
    public static string GenerateHash( this string valor ) { // o this indica que o método é uma extensão de string
        var hash = SHA1.Create(); // cria um objeto de criptografia

        var enconding = new ASCIIEncoding(); // cria um objeto de enconding, que é a forma de codificar os caracteres

        var array = enconding.GetBytes( valor ); // converte a string em bytes

        array = hash.ComputeHash( array ); // gera o hash

        var strHexa = new StringBuilder(); // cria um objeto para concatenar os bytes
        
        foreach ( var item in array ) {
            strHexa.Append( item.ToString( "x2" ) ); // converte os bytes em hexadecimal
        }

        return strHexa.ToString(); // retorna o hash
    }
}