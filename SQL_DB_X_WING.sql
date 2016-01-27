-- Base de donnees
SET foreign_key_checks=0; -- Permet de créer les tables dans n'importe quel ordre



-- X_Wing

DROP DATABASE IF EXISTS X_Wing;
CREATE DATABASE IF NOT EXISTS X_Wing;
USE X_Wing;




-- Utilisateur




-- u_X_Wing

GRANT USAGE ON *.* TO 'u_X_Wing'@'localhost';
DROP USER 'u_X_Wing'@'localhost';
CREATE USER 'u_X_Wing'@'localhost' 
IDENTIFIED BY '25QrCxMAX8hhxmt6';
GRANT USAGE ON *.* TO 'u_X_Wing'@'localhost' 
IDENTIFIED BY '25QrCxMAX8hhxmt6' 
WITH MAX_QUERIES_PER_HOUR 0 MAX_CONNECTIONS_PER_HOUR 0 MAX_UPDATES_PER_HOUR 0 MAX_USER_CONNECTIONS 0;
GRANT SELECT, INSERT, UPDATE, DELETE ON X_Wing.* TO 'u_X_Wing'@'localhost';




-- Tables


-- Carte Vaisseau/Pilote

DROP TABLE IF EXISTS `carte_vaisseau_pilote`;
CREATE TABLE IF NOT EXISTS `carte_vaisseau_pilote`
(
	`id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`id_faction` INT UNSIGNED NOT NULL,
	`id_caracteristique_vaisseau` INT UNSIGNED NOT NULL,
	`id_figurine` INT UNSIGNED NOT NULL,
	`nom_vaisseau` VARCHAR(30) NOT NULL,
	`nom_pilote` VARCHAR(30) NOT NULL,
	`points` TINYINT UNSIGNED NOT NULL,
	`nombre_amelioration` TINYINT UNSIGNED DEFAULT 0,
	`description` VARCHAR(255) NOT NULL,
	`valeur_pilotage` TINYINT UNSIGNED,
	`is_unique` BOOLEAN DEFAULT FALSE,
	PRIMARY KEY (`id`),
	FOREIGN KEY (`id_faction`) REFERENCES `faction`(`id`),
	FOREIGN KEY (`id_caracteristique_vaisseau`) REFERENCES `caracteristique_vaisseau`(`id`),
	FOREIGN KEY (`id_figurine`) REFERENCES `figurine`(`id`)
)ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- Figurine

DROP TABLE IF EXISTS `figurine`;
CREATE TABLE IF NOT EXISTS `figurine`
(
	`id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`nombre` TINYINT UNSIGNED,
	`nom` VARCHAR(30),
	PRIMARY KEY (`id`)
)ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- Faction

DROP TABLE IF EXISTS `faction`;
CREATE TABLE IF NOT EXISTS `faction`
(
	`id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`nom` VARCHAR(30),
	PRIMARY KEY (`id`),
	UNIQUE KEY (`nom`)
)ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;


-- Amelioration

DROP TABLE IF EXISTS `amelioration`;
CREATE TABLE IF NOT EXISTS `amelioration`
(
	`id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`nom` VARCHAR(30),
	`points` TINYINT UNSIGNED NOT NULL,
	`description` VARCHAR(255),
	`taille` TINYINT UNSIGNED NOT NULL,
	`valeur_attaque` TINYINT UNSIGNED NOT NULL,
	`valeur_energie` TINYINT UNSIGNED NOT NULL,
	`portee_min` TINYINT UNSIGNED NOT NULL,
	`portee_max` TINYINT UNSIGNED NOT NULL,
	`limite` BOOLEAN DEFAULT FALSE,
	`is_unique` BOOLEAN DEFAULT FALSE,
	PRIMARY KEY (`id`),
	UNIQUE KEY (`nom`)
)ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;


-- Caracteristique vaisseau

DROP TABLE IF EXISTS `caracteristique_vaisseau`;
CREATE TABLE IF NOT EXISTS `caracteristique_vaisseau`
(
	`id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`valeur_attaque` TINYINT UNSIGNED NOT NULL,
	`valeur_energie` TINYINT UNSIGNED NOT NULL,
	`valeur_coque` TINYINT UNSIGNED NOT NULL,
	`valeur_bouclier` TINYINT UNSIGNED NOT NULL,
	`valeur_agilite` TINYINT UNSIGNED NOT NULL,
	`taille`  TINYINT UNSIGNED NOT NULL,
	PRIMARY KEY (`id`)
)ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;


-- Collectionneur

DROP TABLE IF EXISTS `collectionneur`;
CREATE TABLE IF NOT EXISTS `collectionneur`
(
	`id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`identifiant` VARCHAR(30) NOT NULL,
	`password` VARCHAR(30) NOT NULL,
	PRIMARY KEY (`id`),
	UNIQUE KEY (`identifiant`)
)ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;


-- Invite

DROP TABLE IF EXISTS `invite`;
CREATE TABLE IF NOT EXISTS `invite`
(
	`id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`id_collectionneur` INT UNSIGNED NOT NULL,
	`nom` VARCHAR(30) NOT NULL,
	PRIMARY KEY (`id`),
	FOREIGN KEY (`id_collectionneur`) REFERENCES `collectionneur`(`id`),
	UNIQUE KEY (`nom`)
)ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;


-- Escadron

DROP TABLE IF EXISTS `escadron`;
CREATE TABLE IF NOT EXISTS `escadron`
(
	`id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`id_collectionneur` INT UNSIGNED NOT NULL,
	`nom` VARCHAR(30),
	`total_points` SMALLINT UNSIGNED NOT NULL,
	`invite` BOOLEAN DEFAULT FALSE,
	PRIMARY KEY (`id`),
	FOREIGN KEY (`id_collectionneur`) REFERENCES `collectionneur`(`id`),
	UNIQUE KEY (`nom`)
)ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;


-- Type amelioration

DROP TABLE IF EXISTS `type_amelioration`;
CREATE TABLE IF NOT EXISTS `type_amelioration`
(
	`id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`nom` VARCHAR(30),
	PRIMARY KEY (`id`),
	UNIQUE KEY (`nom`)
)ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;








-- Tables associatives


-- Carte Vaisseau/Pilote - Type amelioration

DROP TABLE IF EXISTS `carte_vaisseau_pilote_type_amelioration`;
CREATE TABLE IF NOT EXISTS `carte_vaisseau_pilote_type_amelioration`
(
	`id_carte_vaisseau_pilote` INT UNSIGNED NOT NULL,
	`id_type_amelioration` INT UNSIGNED NOT NULL,
	`quantite` TINYINT UNSIGNED NOT NULL,
	FOREIGN KEY (`id_carte_vaisseau_pilote`) REFERENCES `carte_vaisseau_pilote`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	FOREIGN KEY (`id_type_amelioration`) REFERENCES `type_amelioration`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=latin1 ;


-- Escadron - amelioration

DROP TABLE IF EXISTS `escadron_amelioration`;
CREATE TABLE IF NOT EXISTS `escadron_amelioration`
(
	`id_escadron_carte_vaisseau_pilote` INT UNSIGNED NOT NULL,
	`id_amelioration` INT UNSIGNED NOT NULL,
	`quantite` TINYINT UNSIGNED NOT NULL,
	FOREIGN KEY (`id_escadron_carte_vaisseau_pilote`) REFERENCES `escadron_carte_vaisseau_pilote`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	FOREIGN KEY (`id_amelioration`) REFERENCES `amelioration`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=latin1 ;


-- Escadron - Carte Vaisseau/Pilote

DROP TABLE IF EXISTS `escadron_carte_vaisseau_pilote`;
CREATE TABLE IF NOT EXISTS `escadron_carte_vaisseau_pilote`
(
	`id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`id_carte_vaisseau_pilote` INT UNSIGNED NOT NULL,
	`id_escadron` INT UNSIGNED NOT NULL,
	PRIMARY KEY (`id`),
	FOREIGN KEY (`id_carte_vaisseau_pilote`) REFERENCES `carte_vaisseau_pilote`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	FOREIGN KEY (`id_escadron`) REFERENCES `escadron`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;


-- Collectioneur - Carte Vaisseau/Pilote

DROP TABLE IF EXISTS `collectionneur_carte_vaisseau_pilote`;
CREATE TABLE IF NOT EXISTS `collectionneur_carte_vaisseau_pilote`
(
	`id_collectionneur` INT UNSIGNED NOT NULL,
	`id_carte_vaisseau_pilote` INT UNSIGNED NOT NULL,
	`quantite` TINYINT UNSIGNED NOT NULL,
	FOREIGN KEY (`id_collectionneur`) REFERENCES `collectionneur`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	FOREIGN KEY (`id_carte_vaisseau_pilote`) REFERENCES `carte_vaisseau_pilote`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=latin1 ;


-- Collectionneur - Figurine

DROP TABLE IF EXISTS `collectionneur_figurine`;
CREATE TABLE IF NOT EXISTS `collectionneur_figurine`
(
	`id_collectionneur` INT UNSIGNED NOT NULL,
	`id_figurine` INT UNSIGNED NOT NULL,
	`quantite` TINYINT UNSIGNED NOT NULL,
	FOREIGN KEY (`id_collectionneur`) REFERENCES `collectionneur`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	FOREIGN KEY (`id_figurine`) REFERENCES `figurine`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=latin1 ;


-- Collectionneur - Amelioration

DROP TABLE IF EXISTS `collectionneur_amelioration`;
CREATE TABLE IF NOT EXISTS `collectionneur_amelioration`
(
	`id_collectionneur` INT UNSIGNED NOT NULL,
	`id_amelioration` INT UNSIGNED NOT NULL,
	`quantite` TINYINT UNSIGNED NOT NULL,
	FOREIGN KEY (`id_collectionneur`) REFERENCES `collectionneur`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	FOREIGN KEY (`id_amelioration`) REFERENCES `amelioration`(`id`)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)ENGINE=InnoDB DEFAULT CHARSET=latin1 ;