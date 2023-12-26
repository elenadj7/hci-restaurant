DELIMITER $$
CREATE TRIGGER UpdateItemQuantity AFTER INSERT ON Procurement
FOR EACH ROW
BEGIN
	UPDATE Item i
	JOIN Procurement_has_Item phi ON i.id = phi.item_id
	SET i.quantity = i.quantity + phi.quantity
	WHERE phi.procurement_id = NEW.id;
END $$
DELIMITER ;
