--**********************************************************************************************--
-- Borrar función si existe
IF OBJECT_ID('dbo.ValidarRut', 'FN') IS NOT NULL
BEGIN
    DROP FUNCTION dbo.ValidarRut;
END;
--***********************************************************************************************--
-- Crear función
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
        -- Obtiene el último carácter (dígito verificador)
        SET @digitoVerificador = RIGHT(@rutLimpio, 1);

        -- Verifica si el último carácter es un número
        IF ISNUMERIC(@digitoVerificador) = 1
        BEGIN
            -- Elimina el dígito verificador para el cálculo
            SET @rutLimpio = LEFT(@rutLimpio, LEN(@rutLimpio) - 1);

            -- Calcula el dígito verificador
            WHILE @i > 0
            BEGIN
                SET @suma = @suma + CAST(SUBSTRING(@rutLimpio, @i, 1) AS INT) * (2 + (@suma % 6));
                SET @i = @i - 1;
            END

            SET @digito = 11 - (@suma % 11);

            -- Compara el dígito verificador calculado con el RUT proporcionado
            IF @digito = 10
                SET @resultado = IIF(@digitoVerificador = 'K' OR @digitoVerificador = 'k', 1, 0);
            ELSE IF @digito = 11
                SET @resultado = IIF(@digitoVerificador = '0', 1, 0);
            ELSE
                SET @resultado = IIF(CAST(@digitoVerificador AS INT) = @digito, 1, 0);
        END
        ELSE
        BEGIN
            -- El último carácter es 'K' o 'k'
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
-- RUT a validar antes de la inserción
-- El rut real es 20928183-K
DECLARE @rutAValidar VARCHAR(12) = '20928183-k';

-- Variable para almacenar el resultado de la validación
DECLARE @esValido BIT;

-- Llama a la función de validación y almacena el resultado en @esValido
SET @esValido = dbo.ValidarRut(@rutAValidar);

-- Verifica si el RUT es válido antes de insertarlo
IF @esValido = 1
BEGIN
    -- El RUT es válido, por lo que puedes proceder con la inserción
    INSERT INTO usuario (rut, nombre, apellido_paterno, apellido_materno, correo_electronico, contraseña, tipo_perfil_tipo_perfil_id)
    VALUES (@rutAValidar, 'Tabita', 'Melo', 'Vera', 'ta.melo@duocuc.cl', 'Student2023', 1);

    PRINT 'El RUT se insertó correctamente.';
END
ELSE
BEGIN
    -- El RUT no es válido, no se realiza la inserción
    PRINT 'El RUT no es válido. No se realizó la inserción.';
END;
--****************************************************************************************************
-- Resultado en la tabla
SELECT * FROM USUARIO;