--USE HorasBecaDB
CREATE PROCEDURE insertar_solicitud
	@semestre INT,
	@cedula	INT,
	@carnet INT,
	@nombre VARCHAR(20),
	@apellido1 VARCHAR(20),
	@apellido2 VARCHAR(20),
	@telefono INT,
	@tipo_beca VARCHAR(20),
	@email VARCHAR(20),
	@ponderado_general FLOAT(4),
	@ponderado_semestral FLOAT(4),
	@cumple_requisitos VARCHAR(4),
	@cuenta_bancaria VARCHAR(20),
	@screen_ponderado_general VARCHAR(50),
	@screen_ponderado_semestral VARCHAR(50),
	@screen_cuenta_bancaria VARCHAR(50), 
	@estado_estudiante VARCHAR(12),
	@estado_sistema VARCHAR(12),
	@tiene_nombramiento VARCHAR(4),
	@horas_nombradas INT,
	@tipo_beca_nombrada VARCHAR(20),
	@lugar_nombramiento VARCHAR(50)
AS
	DECLARE @fecha DATE;
	SET @fecha = GETDATE();

	INSERT INTO SOLICITUD VALUES
	(@fecha,
	@semestre,
	@cedula,
	@carnet,
	@nombre,
	@apellido1,
	@apellido2,
	@telefono,
	@tipo_beca,
	@email,
	@ponderado_general,
	@ponderado_semestral,
	@cumple_requisitos,
	@cuenta_bancaria,
	@screen_ponderado_general,
	@screen_ponderado_semestral,
	@screen_cuenta_bancaria, 
	@estado_estudiante,
	@estado_sistema,
	@tiene_nombramiento,
	@horas_nombradas,
	@tipo_beca_nombrada,
	@lugar_nombramiento)
GO

CREATE PROCEDURE eliminar_solicitud
	@id_solicitud INT
AS
	DELETE FROM SOLICITUD WHERE id_solicitud=@id_solicitud
GO

CREATE PROCEDURE insertar_solicitud_especial
	@id_solicitud INT,
	@horas_disponibles INT

AS 
	INSERT INTO SOLICITUD_ESPECIAL VALUES(
	@id_solicitud,
	@horas_disponibles)
GO

CREATE PROCEDURE insertar_solicitud_ta
	@id_solicitud INT,
	@id_curso VARCHAR(12),
	@nombre_curso VARCHAR(50),
	@nota INT,
	@responsable VARCHAR(50),
	@screen_nota VARCHAR(50)
AS
	INSERT INTO SOLICITUD_TA VALUES(
	@id_solicitud,
	@id_curso,
	@nombre_curso,
	@nota,
	@responsable,
	@screen_nota)
GO

CREATE PROCEDURE insertar_solicitud_certificacion
	@carnet INT,
	@semestre INT,
	@año INT,
	@estado VARCHAR(12)
AS
	INSERT INTO SOLICITUD_CERTIFICACION VALUES(
	@carnet,
	@semestre,
	@año,
	@estado)
GO

CREATE PROCEDURE insertar_certificacion
	@id_solicitud INT,
	@carnet INT,
	@nombre VARCHAR(20),
	@apellido1 VARCHAR(20),
	@apellido2 VARCHAR(20),
	@semestre INT,
	@año INT,
	@horas INT,
	@responsable VARCHAR(30)
	
AS
	INSERT INTO CERTIFICACION VALUES(
	@id_solicitud,
	@carnet,
	@nombre,
	@apellido1,
	@apellido2,
	@semestre,
	@año,
	@horas,
	@responsable)
GO

CREATE PROCEDURE insertar_solicitud_cancelacion
	@id_solicitud INT,
	@observacion VARCHAR(300)
AS
	INSERT INTO SOLICITUD_CANCELACION VALUES(
	@id_solicitud,
	@observacion)
GO

CREATE PROCEDURE cancelar
	@id_solicitud INT
AS
	UPDATE SOLICITUD
	SET estado_estudiante='cancelada' 
	WHERE id_solicitud=@id_solicitud	

	UPDATE SOLICITUD
	SET estado_sistema='cancelada'
	WHERE id_solicitud=@id_solicitud	
GO


CREATE PROCEDURE avalar
	@id_solicitud INT
AS 
	UPDATE SOLICITUD
	SET estado_sistema='avalada'
	WHERE id_solicitud=@id_solicitud

GO

CREATE PROCEDURE no_avalar
	@id_solicitud INT,
	@observacion VARCHAR(100)
AS
	INSERT INTO NO_AVALADA VALUES
	(@id_solicitud,
	@observacion)

	UPDATE SOLICITUD
	SET estado_sistema='noAvalada'
	WHERE id_solicitud=@id_solicitud

GO 

CREATE PROCEDURE aprobar
	@id_solicitud INT,
	@horas INT,
	@responsable VARCHAR(25),
	@horas_extra INT
AS
	DECLARE @carnet INT;
	SELECT @carnet = carnet FROM SOLICITUD 	WHERE id_solicitud=@id_solicitud;

	INSERT INTO APROBADA VALUES
	(@id_solicitud,
	@carnet,
	@horas,
	@responsable,
	@horas_extra)

	UPDATE SOLICITUD
	SET estado_sistema='aprobada'
	WHERE id_solicitud=@id_solicitud
GO

CREATE PROCEDURE get_carnet
	@id_solicitud INT
AS
	SELECT carnet
	FROM SOLICITUD
	WHERE id_solicitud=@id_solicitud
GO

CREATE PROCEDURE rechazar	
	@id_solicitud INT,
	@observacion VARCHAR(300)
AS
	INSERT INTO RECHAZADA VALUES
	(@id_solicitud,
	@observacion)
	
	UPDATE SOLICITUD
	SET estado_sistema='rechazada'
	WHERE id_solicitud=@id_solicitud
GO


CREATE PROCEDURE asignar_extras
	@carnet INT,
	@horas INT
AS
	UPDATE APROBADA
	SET horas_extra=@horas
	WHERE carnet=@carnet
GO

CREATE PROCEDURE evaluar
	@carnet INT,
	@observacion VARCHAR(10),
	@responsable VARCHAR(25)
AS
    DECLARE @fecha DATE;
	SET @fecha = GETDATE();

	INSERT INTO EVALUACION VALUES
	(@carnet,
	@observacion,
	@responsable,
	@fecha)
GO

CREATE PROCEDURE ingresar_periodo
	@fecha_inicio DATE,
	@fecha_final DATE
AS
	DELETE FROM PERIODO
	INSERT INTO PERIODO VALUES(@fecha_inicio,@fecha_final)
GO



CREATE PROCEDURE get_carnet_nombre
	@id INT
AS
	SELECT carnet,nombre, apellido1, apellido2 
	FROM SOLICITUD 
	WHERE id_solicitud=@id 
GO
