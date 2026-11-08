namespace PracticaApi
{
    public class model
    {
        public class Usuario
        {
            public int id { get; set; }
            public string correo { get; set; } = " ";
            public string claveHash { get; set; } = " ";
            public string nombre { get; set; } = " ";
            public bool disponible { get; set; } = true;
            public int aniosExperiencia { get; set; } = 0;

            //dto = datos que llegan o salen por internet (json)
            public record registroDTO(string nombre, string correo, string clave);
            public record loginDTO(string correo, string clave);
            public record perfilDTO(string nombre, bool disponible, int aniosExperiencia);




        }
    }
}
