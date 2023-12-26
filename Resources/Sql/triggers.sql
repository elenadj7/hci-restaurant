DELIMITER $$
CREATE TRIGGER UpdateItemQuantityUp AFTER INSERT ON `Procurement`
FOR EACH ROW
BEGIN
	UPDATE `Item` i
	JOIN `Procurement_has_Item` phi ON i.id = phi.item_id
	SET i.quantity = i.quantity + phi.quantity
	WHERE phi.procurement_id = NEW.id;
END $$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER UpdateItemQuantityDown AFTER INSERT ON `Order`
FOR EACH ROW
BEGIN
	UPDATE `Item` i
	JOIN `Order_has_Item` ohi ON i.id = ohi.item_id
	SET i.quantity = i.quantity - ohi.quantity
	WHERE ohi.order_id = NEW.id;
END $$
DELIMITER ;
