--USE HorasBecaDB
SELECT * FROM SOLICITUD
SELECT * FROM NO_AVALADA

SELECT * FROM APROBADA


SELECT * FROM EVALUACION
SELECT * FROM SOLICITUD_CANCELACION
SELECT * FROM SOLICITUD_ESPECIAL
SELECT * FROM SOLICITUD_CERTIFICACION
SELECT * FROM SOLICITUD_TA
SELECT * FROM CERTIFICACION
SELECT * FROM RECHAZADA
SELECT * FROM PERIODO

DROP TABLE SOLICITUD
DROP TABLE NO_AVALADA
DROP TABLE APROBADA
DROP TABLE RECHAZADA
DROP TABLE HORAS_ESTUDIANTE 
DROP TABLE EVALUACION
DROP TABLE PERIODO
DROP TABLE SOLICITUD_CANCELACION
DROP TABLE SOLICITUD_CERTIFICACION
DROP TABLE SOLICITUD_ESPECIAL
DROP TABLE SOLICITUD_TA
DROP TABLE CERTIFICACION

DROP PROCEDURE aprobar
DROP PROCEDURE asignar_extras
DROP PROCEDURE asignar_horas
DROP PROCEDURE borrar_periodo
DROP PROCEDURE evaluar
DROP PROCEDURE get_carnet_nombre
DROP PROCEDURE get_final
DROP PROCEDURE get_inicio
DROP PROCEDURE get_periodo
DROP PROCEDURE ingresar_periodo
DROP PROCEDURE insertar_solicitud
DROP PROCEDURE insertar_solicitud_cancelacion
DROP PROCEDURE no_avalar
DROP PROCEDURE rechazar
DROP PROCEDURE validar

DELETE FROM HORAS_ESTUDIANTE
DELETE FROM APROBADA

insertar_solicitud 1,206920,20141,'Anderson','Taylor','Cordero',89490,'estudiante','taylor@gmail.com',70.252,70.0789,'si','200-01-076',NULL,NULL,NULL,'enviada','pendiente','no',0,NULL,NULL

avalar 1
no_avalar 2,'no cumple ponderado'
aprobar 3,15,'MarcoMM'
rechazar 4,'no cumple con algo x'
asignar_horas 123456, 50
asignar_extras 123456,2
evaluar 123,'no trabaja bien','MArcoMM'


Select *from PERIODO
ingresar_periodo '2015/05/05','2018/01/01'
borrar_periodo
get_periodo

DECLARE @inicio DATE;
SET @inicio = GETDATE();
SELECT @inicio;

SELECT id_solicitud from [APROBADA] where responsable='marco'

get_carnet_nombre 14