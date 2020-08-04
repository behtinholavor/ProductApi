DROP FUNCTION IF EXISTS `fn_funcaousuario`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `fn_funcaousuario`(id_usuario INT) RETURNS varchar(150) CHARSET utf8 COLLATE utf8_bin
    DETERMINISTIC
BEGIN	
	DECLARE finished INTEGER DEFAULT 0;    
    DECLARE funcao VARCHAR(150) DEFAULT "";    
        
    DECLARE cur_funcao CURSOR FOR 
		SELECT DISTINCT 
			`funcao`.`funcao` 									
		FROM `funcaousuario`
			JOIN `funcao` ON `funcaousuario`.`id_funcao` = `funcao`.`id`
		WHERE 
			`funcaousuario`.`id_usuario` = id_usuario
		ORDER BY `funcaousuario`.`id` ASC;                                    
        
    DECLARE CONTINUE HANDLER 
		FOR NOT FOUND SET finished = 1;    		
        
    OPEN cur_funcao;
    
    getFuncoes: LOOP
        FETCH cur_funcao INTO funcao;
        IF finished = 1 THEN 
            LEAVE getFuncoes;
        END IF;
        SET funcao = CONCAT(funcao," | ",funcao);
    END LOOP getFuncoes;	  	
    CLOSE cur_funcao;  	
    
    RETURN funcao;
END$$
DELIMITER ;