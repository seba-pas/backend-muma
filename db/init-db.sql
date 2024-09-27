-- init.sql

-------------------------------------------------
-- Crear tablas
-------------------------------------------------

CREATE TABLE IF NOT EXISTS "TipoRegistros" (
    id SERIAL PRIMARY KEY,
    descripcion VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS "Provincias" (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS "Ciudades" (
	id SERIAL PRIMARY KEY,
	idProvincia integer NOT NULL,
	foreign key (idProvincia) references "Provincias",
    nombre VARCHAR(100) NOT NULL
);

create table if not exists "Usuarios" (

	id serial primary key,
	email varchar(100) not null unique,
	nombre varchar(100) null,
	apellido varchar(100) null,
	password varchar(100) not null,
	idTipoRegistro integer not null,
	foreign key (idTipoRegistro) references "TipoRegistros"
);

create table if not exists "Protectoras" (

	id serial primary key,	
	usuarioAsociado integer not null,
	foreign key (usuarioAsociado) references "Usuarios",

	nombre varchar(100) not null,
	descripcion varchar(300) not null,
	paginaWeb varchar(150) null,
	instagram varchar(150) null,
	facebook varchar(150) null,

	idCiudad integer not null,
	foreign key (idCiudad) references "Ciudades",

	calle varchar(200) null,
	numero varchar(50) null,
	piso varchar(20) null,
	departamento varchar(20) null,

	cantidadDeMascotas int not null
	
);


-------------------------------------------------
-- Seedear tablas
-------------------------------------------------
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM "TipoRegistros") THEN
        INSERT INTO "TipoRegistros" (descripcion) VALUES ('Protectora'), ('Mascotero');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM "Provincias") THEN
        INSERT INTO "Provincias" (nombre) VALUES ('Buenos Aires'), ('Santa Fe'), ('Córdoba'), ('Mendoza'), ('Salta');
    END IF;
END $$;

DO $$
DECLARE idProvincia integer;
BEGIN

	IF NOT EXISTS (SELECT 1 FROM "Ciudades") THEN

		idProvincia := (select id from "Provincias" where nombre = 'Buenos Aires');
		INSERT INTO "Ciudades" (idProvincia, nombre) VALUES (idProvincia, 'CABA'), (idProvincia, 'Quilmes'), (idProvincia, 'Avellaneda'), (idProvincia, 'Morón'), (idProvincia, 'Tigre');

		idProvincia := (select id from "Provincias" where nombre = 'Santa Fe');
		INSERT INTO "Ciudades" (idProvincia, nombre) VALUES (idProvincia, 'Santa Fe'), (idProvincia, 'Rosario'), (idProvincia, 'Rafaela'), (idProvincia, 'Reconquista'), (idProvincia, 'Venado Tuerto');

		idProvincia := (select id from "Provincias" where nombre = 'Córdoba');
		INSERT INTO "Ciudades" (idProvincia, nombre) VALUES (idProvincia, 'Córdoba'), (idProvincia, 'Villa María'), (idProvincia, 'Río Cuarto'), (idProvincia, 'Villa Carlos Paz');

		idProvincia := (select id from "Provincias" where nombre = 'Mendoza');
		INSERT INTO "Ciudades" (idProvincia, nombre) VALUES (idProvincia, 'Mendoza'), (idProvincia, 'San Rafael'), (idProvincia, 'Godoy Cruz'), (idProvincia, 'Luján de Cuyo'), (idProvincia, 'Rivadavia');

		idProvincia := (select id from "Provincias" where nombre = 'Salta');
		INSERT INTO "Ciudades" (idProvincia, nombre) VALUES (idProvincia, 'Salta'), (idProvincia, 'Orán'), (idProvincia, 'Tartagal');
		
	END IF;

END $$;
