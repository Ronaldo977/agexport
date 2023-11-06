--Creaci�n de secuencias
CREATE SEQUENCE factura_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE detalle_seq START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE correlativo_seq START WITH 2000 INCREMENT BY 1;


--Tabla factura
CREATE TABLE factura (
  id_factura NUMBER(10) NOT NULL,
  nit VARCHAR2(13) NOT NULL,
  nombre VARCHAR2(255) NOT NULL,
  fecha DATE NOT NULL,
  correlativo NUMBER(10) NOT NULL,
  PRIMARY KEY (id_factura)
);

--Tabla detalle_factura
CREATE TABLE detalle_factura (
  id_producto NUMBER(10) NOT NULL,
  id_factura NUMBER(10) NOT NULL,
  producto VARCHAR2(255) NOT NULL,
  cantidad NUMBER(10) NOT NULL,
  precio NUMBER(10) NOT NULL,
  total NUMBER(10) NOT NULL,
  PRIMARY KEY (id_producto),
  FOREIGN KEY (id_factura) REFERENCES factura(id_factura)
);


-- Trigger para actualizar automoticamente total al modificar valor o cantidad
CREATE OR REPLACE TRIGGER update_total_trigger
BEFORE UPDATE ON detalle_factura
FOR EACH ROW
BEGIN
  -- Recalcular el total si se actualiza valor o cantidad
  IF :NEW.valor != :OLD.valor OR :NEW.cantidad != :OLD.cantidad THEN
    :NEW.total := :NEW.cantidad * :NEW.valor;
  END IF;
END;
/


-- Procedimiento para crear triggers de asignaci�n de ids
CREATE OR REPLACE PROCEDURE create_triggers AS
BEGIN
  -- Trigger para asignar autom�ticamente id a las facturas
  EXECUTE IMMEDIATE '
    CREATE OR REPLACE TRIGGER factura_trigger
    BEFORE INSERT ON factura
    FOR EACH ROW
    BEGIN
        SELECT factura_seq.NEXTVAL
        INTO :NEW.id_factura
        FROM dual;
    END;';

  -- Trigger para asignar autom�ticamente correlativos iniciando en 2000
  EXECUTE IMMEDIATE '
    CREATE OR REPLACE TRIGGER correlativo_trigger
    BEFORE INSERT ON factura
    FOR EACH ROW
    BEGIN
        SELECT correlativo_seq.NEXTVAL
        INTO :NEW.correlativo
        FROM dual;
    END;';

  -- Trigger para asignar autom�ticamente id a los detalles de factura
  EXECUTE IMMEDIATE '
    CREATE OR REPLACE TRIGGER detalle_trigger
    BEFORE INSERT ON detalle_factura
    FOR EACH ROW
    BEGIN
        SELECT detalle_seq.NEXTVAL
        INTO :NEW.id_producto
        FROM dual;
        :NEW.total := :NEW.cantidad * :NEW.valor;
    END;';
END;
/

-- Llamar al procedimiento para crear triggers
EXEC create_triggers;




-- Trigger para insertar la fecha actual en la tabla de facturas
CREATE TRIGGER trigger_fecha_factura
BEFORE INSERT ON factura
FOR EACH ROW
BEGIN
  :new.fecha := SYSDATE;
END;

