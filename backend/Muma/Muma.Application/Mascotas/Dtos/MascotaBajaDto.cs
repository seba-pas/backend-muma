using System.ComponentModel.DataAnnotations;

namespace Muma.Application.Mascotas.Dtos;

public class MascotaBajaDto : IValidatableObject
{
    [Required]
    public string Motivo { get; set; }
    public int? IdMascotero { get; set; }
    public DateTime? FechaAdopcion { get; set; }
    public string? Descripcion { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Motivo == "otro" && string.IsNullOrWhiteSpace(Descripcion))
        {
            yield return new ValidationResult(
                "El campo 'Descripcion' es obligatorio cuando el motivo es 'otro'.",
                [nameof(Descripcion)]);
        }

        if (Motivo == "adopcion")
        {
            if (IdMascotero == null)
            {
                yield return new ValidationResult(
                    "El campo 'IdMascotero' es obligatorio cuando el motivo es 'adopcion'.",
                    [nameof(IdMascotero)]);
            }

            if (FechaAdopcion == null)
            {
                yield return new ValidationResult(
                    "El campo 'FechaAdopcion' es obligatorio cuando el motivo es 'adopcion'.",
                    [nameof(FechaAdopcion)]);
            }
        }
    }
}