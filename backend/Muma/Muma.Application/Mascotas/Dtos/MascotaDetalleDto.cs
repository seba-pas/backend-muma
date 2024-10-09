
namespace Muma.Application.Mascotas.Dtos;

public class MascotaDetalleDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string? TipoAnimal { get; set; }
    public string? Raza { get; set; }
    public string? Descripcion { get; set; }
    public string? Sexo { get; set; }
    public string? Tamano { get; set; }
    public int Edad { get; set; }
    public string? Ciudad { get; set; }
    public string? TemperamentoConAnimales { get; set; }
    public string? TemperamentoConPersonas { get; set; }
    public string? Estado { get; set; }
    public ProtectoraDto? Protectora { get; set; }
    public ContactoDto? Contacto { get; set; }
    public List<string>? Fotos { get; set; }

}

public class ProtectoraDto
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
}

public class ContactoDto
{
    public string? Email { get; set; }
    public string? PaginaWeb { get; set; }
}