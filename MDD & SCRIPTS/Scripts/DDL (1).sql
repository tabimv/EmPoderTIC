-- Generado por Oracle SQL Developer Data Modeler 19.4.0.350.1424
--   en:        2023-10-23 12:10:15 CLST
--   sitio:      SQL Server 2012
--   tipo:      SQL Server 2012



CREATE TABLE AREA 
    (
     area_id INTEGER IDENTITY(1, 1) NOT NULL , 
     area_conocimiento VARCHAR (100) NOT NULL 
    )
GO

ALTER TABLE AREA ADD CONSTRAINT AREA_PK PRIMARY KEY CLUSTERED (area_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE ASISTENCIA 
    (
     registro_asistencia_evento BIT NOT NULL  DEFAULT 0, -- Por defecto False (No asistió) , 
     fecha_registro_asistencia DATE NOT NULL , 
     USUARIO_rut VARCHAR (12) NOT NULL , 
     EVENTO_evento_id INTEGER NOT NULL 
    )
GO

ALTER TABLE ASISTENCIA ADD CONSTRAINT ASISTENCIA_PK PRIMARY KEY CLUSTERED (USUARIO_rut, EVENTO_evento_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE CERTIFICACION 
    (
     certificacion_id INTEGER  IDENTITY(1, 1) NOT NULL , 
     tiene_certificacion BIT NOT NULL , 
     NIVEL_nivel_id INTEGER NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , '1 = FALSO
2 = FALSO
3 = VERDADERO' , 'USER' , 'dbo' , 'TABLE' , 'CERTIFICACION' , 'COLUMN' , 'tiene_certificacion' 
GO

ALTER TABLE CERTIFICACION ADD CONSTRAINT CERTIFICACION_PK PRIMARY KEY CLUSTERED (certificacion_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE CERTIFICADO 
    (
     certificado_id INTEGER IDENTITY(1, 1)  NOT NULL , 
     nombre VARCHAR (100) NOT NULL , 
     descripcion VARCHAR (100) NOT NULL , 
     certificado_url VARCHAR (200) NOT NULL , 
     fecha_otorgamiento DATETIME NOT NULL , 
     CERTIFICACION_certificacion_id INTEGER NOT NULL , 
     INSIGNIA_insignia_id INTEGER NOT NULL , 
     USUARIO_rut VARCHAR (12) NOT NULL , 
     AREA_area_id INTEGER NOT NULL 
    )
GO

ALTER TABLE CERTIFICADO ADD CONSTRAINT CERTIFICADO_PK PRIMARY KEY CLUSTERED (certificado_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE COMPETENCIA 
    (
     competencia_id INTEGER IDENTITY(1, 1)  NOT NULL , 
     competencia_conocimiento VARCHAR (100) NOT NULL , 
     AREA_area_id INTEGER NOT NULL , 
     NIVEL_nivel_id INTEGER NOT NULL 
    )
GO

ALTER TABLE COMPETENCIA ADD CONSTRAINT COMPETENCIA_PK PRIMARY KEY CLUSTERED (competencia_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE CONTROL_INSIGNIA 
    (
     insignia_bloqueada BIT NOT NULL  DEFAULT 1, -- Por defecto True (Bloqueada), 
     USUARIO_rut VARCHAR (12) NOT NULL , 
     fecha_otorgamiento DATE NOT NULL , 
     INSIGNIA_insignia_id INTEGER NOT NULL 
    )
GO

ALTER TABLE CONTROL_INSIGNIA ADD CONSTRAINT CONTROL_INSIGNIA_PK PRIMARY KEY CLUSTERED (USUARIO_rut, INSIGNIA_insignia_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE EVENTO 
    (
     evento_id INTEGER IDENTITY(1, 1)  NOT NULL , 
     nombre VARCHAR (100) NOT NULL , 
     descripcion VARCHAR (1000) NOT NULL , 
     tipo_evento VARCHAR (100) NOT NULL , 
     fecha_evento DATE NOT NULL , 
     hora_evento TIME NOT NULL , 
     lugar_evento VARCHAR (50) NOT NULL , 
     AREA_area_id INTEGER NOT NULL , 
     COMPETENCIA_competencia_id INTEGER NOT NULL 
    )
GO

ALTER TABLE EVENTO ADD CONSTRAINT EVENTO_PK PRIMARY KEY CLUSTERED (evento_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE INSIGNIA 
    (
     insignia_id INTEGER  IDENTITY(1, 1) NOT NULL , 
     nombre VARCHAR (100) NOT NULL , 
     descripción VARCHAR (1000) NOT NULL , 
     objetivo VARCHAR (500) NOT NULL , 
     imagen_url VARCHAR (200) NOT NULL , 
     NIVEL_nivel_id INTEGER NOT NULL , 
     AREA_area_id INTEGER NOT NULL , 
     COMPETENCIA_competencia_id INTEGER NOT NULL , 
     CERTIFICACION_certificacion_id INTEGER , 
     TIPO_PERFIL_tipo_perfil_id INTEGER NOT NULL , 
     EVENTO_evento_id INTEGER NOT NULL 
    )
GO

ALTER TABLE INSIGNIA ADD CONSTRAINT INSIGNIA_PK PRIMARY KEY CLUSTERED (insignia_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE NIVEL 
    (
     nivel_id INTEGER  IDENTITY(1, 1) NOT NULL , 
     categoría_nivel_insignia VARCHAR (100) NOT NULL 
    )
GO

ALTER TABLE NIVEL ADD CONSTRAINT NIVEL_PK PRIMARY KEY CLUSTERED (nivel_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE OTORGAR_INSIGNIA_P2 
    (
     registro_insignia_evento BIT NOT NULL  DEFAULT 0, -- Por defecto False (No Recibe Insignia),  
     contribucion_evento VARCHAR (500) NOT NULL , 
     fecha_registro_otorgamiento DATE NOT NULL , 
     USUARIO_rut VARCHAR (12) NOT NULL , 
     EVENTO_evento_id INTEGER NOT NULL 
    )
GO

ALTER TABLE OTORGAR_INSIGNIA_P2 ADD CONSTRAINT OTORGAR_INSIGNIA_P2_PK PRIMARY KEY CLUSTERED (USUARIO_rut, EVENTO_evento_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE OTORGAR_INSIGNIA_P3 
    (
     registro_insignia_evento BIT NOT NULL  DEFAULT 0, -- Por defecto False (No Recibe Insignia),  
     contribucion_evento VARCHAR (500) NOT NULL , 
     fecha_registro_otorgamiento DATE NOT NULL , 
     USUARIO_rut VARCHAR (12) NOT NULL , 
     EVENTO_evento_id INTEGER NOT NULL 
    )
GO

ALTER TABLE OTORGAR_INSIGNIA_P3 ADD CONSTRAINT OTORGAR_INSIGNIA_P3_PK PRIMARY KEY CLUSTERED (USUARIO_rut, EVENTO_evento_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE TIPO_PERFIL 
    (
     tipo_perfil_id INTEGER  IDENTITY(1, 1) NOT NULL , 
     nombre_tipo_perfil VARCHAR (100) NOT NULL , 
     dominio_correo VARCHAR (100) NOT NULL 
    )
GO

ALTER TABLE TIPO_PERFIL ADD CONSTRAINT TIPO_PERFIL_PK PRIMARY KEY CLUSTERED (tipo_perfil_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE USUARIO 
    (
     rut VARCHAR (12) NOT NULL , 
     nombre VARCHAR (100) NOT NULL , 
     apellido_paterno VARCHAR (100) NOT NULL , 
     apellido_materno VARCHAR (100) NOT NULL , 
     correo_electronico VARCHAR (100) NOT NULL , 
     contraseña VARCHAR (100) NOT NULL , 
     TIPO_PERFIL_tipo_perfil_id INTEGER NOT NULL 
    )
GO

ALTER TABLE USUARIO ADD CONSTRAINT USUARIO_PK PRIMARY KEY CLUSTERED (rut)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

ALTER TABLE ASISTENCIA 
    ADD CONSTRAINT ASISTENCIA_EVENTO_FK FOREIGN KEY 
    ( 
     EVENTO_evento_id
    ) 
    REFERENCES EVENTO 
    ( 
     evento_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE ASISTENCIA 
    ADD CONSTRAINT ASISTENCIA_USUARIO_FK FOREIGN KEY 
    ( 
     USUARIO_rut
    ) 
    REFERENCES USUARIO 
    ( 
     rut 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE CERTIFICACION 
    ADD CONSTRAINT CERTIFICACION_NIVEL_FK FOREIGN KEY 
    ( 
     NIVEL_nivel_id
    ) 
    REFERENCES NIVEL 
    ( 
     nivel_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE CERTIFICADO 
    ADD CONSTRAINT CERTIFICADO_AREA_FK FOREIGN KEY 
    ( 
     AREA_area_id
    ) 
    REFERENCES AREA 
    ( 
     area_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE CERTIFICADO 
    ADD CONSTRAINT CERTIFICADO_CERTIFICACION_FK FOREIGN KEY 
    ( 
     CERTIFICACION_certificacion_id
    ) 
    REFERENCES CERTIFICACION 
    ( 
     certificacion_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE CERTIFICADO 
    ADD CONSTRAINT CERTIFICADO_INSIGNIA_FK FOREIGN KEY 
    ( 
     INSIGNIA_insignia_id
    ) 
    REFERENCES INSIGNIA 
    ( 
     insignia_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE CERTIFICADO 
    ADD CONSTRAINT CERTIFICADO_USUARIO_FK FOREIGN KEY 
    ( 
     USUARIO_rut
    ) 
    REFERENCES USUARIO 
    ( 
     rut 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE COMPETENCIA 
    ADD CONSTRAINT COMPETENCIA_AREA_FK FOREIGN KEY 
    ( 
     AREA_area_id
    ) 
    REFERENCES AREA 
    ( 
     area_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE COMPETENCIA 
    ADD CONSTRAINT COMPETENCIA_NIVEL_FK FOREIGN KEY 
    ( 
     NIVEL_nivel_id
    ) 
    REFERENCES NIVEL 
    ( 
     nivel_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE CONTROL_INSIGNIA 
    ADD CONSTRAINT CONTROL_INSIGNIA_INSIGNIA_FK FOREIGN KEY 
    ( 
     INSIGNIA_insignia_id
    ) 
    REFERENCES INSIGNIA 
    ( 
     insignia_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE CONTROL_INSIGNIA 
    ADD CONSTRAINT CONTROL_INSIGNIA_USUARIO_FK FOREIGN KEY 
    ( 
     USUARIO_rut
    ) 
    REFERENCES USUARIO 
    ( 
     rut 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE EVENTO 
    ADD CONSTRAINT EVENTO_AREA_FK FOREIGN KEY 
    ( 
     AREA_area_id
    ) 
    REFERENCES AREA 
    ( 
     area_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE EVENTO 
    ADD CONSTRAINT EVENTO_COMPETENCIA_FK FOREIGN KEY 
    ( 
     COMPETENCIA_competencia_id
    ) 
    REFERENCES COMPETENCIA 
    ( 
     competencia_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE INSIGNIA 
    ADD CONSTRAINT INSIGNIA_AREA_FK FOREIGN KEY 
    ( 
     AREA_area_id
    ) 
    REFERENCES AREA 
    ( 
     area_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE INSIGNIA 
    ADD CONSTRAINT INSIGNIA_CERTIFICACION_FK FOREIGN KEY 
    ( 
     CERTIFICACION_certificacion_id
    ) 
    REFERENCES CERTIFICACION 
    ( 
     certificacion_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE INSIGNIA 
    ADD CONSTRAINT INSIGNIA_COMPETENCIA_FK FOREIGN KEY 
    ( 
     COMPETENCIA_competencia_id
    ) 
    REFERENCES COMPETENCIA 
    ( 
     competencia_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE INSIGNIA 
    ADD CONSTRAINT INSIGNIA_EVENTO_FK FOREIGN KEY 
    ( 
     EVENTO_evento_id
    ) 
    REFERENCES EVENTO 
    ( 
     evento_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE INSIGNIA 
    ADD CONSTRAINT INSIGNIA_NIVEL_FK FOREIGN KEY 
    ( 
     NIVEL_nivel_id
    ) 
    REFERENCES NIVEL 
    ( 
     nivel_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE INSIGNIA 
    ADD CONSTRAINT INSIGNIA_TIPO_PERFIL_FK FOREIGN KEY 
    ( 
     TIPO_PERFIL_tipo_perfil_id
    ) 
    REFERENCES TIPO_PERFIL 
    ( 
     tipo_perfil_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE OTORGAR_INSIGNIA_P2 
    ADD CONSTRAINT OTORGAR_INSIGNIA_P2_EVENTO_FK FOREIGN KEY 
    ( 
     EVENTO_evento_id
    ) 
    REFERENCES EVENTO 
    ( 
     evento_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE OTORGAR_INSIGNIA_P2 
    ADD CONSTRAINT OTORGAR_INSIGNIA_P2_USUARIO_FK FOREIGN KEY 
    ( 
     USUARIO_rut
    ) 
    REFERENCES USUARIO 
    ( 
     rut 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE OTORGAR_INSIGNIA_P3 
    ADD CONSTRAINT OTORGAR_INSIGNIA_P3_EVENTO_FK FOREIGN KEY 
    ( 
     EVENTO_evento_id
    ) 
    REFERENCES EVENTO 
    ( 
     evento_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE OTORGAR_INSIGNIA_P3 
    ADD CONSTRAINT OTORGAR_INSIGNIA_P3_USUARIO_FK FOREIGN KEY 
    ( 
     USUARIO_rut
    ) 
    REFERENCES USUARIO 
    ( 
     rut 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIO 
    ADD CONSTRAINT USUARIO_TIPO_PERFIL_FK FOREIGN KEY 
    ( 
     TIPO_PERFIL_tipo_perfil_id
    ) 
    REFERENCES TIPO_PERFIL 
    ( 
     tipo_perfil_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO



-- Informe de Resumen de Oracle SQL Developer Data Modeler: 
-- 
-- CREATE TABLE                            13
-- CREATE INDEX                             0
-- ALTER TABLE                             37
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE DATABASE                          0
-- CREATE DEFAULT                           0
-- CREATE INDEX ON VIEW                     0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE ROLE                              0
-- CREATE RULE                              0
-- CREATE SCHEMA                            0
-- CREATE SEQUENCE                          0
-- CREATE PARTITION FUNCTION                0
-- CREATE PARTITION SCHEME                  0
-- 
-- DROP DATABASE                            0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0