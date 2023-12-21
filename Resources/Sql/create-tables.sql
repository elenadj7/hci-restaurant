-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema myrestaurant
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema myrestaurant
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `myrestaurant` DEFAULT CHARACTER SET utf8 ;
USE `myrestaurant` ;

-- -----------------------------------------------------
-- Table `myrestaurant`.`User`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myrestaurant`.`User` (
  `username` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `surname` VARCHAR(45) NOT NULL,
  `salary` INT NOT NULL,
  `role` TINYINT NOT NULL,
  PRIMARY KEY (`username`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `myrestaurant`.`Status`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myrestaurant`.`Status` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `myrestaurant`.`Table`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myrestaurant`.`Table` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `seats_number` INT NOT NULL,
  `is_busy` TINYINT NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `myrestaurant`.`Order`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myrestaurant`.`Order` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `created` DATETIME NOT NULL,
  `note` VARCHAR(200) NULL,
  `status_id` INT NOT NULL,
  `table_id` INT NOT NULL,
  `user_username` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Order_Status1_idx` (`status_id` ASC) VISIBLE,
  INDEX `fk_Order_Table1_idx` (`table_id` ASC) VISIBLE,
  INDEX `fk_Order_User1_idx` (`user_username` ASC) VISIBLE,
  CONSTRAINT `fk_Order_Status1`
    FOREIGN KEY (`status_id`)
    REFERENCES `myrestaurant`.`Status` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Order_Table1`
    FOREIGN KEY (`table_id`)
    REFERENCES `myrestaurant`.`Table` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Order_User1`
    FOREIGN KEY (`user_username`)
    REFERENCES `myrestaurant`.`User` (`username`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `myrestaurant`.`Category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myrestaurant`.`Category` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `myrestaurant`.`Item`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myrestaurant`.`Item` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(50) NOT NULL,
  `price` DECIMAL(5,2) NOT NULL,
  `description` VARCHAR(200) NULL,
  `quantity` INT NOT NULL,
  `category_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Item_Category1_idx` (`category_id` ASC) VISIBLE,
  CONSTRAINT `fk_Item_Category1`
    FOREIGN KEY (`category_id`)
    REFERENCES `myrestaurant`.`Category` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `myrestaurant`.`Order_has_Item`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myrestaurant`.`Order_has_Item` (
  `order_id` INT NOT NULL,
  `item_id` INT NOT NULL,
  `quantity` INT NOT NULL,
  PRIMARY KEY (`order_id`, `item_id`),
  INDEX `fk_Order_has_Item_Item1_idx` (`item_id` ASC) VISIBLE,
  INDEX `fk_Order_has_Item_Order1_idx` (`order_id` ASC) VISIBLE,
  CONSTRAINT `fk_Order_has_Item_Order1`
    FOREIGN KEY (`order_id`)
    REFERENCES `myrestaurant`.`Order` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Order_has_Item_Item1`
    FOREIGN KEY (`item_id`)
    REFERENCES `myrestaurant`.`Item` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `myrestaurant`.`Settings`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myrestaurant`.`Settings` (
  `user_username` VARCHAR(45) NOT NULL,
  `theme` VARCHAR(45) NOT NULL,
  `language` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`user_username`),
  CONSTRAINT `fk_Settings_User1`
    FOREIGN KEY (`user_username`)
    REFERENCES `myrestaurant`.`User` (`username`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `myrestaurant`.`Procurement`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myrestaurant`.`Procurement` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_username` VARCHAR(45) NOT NULL,
  `is_finished` TINYINT NOT NULL,
  `ordered` DATE NOT NULL,
  `arrived` DATE,
  PRIMARY KEY (`id`),
  INDEX `fk_Procurement_User1_idx` (`user_username` ASC) VISIBLE,
  CONSTRAINT `fk_Procurement_User1`
    FOREIGN KEY (`user_username`)
    REFERENCES `myrestaurant`.`User` (`username`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `myrestaurant`.`Procurement_has_Item`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myrestaurant`.`Procurement_has_Item` (
  `procurement_id` INT NOT NULL,
  `item_id` INT NOT NULL,
  `quantity` INT NOT NULL,
  `purchase_price` DECIMAL(5,2) NOT NULL,
  PRIMARY KEY (`procurement_id`, `item_id`),
  INDEX `fk_Procurement_has_Item_Item1_idx` (`item_id` ASC) VISIBLE,
  INDEX `fk_Procurement_has_Item_Procurement1_idx` (`procurement_id` ASC) VISIBLE,
  CONSTRAINT `fk_Procurement_has_Item_Procurement1`
    FOREIGN KEY (`procurement_id`)
    REFERENCES `myrestaurant`.`Procurement` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Procurement_has_Item_Item1`
    FOREIGN KEY (`item_id`)
    REFERENCES `myrestaurant`.`Item` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;