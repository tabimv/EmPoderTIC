--***************************************************************
-- *Obligatorio**
-- TIPO_PERFIL
INSERT INTO TIPO_PERFIL (nombre_tipo_perfil, dominio_correo)
VALUES 
('Estudiante y/o Titulado', 'duocuc.cl'),
('Docente', '@profesor.duoc.cl'),
('Colaborador administrativo', 'duoc.cl'),
('Administrador', 'Confidencial');
--***************************************************************
-- USUARIO
INSERT INTO usuario (rut, nombre, apellido_paterno, apellido_materno, correo_electronico, contraseña, tipo_perfil_tipo_perfil_id, estado_confirmacion, activo)
VALUES ('20489000-5', 'Ignacio', 'Selman', 'Caro', 'empodertic@gmail.com', 'admin2023', 4, 1, 1);
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
--*************************************************************************************************************************************************************************
-- EVENTO 
INSERT INTO EVENTO (nombre, descripcion, tipo_evento, fecha_evento, hora_evento, lugar_evento, AREA_area_id, COMPETENCIA_competencia_id)
VALUES
   ('Taller de Empoderamiento Femenino', 'Taller sobre empoderamiento femenino', 'Taller', '2023-07-20', '09:00:00', 'CITT, Sede San Andres Duoc UC',  1, 1),
   ('Curso de Empoderamiento Femenino', 'Taller sobre empoderamiento femenino', 'Curso', '2023-08-20', '09:00:00', 'CITT, Sede San Andres Duoc UC',  1, 2),
   ('Curso de Empoderamiento Femenino', 'Taller sobre empoderamiento femenino', 'Curso', '2023-09-20', '09:00:00', 'CITT, Sede San Andres Duoc UC',   1, 3);
--******************************************************************************************************************************************************************************
-- CERTIFICACIÓN
INSERT INTO CERTIFICACION (tiene_certificacion, nivel_nivel_id)
VALUES
	( 0, 1), -- Sin certificación
	( 0, 2), -- Sin certificación 
    ( 1, 3); -- Certificación 
--*******************************************
-- INSIGNIA ALUMNOS Y/O TITULADOS DUOC UC
INSERT INTO INSIGNIA (nombre, descripción, objetivo, imagen_url, NIVEL_nivel_id, AREA_area_id, COMPETENCIA_competencia_id, CERTIFICACION_certificacion_id, EVENTO_evento_id, TIPO_PERFIL_tipo_perfil_id)
VALUES
    ('Insignia de Colaboración', ' Reconocimiento por participación activa en al menos 1 taller o evento de liderazgo femenino, destacándote en la promoción \
	de la colaboración efectiva, trabajo en equipo y construcción de redes sólidas.', 'Liderar y contribuir en entornos diversos y colaborativos a través de la participación activa en eventos que promuevan estas habilidades.',
	'https://i.ibb.co/SnHKy07/Insignia1-Estudiante.png',  1, 1, 1, 1, 1, 1),
    ( 'Insignia de Inspiración', 'Reconocimiento por asistir y participar activamente en al menos 2 talleres o eventos de liderazgo femenino, centrados en el desarrollo de habilidades de liderazgo para inspirar y guiar a otros \
	con confianza y empatía', 'Adquirir conocimientos y herramientas para ser una fuente de inspiración y liderar con confianza y empatía en diversos contextos.' , 'https://i.ibb.co/gZVqttg/Insignia2-Estudiante.png', 2, 1, 2, 2, 2, 1),
	('Insignia EmPodera-TIC', 'Reconocimiento por participar activamente en al menos 3 talleres o eventos de liderazgo femenino, demostrando liderazgo excepcional al empoderar a otros, promover la igualdad de género y crear un \ 
	impacto positivo en la comunidad.','Ser un modelo a seguir en el liderazgo femenino, especialmente en el ámbito tecnológico, mediante el liderazgo, la promoción de la igualdad y la generación de impacto positivo en la comunidad.', 'https://i.ibb.co/pdn9YCT/Insignia3-Estudiante.png', 3, 1, 3, 3,3, 1);
--********************************************************************************************************************************************************************************************
-- INSIGNIA DOCENTE DUOC UC
INSERT INTO INSIGNIA (nombre, descripción, objetivo, imagen_url, NIVEL_nivel_id, AREA_area_id, COMPETENCIA_competencia_id, CERTIFICACION_certificacion_id, EVENTO_evento_id, TIPO_PERFIL_tipo_perfil_id)
VALUES
    ('Insignia de Mentoría y Guía', 'Participar o colaborar en al menos 1 taller o evento de liderazgo femenino que promuevan el \
	crecimiento personal, la adquisición de habilidades de liderazgo, la construcción de redes sólidas y la inspiración de mujeres para asumir \
	roles de liderazgo en diversos ámbitos de la sociedad.', 'contribuir de manera significativa al fortalecimiento del liderazgo y empoderamiento \
	de las mujeres a través de la mentoría y la guía activa en eventos específicos de liderazgo femenino. ', 'https://i.ibb.co/WggL44Y/insignia-docente-1.png',  1, 1, 1, 1, 1, 2),
    ( 'Insignia de Facilitador de Inspiración', 'Participar o colaborar en al menos 2 talleres o eventos de liderazgo femenino que promuevan \
	la inspiración el empoderamiento y el desarrollo personal. Tu contribución en estos espacios debe reflejar un compromiso genuino para \
	inspirar a otros y fomentar un entorno de liderazgo positivo', 'Contribuir de manera significativa a estos eventos, compartiendo nuestras \
	experiencias, conocimientos y perspectivas para motivar y guiar a otras personas. Nuestro compromiso es inspirar a las participantes, \
	fomentar un entorno positivo de liderazgo y brindar apoyo a quienes buscan crecer y alcanzar sus metas.', 'https://i.ibb.co/qMqQFqC/insignia-docente-2.png', 2, 1, 2, 2, 2, 2),
	('Insignia EmPodera-TIC', 'Participar o colaborar activamente en al menos 3 talleres o eventos de liderazgo femenino, demostrando un \
	liderazgo excepcional al empoderar a otros, promover la igualdad de género y crear un impacto positivo en la comunidad, con \
	el propósito de ser un modelo a seguir en el liderazgo femenino.','Contribuir de manera sobresaliente al empoderamiento de las mujeres \
	al liderar y participar activamente en eventos de liderazgo femenino', 'https://i.ibb.co/NyZJkyy/insignia-docente-3.png', 3, 1, 3, 3,3, 2);


--************************************************************************************************************************************************************************
-- INSIGNIA COLABORADOR ADMINISTRATIVO DE DUOC UC
INSERT INTO INSIGNIA (nombre, descripción, objetivo, imagen_url, NIVEL_nivel_id, AREA_area_id, COMPETENCIA_competencia_id, CERTIFICACION_certificacion_id, EVENTO_evento_id, TIPO_PERFIL_tipo_perfil_id)
VALUES
    ('Insignia de Colaboración', 'Reconocimiento por participar o colaborar en al menos 1 taller o evento de liderazgo femenino que promueva la colaboración efectiva, el trabajo en equipo y la construcción de redes sólidas.\
	El objetivo es liderar y contribuir en entornos diversos y colaborativos.', 'Participar activamente en eventos que fomenten la colaboración efectiva, el trabajo en equipo y la construcción de redes sólidas para liderar\
	y contribuir en entornos diversos y colaborativos.', 'https://i.ibb.co/mN0RvQ4/Insignia-Colaborador-1.png',  1, 1, 1, 1, 1, 3),
    ( 'Insignia de Impacto', 'Reconocimiento por participar o colaborar activamente en al menos 2 talleres o eventos de liderazgo femenino centrados en el desarrollo de habilidades de liderazgo. El propósito es adquirir \
	conocimientos y herramientas para inspirar y guiar a otros con confianza y empatía.', ' Participar activamente en eventos que permitan adquirir conocimientos y herramientas para inspirar y guiar a otros con confianza y empatía, \
	centrándose en el desarrollo de habilidades de liderazgo.' , 'https://i.ibb.co/Tthgxwn/Insignia-Colaborador-2.png', 2, 1, 2, 2, 2, 3),
	('Insignia EmPodera-TIC', 'Reconocimiento por participar o colaborar activamente en al menos 3 talleres o eventos de liderazgo femenino, demostrando un liderazgo excepcional al empoderar a otros, promover la igualdad de género y \ 
	crear un impacto positivo en la comunidad. El propósito es ser un modelo a seguir en el liderazgo femenino.','Participar de manera activa en eventos de liderazgo femenino, mostrando liderazgo al empoderar a otros, promover la igualdad \
	de género y crear un impacto positivo en la comunidad.', 'https://i.ibb.co/fF914FC/Insignia-Colaborador-3.png', 3, 1, 3, 3,3, 3);
--************************************************************************************************************************************************************************
-- ASISTENCIA
-- CREAR USUARIO Y SEGUIR EJEMPLOS
INSERT INTO ASISTENCIA (registro_asistencia_evento, fecha_registro_asistencia, USUARIO_rut, EVENTO_evento_id)
VALUES
  (1, '2023-07-20', '20928183-k', 1), -- Usuario 1 registrado en el evento
  (1, '2023-08-20', '20928183-k', 2), -- Usuario 1 registrado en el evento
  (0, '2023-09-20', '20928183-k', 3); -- Usuario 1 NO registrado en el evento
--***********************************************************************************************************************************************
-- OTORGAR_INSIGNIA DOCENTES
-- CREAR USUARIOS Y SEGUIR EJEMPLOS
INSERT INTO OTORGAR_INSIGNIA_P2 (registro_insignia_evento, contribucion_evento, fecha_registro_otorgamiento, USUARIO_rut, EVENTO_evento_id)
VALUES 
     (1, 'Participación y organización de Evento a Talleres de Liderazgo Femenino', '2023-07-20', '20441350-9', 1), -- Se le otorga Insignia
	 (0, 'Participación y organización de Evento a Talleres de Liderazgo Femenino', '2023-08-20', '20441350-9', 2), -- No le otorga Insignia
	 (1, 'Participación y organización de Evento a Talleres de Liderazgo Femenino', '2023-09-20', '20441350-9', 3); -- Se le otorga Insignia
--*********************************************************************************************************************************************************************************
-- CERTIFICADO
INSERT INTO CERTIFICADO (nombre, descripcion, CERTIFICACION_certificacion_id, INSIGNIA_insignia_id, AREA_area_id)
VALUES
    ('Certificado de Liderazgo Femenino', 'certificado', 3, 3, 1);
	--*****************************************************************************************************************************************************************************
-- USUARIO_CERTIFICADO
INSERT INTO USUARIO_CERTIFICADO (fecha_otorgamiento, certificado_url, CERTIFICADO_certificado_id, USUARIO_rut)
VALUES 
      ('2023-09-20', NULL, 1, '20928183-k');