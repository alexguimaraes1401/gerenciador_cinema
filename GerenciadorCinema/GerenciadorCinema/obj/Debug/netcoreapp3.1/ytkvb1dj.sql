CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `filme` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Titulo` longtext CHARACTER SET utf8mb4 NULL,
    `Descricao` longtext CHARACTER SET utf8mb4 NULL,
    `Duracao` decimal(65,30) NOT NULL,
    CONSTRAINT `PK_filme` PRIMARY KEY (`Id`)
);

CREATE TABLE `Sala` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Descricao` longtext CHARACTER SET utf8mb4 NULL,
    `QuantidadeAssentos` int NOT NULL,
    CONSTRAINT `PK_Sala` PRIMARY KEY (`Id`)
);

CREATE TABLE `Sessao` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Data` datetime(6) NOT NULL,
    `Horario` longtext CHARACTER SET utf8mb4 NULL,
    `ValorIngresso` float NOT NULL,
    `TipoAnimacao` int NOT NULL,
    `TipoAudio` int NOT NULL,
    `FilmeId` int NULL,
    `SalaId` int NULL,
    CONSTRAINT `PK_Sessao` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Sessao_filme_FilmeId` FOREIGN KEY (`FilmeId`) REFERENCES `filme` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Sessao_Sala_SalaId` FOREIGN KEY (`SalaId`) REFERENCES `Sala` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Sessao_FilmeId` ON `Sessao` (`FilmeId`);

CREATE INDEX `IX_Sessao_SalaId` ON `Sessao` (`SalaId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210211145040_initial', '3.1.8');

