CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `wv_usuario` AS
    SELECT 
        `usuario`.`id` AS `id`,
        `usuario`.`nome` AS `nome`,
        `usuario`.`fone` AS `fone`,
        `usuario`.`email` AS `email`,
        `usuario`.`usuario` AS `usuario`,
        `usuario`.`senha` AS `senha`,
        `usuario`.`validade` AS `validade`,
        `usuario`.`id_funcao` AS `id_funcao`,
        `funcao`.`funcao` AS `funcao`
    FROM
        (`usuario`
        JOIN `funcao` ON ((`usuario`.`id_funcao` = `funcao`.`id`)))