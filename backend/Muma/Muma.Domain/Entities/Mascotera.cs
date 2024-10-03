namespace Muma.Domain.Entities;

public class Mascotero
{
    public int Id { get; set; }
    public int UsuarioAsociadoId { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
}
