DELIMITER $$
CREATE PROCEDURE GetUser(IN username_ VARCHAR(255), IN password_ VARCHAR(255), OUT status_ INT)
BEGIN
    DECLARE user_count INT;

    SELECT COUNT(*) INTO user_count FROM `User` WHERE username = username_ AND `password` = password_;

    IF user_count > 0 THEN
		SET status_ = 1;
        SELECT * FROM `User` WHERE username = username_;
    ELSE
		SET status_ = 0;
        SELECT 'User not found!' AS Message;
    END IF;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetAllUsers()
BEGIN
	SELECT * FROM `User` WHERE `role`= 0;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetTheme(in username_ VARCHAR(255))
BEGIN
	SELECT `theme` FROM `Settings` WHERE `user_username` = username_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetLanguage(in username_ VARCHAR(255))
BEGIN
	SELECT `language` FROM `Settings` WHERE `user_username` = username_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE UpdateLanguage(IN username_ VARCHAR(255), IN newLanguage_ VARCHAR(255))
BEGIN
	UPDATE `Settings` SET `language` = newLanguage_ WHERE `user_username` = username_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE UpdateTheme(IN username_ VARCHAR(255), IN newTheme_ VARCHAR(255))
BEGIN
	UPDATE `Settings` SET `theme` = newTheme_ WHERE `user_username` = username_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE AddLanguageAndTheme(IN username_ VARCHAR(255))
BEGIN
	INSERT INTO `Settings` (`user_username`, `theme`, `language`) VALUES (username_, "Light", "English");
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE DeleteUser(IN username_ VARCHAR(255))
BEGIN
	DELETE FROM `User` WHERE `username` = username_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE AddUser(IN username_ VARCHAR(255), IN password_ VARCHAR(255), 
IN name_ VARCHAR(255), IN surname_ VARCHAR(255), IN salary_ INT)
BEGIN
	INSERT INTO `User`(`username`, `password`, `name`, `surname`, `salary`, `role`) 
    VALUES(username_, password_, name_, surname_, salary_, 0);
    CALL AddLanguageAndTheme(username_);
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE UpdateUser(IN username_ VARCHAR(255), IN name_ VARCHAR(255), IN surname_ VARCHAR(255), IN salary_ INT)
BEGIN
	UPDATE `User` SET name = name_, surname = surname_, salary = salary_ WHERE username = username_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE FindUsersByFilter(IN filter_ VARCHAR(255))
BEGIN
    SELECT * FROM `User`
    WHERE (`name` LIKE CONCAT('%', filter_, '%')
    OR `surname` LIKE CONCAT('%', filter_, '%'))
    AND `role` = 0;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetAllItems()
BEGIN
    SELECT `Item`.id, `Item`.name, `Item`.price, `Item`.description, `Item`.quantity, `Category`.name AS category_name
    FROM `Item`
    INNER JOIN `Category` ON Item.category_id = Category.id;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE AddItem(IN name_ VARCHAR(255), IN price_ DECIMAL, IN description_ VARCHAR(255), IN quantity_ INT, IN category_id_ INT)
BEGIN
    INSERT INTO `Item`(name, price, description, quantity, category_id) values(name_, price_, description_, quantity_, category_id_);
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE DeleteItem(IN id_ INT)
BEGIN
    DELETE FROM `Item` WHERE id = id_; 
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetAllCategories()
BEGIN
    SELECT * FROM `Category`;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetItemsByCategory(IN category_id INT)
BEGIN
    SELECT `Item`.id, `Item`.name, `Item`.price, `Item`.description, `Item`.quantity, `Category`.name AS category_name
    FROM `Item`
    INNER JOIN `Category` ON Item.category_id = Category.id
    WHERE Item.category_id = category_id;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetAllProcurements(IN user_username_ VARCHAR(255))
BEGIN
    SELECT * FROM `Procurement` WHERE user_username = user_username_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE AddProcurement(IN user_username_ VARCHAR(255), OUT id_ INT)
BEGIN
    INSERT INTO `Procurement` (user_username, ordered)
    VALUES (user_username_, NOW());
    SELECT LAST_INSERT_ID() INTO id_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE AddProcurementHasItem(IN procurement_id_ INT, IN item_id_ INT, IN quantity_ INT, IN purchase_price_ DECIMAL)
BEGIN
    INSERT INTO `Procurement_has_Item`(procurement_id, item_id, quantity, purchase_price)
    VALUES(procurement_id_, item_id_, quantity_, purchase_price_);
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetItemDataByUsernameAndProcurementId(
    IN username_ VARCHAR(50),
    IN procurement_id_ INT
)
BEGIN
    SELECT
		i.id AS itemId,
        p.id AS procurementId,
        i.name,
        i.price,
        i.description,
        c.name AS categoryName,
        i.quantity,
        phi.purchase_price AS purchaseprice
    FROM
        `Item` i
    INNER JOIN
        `Procurement_has_Item` phi ON i.id = phi.item_id
    INNER JOIN
        `Procurement` p ON phi.procurement_id = p.id
    INNER JOIN
        `Category` c ON i.category_id = c.id
    WHERE
        p.user_username = username_
        AND p.id = procurement_id_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE DeleteProcurement(IN id_ INT)
BEGIN
    DELETE FROM `Procurement` WHERE id = id_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE DeleteProcurementHasItem(IN procurement_id_ INT, IN item_id_ INT)
BEGIN
    DELETE FROM `Procurement_has_Item` WHERE item_id = item_id_ AND procurement_id = procurement_id_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE AddTable(IN seats_number_ INT)
BEGIN
    INSERT INTO `Table`(seats_number) VALUES (seats_number_);
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE DeleteTable(IN id_ INT)
BEGIN
    DELETE FROM `Table` WHERE id = id_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetAllTables()
BEGIN
    SELECT * FROM `Table`;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE AddRandomTables()
BEGIN
    DECLARE counter INT DEFAULT 1;
    DECLARE randomSeats INT;

    WHILE counter <= 25 DO
        SET randomSeats = FLOOR(RAND() * (10 - 2 + 1)) + 2;
        CALL AddTable(randomSeats);
        SET counter = counter + 1;
    END WHILE;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetOrdersByUsername(IN user_username_ VARCHAR(255))
BEGIN
    SELECT * FROM `Order` WHERE user_username = user_username_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE DeleteOrder(IN id_ INT)
BEGIN
    DELETE FROM `Order` WHERE id = id_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE AddOrder(IN table_id_ INT, IN user_username_ VARCHAR(255), OUT id_ INT)
BEGIN
    INSERT INTO `Order`(created, table_id, user_username) VALUES(NOW(), table_id_, user_username_);
    SELECT LAST_INSERT_ID() INTO id_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetItemDataByUsernameAndOrderId(
    IN username_ VARCHAR(50),
    IN order_id_ INT
)
BEGIN
    SELECT
		i.id AS itemId,
        o.id AS orderId,
        i.name,
        i.price,
        i.description,
        c.name AS categoryName,
        ohi.quantity
    FROM
        `Item` i
    INNER JOIN
        `Order_has_Item` ohi ON i.id = ohi.item_id
    INNER JOIN
        `Order` o ON ohi.order_id = o.id
    INNER JOIN
        `Category` c ON i.category_id = c.id
    WHERE
        o.user_username = username_
        AND o.id = order_id_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE DeleteOrderHasItem(IN order_id_ INT, IN item_id_ INT)
BEGIN
    DELETE FROM `Order_has_Item` WHERE item_id = item_id_ AND order_id = order_id_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE AddOrderHasItem(IN order_id_ INT, IN item_id_ INT, IN quantity_ INT)
BEGIN
    INSERT INTO `Order_has_Item`(order_id, item_id, quantity)
    VALUES(order_id_, item_id_, quantity_);
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetOrderById(IN id_ INT)
BEGIN
    SELECT * FROM `Order` WHERE id = id_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE GetProcurementById(IN id_ INT)
BEGIN
    SELECT * FROM `Procurement` WHERE id = id_;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE UpdateItem(IN id_ INT, IN price_ DECIMAL, IN quantity_ INT)
BEGIN
    UPDATE `Item` SET price = price_, quantity = quantity_ WHERE id = id_;
END $$
DELIMITER ;