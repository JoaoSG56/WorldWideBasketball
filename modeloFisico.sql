-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema WWB
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema WWB
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `WWB` DEFAULT CHARACTER SET utf8 ;
USE `WWB` ;

-- -----------------------------------------------------
-- Table `WWB`.`Utilizador`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `WWB`.`Utilizador` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Username` VARCHAR(30) NOT NULL,
  `Password` VARCHAR(30) NOT NULL,
  `Data_de_nascimento` DATE NOT NULL,
  `Email` VARCHAR(45) NOT NULL,
  `Nome` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Username_UNIQUE` (`Username` ASC) VISIBLE,
  UNIQUE INDEX `Email_UNIQUE` (`Email` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WWB`.`Liga`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `WWB`.`Liga` (
  `Id` INT NOT NULL,
  `Nome` VARCHAR(45) NOT NULL,
  `Localizacao` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WWB`.`Equipa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `WWB`.`Equipa` (
  `Id` INT NOT NULL,
  `Nome` VARCHAR(45) NOT NULL,
  `Localizacao` VARCHAR(45) NOT NULL,
  `Liga_Id` INT NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `fk_Equipa_Liga1_idx` (`Liga_Id` ASC) VISIBLE,
  CONSTRAINT `fk_Equipa_Liga1`
    FOREIGN KEY (`Liga_Id`)
    REFERENCES `WWB`.`Liga` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WWB`.`Estatísticas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `WWB`.`Estatísticas` (
  `Pontos_a_favor` INT NOT NULL,
  `Pontos_contra` INT NOT NULL,
  `Vitorias` INT NOT NULL,
  `Derrotas` INT NOT NULL,
  `Classificacao` INT NOT NULL,
  `Dif_pontos` INT NOT NULL,
  `Equipa_Id` INT NOT NULL,
  PRIMARY KEY (`Equipa_Id`),
  INDEX `fk_Estatísticas_Equipa_idx` (`Equipa_Id` ASC) VISIBLE,
  CONSTRAINT `fk_Estatísticas_Equipa`
    FOREIGN KEY (`Equipa_Id`)
    REFERENCES `WWB`.`Equipa` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WWB`.`Jogo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `WWB`.`Jogo` (
  `Id` INT NOT NULL,
  `Data` DATE NOT NULL,
  `Hora` TIME NOT NULL,
  `Resultado` VARCHAR(45) NOT NULL,
  `Equipa_Casa` INT NOT NULL,
  `Equipa_Visitante` INT NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `fk_Jogo_Equipa1_idx` (`Equipa_Casa` ASC) VISIBLE,
  INDEX `fk_Jogo_Equipa2_idx` (`Equipa_Visitante` ASC) VISIBLE,
  CONSTRAINT `fk_Jogo_Equipa1`
    FOREIGN KEY (`Equipa_Casa`)
    REFERENCES `WWB`.`Equipa` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Jogo_Equipa2`
    FOREIGN KEY (`Equipa_Visitante`)
    REFERENCES `WWB`.`Equipa` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WWB`.`Equipas_favoritas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `WWB`.`Equipas_favoritas` (
  `Utilizador_Id` INT NOT NULL,
  `Equipa_Id` INT NOT NULL,
  PRIMARY KEY (`Utilizador_Id`, `Equipa_Id`),
  INDEX `fk_Equipas_favoritas_Equipa1_idx` (`Equipa_Id` ASC) VISIBLE,
  CONSTRAINT `fk_Favoritos_Utilizador1`
    FOREIGN KEY (`Utilizador_Id`)
    REFERENCES `WWB`.`Utilizador` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Equipas_favoritas_Equipa1`
    FOREIGN KEY (`Equipa_Id`)
    REFERENCES `WWB`.`Equipa` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WWB`.`Ligas_favoritas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `WWB`.`Ligas_favoritas` (
  `Utilizador_Id` INT NOT NULL,
  `Liga_Id` INT NOT NULL,
  PRIMARY KEY (`Utilizador_Id`, `Liga_Id`),
  INDEX `fk_Ligas_favoritas_Liga1_idx` (`Liga_Id` ASC) VISIBLE,
  CONSTRAINT `fk_Ligas_favoritas_Utilizador1`
    FOREIGN KEY (`Utilizador_Id`)
    REFERENCES `WWB`.`Utilizador` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Ligas_favoritas_Liga1`
    FOREIGN KEY (`Liga_Id`)
    REFERENCES `WWB`.`Liga` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
