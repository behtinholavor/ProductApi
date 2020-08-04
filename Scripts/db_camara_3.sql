DROP TRIGGER IF EXISTS trg_agenda_au;
DROP TRIGGER IF EXISTS trg_agenda_ai;

DELIMITER $$
CREATE DEFINER = CURRENT_USER TRIGGER trg_agenda_au AFTER UPDATE ON agenda 
FOR EACH ROW BEGIN
    IF (new.status <> old.status) THEN
		INSERT INTO agenda_log (`id_agenda`, `status`) VALUES (new.id, new.status);    
    END IF; 
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER = CURRENT_USER TRIGGER trg_agenda_ai AFTER INSERT ON agenda 
FOR EACH ROW BEGIN
    IF (new.status = 'AGENDADO') THEN
		INSERT INTO agenda_log (`id_agenda`, `status`) VALUES (new.id, new.status);    
    END IF;    
END$$
DELIMITER ;

