--***************************************************************
-- NIVEL
INSERT INTO nivel (categoría_nivel_insignia)
VALUES
    ('Principiante'),
    ('Intermedio'),
    ('Avanzado');
--****************************************************************
-- AREA
INSERT INTO AREA (area_conocimiento)
VALUES
    ('Liderazgo Femenino'),
	('Programación');
--****************************************************************
-- COMPETENCIA
INSERT INTO COMPETENCIA (competencia_conocimiento, area_area_id, nivel_nivel_id)
VALUES
    ('Colaboración', 1, 1),
    ('Inspiración', 1, 2),
    ('Empoderamiento', 1, 3);

INSERT INTO COMPETENCIA (competencia_conocimiento, area_area_id, nivel_nivel_id)
VALUES
    ('Colaboración', 2, 1),
    ('Inspiración', 2, 2),
    ('Empoderamiento', 2, 3);
--*************************************************************************************************************************************************************************
-- Elimina el índice único existente si ya existe
IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'EVENTO__IDX' AND object_id = OBJECT_ID('dbo.EVENTO'))
BEGIN
    DROP INDEX dbo.EVENTO.EVENTO__IDX;
END

-- Crea un índice único permitiendo valores nulos
CREATE UNIQUE NONCLUSTERED INDEX EVENTO__IDX ON dbo.EVENTO (INSIGNIA_insignia_id) WHERE INSIGNIA_insignia_id IS NOT NULL;
--xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
-- EVENTO -- INSERTAR SIN FK DE INSIGNIA
INSERT INTO EVENTO (nombre, descripcion, tipo_evento, fecha_evento, hora_evento, lugar_evento, AREA_area_id, COMPETENCIA_competencia_id, USUARIO_rut)
VALUES
   ('Taller de Empoderamiento Femenino', 'Taller sobre empoderamiento femenino', 'Taller', '2023-07-20', '09:00:00', 'CITT, Sede San Andres Duoc UC',  1, 1, '20928183-k'),
   ('Curso de Empoderamiento Femenino', 'Taller sobre empoderamiento femenino', 'Curso', '2023-07-20', '09:00:00', 'CITT, Sede San Andres Duoc UC',  1, 2, '20928183-k'),
   ('Curso de Empoderamiento Femenino', 'Taller sobre empoderamiento femenino', 'Curso', '2023-07-20', '09:00:00', 'CITT, Sede San Andres Duoc UC',   1, 3, '20928183-k');
--******************************************************************************************************************************************************************************
-- ASISTENCIA

INSERT INTO ASISTENCIA (registro_asistencia_evento, fecha_registro_asistencia, USUARIO_rut, EVENTO_evento_id)
VALUES
  (1, '2023-07-20','20928183-k', 1), -- Usuario 1 registrado en el evento
  (1, '2023-07-20', '20928183-k', 2), -- Usuario 1 registrado en el evento
  (0, '2023-07-20', '20928183-k', 3); -- Usuario 1 NO registrado en el evento
--***********************************************************************************************************************************************
-- CERTIFICACIÓN
INSERT INTO CERTIFICACION (tiene_certificacion, nivel_nivel_id)
VALUES
	( 0, 1), -- Sin certificación
	( 0, 2), -- Sin certificación 
    ( 1, 3); -- Certificación 

--*******************************************
-- INSIGNIA 
SELECT * FROM INSIGNIA;
INSERT INTO INSIGNIA (nombre, descripción, objetivo, imagen_url, fecha_otorgamiento, insignia_bloqueada, NIVEL_nivel_id, AREA_area_id, COMPETENCIA_competencia_id, CERTIFICACION_certificacion_id, EVENTO_evento_id)
VALUES
    ('Insignia de Colaboración', 'Participar activamente en al menos 1 taller o evento de liderazgo femenino que promuevan \
	la colaboración efectiva, el trabajo en equipo y la construcción de redes sólidas, con el objetivo de liderar y contribuir \
	en entornos diversos y colaborativos', 'objetivo', 'https://i.ibb.co/SnHKy07/Insignia1-Estudiante.png', '2023-07-20', 0, 1, 1, 1, 1, 2),
    ( 'Insignia de Inspiración', 'Asistir y participar activamente en al menos 2 talleres o eventos de liderazgo femenino \
	centrado en el desarrollo de habilidades de liderazgo, con el propósito de adquirir conocimientos y herramientas para inspirar\
	y guiar a otros con confianza y empatía.', 'objetivo' , 'https://i.ibb.co/gZVqttg/Insignia2-Estudiante.png', '2023-08-24', 0, 2, 1, 2, 2, 3),
	('Insignia EmPodera-TIC', 'Participar activamente en al menos 3 talleres o eventos de liderazgo femenino, demostrando un \
	liderazgo excepcional al empoderar a otros, promover la igualdad de género y crear un impacto positivo en la comunidad, con \
	el propósito de ser un modelo a seguir en el liderazgo femenino.','objetivo', 'https://i.ibb.co/pdn9YCT/Insignia3-Estudiante.png', '2023-09-28', 1, 3, 1, 3, 3,4);
--********************************************************************************************************************************************************************************************
-- Asignar ID de Insignia al EVENTO
UPDATE EVENTO
SET INSIGNIA_insignia_id = 1
WHERE evento_id = 1;

UPDATE EVENTO
SET INSIGNIA_insignia_id = 2
WHERE evento_id = 2;

UPDATE EVENTO
SET INSIGNIA_insignia_id = 3
WHERE evento_id = 3;

--**************************************
-- CERTIFICADO
INSERT INTO CERTIFICADO (nombre, descripcion, certificado_url, fecha_otorgamiento, CERTIFICACION_certificacion_id, INSIGNIA_insignia_id)
VALUES
    ('Certificado de Liderazgo Femenino', 'certificado', 'https://drive.google.com/file/d/1F6YgHUjvNKs8pK6_wm9rD20SGS0_KyLF/view?usp=drive_link', '2023-09-14', 3, 3);


INSERT INTO CERTIFICADO (nombre, descripcion, certificado_url, fecha_otorgamiento, CERTIFICACION_certificacion_id, INSIGNIA_insignia_id)
VALUES
    ('Certificado de Liderazgo Femenino', 'certificado', 'https://drive.google.com/file/d/1F6YgHUjvNKs8pK6_wm9rD20SGS0_KyLF/view?usp=drive_link', '2023-09-14', 3, 6);