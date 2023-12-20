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
	INSERT INTO `Settings` (`user_username`, `theme`, `language`) VALUES (username_, "Dark", "English");
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
    SELECT `Item`.id, `Item`.name, `Item`.price, `Item`.description, `Item`.is_available, `Category`.name AS category_name
    FROM `Item`
    INNER JOIN `Category` ON Item.category_id = Category.id;
END $$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE AddItem(IN name_ VARCHAR(255), IN price_ DECIMAL, IN description_ VARCHAR(255), IN is_available_ TINYINT, IN category_id_ INT)
BEGIN
    INSERT INTO `Item`(name, price, description, is_available, category_id) values(name_, price_, description_, is_available_, category_id_);
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
    SELECT `Item`.id, `Item`.name, `Item`.price, `Item`.description, `Item`.is_available, `Category`.name AS category_name
    FROM `Item`
    INNER JOIN `Category` ON Item.category_id = Category.id
    WHERE Item.category_id = category_id;
END $$
DELIMITER ;
