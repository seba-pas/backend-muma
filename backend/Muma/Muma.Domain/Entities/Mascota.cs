
namespace Muma.Domain.Entities;

public class Mascota
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? TipoAnimal { get; set; }
    public string? Raza { get; set; }
    public string? Descripcion { get; set; }
    public string? TemperamentoConAnimales { get; set; }
    public string? TemperamentoConPersonas { get; set; }
    public string? Sexo { get; set; }
    public string? Tamano { get; set; }
    public string? Ciudad { get; set; }
    public int Edad { get; set; }
    public string? MesAnioNacimiento { get; set; }
    public List<string>? Fotos { get; set; }

    public bool Baja { get; set; }
    public string? MotivoBaja { get; set; }
    public int? IdMascotero { get; set; }
    public DateTime? FechaAdopcion { get; set; }
    public string? DescripcionBaja { get; set; }

    public int ProtectoraId { get; set; }
    public Protectora? Protectora { get; set; }
}