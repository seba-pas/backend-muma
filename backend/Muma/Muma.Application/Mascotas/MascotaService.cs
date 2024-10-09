using Microsoft.EntityFrameworkCore;
using Muma.Application.Common;
using Muma.Application.Mascotas.Dtos;
using Muma.Domain.Entities;

namespace Muma.Application.Mascotas;

public class MascotaService(IApplicationDbContext context)
{
    private readonly IApplicationDbContext _context = context;

    public async Task<OperationResult<MascotaDto>> CrearMascota(MascotaDto input)
    {
        var result = new OperationResult<MascotaDto>();

        try
        {
            DateTime fechaNacimiento;
            if (DateTime.TryParseExact(input.MesAnioNacimiento + "-01", "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out fechaNacimiento))
            {
                var edad = DateTime.Now.Year - fechaNacimiento.Year;
                if (DateTime.Now.Month < fechaNacimiento.Month)
                    edad--;

                var nuevaMascota = new Mascota()
                {
                    Nombre = input.Nombre,
                    TipoAnimal = input.TipoAnimal,
                    Raza = input.Raza,
                    Descripcion = input.Descripcion,
                    Sexo = input.Sexo,
                    Tamano = input.Tamano,
                    Ciudad = input.Ciudad,
                    ProtectoraId = input.ProtectoraId,
                    MesAnioNacimiento = input.MesAnioNacimiento,
                    Edad = edad,
                    Fotos = input.Fotos,
                    TemperamentoConAnimales = input.TemperamentoConAnimales,
                    TemperamentoConPersonas = input.TemperamentoConPersonas
                };

                await _context.Mascotas.AddAsync(nuevaMascota);
                await _context.SaveChangesAsync(new CancellationToken());

                var dto = MascotaMaps.MapToDto(nuevaMascota);
                dto.Id = nuevaMascota.Id;
                result.SetResult(dto);
            }
            else
            {
                result.AddError("El formato de 'MesAnioNacimiento' no es válido.");
            }
        }
        catch (Exception ex)
        {
            result.AddError(ex.Message);
        }

        return result;
    }

    public async Task<MascotaDetalleDto?> ObtenerMascota(int id)
    {
        var mascota = await _context.Mascotas
         .Include(m => m.Protectora)
         .ThenInclude(p => p.UsuarioAsociado)
         .FirstOrDefaultAsync(m => m.Id == id);

        if (mascota == null)
        {
            return null;
        }

        DateTime fechaNacimiento;
        DateTime.TryParseExact(mascota.MesAnioNacimiento + "-01", "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out fechaNacimiento);
        var edad = DateTime.Now.Year - fechaNacimiento.Year;
        if (DateTime.Now.Month < fechaNacimiento.Month)
            edad--;

        var mascotaDetalle = new MascotaDetalleDto
        {
            Id = mascota.Id,
            Nombre = mascota.Nombre,
            TipoAnimal = mascota.TipoAnimal,
            Raza = mascota.Raza,
            Descripcion = mascota.Descripcion,
            Sexo = mascota.Sexo,
            Tamano = mascota.Tamano,
            Edad = edad,
            Ciudad = mascota.Ciudad,
            TemperamentoConAnimales = string.IsNullOrWhiteSpace(mascota.TemperamentoConAnimales) ? "-" : mascota.TemperamentoConAnimales,
            TemperamentoConPersonas = string.IsNullOrWhiteSpace(mascota.TemperamentoConPersonas) ? "-" : mascota.TemperamentoConPersonas,
            Estado = mascota.Baja ? "No Disponible" : "Disponible",
            Fotos = mascota.Fotos,
            Protectora = new ProtectoraDto
            {
                Id = mascota.ProtectoraId,
                Nombre = mascota.Protectora.Nombre
            },
            Contacto = new ContactoDto
            {
                Email = mascota.Protectora.UsuarioAsociado.Email,
                PaginaWeb = mascota.Protectora.PaginaWeb
            }
        };

        return mascotaDetalle;
    }


    public async Task<IEnumerable<MascotaDto>> ObtenerTodasMascotas()
    {
        var mascotas = await _context.Mascotas.ToListAsync();
        return mascotas.Select(m => {
            DateTime fechaNacimiento;
            DateTime.TryParseExact(m.MesAnioNacimiento + "-01", "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out fechaNacimiento);
            var edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (DateTime.Now.Month < fechaNacimiento.Month)
                edad--;

            return new MascotaDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                TipoAnimal = m.TipoAnimal,
                Raza = m.Raza,
                Descripcion = m.Descripcion,
                Sexo = m.Sexo,
                Tamano = m.Tamano,
                Edad = edad,
                Ciudad = m.Ciudad,
                MesAnioNacimiento = m.MesAnioNacimiento,
                TemperamentoConAnimales = string.IsNullOrWhiteSpace(m.TemperamentoConAnimales) ? "-" : m.TemperamentoConAnimales,
                TemperamentoConPersonas = string.IsNullOrWhiteSpace(m.TemperamentoConPersonas) ? "-" : m.TemperamentoConPersonas,
                Estado = m.Baja ? "No Disponible" : "Disponible",
                Fotos = m.Fotos,
                ProtectoraId = m.ProtectoraId
            };
        }).ToList();
    }

    public async Task<OperationResult<MascotaDto>> ModificarMascota(int id, MascotaDto input)
    {
        var result = new OperationResult<MascotaDto>();
        try
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                result.AddError("Mascota no encontrada");
                return result;
            }

            mascota.Nombre = input.Nombre;
            mascota.TipoAnimal = input.TipoAnimal;
            mascota.Raza = input.Raza;
            mascota.Descripcion = input.Descripcion;
            mascota.TemperamentoConAnimales = input.TemperamentoConAnimales;
            mascota.TemperamentoConPersonas = input.TemperamentoConPersonas;
            mascota.Sexo = input.Sexo;
            mascota.Tamano = input.Tamano;
            mascota.Ciudad = input.Ciudad;
            mascota.ProtectoraId = input.ProtectoraId;
            mascota.MesAnioNacimiento = input.MesAnioNacimiento;
            mascota.Fotos = input.Fotos;

            await _context.SaveChangesAsync(new CancellationToken());

            result.SetResult(MascotaMaps.MapToDto(mascota));
        }
        catch (Exception ex)
        {
            result.AddError(ex.Message);
        }
        return result;
    }

    public async Task<OperationResult<object>> DarDeBajaMascota(int id, MascotaBajaDto input, int idUsuario)
    {
        var result = new OperationResult<object>();

        try
        {

            var protectora = await _context.Protectoras
                .FirstOrDefaultAsync(p => p.UsuarioAsociadoId == idUsuario);

            if (protectora == null)
            {
                result.AddError("Protectora no encontrada o no autorizada");
                return result;
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null || mascota.ProtectoraId != protectora.Id)
            {
                result.AddError("Mascota no encontrada o no pertenece a la protectora autenticada");
                return result;
            }

            if (mascota.Baja)
            {
                result.AddError("La mascota ya ha sido dada de baja");
                return result;
            }

            if (input.Motivo == "adopcion")
            {
                if (input.IdMascotero == null || input.FechaAdopcion == null)
                {
                    result.AddError("Se requiere idMascotero y fechaAdopcion para la baja por adopción");
                    return result;
                }

                mascota.Baja = true;
                mascota.MotivoBaja = "adopcion";
                mascota.IdMascotero = input.IdMascotero;
                mascota.FechaAdopcion = DateTime.SpecifyKind(input.FechaAdopcion.Value, DateTimeKind.Utc);
            }
            else if (input.Motivo == "otro")
            {
                if (string.IsNullOrWhiteSpace(input.Descripcion))
                {
                    result.AddError("Se requiere una descripción para la baja por otro motivo");
                    return result;
                }

                mascota.Baja = true;
                mascota.MotivoBaja = "otro";
                mascota.DescripcionBaja = input.Descripcion;
            }
            else
            {
                result.AddError("Motivo de baja inválido");
                return result;
            }

            await _context.SaveChangesAsync(new CancellationToken());

            if (input.Motivo == "adopcion")
            {
                result.SetResult(new
                {
                    message = "Mascota eliminada por adopción",
                    idMascota = mascota.Id,
                    idMascotero = mascota.IdMascotero,
                    fechaAdopcion = mascota.FechaAdopcion
                });
            }
            else
            {
                result.SetResult(new
                {
                    message = "Mascota eliminada por otro motivo",
                    idMascota = mascota.Id,
                    motivo = "Otro",
                    descripcion = mascota.DescripcionBaja
                });
            }
        }
        catch (Exception ex)
        {
            result.AddError(ex.Message);
        }

        return result;
    }

    public static List<string>? ValidarCreacionModificacionMascota(MascotaDto input)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(input.Nombre))
            errors.Add("El campo 'nombre' es obligatorio.");
        else if (input.Nombre.Length > 50)
            errors.Add("El campo 'nombre' no puede exceder 50 caracteres.");
        if (string.IsNullOrWhiteSpace(input.TipoAnimal)) errors.Add("El campo 'tipoAnimal' es obligatorio.");
        if (string.IsNullOrWhiteSpace(input.Descripcion))
            errors.Add("El campo 'descripcion' es obligatorio.");
        else if (input.Descripcion.Length > 255)
            errors.Add("El campo 'descripcion' no puede exceder 255 caracteres.");
        if (string.IsNullOrWhiteSpace(input.Sexo))
            errors.Add("El campo 'sexo' es obligatorio.");
        else if (input.Sexo != "Macho" && input.Sexo != "Hembra")
            errors.Add("El campo 'sexo' solo puede ser 'Macho' o 'Hembra'.");

        if (string.IsNullOrWhiteSpace(input.Tamano))
            errors.Add("El campo 'tamano' es obligatorio.");
        else if (input.Tamano != "Pequeño" && input.Tamano != "Mediano" && input.Tamano != "Grande")
            errors.Add("El campo 'tamano' solo puede ser 'Pequeño', 'Mediano' o 'Grande'.");

        if (string.IsNullOrWhiteSpace(input.Ciudad))
            errors.Add("El campo 'ciudad' es obligatorio.");
        else if (input.Ciudad.Length > 100)
            errors.Add("El campo 'ciudad' no puede exceder 100 caracteres.");

        if (string.IsNullOrWhiteSpace(input.MesAnioNacimiento))
            errors.Add("El campo 'mesAnioNacimiento' es obligatorio.");
        else if (!DateTime.TryParseExact(input.MesAnioNacimiento + "-01", "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
            errors.Add("El campo 'mesAnioNacimiento' debe tener el formato YYYY-MM.");

        if (input.ProtectoraId <= 0) errors.Add("El campo 'ProtectoraId' es obligatorio.");


        if (input.Fotos == null || input.Fotos.Count == 0)
            errors.Add("Debe haber al menos una foto.");
        if (input.Fotos.Count > 10)
            errors.Add("No se pueden enviar más de 10 fotos.");
        foreach (var foto in input.Fotos)
        {
            if (!Uri.TryCreate(foto, UriKind.Absolute, out var uriResult) || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                errors.Add($"La URL '{foto}' no es válida.");
        }

        return errors.Count != 0 ? errors : null;
    }
}