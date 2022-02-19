-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Roles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Roles` (
  `IdRole` BIGINT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`IdRole`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Users` (
  `IdUser` BIGINT NOT NULL AUTO_INCREMENT,
  `Username` VARCHAR(12) NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `Password` VARBINARY(100) NOT NULL,
  `FK_IdRole` BIGINT NOT NULL,
  `Created` DATETIME NOT NULL,
  `Updated` DATETIME NOT NULL,
  PRIMARY KEY (`IdUser`),
  INDEX `fk_users_rols_idx` (`FK_IdRole` ASC) VISIBLE,
  CONSTRAINT `fk_users_rols`
    FOREIGN KEY (`FK_IdRole`)
    REFERENCES `mydb`.`Roles` (`IdRole`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Categories`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Categories` (
  `IdCategory` BIGINT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`IdCategory`),
  UNIQUE INDEX `IdCategory_UNIQUE` (`IdCategory` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Products`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Products` (
  `IdProducts` BIGINT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  `Price` DECIMAL NOT NULL,
  `Stock` INT NOT NULL,
  `Discount` DECIMAL NULL,
  `UnitSold` INT NOT NULL,
  `ImageSrc` VARCHAR(45) NULL,
  `Created` DATETIME NOT NULL,
  `Update` DATETIME NOT NULL,
  `FK_IdCategory` BIGINT NOT NULL,
  PRIMARY KEY (`IdProducts`),
  INDEX `fk_Products_Categories1_idx` (`FK_IdCategory` ASC) VISIBLE,
  CONSTRAINT `fk_Products_Categories1`
    FOREIGN KEY (`FK_IdCategory`)
    REFERENCES `mydb`.`Categories` (`IdCategory`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Carts`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Carts` (
  `IdCart` BIGINT NOT NULL AUTO_INCREMENT,
  `FK_IdUser` BIGINT NOT NULL,
  `Discount` DECIMAL NOT NULL,
  `Subtotal` DECIMAL NOT NULL,
  `Taxes` DECIMAL NOT NULL,
  `Total` DECIMAL NOT NULL,
  `Sold` TINYINT NOT NULL DEFAULT False,
  `Created` DATETIME NOT NULL,
  `Updated` DATETIME NOT NULL,
  PRIMARY KEY (`IdCart`),
  INDEX `fk_Cart_Users1_idx` (`FK_IdUser` ASC) VISIBLE,
  CONSTRAINT `fk_Cart_Users1`
    FOREIGN KEY (`FK_IdUser`)
    REFERENCES `mydb`.`Users` (`IdUser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Cart_Products`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Cart_Products` (
  `FK_IdCart` BIGINT NOT NULL,
  `FK_IdProduct` BIGINT NOT NULL,
  `Quantity` INT NOT NULL,
  `Price` DECIMAL NOT NULL,
  `Discount` DECIMAL NOT NULL,
  `Subtotal` DECIMAL NOT NULL,
  PRIMARY KEY (`FK_IdCart`, `FK_IdProduct`),
  INDEX `fk_Cart_has_Products_Products1_idx` (`FK_IdProduct` ASC) VISIBLE,
  INDEX `fk_Cart_has_Products_Cart1_idx` (`FK_IdCart` ASC) VISIBLE,
  CONSTRAINT `fk_Cart_has_Products_Cart1`
    FOREIGN KEY (`FK_IdCart`)
    REFERENCES `mydb`.`Carts` (`IdCart`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Cart_has_Products_Products1`
    FOREIGN KEY (`FK_IdProduct`)
    REFERENCES `mydb`.`Products` (`IdProducts`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;