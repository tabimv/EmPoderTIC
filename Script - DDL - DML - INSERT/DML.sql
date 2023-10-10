-- Drop constraints with IF EXISTS
ALTER TABLE asistencia DROP CONSTRAINT IF EXISTS asistencia_usuario_fk;
ALTER TABLE certificacion DROP CONSTRAINT IF EXISTS certificacion_nivel_fk;
ALTER TABLE certificado DROP CONSTRAINT IF EXISTS certificado_certificacion_fk;
ALTER TABLE competencia DROP CONSTRAINT IF EXISTS competencia_area_fk;
ALTER TABLE competencia DROP CONSTRAINT IF EXISTS competencia_nivel_fk;
ALTER TABLE evento DROP CONSTRAINT IF EXISTS evento_area_fk;
ALTER TABLE evento DROP CONSTRAINT IF EXISTS evento_asistencia_fk;
ALTER TABLE evento DROP CONSTRAINT IF EXISTS evento_competencia_fk;
ALTER TABLE evento DROP CONSTRAINT IF EXISTS evento_insignia_fk;
ALTER TABLE evento DROP CONSTRAINT IF EXISTS evento_tipo_evento_fk;
ALTER TABLE evento DROP CONSTRAINT IF EXISTS evento_usuario_fk;
ALTER TABLE insignia DROP CONSTRAINT IF EXISTS insignia_certificacion_fk;
ALTER TABLE insignia DROP CONSTRAINT IF EXISTS insignia_dato_logro_fk;
ALTER TABLE insignia DROP CONSTRAINT IF EXISTS insignia_nivel_fk;
ALTER TABLE usuario DROP CONSTRAINT IF EXISTS usuario_tipo_perfil_fk;

-- Drop tables with IF EXISTS
DROP TABLE IF EXISTS usuario;
DROP TABLE IF EXISTS tipo_perfil;
DROP TABLE IF EXISTS tipo_evento;
DROP TABLE IF EXISTS nivel;
DROP TABLE IF EXISTS insignia;
DROP TABLE IF EXISTS evento;
DROP TABLE IF EXISTS dato_logro;
DROP TABLE IF EXISTS competencia;
DROP TABLE IF EXISTS certificado;
DROP TABLE IF EXISTS certificacion;
DROP TABLE IF EXISTS asistencia;
DROP TABLE IF EXISTS area;
DROP TABLE IF EXISTS TipoPerfils;