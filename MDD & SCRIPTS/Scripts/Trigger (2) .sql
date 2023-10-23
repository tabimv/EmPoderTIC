DROP TRIGGER InsertarControlInsigniaPorAsistencia;
DROP TRIGGER  InsertarControlInsigniaPorOtorgamiento;
DROP TRIGGER InsertarControlInsigniaPorOtorgamientoAdministrativo;
--***************************************************************************************
-- Trigger para la tabla CONTROL_INSIGNIA según la ASISTENCIA para un Alumno y/o Títulado
CREATE TRIGGER InsertarControlInsigniaPorAsistencia
ON ASISTENCIA
AFTER INSERT
AS
BEGIN
    -- Insertar registros en CONTROL_INSIGNIA basados en la lógica especificada
    INSERT INTO CONTROL_INSIGNIA (insignia_bloqueada, USUARIO_rut, fecha_otorgamiento, INSIGNIA_insignia_id)
    SELECT 
        CASE
            WHEN i.registro_asistencia_evento = 0 THEN 1 -- Bloquear insignia si registro_asistencia_evento es 0
            WHEN i.registro_asistencia_evento = 1 AND 
                ((SELECT TOP 1 ci.insignia_bloqueada
                FROM CONTROL_INSIGNIA ci
                JOIN INSIGNIA ins ON ins.insignia_id = ci.INSIGNIA_insignia_id
                JOIN AREA a ON a.area_id = ins.AREA_area_id
                WHERE ci.USUARIO_rut = i.USUARIO_rut AND ins.AREA_area_id = a.area_id AND ins.TIPO_PERFIL_tipo_perfil_id = 1
                ORDER BY fecha_otorgamiento DESC) = 1) THEN 1
            ELSE 0 -- Default: Bloquear insignia si no se cumplen las condiciones anteriores
        END AS insignia_bloqueada,
        i.USUARIO_rut, -- Rut del usuario
        i.fecha_registro_asistencia, -- Fecha de asistencia
        ins.insignia_id -- Obtener el INSIGNIA_insignia_id desde la tabla INSIGNIA
    FROM INSERTED i
    INNER JOIN INSIGNIA ins ON i.EVENTO_evento_id = ins.EVENTO_evento_id
    WHERE NOT EXISTS (
        SELECT 1
        FROM CONTROL_INSIGNIA ci
        WHERE ci.USUARIO_rut = i.USUARIO_rut
          AND ci.INSIGNIA_insignia_id = ins.insignia_id AND ins.TIPO_PERFIL_tipo_perfil_id = 1
    )
	 AND ins.TIPO_PERFIL_tipo_perfil_id = 1
    ORDER BY ins.NIVEL_nivel_id; -- Ordenar las insignias por nivel
END;
--**********************************************************************************************
-- Trigger para la tabla OTORGAR_INSIGNIA del perfil para un Docente

CREATE TRIGGER InsertarControlInsigniaPorOtorgamiento
ON OTORGAR_INSIGNIA_P2
AFTER INSERT
AS
BEGIN
    -- Insertar registros en CONTROL_INSIGNIA basados en la lógica especificada
    INSERT INTO CONTROL_INSIGNIA (insignia_bloqueada, USUARIO_rut, fecha_otorgamiento, INSIGNIA_insignia_id)
    SELECT 
        CASE
            WHEN oi.registro_insignia_evento = 0 THEN 1 -- Bloquear insignia si registro_asistencia_evento es 0
            WHEN oi.registro_insignia_evento = 1 AND 
                ((SELECT TOP 1 ci.insignia_bloqueada
                FROM CONTROL_INSIGNIA ci
                JOIN INSIGNIA ins ON ins.insignia_id = ci.INSIGNIA_insignia_id
                JOIN AREA a ON a.area_id = ins.AREA_area_id
                WHERE ci.USUARIO_rut = oi.USUARIO_rut AND ins.AREA_area_id = a.area_id AND ins.TIPO_PERFIL_tipo_perfil_id = 2
                ORDER BY fecha_otorgamiento DESC) = 1) THEN 1
            ELSE 0 -- Default: Bloquear insignia si no se cumplen las condiciones anteriores
        END AS insignia_bloqueada,
        oi.USUARIO_rut, -- Rut del usuario
        oi.fecha_registro_otorgamiento, -- Fecha de otorgamiento de la insignia
        ins.insignia_id -- Obtener el INSIGNIA_insignia_id desde la tabla INSIGNIA
    FROM INSERTED oi
    INNER JOIN INSIGNIA ins ON oi.EVENTO_evento_id = ins.EVENTO_evento_id
    WHERE NOT EXISTS (
        SELECT 1
        FROM CONTROL_INSIGNIA ci
        WHERE ci.USUARIO_rut = oi.USUARIO_rut
          AND ci.INSIGNIA_insignia_id = ins.insignia_id AND ins.TIPO_PERFIL_tipo_perfil_id = 2
    )
    AND ins.TIPO_PERFIL_tipo_perfil_id = 2
    ORDER BY ins.NIVEL_nivel_id; -- Ordenar las insignias por nivel
END;
--**********************************************************************************************
-- Trigger para la tabla OTORGAR_INSIGNIA del perfil para un Colaborador Administrativo de Duoc UC

CREATE TRIGGER InsertarControlInsigniaPorOtorgamientoAdministrativo
ON OTORGAR_INSIGNIA_P3
AFTER INSERT
AS
BEGIN
    -- Insertar registros en CONTROL_INSIGNIA basados en la lógica especificada
    INSERT INTO CONTROL_INSIGNIA (insignia_bloqueada, USUARIO_rut, fecha_otorgamiento, INSIGNIA_insignia_id)
    SELECT 
        CASE
            WHEN oi.registro_insignia_evento = 0 THEN 1 -- Bloquear insignia si registro_asistencia_evento es 0
            WHEN oi.registro_insignia_evento = 1 AND 
                ((SELECT TOP 1 ci.insignia_bloqueada
                FROM CONTROL_INSIGNIA ci
                JOIN INSIGNIA ins ON ins.insignia_id = ci.INSIGNIA_insignia_id
                JOIN AREA a ON a.area_id = ins.AREA_area_id
                WHERE ci.USUARIO_rut = oi.USUARIO_rut AND ins.AREA_area_id = a.area_id AND ins.TIPO_PERFIL_tipo_perfil_id = 3
                ORDER BY fecha_otorgamiento DESC) = 1) THEN 1
            ELSE 0 -- Default: Bloquear insignia si no se cumplen las condiciones anteriores
        END AS insignia_bloqueada,
        oi.USUARIO_rut, -- Rut del usuario
        oi.fecha_registro_otorgamiento, -- Fecha de otorgamiento de la insignia
        ins.insignia_id -- Obtener el INSIGNIA_insignia_id desde la tabla INSIGNIA
    FROM INSERTED oi
    INNER JOIN INSIGNIA ins ON oi.EVENTO_evento_id = ins.EVENTO_evento_id
    WHERE NOT EXISTS (
        SELECT 1
        FROM CONTROL_INSIGNIA ci
        WHERE ci.USUARIO_rut = oi.USUARIO_rut
          AND ci.INSIGNIA_insignia_id = ins.insignia_id AND ins.TIPO_PERFIL_tipo_perfil_id = 3
    )
    AND ins.TIPO_PERFIL_tipo_perfil_id = 3
    ORDER BY ins.NIVEL_nivel_id; -- Ordenar las insignias por nivel
END;

