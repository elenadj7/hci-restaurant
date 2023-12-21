DELIMITER $$
CREATE TRIGGER UpdateItemQuantity AFTER UPDATE ON Procurement
FOR EACH ROW
BEGIN
    IF NEW.is_finished = 1 THEN
        UPDATE Item i
        JOIN Procurement_has_Item phi ON i.id = phi.item_id
        SET i.quantity = i.quantity + phi.quantity
        WHERE phi.procurement_id = NEW.id;
    END IF;
END $$
DELIMITER ;
