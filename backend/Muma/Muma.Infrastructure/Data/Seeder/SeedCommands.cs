using Muma.Infrastructure.Data.Persistence;

namespace Muma.Infrastructure.Data.Seeder;

public static class SeedCommands
{
    public static string CreateTables => @"
            CREATE TABLE IF NOT EXISTS ""TipoRegistros"" (
                id SERIAL PRIMARY KEY,
                descripcion VARCHAR(100) NOT NULL UNIQUE
            );

            CREATE TABLE IF NOT EXISTS ""Provincias"" (
                id SERIAL PRIMARY KEY,
                nombre VARCHAR(100) NOT NULL UNIQUE
            );

            CREATE TABLE IF NOT EXISTS ""Ciudades"" (
                id SERIAL PRIMARY KEY,
                idProvincia INTEGER NOT NULL,
                FOREIGN KEY (idProvincia) REFERENCES ""Provincias"",
                nombre VARCHAR(100) NOT NULL
            );

            CREATE TABLE IF NOT EXISTS ""Usuarios"" (
                id SERIAL PRIMARY KEY,
                email VARCHAR(100) NOT NULL UNIQUE,
                nombre VARCHAR(100),
                apellido VARCHAR(100),
                password VARCHAR(100) NOT NULL,
                idTipoRegistro INTEGER NOT NULL,
                FOREIGN KEY (idTipoRegistro) REFERENCES ""TipoRegistros""
            );

            CREATE TABLE IF NOT EXISTS ""Protectoras"" (
                id SERIAL PRIMARY KEY,
                usuarioAsociado INTEGER NOT NULL,
                FOREIGN KEY (usuarioAsociado) REFERENCES ""Usuarios"",
                nombre VARCHAR(100) NOT NULL,
                descripcion VARCHAR(300) NOT NULL,
                paginaWeb VARCHAR(150),
                instagram VARCHAR(150),
                facebook VARCHAR(150),
                idCiudad INTEGER NOT NULL,
                FOREIGN KEY (idCiudad) REFERENCES ""Ciudades"",
                calle VARCHAR(200),
                numero VARCHAR(50),
                piso VARCHAR(20),
                departamento VARCHAR(20),
                cantidadDeMascotas INT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS ""Mascoteros"" (
                id SERIAL PRIMARY KEY,
                usuarioAsociado INTEGER NOT NULL,
                FOREIGN KEY (usuarioAsociado) REFERENCES ""Usuarios"",
                nombre VARCHAR(300) NOT NULL,
                email VARCHAR(100) NOT NULL
            );

            CREATE TABLE IF NOT EXISTS ""Mascotas"" (
                id SERIAL PRIMARY KEY,
                nombre VARCHAR(100) NOT NULL,
                tipoAnimal VARCHAR(50) NOT NULL,
                raza VARCHAR(100),
                descripcion VARCHAR(300),
                sexo VARCHAR(10) NOT NULL,
                tamano VARCHAR(20) NOT NULL,
                ciudad VARCHAR(100) NOT NULL,
                mesAnioNacimiento VARCHAR(7) NOT NULL,
                edad INTEGER NOT NULL,
                protectoraId INTEGER NOT NULL,
                FOREIGN KEY (protectoraId) REFERENCES ""Protectoras"",
                fotos TEXT[] NOT NULL,
                baja BOOLEAN NOT NULL DEFAULT FALSE,
                motivobaja VARCHAR(300),
                descripcionbaja VARCHAR(300),
                fechaadopcion DATE,
                idmascotero INTEGER,
                temperamentoconanimales VARCHAR(100),
                temperamentoconpersonas VARCHAR(100)
            );
          ";

    public static string SeedTipoRegistros => @"
            DO $$
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM ""TipoRegistros"") THEN
                    INSERT INTO ""TipoRegistros"" (descripcion) VALUES ('Protectora'), ('Mascotero');
                END IF;
            END $$;";

    public static string SeedProvincias => @"
            DO $$
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM ""Provincias"") THEN
                    INSERT INTO ""Provincias"" (nombre) VALUES ('Buenos Aires'), ('Santa Fe'), ('Córdoba'), ('Mendoza'), ('Salta');
                END IF;
            END $$;";

    public static string SeedCiudades => @"
            DO $$
            DECLARE idProvincia INTEGER;
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM ""Ciudades"") THEN

                    idProvincia := (SELECT id FROM ""Provincias"" WHERE nombre = 'Buenos Aires');
                    INSERT INTO ""Ciudades"" (idProvincia, nombre) 
                    VALUES (idProvincia, 'CABA'), (idProvincia, 'Quilmes'), (idProvincia, 'Avellaneda'), (idProvincia, 'Morón'), (idProvincia, 'Tigre');

                    idProvincia := (SELECT id FROM ""Provincias"" WHERE nombre = 'Santa Fe');
                    INSERT INTO ""Ciudades"" (idProvincia, nombre)
                    VALUES (idProvincia, 'Santa Fe'), (idProvincia, 'Rosario'), (idProvincia, 'Rafaela'), (idProvincia, 'Reconquista'), (idProvincia, 'Venado Tuerto');

                    idProvincia := (SELECT id FROM ""Provincias"" WHERE nombre = 'Córdoba');
                    INSERT INTO ""Ciudades"" (idProvincia, nombre)
                    VALUES (idProvincia, 'Córdoba'), (idProvincia, 'Villa María'), (idProvincia, 'Río Cuarto'), (idProvincia, 'Villa Carlos Paz');

                    idProvincia := (SELECT id FROM ""Provincias"" WHERE nombre = 'Mendoza');
                    INSERT INTO ""Ciudades"" (idProvincia, nombre)
                    VALUES (idProvincia, 'Mendoza'), (idProvincia, 'San Rafael'), (idProvincia, 'Godoy Cruz'), (idProvincia, 'Luján de Cuyo'), (idProvincia, 'Rivadavia');

                    idProvincia := (SELECT id FROM ""Provincias"" WHERE nombre = 'Salta');
                    INSERT INTO ""Ciudades"" (idProvincia, nombre)
                    VALUES (idProvincia, 'Salta'), (idProvincia, 'Orán'), (idProvincia, 'Tartagal');

                END IF;
            END $$;";

   
}
