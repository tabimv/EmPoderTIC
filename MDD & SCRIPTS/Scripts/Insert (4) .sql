--***************************************************************
-- NIVEL
INSERT INTO nivel (categor�a_nivel_insignia)
VALUES
    ('Principiante'),
    ('Intermedio'),
    ('Avanzado');
--****************************************************************
-- AREA
INSERT INTO AREA (area_conocimiento)
VALUES
    ('Liderazgo Femenino'),
	('Programaci�n');
--****************************************************************
-- COMPETENCIA
INSERT INTO COMPETENCIA (competencia_conocimiento, area_area_id, nivel_nivel_id)
VALUES
    ('Colaboraci�n', 1, 1),
    ('Inspiraci�n', 1, 2),
    ('Empoderamiento', 1, 3);

--*************************************************************************************************************************************************************************
-- EVENTO 
INSERT INTO EVENTO (nombre, descripcion, tipo_evento, fecha_evento, hora_evento, lugar_evento, AREA_area_id, COMPETENCIA_competencia_id)
VALUES
   ('Taller de Empoderamiento Femenino', 'Taller sobre empoderamiento femenino', 'Taller', '2023-07-20', '09:00:00', 'CITT, Sede San Andres Duoc UC',  1, 1),
   ('Curso de Empoderamiento Femenino', 'Taller sobre empoderamiento femenino', 'Curso', '2023-08-20', '09:00:00', 'CITT, Sede San Andres Duoc UC',  1, 2),
   ('Curso de Empoderamiento Femenino', 'Taller sobre empoderamiento femenino', 'Curso', '2023-09-20', '09:00:00', 'CITT, Sede San Andres Duoc UC',   1, 3);
--******************************************************************************************************************************************************************************
-- CERTIFICACI�N
INSERT INTO CERTIFICACION (tiene_certificacion, nivel_nivel_id)
VALUES
	( 0, 1), -- Sin certificaci�n
	( 0, 2), -- Sin certificaci�n 
    ( 1, 3); -- Certificaci�n 

--*******************************************
-- INSIGNIA ALUMNOS Y/O TITULADOS DUOC UC
INSERT INTO INSIGNIA (nombre, descripci�n, objetivo, imagen_url, NIVEL_nivel_id, AREA_area_id, COMPETENCIA_competencia_id, CERTIFICACION_certificacion_id, EVENTO_evento_id, TIPO_PERFIL_tipo_perfil_id)
VALUES
    ('Insignia de Colaboraci�n', 'Participar activamente en al menos 1 taller o evento de liderazgo femenino que promuevan \
	la colaboraci�n efectiva, el trabajo en equipo y la construcci�n de redes s�lidas, con el objetivo de liderar y contribuir \
	en entornos diversos y colaborativos', 'objetivo', 'https://i.ibb.co/SnHKy07/Insignia1-Estudiante.png',  1, 1, 1, 1, 1, 1),
    ( 'Insignia de Inspiraci�n', 'Asistir y participar activamente en al menos 2 talleres o eventos de liderazgo femenino \
	centrado en el desarrollo de habilidades de liderazgo, con el prop�sito de adquirir conocimientos y herramientas para inspirar\
	y guiar a otros con confianza y empat�a.', 'objetivo' , 'https://i.ibb.co/gZVqttg/Insignia2-Estudiante.png', 2, 1, 2, 2, 2, 1),
	('Insignia EmPodera-TIC', 'Participar activamente en al menos 3 talleres o eventos de liderazgo femenino, demostrando un \
	liderazgo excepcional al empoderar a otros, promover la igualdad de g�nero y crear un impacto positivo en la comunidad, con \
	el prop�sito de ser un modelo a seguir en el liderazgo femenino.','objetivo', 'https://i.ibb.co/pdn9YCT/Insignia3-Estudiante.png', 3, 1, 3, 3,3, 1);
--********************************************************************************************************************************************************************************************
-- INSIGNIA DOCENTE DUOC UC
INSERT INTO INSIGNIA (nombre, descripci�n, objetivo, imagen_url, NIVEL_nivel_id, AREA_area_id, COMPETENCIA_competencia_id, CERTIFICACION_certificacion_id, EVENTO_evento_id, TIPO_PERFIL_tipo_perfil_id)
VALUES
    ('Insignia de Mentor�a y Gu�a', 'Participar o colaborar en al menos 1 taller o evento de liderazgo femenino que promuevan el \
	crecimiento personal, la adquisici�n de habilidades de liderazgo, la construcci�n de redes s�lidas y la inspiraci�n de mujeres para asumir \
	roles de liderazgo en diversos �mbitos de la sociedad.', 'contribuir de manera significativa al fortalecimiento del liderazgo y empoderamiento \
	de las mujeres a trav�s de la mentor�a y la gu�a activa en eventos espec�ficos de liderazgo femenino. ', 'https://i.ibb.co/WggL44Y/insignia-docente-1.png',  1, 1, 1, 1, 1, 2),
    ( 'Insignia de Facilitador de Inspiraci�n', 'Participar o colaborar en al menos 2 talleres o eventos de liderazgo femenino que promuevan \
	la inspiraci�n el empoderamiento y el desarrollo personal. Tu contribuci�n en estos espacios debe reflejar un compromiso genuino para \
	inspirar a otros y fomentar un entorno de liderazgo positivo', 'Contribuir de manera significativa a estos eventos, compartiendo nuestras \
	experiencias, conocimientos y perspectivas para motivar y guiar a otras personas. Nuestro compromiso es inspirar a las participantes, \
	fomentar un entorno positivo de liderazgo y brindar apoyo a quienes buscan crecer y alcanzar sus metas.', 'https://i.ibb.co/WggL44Y/insignia-docente-2.png', 2, 1, 2, 2, 2, 2),
	('Insignia EmPodera-TIC', 'Participar o colaborar activamente en al menos 3 talleres o eventos de liderazgo femenino, demostrando un \
	liderazgo excepcional al empoderar a otros, promover la igualdad de g�nero y crear un impacto positivo en la comunidad, con \
	el prop�sito de ser un modelo a seguir en el liderazgo femenino.','Contribuir de manera sobresaliente al empoderamiento de las mujeres \
	al liderar y participar activamente en eventos de liderazgo femenino', 'https://i.ibb.co/NyZJkyy/insignia-docente-3.png', 3, 1, 3, 3,3, 2);
--************************************************************************************************************************************************************************
-- INSIGNIA COLABORADOR ADMINISTRATIVO DE DUOC UC
INSERT INTO INSIGNIA (nombre, descripci�n, objetivo, imagen_url, NIVEL_nivel_id, AREA_area_id, COMPETENCIA_competencia_id, CERTIFICACION_certificacion_id, EVENTO_evento_id, TIPO_PERFIL_tipo_perfil_id)
VALUES
    ('Insignia de Colaboraci�n', 'Participar o colaborar en al menos 1 taller o evento de liderazgo femenino que promuevan \
	la colaboraci�n efectiva, el trabajo en e	|quipo y la construcci�n de redes s�lidas, con el objetivo de liderar y contribuir \
	en entornos diversos y colaborativos', 'objetivo', 'https://i.ibb.co/mN0RvQ4/Insignia-Colaborador-1.png',  1, 1, 1, 1, 1, 3),
    ( 'Insignia de Impacto', 'Participar o colaborar activamente en al menos 2 talleres o eventos de liderazgo femenino \
	centrado en el desarrollo de habilidades de liderazgo, con el prop�sito de adquirir conocimientos y herramientas para inspirar\
	y guiar a otros con confianza y empat�a.', 'objetivo' , 'https://i.ibb.co/Tthgxwn/Insignia-Colaborador-2.png', 2, 1, 2, 2, 2, 3),
	('Insignia EmPodera-TIC', 'Participar o colaborar activamente en al menos 3 talleres o eventos de liderazgo femenino, demostrando un \
	liderazgo excepcional al empoderar a otros, promover la igualdad de g�nero y crear un impacto positivo en la comunidad, con \
	el prop�sito de ser un modelo a seguir en el liderazgo femenino.','objetivo', 'https://i.ibb.co/fF914FC/Insignia-Colaborador-3.png', 3, 1, 3, 3,3, 3);
--************************************************************************************************************************************************************************
-- ASISTENCIA
INSERT INTO ASISTENCIA (registro_asistencia_evento, fecha_registro_asistencia, USUARIO_rut, EVENTO_evento_id)
VALUES
  (1, '2023-07-20', '20928183-k', 1), -- Usuario 1 registrado en el evento
  (1, '2023-08-20', '20928183-k', 2), -- Usuario 1 registrado en el evento
  (0, '2023-09-20', '20928183-k', 3); -- Usuario 1 NO registrado en el evento
--***********************************************************************************************************************************************
-- OTORGAR_INSIGNIA DOCENTES
INSERT INTO OTORGAR_INSIGNIA_P2 (registro_insignia_evento, contribucion_evento, fecha_registro_otorgamiento, USUARIO_rut, EVENTO_evento_id)
VALUES 
     (1, 'Participaci�n y organizaci�n de Evento a Talleres de Liderazgo Femenino', '2023-07-20', '20441350-9', 1), -- Se le otorga Insignia
	 (0, 'Participaci�n y organizaci�n de Evento a Talleres de Liderazgo Femenino', '2023-08-20', '20441350-9', 2), -- No le otorga Insignia
	 (1, 'Participaci�n y organizaci�n de Evento a Talleres de Liderazgo Femenino', '2023-09-20', '20441350-9', 3); -- Se le otorga Insignia
--*********************************************************************************************************************************************************************************
-- CERTIFICADO
INSERT INTO CERTIFICADO (nombre, descripcion, certificado_url, fecha_otorgamiento, CERTIFICACION_certificacion_id, INSIGNIA_insignia_id, USUARIO_rut, AREA_area_id)
VALUES
    ('Certificado de Liderazgo Femenino', 'certificado', 'https://drive.google.com/file/d/1F6YgHUjvNKs8pK6_wm9rD20SGS0_KyLF/view?usp=drive_link', '2023-07-22', 3, 3, '20928183-k', 1);
	--*****************************************************************************************************************************************************************************