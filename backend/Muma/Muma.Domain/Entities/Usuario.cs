namespace Muma.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public int IdTipoRegistro { get; set; }

        public TipoRegistro TipoRegistro { get; set; }
    }
}
