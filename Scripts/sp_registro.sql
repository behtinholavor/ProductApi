DROP PROCEDURE IF EXISTS `sp_registro`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_registro`(
  IN tipo ENUM('MEDIAÇÃO', 'ORIENTAÇÃO'),
  IN assunto VARCHAR(500),
  IN mensagem VARCHAR(1000),
  IN nome VARCHAR(500),
  IN fone VARCHAR(15),
  IN email VARCHAR(500),
  IN horario DATETIME,
  IN observacao VARCHAR(1000),
  OUT protocolo VARCHAR(100))
BEGIN
  DECLARE uu_id VARCHAR(100);
  SET uu_id  = (SELECT UUID());  

  INSERT INTO `agenda` 
	(`status`, `horario`, `observacao`, `uu_id`)
  VALUES 
	('AGENDADO', horario, observacao, uu_id);
    
  INSERT INTO `registro`
	(`tipo`, `assunto`, `mensagem`, `nome`,  `fone`, `email`, `uu_id`) 
  VALUES
	(tipo , assunto, nome, mensagem, fone, email, uu_id);
  
  SET protocolo = uu_id;
  SELECT protocolo;
END$$
DELIMITER ;





