
DROP TABLE IF EXISTS product;

CREATE TABLE product (
  id BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  description VARCHAR(5000),    
  price DOUBLE PRECISION,
  quantity DOUBLE PRECISION,
  unit ENUM('UN', 'KG', 'M', 'PC', 'CX', 'L') NOT NULL,
  category ENUM('MATÉRIA-PRIMA', 'INSUMO') NOT NULL,
  inserted DATETIME,
  modified DATETIME,  
  CONSTRAINT pk_product PRIMARY KEY (id)  
);