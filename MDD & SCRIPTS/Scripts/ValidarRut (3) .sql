--**********************************************************************************************--
-- Borrar funci�n si existe
IF OBJECT_ID('dbo.ValidarRut', 'FN') IS NOT NULL
BEGIN
    DROP FUNCTION dbo.ValidarRut;
END;
--***********************************************************************************************--
-- Crear funci�n
CREATE FUNCTION dbo.ValidarRut (@rut VARCHAR(12))
RETURNS BIT
AS
BEGIN
    DECLARE @rutLimpio VARCHAR(12);
    DECLARE @suma INT = 0;
    DECLARE @digito INT;
    DECLARE @i INT = LEN(@rut);
    DECLARE @resultado BIT = 0;
    DECLARE @digitoVerificador VARCHAR(1);

    -- Elimina puntos y guiones del RUT (si los tiene)
    SET @rutLimpio = REPLACE(REPLACE(@rut, '.', ''), '-', '');

    -- Verifica que el RUT tenga al menos 2 caracteres
    IF LEN(@rutLimpio) >= 2
    BEGIN
        -- Obtiene el �ltimo car�cter (d�gito verificador)
        SET @digitoVerificador = RIGHT(@rutLimpio, 1);

        -- Verifica si el �ltimo car�cter es un n�mero
        IF ISNUMERIC(@digitoVerificador) = 1
        BEGIN
            -- Elimina el d�gito verificador para el c�lculo
            SET @rutLimpio = LEFT(@rutLimpio, LEN(@rutLimpio) - 1);

            -- Calcula el d�gito verificador
            WHILE @i > 0
            BEGIN
                SET @suma = @suma + CAST(SUBSTRING(@rutLimpio, @i, 1) AS INT) * (2 + (@suma % 6));
                SET @i = @i - 1;
            END

            SET @digito = 11 - (@suma % 11);

            -- Compara el d�gito verificador calculado con el RUT proporcionado
            IF @digito = 10
                SET @resultado = IIF(@digitoVerificador = 'K' OR @digitoVerificador = 'k', 1, 0);
            ELSE IF @digito = 11
                SET @resultado = IIF(@digitoVerificador = '0', 1, 0);
            ELSE
                SET @resultado = IIF(CAST(@digitoVerificador AS INT) = @digito, 1, 0);
        END
        ELSE
        BEGIN
            -- El �ltimo car�cter es 'K' o 'k'
            SET @resultado = IIF(@digitoVerificador = 'K' OR @digitoVerificador = 'k', 1, 0);
        END
    END

    RETURN @resultado;
END;


--***************************************************************************************--
-- Insertar un registro en la tabla tipo_perfil
-- Necesario para insertar datos en la tabla usuario.
INSERT INTO TIPO_PERFIL (nombre_tipo_perfil, dominio_correo)
VALUES 
('Estudiante y/o Titulado', '@duocuc.cl'),
('Docente', '@profesor.duoc.cl'),
('Colaborador administrativo', '@duoc.cl'),
('Administrador', '@gmail.com');


--***************************************************************************************--
-- RUT a validar antes de la inserci�n
-- El rut real es 20928183-K
DECLARE @rutAValidar VARCHAR(12) = '20928183-k';

-- Variable para almacenar el resultado de la validaci�n
DECLARE @esValido BIT;

-- Llama a la funci�n de validaci�n y almacena el resultado en @esValido
SET @esValido = dbo.ValidarRut(@rutAValidar);

-- Verifica si el RUT es v�lido antes de insertarlo
IF @esValido = 1
BEGIN
    -- El RUT es v�lido, por lo que puedes proceder con la inserci�n
    INSERT INTO usuario (rut, nombre, apellido_paterno, apellido_materno, correo_electronico, contrase�a, tipo_perfil_tipo_perfil_id)
    VALUES (@rutAValidar, 'Tabita', 'Melo', 'Vera', 'ta.melo@duocuc.cl', 'Student2023', 1);

    PRINT 'El RUT se insert� correctamente.';
END
ELSE
BEGIN
    -- El RUT no es v�lido, no se realiza la inserci�n
    PRINT 'El RUT no es v�lido. No se realiz� la inserci�n.';
END;
--****************************************************************************************************
-- Resultado en la tabla
SELECT * FROM USUARIO;