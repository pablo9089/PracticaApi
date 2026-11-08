using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace PracticaApi
{
    public class configJWT
    {
        //Llave secreta: con ella el servidor firma los tokens y luego comprueba qye nadie los altero.
        // Debw tener 32 caracteres o mas. En un proyecto real no va en el codigo se usa user-secrets

        public static readonly SymmetricSecurityKey Llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("llave-secreta-de-la-practica-maui-2026-cambiala"));


    }
}
