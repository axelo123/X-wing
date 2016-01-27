
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

-- --------------------------------------------------------

--
-- Base de données: `oenologie`
--

DROP DATABASE IF EXISTS `oenologie`;
CREATE DATABASE IF NOT EXISTS `oenologie` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `oenologie`;

-- --------------------------------------------------------

--
-- Utilisateur: `u_oenologue`
--

GRANT USAGE ON *.* TO 'u_oenologue'@'%';
DROP USER 'u_oenologue'@'%';
CREATE USER 'u_oenologue'@'%' IDENTIFIED BY 'mZTbURtCucb92Grf';
GRANT USAGE ON *.* TO 'u_oenologue'@'%' IDENTIFIED BY 'mZTbURtCucb92Grf' WITH MAX_QUERIES_PER_HOUR 0 MAX_CONNECTIONS_PER_HOUR 0 MAX_UPDATES_PER_HOUR 0 MAX_USER_CONNECTIONS 0;
GRANT SELECT, INSERT, UPDATE, DELETE ON oenologie.* TO 'u_oenologue'@'%';

-- --------------------------------------------------------

--
-- Structure de la table `avis`
--

DROP TABLE IF EXISTS `avis`;
CREATE TABLE IF NOT EXISTS `avis` (
`id` int(11) NOT NULL,
  `ref_oenologue` int(11) NOT NULL,
  `ref_vin` int(11) NOT NULL,
  `cote` smallint(6) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=337 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `avis`
--

INSERT INTO `avis` (`id`, `ref_oenologue`, `ref_vin`, `cote`) VALUES
(1, 2, 57, 1),
(2, 5, 53, 1),
(3, 10, 38, 1),
(4, 2, 19, 1),
(5, 2, 28, 1),
(6, 10, 13, 1),
(7, 2, 53, 2),
(8, 5, 38, 1),
(9, 2, 59, 1),
(10, 10, 45, 2),
(11, 10, 43, 2),
(12, 2, 35, 1),
(13, 5, 64, 1),
(14, 1, 36, 2),
(15, 5, 26, 1),
(16, 5, 34, 1),
(17, 10, 59, 2),
(18, 10, 37, 2),
(19, 2, 8, 2),
(20, 10, 55, 2),
(21, 10, 27, 2),
(22, 10, 26, 1),
(23, 2, 69, 2),
(24, 10, 58, 3),
(25, 10, 2, 2),
(26, 5, 55, 2),
(27, 2, 67, 3),
(28, 10, 54, 3),
(29, 2, 27, 3),
(30, 2, 7, 3),
(31, 10, 7, 3),
(32, 1, 24, 2),
(33, 10, 69, 2),
(34, 10, 60, 4),
(35, 1, 9, 2),
(36, 10, 49, 3),
(37, 2, 1, 4),
(38, 10, 50, 3),
(39, 8, 67, 4),
(40, 5, 5, 3),
(41, 2, 5, 5),
(42, 5, 28, 3),
(43, 5, 19, 3),
(44, 2, 46, 3),
(45, 5, 72, 3),
(46, 2, 45, 5),
(47, 1, 21, 2),
(48, 10, 30, 3),
(49, 7, 17, 4),
(50, 5, 1, 2),
(51, 3, 54, 3),
(52, 5, 54, 2),
(53, 10, 14, 4),
(54, 5, 47, 3),
(55, 1, 64, 2),
(56, 10, 70, 2),
(57, 10, 1, 4),
(58, 2, 60, 5),
(59, 10, 71, 5),
(60, 5, 32, 3),
(61, 2, 30, 2),
(62, 7, 54, 4),
(63, 3, 40, 3),
(64, 1, 56, 2),
(65, 10, 20, 3),
(66, 10, 53, 5),
(67, 5, 65, 2),
(68, 1, 41, 2),
(69, 7, 27, 6),
(70, 2, 16, 5),
(71, 5, 59, 4),
(72, 2, 20, 3),
(73, 1, 27, 4),
(74, 2, 14, 4),
(75, 10, 25, 4),
(76, 1, 2, 4),
(77, 7, 71, 4),
(78, 5, 61, 4),
(79, 10, 5, 3),
(80, 5, 18, 3),
(81, 7, 50, 4),
(82, 6, 70, 4),
(83, 10, 28, 3),
(84, 2, 22, 4),
(85, 10, 15, 3),
(86, 2, 13, 4),
(87, 7, 56, 4),
(88, 6, 5, 6),
(89, 10, 12, 3),
(90, 2, 48, 3),
(91, 9, 68, 4),
(92, 2, 41, 4),
(93, 10, 17, 3),
(94, 2, 72, 4),
(95, 1, 5, 8),
(96, 5, 6, 3),
(97, 9, 67, 8),
(98, 2, 63, 4),
(99, 1, 34, 6),
(100, 5, 40, 3),
(101, 6, 58, 8),
(102, 2, 50, 3),
(103, 8, 41, 4),
(104, 9, 4, 4),
(105, 5, 2, 4),
(106, 8, 60, 4),
(107, 2, 65, 5),
(108, 10, 48, 4),
(109, 2, 4, 4),
(110, 3, 37, 6),
(111, 2, 2, 3),
(112, 1, 30, 6),
(113, 6, 60, 4),
(114, 6, 57, 4),
(115, 2, 47, 3),
(116, 2, 58, 5),
(117, 7, 36, 4),
(118, 5, 67, 4),
(119, 10, 22, 4),
(120, 10, 68, 4),
(121, 2, 40, 4),
(122, 7, 22, 6),
(123, 9, 69, 8),
(124, 5, 16, 4),
(125, 7, 51, 8),
(126, 10, 33, 3),
(127, 5, 11, 3),
(128, 2, 17, 4),
(129, 9, 55, 8),
(130, 3, 60, 9),
(131, 7, 4, 4),
(132, 5, 7, 4),
(133, 5, 22, 5),
(134, 5, 15, 4),
(135, 6, 32, 8),
(136, 7, 44, 6),
(137, 2, 64, 3),
(138, 5, 20, 5),
(139, 5, 37, 4),
(140, 10, 62, 3),
(141, 2, 23, 3),
(142, 1, 42, 4),
(143, 8, 34, 8),
(144, 2, 11, 5),
(145, 2, 10, 5),
(146, 10, 66, 5),
(147, 10, 42, 5),
(148, 9, 39, 8),
(149, 3, 48, 6),
(150, 2, 38, 4),
(151, 10, 21, 4),
(152, 3, 24, 6),
(153, 7, 38, 8),
(154, 1, 28, 8),
(155, 7, 48, 6),
(156, 8, 35, 8),
(157, 6, 7, 8),
(158, 7, 68, 6),
(159, 1, 66, 8),
(160, 3, 61, 9),
(161, 5, 63, 4),
(162, 6, 45, 8),
(163, 1, 67, 10),
(164, 7, 2, 8),
(165, 7, 24, 6),
(166, 6, 19, 8),
(167, 8, 56, 4),
(168, 6, 21, 4),
(169, 1, 33, 4),
(170, 5, 9, 4),
(171, 1, 11, 8),
(172, 7, 40, 10),
(173, 1, 47, 8),
(174, 1, 55, 6),
(175, 5, 35, 4),
(176, 2, 42, 5),
(177, 7, 69, 6),
(178, 7, 23, 6),
(179, 10, 23, 4),
(180, 7, 39, 6),
(181, 1, 17, 10),
(182, 2, 31, 5),
(183, 5, 13, 4),
(184, 8, 54, 16),
(185, 9, 5, 12),
(186, 6, 37, 6),
(187, 1, 58, 10),
(188, 3, 26, 9),
(189, 6, 1, 10),
(190, 10, 72, 5),
(191, 8, 47, 8),
(192, 5, 31, 5),
(193, 9, 6, 8),
(194, 8, 49, 12),
(195, 9, 44, 8),
(196, 6, 6, 8),
(197, 1, 3, 8),
(198, 8, 63, 8),
(199, 3, 39, 12),
(200, 8, 66, 8),
(201, 1, 14, 6),
(202, 7, 20, 8),
(203, 5, 42, 5),
(204, 6, 9, 6),
(205, 6, 62, 10),
(206, 8, 1, 12),
(207, 1, 51, 6),
(208, 6, 69, 8),
(209, 1, 20, 6),
(210, 8, 44, 8),
(211, 3, 58, 6),
(212, 1, 12, 8),
(213, 1, 23, 10),
(214, 6, 10, 10),
(215, 7, 72, 6),
(216, 7, 53, 10),
(217, 9, 21, 8),
(218, 6, 44, 8),
(219, 7, 57, 8),
(220, 9, 37, 12),
(221, 9, 34, 12),
(222, 9, 49, 8),
(223, 8, 58, 12),
(224, 7, 59, 8),
(225, 7, 30, 8),
(226, 1, 39, 10),
(227, 8, 45, 20),
(228, 7, 3, 10),
(229, 1, 57, 10),
(230, 4, 50, 20),
(231, 3, 51, 9),
(232, 3, 5, 9),
(233, 8, 19, 12),
(234, 1, 71, 8),
(235, 9, 32, 16),
(236, 9, 8, 8),
(237, 8, 29, 12),
(238, 9, 33, 12),
(239, 8, 46, 8),
(240, 1, 46, 10),
(241, 9, 38, 16),
(242, 3, 66, 6),
(243, 8, 51, 8),
(244, 3, 62, 6),
(245, 6, 4, 10),
(246, 6, 35, 8),
(247, 7, 49, 8),
(248, 9, 16, 12),
(249, 3, 67, 15),
(250, 6, 16, 10),
(251, 9, 13, 8),
(252, 9, 48, 16),
(253, 3, 3, 9),
(254, 9, 3, 12),
(255, 3, 9, 15),
(256, 8, 14, 16),
(257, 1, 7, 10),
(258, 9, 63, 16),
(259, 8, 69, 12),
(260, 3, 1, 15),
(261, 8, 10, 16),
(262, 8, 50, 16),
(263, 3, 15, 9),
(264, 7, 65, 10),
(265, 3, 32, 9),
(266, 8, 21, 12),
(267, 9, 53, 16),
(268, 3, 69, 12),
(269, 6, 33, 8),
(270, 6, 66, 8),
(271, 3, 49, 9),
(272, 8, 15, 16),
(273, 8, 40, 16),
(274, 3, 53, 15),
(275, 9, 43, 16),
(276, 3, 4, 12),
(277, 8, 18, 12),
(278, 1, 18, 10),
(279, 6, 68, 8),
(280, 8, 68, 16),
(281, 4, 21, 20),
(282, 8, 8, 12),
(283, 9, 42, 12),
(284, 4, 49, 20),
(285, 8, 42, 12),
(286, 6, 13, 10),
(287, 3, 55, 12),
(288, 4, 15, 40),
(289, 3, 68, 9),
(290, 3, 13, 12),
(291, 4, 58, 40),
(292, 3, 56, 12),
(293, 8, 61, 20),
(294, 9, 72, 16),
(295, 4, 38, 20),
(296, 3, 18, 15),
(297, 9, 29, 16),
(298, 8, 32, 16),
(299, 9, 28, 20),
(300, 9, 22, 20),
(301, 4, 33, 20),
(302, 8, 25, 16),
(303, 8, 22, 20),
(304, 4, 27, 60),
(305, 9, 62, 16),
(306, 3, 33, 15),
(307, 4, 5, 80),
(308, 4, 43, 60),
(309, 4, 39, 80),
(310, 4, 34, 80),
(311, 4, 44, 60),
(312, 4, 30, 80),
(313, 4, 31, 80),
(314, 4, 54, 60),
(315, 4, 8, 80),
(316, 4, 7, 100),
(317, 4, 40, 60),
(318, 4, 28, 60),
(319, 4, 47, 40),
(320, 4, 42, 40),
(321, 4, 4, 60),
(322, 4, 37, 60),
(323, 4, 52, 100),
(324, 4, 53, 80),
(325, 4, 12, 100),
(326, 4, 13, 60),
(327, 4, 6, 100),
(328, 4, 29, 60),
(329, 4, 46, 80),
(330, 4, 20, 60),
(331, 4, 69, 100),
(332, 4, 56, 60),
(333, 4, 17, 100),
(334, 4, 32, 80),
(335, 4, 25, 80),
(336, 4, 2, 100);

-- --------------------------------------------------------

--
-- Structure de la table `categorie`
--

DROP TABLE IF EXISTS `categorie`;
CREATE TABLE IF NOT EXISTS `categorie` (
`id` int(11) NOT NULL,
  `nom` varchar(80) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `categorie`
--

INSERT INTO `categorie` (`id`, `nom`) VALUES
(2, 'Les Beaujolais'),
(1, 'Vins d''Alsace');

-- --------------------------------------------------------

--
-- Structure de la table `oenologue`
--

DROP TABLE IF EXISTS `oenologue`;
CREATE TABLE IF NOT EXISTS `oenologue` (
`id` int(11) NOT NULL,
  `nom` varchar(120) NOT NULL,
  `indice_confiance` double NOT NULL,
  `cotation_minimale` smallint(6) NOT NULL,
  `cotation_maximale` smallint(6) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `oenologue`
--

INSERT INTO `oenologue` (`id`, `nom`, `indice_confiance`, `cotation_minimale`, `cotation_maximale`) VALUES
(1, 'Hartmann Marc', 0.98, 0, 10),
(2, 'König Barbara', 1, 0, 5),
(3, 'de Lafosse Jean-Charles', 0.76, 0, 15),
(4, 'Pipperzeel Jan', 0.78, 0, 100),
(5, 'Rivières Anne-Marie', 0.89, 0, 5),
(6, 'Browning Penelop', 0.31, 0, 10),
(7, 'Duchemin Robert', 0.88, 0, 10),
(8, 'Louteker Georges', 0.74, 0, 20),
(9, 'Groulez Michel', 0.29, 0, 20),
(10, 'Müggler Kurt', 0.69, 0, 5);

-- --------------------------------------------------------

--
-- Structure de la table `souscategorie`
--

DROP TABLE IF EXISTS `souscategorie`;
CREATE TABLE IF NOT EXISTS `souscategorie` (
`id` int(11) NOT NULL,
  `ref_categorie` int(11) NOT NULL,
  `nom` varchar(80) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `souscategorie`
--

INSERT INTO `souscategorie` (`id`, `ref_categorie`, `nom`) VALUES
(1, 1, 'Vins d''Alsace'),
(2, 2, 'Beaujolais'),
(3, 2, 'Morgon'),
(4, 2, 'Chiroubles'),
(5, 2, 'Chénas'),
(6, 2, 'Julienas'),
(7, 1, 'Crémants d''Alsace'),
(8, 1, 'Sélections Dopff au moulin'),
(9, 2, 'Côtes de Brouilly'),
(10, 2, 'Brouilly'),
(11, 2, 'Moulin à vent'),
(12, 1, 'Alsace grand cru'),
(13, 2, 'Fleurie'),
(14, 1, 'Vendanges tardives'),
(15, 2, 'Régnié'),
(16, 1, 'Sélections de grain noble'),
(17, 2, 'Saint-Amour');

-- --------------------------------------------------------

--
-- Structure de la table `vin`
--

DROP TABLE IF EXISTS `vin`;
CREATE TABLE IF NOT EXISTS `vin` (
`id` int(11) NOT NULL,
  `ref_souscategorie` int(11) NOT NULL,
  `nom` varchar(160) NOT NULL,
  `description` text NOT NULL,
  `accords_culinaires` text NOT NULL,
  `garde` text NOT NULL,
  `nombre_bouteilles` smallint(6) NOT NULL,
  `contenance_bouteille` smallint(6) NOT NULL,
  `prix_indicatif_bouteille` decimal(8,2) NOT NULL,
  `prix_indicatif_caisse` decimal(8,2) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=73 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `vin`
--

INSERT INTO `vin` (`id`, `ref_souscategorie`, `nom`, `description`, `accords_culinaires`, `garde`, `nombre_bouteilles`, `contenance_bouteille`, `prix_indicatif_bouteille`, `prix_indicatif_caisse`) VALUES
(1, 1, 'Riquewihr-Edel', 'Assemblage de plusieurs cépages. | Fruité et floral, le nez est marqué par des notes de fleurs blanches. L''attaque est franche, le palais puissant, c''est un vin racé et d''une bonne harmonie.', 'comme le Sylvaner mais en plus élégant car plus souple se marie avec tous mets exigeant un vin blanc sec', '', 6, 75, '6.40', '38.40'),
(2, 2, 'Château de Montmelas 2003', 'Vin frais et gouleyant. | Arômes de petit fruits rouges.', 'un osso-bucco, ou des cailles aux raisins, ou un carré de porc au curry et aux pommes', '', 6, 75, '6.50', '39.00'),
(3, 3, 'Les Griottes 2004', 'Vin à la fois fruité et puissant. | Arômes de griotte, abricot et chocolat amer.', 'un pâté de campagne, ou un ragoût de mouton, ou un canard sauvage aux petits oignons, ou un faisan à la Normande, ou un gigot', '', 6, 75, '7.80', '46.80'),
(4, 4, 'Les Muriers 2003', 'Vin souple et fruité. | Arômes de fraise des bois, mûre et cassis.', 'une andouillette grillée de préférence, une viande blanche, ou une volaille, ou un gigot, ou un civet de lapin, ou un gibier à plume, ou un chapon à la mousse de foie gras, ou des charcuteries fines', '', 6, 75, '7.30', '43.80'),
(5, 5, 'Les Maras 2004', 'Vin corsé et charnu. | Arômes de violette et fraise des bois.', 'un pigeon aux petits pois, ou des cailles rôties, ou un gigot, ou un chevreau, ou pour accompagner un fromage fort, ou une daube beaujolaise', '', 6, 75, '7.30', '43.80'),
(6, 6, 'Les Burlats 2002', 'Le Juliénas, qui d''après l''étymologie tiendrait son nom de Jules César, est situé à la porte nord du vignoble Beaujolais. Il s''étend sur 580 hectares de terroirs complémentaires granitiques à l''ouest et sédimentaires à l''Est. | Vin charpenté. Arômes de cerise, chocolat chaud et poire.', 'un coq au vin, ou toute autre volaille en sauce', '', 6, 75, '7.80', '46.80'),
(7, 7, 'Blanc de Noir Brut 2002', 'Vin blanc, Alsace, Crémants d''Alsace', 'au moment de l''apéritif, ou tout au long d''un repas', '', 6, 75, '11.40', '68.40'),
(8, 8, 'Riesling de Riquewihr 2004', 'Sigillé Confrérie St-Etienne | Médaille d''Or au concours Riesling du monde | Sec, racé, délicatement fruité, il offre un bouquet d''une grande finesse avec des nuances parfois minérales ou florales.', 'crustacés, une choucroute garnie, des escargots, un coq au Riesling, un lapin, un Saint Pierre à l''oseille, un brochet à la crème, une matelote au Riesling, un sandre au Riesling, une truite au bleu, une anguille au vert, un kougelhopf de saumon et broche', '', 6, 75, '12.10', '72.60'),
(9, 8, 'Dopff Château Du Moulin 2002', 'Vin blanc, Alsace, Sélections Dopff Au Moulin', '', '', 6, 75, '8.30', '49.80'),
(10, 9, 'La Montagne Bleue 2003', 'Vin racé. | Arômes de raisins frais avec des notes minérales.', 'un civet de lapin, ou un gibier à plume, ou un chapon à la mousse de foie gras, ou des charcuteries fines', '', 6, 75, '14.80', '88.80'),
(11, 3, 'Réserve Côte de PY 2003', 'Cuvée Réserve Côte de PY', 'un pâté de campagne, ou un ragoût de mouton, ou un canard sauvage aux petits oignons, ou un faisan à la Normande, ou un gigot', '', 6, 75, '14.80', '88.80'),
(12, 10, 'Château de Pierreux 2003', 'Cuvée Réserve du Château de Pierreux', 'viande blanche voire même une viande rouge, ou une volaille', '', 6, 75, '16.10', '96.60'),
(13, 1, 'Riesling Réserve Cuvée Europe 2004', 'Sec, racé, délicatement fruité, il offre un bouquet d ?une grande finesse avec des nuances parfois minérales ou florales.', 'avec de la salade et des fruits de mer', '', 6, 75, '8.40', '50.40'),
(14, 1, 'Spécial Fruit de Mer 2004', 'Assemblage harmonieux de différents cépages tels que SYLVANER et PINOT BLANC pour le corps et la vivacité et une touche de RIESLING et de MUSCAT pour la finesse.', 'avec crustacés et fruits de mer', '', 6, 75, '6.80', '40.80'),
(15, 7, 'Chardonnay Brut 2002', 'Jolie robe jaune clair, ce crémant, issu du seul chardonnay, présente des arômes délicats et affirmés. Les notes grillées se mêlent à des nuances florales et fruitées. Le palais devient amples, frais et très équilibré. Un vin racé et superbe.', 'au moment de l''apéritif, ou tout au long d''un repas', '', 6, 75, '11.40', '68.40'),
(16, 4, 'Les Muriers 2004', 'Vin souple et fruité. | Arômes de fraise des bois, mûre et cassis.', 'une andouillette grillée de préférence, une viande blanche, ou une volaille, ou un gigot, ou un civet de lapin, ou un gibier à plume, ou un chapon à la mousse de foie gras, ou des charcuteries fines', '', 6, 75, '7.30', '43.80'),
(17, 11, 'Dom. Champ de Cour 2003', 'Propriété de la Famille Mommessin depuis 1962, la réserve du domaine de Champ de Cour est élaboré avec la collaboration de Sylvain Pitiot sur une séléction des meilleures parcelles.', 'un pigeon aux petits pois, ou des cailles rôties, ou un gigot, ou un chevreau, ou pour accompagner un fromage fort, ou une daube beaujolaise', '', 6, 75, '13.00', '78.00'),
(18, 8, 'Pinot Noir Coeur de Barrique 2003', 'Structure charpentée et plus complexe dû à la vinification en barrique de chêne.', 'foies de canard au chou, des côtelettes de chevreuil aux airelles, une aiguillette de boeuf braisée au Pinot noir, un civet de lièvre, ou un canard au Pinot noir', 'facilement 5 ans', 6, 75, '14.80', '88.80'),
(19, 2, 'Château de Montmelas 2004', 'Vin frais et gouleyant. | Arômes de petit fruits rouges.', 'un osso-bucco, ou des cailles aux raisins, ou un carré de porc au curry et aux pommes', '', 6, 75, '6.50', '39.00'),
(20, 6, 'Domaine de la Conseillère 2004', 'Bouche bien assise sur des tanins présents, un très beau fruit et une remarquable fraîcheur. Vin harmonieux, structuré et long. | Accord : filet de porc', '', '', 6, 75, '8.90', '53.40'),
(21, 12, 'Gewurztraminer Brand de Turckhei 2003', 'D''une grande finesse, subtils, d''un fruité très particulier, les vins du Grand Cru Brand présentent un équilibre royal.', 'Vin de réception par excellence, à la fois à l''apéritif et au dessert', 'atteindra son meilleur niveau dans 4 à 5 ans', 6, 75, '16.10', '96.60'),
(22, 1, 'Gewurztraminer Réserve 2004', '''Le terme ''Réserve'' couvre deux sélections opérées : | - l''une au pressoir, par le choix de raisins en provenance d''excellentes expositions ayant une teneur en sucre optimale. | - l''autre, en cave, après fermentation et à la suite d''une dégustation à l''aveugle des plus belles cuvées.', 'harengs marinés à la créme', '', 6, 75, '10.10', '60.60'),
(23, 12, 'Riesling Schoenenbourg de Riquewihr 2002', '5 Etoiles au Guide DECANTER | Médaille d''Or au concours Riesling du monde | Sec, racé, délicatement fruité, il offre un bouquet d''une grande finesse, note de guimauve et d''épices douces. Excellent vin de garde, le terroir est particulièrement adapté au Riesling. Sur le Schoenenberg le Riesling est Roi. Ce lieu-dit est établi sur des terrains riches en éléments fertilisants et qui retiennent bien l''eau.', 'avec tous les fruits de mer, les poissons fins, qu''ils soient de mer ou de rivière', 'apogée vers 2005', 6, 75, '15.20', '91.20'),
(24, 1, 'Pinot Noir Rouge des 2 Cerfs 2004', 'Il est le seul cépage en Alsace à produire un vin rouge dont le fruité typique évoque la cerise.', 'avec tout un repas', '', 6, 75, '9.00', '54.00'),
(25, 10, 'Reserve du Château de Briante 2003', 'Les raisins sont triés avant mise en cuve et vinifiés dans l''esprit du Moulin à Vent Réserve du Domaine Champs de Cour avec une cuvaison longue.', 'viande blanche voire même une viande rouge, ou une volaille', '', 6, 75, '8.90', '53.40'),
(26, 8, 'Gewurztraminer de Riquewihr 2003', 'Sigillé Confrérie St-Etienne | Médaille d''argent à Colmar | Corsé et charpenté, il développe de riches arômes de fruits, de fleurs et d''épices.Vin d''appéritif.', 'avec des mets relevés, des rognons et avec le fromage alsacien, par excellence le Munster', '', 6, 75, '13.20', '79.20'),
(27, 13, 'La Cerisaie 2004', 'Vin fin et délicat. | Arômes de violette, iris, groseille et cerise.', 'une andouillette grillée, ou un poulet rôti, ou une fricassée de volailles, ou un gigot, ou une terrine de canard', '', 6, 75, '10.50', '63.00'),
(28, 8, 'Muscat de Riquewihr 2004', 'Sigillé Confrérie St-Etienne | Très frais en bouche, et avec ses belles expressions aromatiques. Sa légère rondeur en fin de bouche le rend flatteur.', 'feuilleté d''asperges aux morilles, Kougelhopf ou kouglof, Poissons de rivière nature ou avec un léger beurre blanc', 'vin à boire jeune dans les 2 à 3 ans', 6, 75, '12.40', '74.40'),
(29, 6, 'Domaine de la Conseillère 2003', 'Le village de Juliénas, dans le Beaujolais, a été nommé en l''honneur de Jules César. Le vignoble du Domaine de la Conseillère, qui fait à peine 5 hectares, est situé à l''extrémité nord du Beaujolais, à deux pas du Mâconnais, en Bourgogne. | Accord : filet de porc', '', '', 6, 75, '8.90', '53.40'),
(30, 11, 'Les Canneliers 2003', 'Vin puissant et racé. | Arômes de cassis, cannelle, poire et mûre.', 'un pigeon aux petits pois, ou des cailles rôties, ou un gigot, ou un chevreau, ou pour accompagner un fromage fort, ou une daube beaujolaise', '', 6, 75, '9.10', '54.60'),
(31, 10, 'Château de Pierreux 2004', 'Avec 77 hectares de vignes d''un seul tenant sur l''appellation brouilly, le '' Château de Pierreux '' est l''un des plus grands domaines viticoles beaujolais. Le vignoble s''étend autour du château et au pied du Mont Brouilly, sur un terroir exceptionnel de granit et sable alluviaux.', 'viande blanche voire même une viande rouge, ou une volaille', '', 6, 75, '8.90', '53.40'),
(32, 8, 'Pinot Blanc Tiré Sur Lies 2003', 'Médaille d''Or à Colmar | Tendre et délicat, il allie fraîcheur et souplesse pour représenter un juste milieu dans la gamme des vins d''Alsace.', 'cuisine traditionnelle alsacienne (quiches, tartes flambées ', '', 6, 75, '8.20', '49.20'),
(33, 10, 'Reserve du Château de Briante 2004', 'Bonne et savoureuse présence du fruit sur une structure tannique fondue et une fine acidité. Vin harmonieux, savoureux, construit et long.', 'viande blanche voire même une viande rouge, ou une volaille', '', 6, 75, '8.90', '53.40'),
(34, 10, 'Les Grumières 2003', 'Vin fin et fruité. | Arômes de fraise et myrtille.', 'viande blanche voire même une viande rouge, ou une volaille', '', 6, 75, '8.50', '51.00'),
(35, 1, 'Pinot Noir Rosé Grès Rose 2004', 'Il est le seul cépage en Alsace à produire un vin rosé dont le fruité typique évoque la cerise.', 'avec la plupart des mets, pour l''amateur de vins rosé, un chatoyant compagnon', '', 6, 75, '9.00', '54.00'),
(36, 9, 'Les Reverchons 2004', 'Vin racé. | Arômes de raisins frais avec des notes minérales.', 'un civet de lapin, ou un gibier à plume, ou un chapon à la mousse de foie gras, ou des charcuteries fines', '', 6, 75, '7.30', '43.80'),
(37, 11, 'Les Canneliers 2004', 'Vin puissant et racé. | Arômes de cassis, cannelle, poire et mûre.', 'un pigeon aux petits pois, ou des cailles rôties, ou un gigot, ou un chevreau, ou pour accompagner un fromage fort, ou une daube beaujolaise', '', 6, 75, '9.10', '54.60'),
(38, 7, 'Wild Brut (Non Dosé) 2002', 'L''appellation Crémant d''Alsace Contrôlée est un vin d''Alsace vinifié en méthode traditionnelle suivant une législation rigoureuse donnant toutes les garanties d''un produit de grande classe. Issu des variétés de Pinot, il étonne le dégustateur non averti par sa qualité.', 'au moment de l''apéritif, ou tout au long d''un repas', '', 6, 75, '10.90', '65.40'),
(39, 1, 'Pinot Blanc 2004', 'Tendre et délicat, il allie fraîcheur et souplesse pour représenter un juste milieu dans la gamme des vins d''Alsace.', 'avec charcuteries, terrines, tartes flambées, tourtes et même volailles et viandes blanches', '', 6, 75, '6.50', '39.00'),
(40, 3, 'Les Griottes 2003', 'Bouche solide, bien structurée sur de fins tanins, un fruit parfait et une bonne fraîcheur. Vin complet, expressif et très persistant.', 'un pâté de campagne, ou un ragoût de mouton, ou un canard sauvage aux petits oignons, ou un faisan à la Normande, ou un gigot', '', 6, 75, '7.80', '46.80'),
(41, 8, 'Sylvaner de Riquewihr 2003', 'Médaille d''or à Colmar | Sec, racé, délicatement fruité, il offre un bouquet d''une grande finesse avec des nuances minérales ou florales.', 'avec de la charcuterie, des crudités et des fruits de mer ou avec tous les mets demandant un vin blanc sec', '', 6, 75, '7.10', '42.60'),
(42, 14, 'Tokay Pinot Gris de Riquewihr 2002', 'Il développe une oppulence et une saveur caractéristiques. Bouche : grasse, harmonieuse, dominée par des parfums fruités et une finale acidulée.', 'sur du foie gras d''oie frais', '', 6, 75, '30.20', '181.20'),
(43, 10, 'Les Grumières 2004', 'Vin fin et fruité. | Arômes de fraise et myrtille.', 'viande blanche voire même une viande rouge, ou une volaille', '', 6, 75, '8.50', '51.00'),
(44, 1, 'Sylvaner 2004', 'Remarquablement frais et léger, avec un fruité discret.', 'avec de la charcuterie, des crudités et des fruits de mer ou avec tous les mets demandant un vin blanc sec', '', 6, 75, '5.90', '35.40'),
(45, 7, 'Brut Rosé', 'D''un rosé tendre et d''un perlé fin, le Brut Rosé de Dopff est ample, équilibré et fruité. La finale conclut la dégustation dans une belle harmonie. A mettre dans sa cave et à servir à table sur des viandes blanches.', 'sur des viandes blanches', '', 6, 75, '10.90', '65.40'),
(46, 7, 'Magnum Cuvée Julien Brut', 'Crémant assemblant 2 cépages se présente sous une élégante robe brillante à reflets jaunes, le nez est léger, fin, aux nuances florales. Agréable fraîcheur en finale.', 'au moment de l''apéritif, ou tout au long d''un repas', '', 3, 150, '21.90', '65.70'),
(47, 8, 'Tokay Pinot Gris de Riquewihr 2001', 'Sigillé Confrérie St-Etienne | Médaille d''or à Colmar | Il développe une opulence et une saveur caractéristiques. Charpenté, rond et long en bouche, il présente des arômes complexes de sous-bois, parfois légèrement fumés.', '', '', 6, 75, '13.70', '82.20'),
(48, 7, 'Collection Brut 1997', 'Cuvée de l''An 2000 | Le Crémant a une robe or pâle. La finesse des bulles confirme la prise de mousse à basse température. On trouve une note de miel qui domine au nez et qui revient au palais. Une pointe d''acidité en finale lui donne son équilibre et son élégance.', 'au moment de l''apéritif, ou tout au long d''un repas', '', 6, 75, '11.40', '68.40'),
(49, 3, 'Domaine de Lathevale 2004', 'Le domaine compte 16ha, tout en AOC Morgon, donc spécialisé en AOC Morgon. Culture traditionnelle avec Labourage d''automne permettant l''enracinement et donc le respect du terroir. Palissage de la totalité des vignes et donc un bon étalement du feuillage. Epampage manuel et maîtrise des rendements, donnant une qualité de raisin exceptionnelle.', 'un pâté de campagne, ou un ragoût de mouton, ou un canard sauvage aux petits oignons, ou un faisan à la Normande, ou un gigot', '', 6, 75, '8.90', '53.40'),
(50, 13, 'Reserve Mommessin 2002', 'Cuvée Reserve Mommessin | Médaille d''or à Mâcon | Coup de Coeur du Guide Hachette | Vin fin et délicat. | Arômes de violette, iris, groseille et cerise. | Guide Hachette : 2004 **', 'des charcuteries fines ou des grillades, ou un poulet à l''estragon, un osso-bucco, ou des cailles aux raisins, ou un carré de porc au curry et aux pommes', '', 6, 75, '15.40', '92.40'),
(51, 14, 'Riesling de Riquewihr 1998', 'Vin rarissime produit dans les millésimes exceptionnels. Indiscutablement le plus grand de tous. Délicatement fruité, il fournit un bouquet d''une grande finesse. Excellent vin de garde, c''est à maturité après environ 7 ans, que ce prodige de délicatesse, d''élégance et de classe, se savourera.', 'sur du crabe, langouste, homard et poissons gras en sauces', '', 6, 75, '28.30', '169.80'),
(52, 15, 'Les Framboisiéres 2004', 'Bouche parfaitement construite sur des tanins élégants, un beau fruité et une excellente acidité. Vin ferme, expressif et très long.', 'une entrée chaude à la crème, ou des charcuteries fines accompagnées ou pas de crudités, un rôti de porc aux myrtilles par exemple, ou une terrine, ou une viande blanche', '', 6, 75, '6.90', '41.40'),
(53, 5, 'Les Maras 2002', 'Bouche riche en fruit sur une structure tannique bien mûre et fondue et sur une fine fraîcheur. Vin souple, gras, suave, long et parfumé.', 'un pigeon aux petits pois, ou des cailles rôties, ou un gigot, ou un chevreau, ou pour accompagner un fromage fort, ou une daube beaujolaise', '', 6, 75, '7.30', '43.80'),
(54, 6, 'Les Burlats 2004', 'Le Juliénas, qui d''après l''étymologie tiendrait son nom de Jules César, est situé à la porte nord du vignoble Beaujolais. Il s''étend sur 580 hectares de terroirs complémentaires granitiques à l''ouest et sédimentaires à l''Est. | Vin charpenté. Arômes de cerise, chocolat chaud et poire.', 'un coq au vin, ou toute autre volaille en sauce', '', 6, 75, '7.80', '46.80'),
(55, 9, 'Les Reverchons 2003', 'Vin racé. | Arômes de raisins frais avec des notes minérales.', 'un civet de lapin, ou un gibier à plume, ou un chapon à la mousse de foie gras, ou des charcuteries fines', '', 6, 75, '7.30', '43.80'),
(56, 16, 'Gewurztraminer de Riquewihr 2002', 'La robe est jaune citron assez dense. Le nez est parfumé, marqué par du pralin et de la rose. La bouche est ronde, sucrée sans que la liqueur devienne piquante. Elle se fait caressante à souhait, et regorge d''arômes de rose sans entrer dans le style bombe sucrée de certains pinots gris. Le millésime 2002 est ici magnifié par cette rondeur caressante qui en fait un vin très agréable à boire aujourd''hui ou bien a conserver de longues années encore, il n''en sera que meilleur.', 'accompagne à merveille foie gras, poularde au gewurztraminer, munster, et pâtisserie', 'apogée vers 2015', 6, 75, '56.90', '341.40'),
(57, 17, 'Les Pêchers 2003', 'Saint Amour est le cru le plus septentrional du vignoble Beaujolais. Les vignes s''étendent sur 280 hectares de plateaux dominant la vallée de la Saône. | Vin charmeur et tendre. | Arômes de cerises, framboise et pêche de vigne.', 'un gibier à plume, ou une volaille rôtie, ou une potée aux flageolets, ou une daube beaujolaise, ou pour accompagner un fromage à pâte molle', '', 6, 75, '9.10', '54.60'),
(58, 7, 'Cuvée Julien Brut', 'Crémant assemblant 2 cépages se présente sous une élégante robe brillante à reflets jaunes, le nez est léger, fin, aux nuances florales. Agréable fraîcheur en finale.', 'au moment de l''apéritif, ou tout au long d''un repas', '', 6, 75, '9.30', '55.80'),
(59, 3, 'Domaine de Lathevale 2003', 'Le domaine compte 16ha, tout en AOC Morgon, donc spécialisé en AOC Morgon. Culture traditionnelle avec Labourage d''automne permettant l''enracinement et donc le respect du terroir. Palissage de la totalité des vignes et donc un bon étalement du feuillage. Epampage manuel et maîtrise des rendements, donnant une qualité de raisin exceptionnelle.', 'un pâté de campagne, ou un ragoût de mouton, ou un canard sauvage aux petits oignons, ou un faisan à la Normande, ou un gigot', '', 6, 75, '8.90', '53.40'),
(60, 1, 'Riesling 2004', 'Vin blanc, Alsace, Vins d''Alsace', 'avec de la charcuterie, des crudités et des fruits de mer', '', 6, 75, '7.50', '45.00'),
(61, 1, 'Muscat Réserve 2004', 'Vin blanc, Alsace, Vins d''Alsace', 'avec des asperges et même avec un melon, apéritif original et éventuellement un vin de de dessert avec des gâteaux secs', '', 6, 75, '8.00', '48.00'),
(62, 12, 'Gewurztraminer Hatschbourg de Ha 2002', 'Les vins du Grand Cru Hatschbourg présentent une excellente typicité, des arômes très développés et une aptitude certaine à la garde.', 'vin de réception par excellence, à la fois à l''apéritif et au dessert', 'atteindra son meilleur niveau dans 4 à 5 ans', 6, 75, '15.70', '94.20'),
(63, 7, 'Cuvée Bartholdi Brut 2002', 'Vin blanc, Alsace, Crémants d''Alsace', 'au moment de l''apéritif, ou tout au long d''un repas', '', 6, 75, '11.40', '68.40'),
(64, 14, 'Gewurztraminer de Riquewihr 2003 V.T.', 'Ce vin à la robe profonde et concentrée, au bouquet incomparable de fruits exotiques, est un produit d''exception. Puissant et séducteur, moelleux, d''une texture dense et grasse, cette merveille d''une longévité au-dessus du commun est à déguster majestueusement lors d''une grande occasion.', 'sur des mets relevés, des rognons et avec le fromage alsacien, par excellence le Munster', '', 6, 75, '29.70', '178.20'),
(65, 1, 'Tokay Pinot Gris Réserve 2004', 'Le terme ''Réserve'' couvre deux sélections opérées : | - l''une au pressoir, par le choix de raisins en provenance d''excellentes expositions ayant une teneur en sucre optimale. | - l''autre, en cave, après fermentation et à la suite d''une dégustation à l''aveugle des plus belles cuvées.', 'saumon fumé, des rougets à l''Antillaise, une volaille rôtie (chapon ou pintadeau), un agneau bien cuit, une terrine de gibier', '', 6, 75, '9.10', '54.60'),
(66, 12, 'Gewurztraminer Sporen de Riquewihr 2003', 'Sigillé Confrérie St-Etienne | Corsé et charpenté, il développe de riches arômes floraux qui évoluent sur le fruit exotique. Les Vins issus du Sporen sont caractérisés par la finesse et la race, la richesse alliée à l''élégance.', 'vin de réception par excellence, à la fois à l''apéritif et au dessert', 'atteindra son meilleur niveau dans 4 à 5 ans', 6, 75, '16.90', '101.40'),
(67, 17, 'Les Pêchers 2004', 'Robe grenat de belle intensité. Nez vigoureux de framboise, de fraise, de prune, de violette, de pivoine et de musc. Bouche finement structurée sur un fruité épanoui et très mûr, une excellente fraîcheur et une persistance nerveuse et très gourmande.', 'un gibier à plume, ou une volaille rôtie, ou une potée aux flageolets, ou une daube beaujolaise, ou pour accompagner un fromage à pâte molle', '', 6, 75, '9.10', '54.60'),
(68, 11, 'Dom. Champ de Cour 2003 (cuvée de la réserve)', 'Cuvée Réserve du Domaine de Champ de Cour | Propriété de la Famille Mommessin depuis 1962, la réserve du domaine de Champ de Cour est élaboré avec la collaboration de Sylvain Pitiot sur une séléction des meilleures parcelles.', 'un osso-bucco, ou des cailles aux raisins, ou un carré de porc au curry et aux pommes', '', 6, 75, '18.00', '108.00'),
(69, 13, 'La Cerisaie 2003', 'Vin fin et délicat. | Très jolie matière fruitée en bouche, sur de fins tanins et une fraîcheur impeccable. Vin harmonieux, croquant, doté d''une persistance soutenue et nerveuse.', 'une andouillette grillée, ou un poulet rôti, ou une fricassée de volailles, ou un gigot, ou une terrine de canard', '', 6, 75, '10.50', '63.00'),
(70, 1, 'Gewurztraminer 2004', 'Corsé et charpenté, il développe de riches arômes de fruits, de fleurs et d''épices. | Vin d''appéritif', 'avec des mets relevés, des rognons et avec le fromage alsacien, par excellence le Munster', '', 6, 75, '9.10', '54.60'),
(71, 11, 'Dom. Champ de Cour 2002', 'Cuvée Réserve du Domaine de Champ de Cour | Propriété de la Famille Mommessin depuis 1962, la réserve du domaine de Champ de Cour est élaboré avec la collaboration de Sylvain Pitiot sur une séléction des meilleures parcelles.', 'un osso-bucco, ou des cailles aux raisins, ou un carré de porc au curry et aux pommes', '', 6, 75, '16.50', '99.00'),
(72, 8, 'Pinot Noir de Riquewihr 2003', 'Sigillé Confrérie St-Etienne | Médaille d''or à Colmar | Le vin d''un rouge intense est élevé en petits fûts de chêne. Après une évolution de deux ou cinq ans en bouteille, les arômes de fruits rouges et la noblesse du cépage s''expriment pleinement.', 'viande ou un fromage', 'facilement 5 ans', 6, 75, '11.60', '69.60');

--
-- Index pour les tables exportées
--

--
-- Index pour la table `avis`
--
ALTER TABLE `avis`
 ADD PRIMARY KEY (`id`), ADD KEY `ref_oenologue` (`ref_oenologue`), ADD KEY `ref_vin` (`ref_vin`), ADD KEY `ref_oenologue_ref_vin` (`ref_oenologue`,`ref_vin`);

--
-- Index pour la table `categorie`
--
ALTER TABLE `categorie`
 ADD PRIMARY KEY (`id`), ADD UNIQUE KEY `nom` (`nom`);

--
-- Index pour la table `oenologue`
--
ALTER TABLE `oenologue`
 ADD PRIMARY KEY (`id`), ADD UNIQUE KEY `nom` (`nom`);

--
-- Index pour la table `souscategorie`
--
ALTER TABLE `souscategorie`
 ADD PRIMARY KEY (`id`), ADD UNIQUE KEY `nom` (`nom`), ADD KEY `ref_categorie` (`ref_categorie`);

--
-- Index pour la table `vin`
--
ALTER TABLE `vin`
 ADD PRIMARY KEY (`id`), ADD UNIQUE KEY `nom` (`nom`), ADD KEY `ref_souscategorie` (`ref_souscategorie`);

--
-- AUTO_INCREMENT pour les tables exportées
--

--
-- AUTO_INCREMENT pour la table `avis`
--
ALTER TABLE `avis`
MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=337;
--
-- AUTO_INCREMENT pour la table `categorie`
--
ALTER TABLE `categorie`
MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT pour la table `oenologue`
--
ALTER TABLE `oenologue`
MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT pour la table `souscategorie`
--
ALTER TABLE `souscategorie`
MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=18;
--
-- AUTO_INCREMENT pour la table `vin`
--
ALTER TABLE `vin`
MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=73;
--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `avis`
--
ALTER TABLE `avis`
ADD CONSTRAINT `rel_avis_vers_vin` FOREIGN KEY (`ref_vin`) REFERENCES `vin` (`id`),
ADD CONSTRAINT `rel_avis_vers_oenologue` FOREIGN KEY (`ref_oenologue`) REFERENCES `oenologue` (`id`);

--
-- Contraintes pour la table `souscategorie`
--
ALTER TABLE `souscategorie`
ADD CONSTRAINT `rel_souscategorie_vers_categorie` FOREIGN KEY (`ref_categorie`) REFERENCES `categorie` (`id`);

--
-- Contraintes pour la table `vin`
--
ALTER TABLE `vin`
ADD CONSTRAINT `rel_vin_vers_souscategorie` FOREIGN KEY (`ref_souscategorie`) REFERENCES `souscategorie` (`id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
