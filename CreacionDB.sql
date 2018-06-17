--USE HorasBecaDB
CREATE TABLE SOLICITUD (
	id_solicitud INT IDENTITY(1,1), 
	fecha DATE,
	semestre INT,
	cedula	INT,
	carnet INT,
	nombre VARCHAR(20),
	apellido1 VARCHAR(20),
	apellido2 VARCHAR(20),
	telefono INT,
	tipo_beca VARCHAR(20),
	email VARCHAR(25),
	ponderado_general FLOAT(4),
	ponderado_semestral FLOAT(4),
	cumple_requisitos VARCHAR(4),
	cuenta_bancaria VARCHAR(20),
	screen_ponderado_general VARCHAR(50),
	screen_ponderado_semestral VARCHAR(50),
	screen_cuenta_bancaria VARCHAR(50),
	estado_estudiante VARCHAR(12),
	estado_sistema VARCHAR(12),
	tiene_nombramiento VARCHAR(4),
	horas_nombradas INT,
	tipo_beca_nombrada VARCHAR(20),
	lugar_nombramiento VARCHAR(50),
	PRIMARY KEY(id_solicitud),
	CONSTRAINT CHK_EESTUDIANTE CHECK(estado_estudiante IN('enviada','cancelada','guardada')),
	CONSTRAINT CHK_ESISTEMA CHECK (estado_sistema IN('noRecibida','pendiente','avalada','noAvalada','aprobada','rechazada','cancelada')),
	CONSTRAINT CHK_TIPOBECA CHECK (tipo_beca IN('estudiante','asistente','tutor','especial')),
	CONSTRAINT CHK_REQUISITOS CHECK (cumple_requisitos IN('si','no')),
	CONSTRAINT CHK_NOMBRAMIENTO CHECK (tiene_nombramiento IN('si','no'))
);

CREATE TABLE SOLICITUD_ESPECIAL(
	id_solicitud INT,
	horas_disponibles INT,
	PRIMARY KEY(id_solicitud),
	FOREIGN KEY(id_solicitud) REFERENCES SOLICITUD(id_solicitud)
);

CREATE TABLE SOLICITUD_TA (
	id_solicitud INT,
	id_curso VARCHAR(12),
	nombre_curso VARCHAR(50),
	nota INT,
	responsable VARCHAR(50),
	screen_nota VARCHAR(50),
	PRIMARY KEY(id_solicitud),
	FOREIGN KEY(id_solicitud) REFERENCES SOLICITUD(id_solicitud)
);


CREATE TABLE SOLICITUD_CERTIFICACION (
	id_solicitud INT IDENTITY(1,1), 
	carnet INT,
	semestre INT,
	año INT,
	estado VARCHAR(12),
	PRIMARY KEY(id_solicitud),
	CONSTRAINT CHK_EESTADO CHECK(estado IN('pendiente','lista'))
);

CREATE TABLE CERTIFICACION(
	id_solicitud INT, 
	carnet INT,
	nombre VARCHAR(20),
	apellido1 VARCHAR(20),
	apellido2 VARCHAR(20),
	semestre INT,
	año INT,
	horas INT,
	responsable VARCHAR(30),
	PRIMARY KEY(id_solicitud),
	FOREIGN KEY(id_solicitud) REFERENCES SOLICITUD_CERTIFICACION(id_solicitud)
);

CREATE TABLE SOLICITUD_CANCELACION(
	id_solicitud INT,
	observacion VARCHAR(300),
	PRIMARY KEY(id_solicitud),
	FOREIGN KEY(id_solicitud) REFERENCES SOLICITUD(id_solicitud)
);

-- Validada, NoValidada, Aprobada, Rechazada 
CREATE TABLE NO_AVALADA(
	id_solicitud INT,
	observacion VARCHAR(100),
	PRIMARY KEY(id_solicitud),
	FOREIGN KEY(id_solicitud) REFERENCES SOLICITUD(id_solicitud)
); 

CREATE TABLE APROBADA(
	id_solicitud INT,
	carnet INT,
	horas INT,
	responsable VARCHAR(25),
	horas_extra INT,
	PRIMARY KEY(id_solicitud),
	FOREIGN KEY(id_solicitud) REFERENCES SOLICITUD(id_solicitud)
);

CREATE TABLE RECHAZADA(
	id_solicitud INT,
	observacion VARCHAR(100),
	PRIMARY KEY(id_solicitud),
	FOREIGN KEY(id_solicitud) REFERENCES SOLICITUD(id_solicitud)
);

CREATE TABLE HORAS_ESTUDIANTE(
	carnet INT,
	horas INT,
	horas_extra INT DEFAULT 0,
	PRIMARY KEY(carnet)
);

CREATE TABLE EVALUACION(
	carnet INT,
	observacion VARCHAR(10),
	responsable VARCHAR(25),
	fecha DATE
	PRIMARY KEY(carnet,observacion,responsable)
);

CREATE TABLE PERIODO(
	fecha_inicio DATE,
	fecha_fin DATE
);



