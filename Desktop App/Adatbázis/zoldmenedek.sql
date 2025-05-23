-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Nov 18. 00:31
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `zoldmenedek`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `changelog`
--

CREATE TABLE `changelog` (
  `id` int(11) NOT NULL,
  `userid` int(11) NOT NULL,
  `msg` varchar(255) NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `changelog`
--

INSERT INTO `changelog` (`id`, `userid`, `msg`, `date`) VALUES
(1, 1, 'megváltoztatta Loki adatait(Reg.szám: 122)', '2024-09-30 12:14:00'),
(3, 1, 'Feltöltött egy új kennelt a(z) elöli udvarba', '2024-09-30 12:17:00'),
(4, 1, 'Feltöltött egy új kennelt a(z) elöli udvarba', '2024-09-30 12:19:00'),
(5, 1, 'Törölt egy  kennelt a(z) elöli udvarból', '2024-09-30 12:23:00'),
(6, 1, 'Áthelyezte Loki(Regszám:122) kutyát a(z) elöli udvar 1 kennelbe', '2024-09-30 12:29:00'),
(7, 1, 'kivette rexike (Regszám: 126) a(z)kölyökudvar udvarból', '2024-09-30 12:32:00'),
(8, 1, 'kivette Loki (Regszám: 122) a(z) elöli udvarból', '2024-09-30 12:33:00'),
(9, 1, 'kivette Frakk (Regszám: 136) kutyát a(z) elöli udvarból', '2024-09-30 12:33:00'),
(10, 1, 'megváltoztatta Loki adatait(Reg.szám: 122)', '2024-09-30 12:55:00'),
(11, 1, 'megváltoztatta Loki adatait(Reg.szám: 122)', '2024-09-30 12:56:00'),
(12, 1, 'Áthelyezte Mici(Regszám:137) kutyát a(z) elöli udvar 4 kennelbe', '2024-10-01 12:06:00'),
(13, 1, 'megváltoztatta kennelnélküli adatait(Reg.szám: 250)', '2024-10-01 12:07:00'),
(14, 1, 'megváltoztatta vacak adatait(Reg.szám: 250)', '2024-10-01 12:08:00'),
(15, 1, 'Áthelyezte Loki(Regszám:122) kutyát a(z) elöli udvar 0 kennelbe', '2024-10-02 23:13:00'),
(16, 1, 'Áthelyezte Loki(Regszám:122) kutyát a(z) elöli udvar 1 kennelbe', '2024-10-02 23:13:00'),
(17, 1, 'Áthelyezte Foltos(Regszám:140) kutyát a(z) elöli udvar 0 kennelbe', '2024-10-02 23:13:00'),
(18, 1, 'Feltöltött egy új kennelt a(z) elöli udvarba', '2024-10-02 23:13:00'),
(19, 1, 'Áthelyezte Maszat(Regszám:156) kutyát a(z) elöli udvar 5 kennelbe', '2024-10-02 23:13:00'),
(20, 1, 'Áthelyezte Rex(Regszám:141) kutyát a(z) elöli udvar 5 kennelbe', '2024-10-02 23:13:00'),
(21, 1, 'Törölt egy  kennelt a(z) elöli udvarból', '2024-10-02 23:14:00'),
(22, 1, 'kivette Hógolyó (Regszám: 235) kutyát a(z) hátsó udvarból', '2024-10-02 23:14:00');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `error`
--

CREATE TABLE `error` (
  `id` int(11) NOT NULL,
  `msg` varchar(1000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `error`
--

INSERT INTO `error` (`id`, `msg`) VALUES
(1, 'Nem adtál nevet a kutyának!'),
(2, 'Hibás születési dátum!'),
(3, 'Hibás bekerülési dátum!'),
(4, 'ez a kutyanév már foglalt!'),
(5, 'Nem adtál meg ivart!'),
(6, 'Nem adtad meg, hogy ivaros-e!'),
(7, 'Nem adtál meg méretet!'),
(8, 'Nem adtál meg telephelyet!');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kennel`
--

CREATE TABLE `kennel` (
  `id` int(11) NOT NULL,
  `udvarid` int(11) NOT NULL,
  `kennelszam` int(11) NOT NULL,
  `kutyak` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `kennel`
--

INSERT INTO `kennel` (`id`, `udvarid`, `kennelszam`, `kutyak`) VALUES
(175, 3, 0, '144-Csibész;'),
(176, 3, 1, '214-Luna;'),
(178, 3, 2, ''),
(182, 2, 0, '148-Vacak;124-Bobi;'),
(185, 4, 0, ''),
(186, 4, 1, '125-Csöpi;'),
(187, 4, 2, '129-Zeusz;'),
(189, 4, 3, '143-Kormi;'),
(190, 4, 4, ''),
(193, 2, 1, '173-Zénó;132-Szilva;145-Zsömi;'),
(195, 2, 2, '133-Samu;194-Lizi;146-Luna;'),
(196, 2, 3, '242-Tamáska;147-Tappancs;174-Sammy;'),
(197, 2, 4, ''),
(198, 2, 5, '127-Max'),
(199, 2, 6, '135-Rézi;'),
(200, 2, 7, '159-Szofi;'),
(202, 2, 8, '166-Mázli;'),
(203, 3, 3, ''),
(204, 3, 4, ''),
(205, 3, 5, '250-vacak;'),
(206, 5, 0, '243-kutya5'),
(207, 5, 1, ''),
(208, 5, 2, ''),
(209, 1, 0, '134-Dió;140-Foltos;'),
(210, 2, 9, '150-Csöpi;'),
(211, 1, 1, '138-Pajti;122-Loki'),
(212, 1, 2, '123-Morzsi;128-Bell;130-Panni;'),
(213, 1, 3, '139-Hektor'),
(217, 1, 4, '156-Maszat;141-Rex;');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kutyak`
--

CREATE TABLE `kutyak` (
  `id` int(11) NOT NULL,
  `regszam` int(5) NOT NULL,
  `nev` varchar(255) NOT NULL,
  `chipszam` varchar(255) NOT NULL,
  `ivar` int(1) NOT NULL,
  `meret` int(1) NOT NULL,
  `szuletes` date NOT NULL,
  `bekerules` date NOT NULL,
  `ivaros` tinyint(1) NOT NULL,
  `telephely` int(1) NOT NULL,
  `foglalt` tinyint(1) NOT NULL,
  `kennel` int(1) NOT NULL,
  `indexkepid` int(11) DEFAULT NULL,
  `visible` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `kutyak`
--

INSERT INTO `kutyak` (`id`, `regszam`, `nev`, `chipszam`, `ivar`, `meret`, `szuletes`, `bekerules`, `ivaros`, `telephely`, `foglalt`, `kennel`, `indexkepid`, `visible`) VALUES
(121, 1500, 'Bogika', '123456789012345', 1, 0, '2024-09-15', '2021-05-10', 0, 0, 1, 0, 0, 0),
(122, 1201, 'Loki', '987654321098765', 1, 0, '2019-07-20', '2020-08-14', 0, 0, 1, 1, 0, 1),
(123, 1500, 'Morzsi', '567890123456789', 1, 1, '2018-03-30', '2019-01-21', 0, 1, 0, 1, 0, 1),
(124, 1500, 'Bobi', '345678901234567', 1, 0, '2017-11-12', '2018-06-25', 0, 0, 1, 1, 0, 1),
(125, 1500, 'Csöpi', '765432109876543', 0, 0, '2021-01-05', '2022-03-11', 0, 1, 0, 1, 0, 1),
(126, 1500, 'rexike', '654321098765432', 0, 0, '2016-05-18', '2017-04-03', 0, 0, 1, 0, 0, 1),
(127, 1500, 'Max', '432109876543210', 1, 1, '2019-09-28', '2020-12-01', 0, 1, 0, 1, 0, 1),
(128, 1500, 'Bell', '890123456789012', 1, 0, '2018-12-07', '2020-02-20', 0, 0, 1, 1, 0, 1),
(129, 1500, 'Zeusz', '321098765432109', 0, 2, '2020-07-30', '2021-09-15', 0, 1, 0, 1, 21, 1),
(130, 1500, 'Panni', '456789012345678', 0, 0, '2017-06-14', '2018-07-22', 0, 0, 1, 1, 0, 1),
(131, 1500, 'Fickó', '109876543210987', 0, 0, '2015-11-03', '2017-01-10', 0, 1, 0, 0, 0, 1),
(132, 1500, 'Szilva', '234567890123456', 1, 0, '2021-03-09', '2022-05-17', 0, 0, 1, 1, 0, 1),
(133, 1500, 'Samu', '987654321234567', 0, 0, '2019-10-12', '2020-11-21', 0, 1, 0, 1, 0, 1),
(134, 1500, 'Dió', '543210987654321', 0, 0, '2020-04-27', '2021-06-05', 0, 0, 1, 1, 0, 1),
(135, 1500, 'Rézi', '678901234567890', 0, 0, '2018-02-11', '2019-09-13', 0, 1, 0, 1, 0, 1),
(136, 1500, 'Frakk', '876543210987654', 0, 0, '2017-08-25', '2018-10-02', 0, 0, 1, 0, 0, 1),
(137, 1500, 'Mici', '345678909876543', 0, 0, '2021-11-29', '2023-02-10', 0, 1, 0, 0, 0, 1),
(138, 1500, 'Pajti', '432109876543218', 0, 0, '2016-04-14', '2017-12-19', 0, 0, 1, 1, 0, 1),
(139, 1500, 'Hektor', '654321234567890', 0, 0, '2019-06-05', '2020-08-07', 0, 1, 0, 1, 0, 1),
(140, 1500, 'Foltos', '321234567890123', 0, 0, '2018-09-17', '2020-01-03', 0, 0, 1, 1, 0, 1),
(141, 1500, 'Rex', '234567890123456', 0, 0, '2015-03-18', '2016-05-23', 0, 1, 0, 1, 0, 1),
(142, 1500, 'Csilla', '876543234567890', 0, 0, '2020-02-11', '2021-06-08', 0, 0, 1, 0, 0, 1),
(143, 1500, 'Kormi', '789012345678901', 0, 0, '2016-12-22', '2017-11-27', 0, 1, 0, 1, 0, 1),
(144, 1500, 'Csibész', '123456789087654', 0, 0, '2019-11-05', '2021-01-16', 0, 0, 1, 1, 0, 1),
(145, 1500, 'Zsömi', '567890987654321', 0, 0, '2017-07-09', '2018-11-14', 0, 1, 0, 1, 0, 1),
(146, 1500, 'Luna', '109876543234567', 0, 0, '2018-03-04', '2019-10-09', 0, 0, 1, 1, 0, 1),
(147, 1500, 'Tappancs', '432109876234567', 0, 0, '2016-09-29', '2017-05-15', 0, 1, 0, 1, 0, 1),
(148, 1500, 'Vacak', '543210987654320', 0, 0, '2019-08-08', '2020-09-18', 0, 0, 1, 1, 0, 1),
(149, 1500, 'Zizi', '789012345678909', 0, 0, '2020-01-15', '2021-04-25', 0, 1, 0, 0, 0, 1),
(150, 1500, 'Csöpi', '890123456789321', 0, 0, '2021-06-23', '2022-09-12', 0, 0, 1, 1, 0, 1),
(151, 1500, 'Léna', '987654320123456', 0, 0, '2017-04-14', '2018-02-28', 0, 1, 0, 0, 0, 1),
(152, 1500, 'Dagi', '765432109876543', 0, 0, '2016-06-21', '2017-08-19', 0, 0, 1, 0, 0, 1),
(153, 1500, 'Zorro', '432109876543219', 0, 0, '2021-09-12', '2022-11-01', 0, 1, 0, 0, 0, 1),
(154, 1500, 'Picur', '654321098765431', 0, 0, '2019-05-15', '2020-03-20', 0, 0, 1, 0, 0, 1),
(155, 1500, 'Füge', '876543210987650', 0, 0, '2018-10-07', '2020-04-25', 0, 1, 0, 0, 0, 1),
(156, 1500, 'Maszat', '210987654321098', 0, 0, '2020-12-12', '2022-01-05', 0, 0, 1, 1, 0, 1),
(157, 1500, 'Vili', '321098765432109', 0, 0, '2019-03-03', '2021-05-12', 0, 1, 0, 0, 0, 1),
(158, 1500, 'Roki', '567890123456782', 0, 0, '2015-11-29', '2017-02-16', 0, 0, 1, 0, 0, 1),
(159, 1500, 'Szofi', '876543210987656', 0, 0, '2017-02-18', '2018-11-05', 0, 1, 0, 1, 0, 1),
(160, 1500, 'Buksi', '543210987654321', 0, 0, '2020-08-15', '2021-09-22', 0, 0, 1, 0, 0, 1),
(161, 1500, 'Mázli', '109876543210988', 0, 0, '2016-04-11', '2017-01-09', 0, 1, 0, 0, 0, 1),
(162, 1500, 'Vanda', '234567890123458', 0, 0, '2018-01-10', '2019-03-18', 0, 0, 1, 0, 0, 1),
(163, 1500, 'Nyafi', '567890987654320', 0, 0, '2019-11-13', '2021-06-28', 0, 1, 0, 0, 0, 1),
(164, 1500, 'Kira', '432109876543213', 0, 0, '2020-07-20', '2021-10-15', 0, 0, 1, 0, 0, 1),
(165, 1500, 'Csoki', '765432109876541', 0, 0, '2017-05-11', '2018-08-23', 0, 1, 0, 0, 0, 1),
(166, 1500, 'Mázli', '543210987654329', 0, 0, '2021-03-21', '2022-06-30', 0, 0, 1, 1, 0, 1),
(167, 1500, 'Bogyó', '345678901234561', 0, 0, '2019-02-27', '2020-05-22', 0, 1, 0, 0, 0, 1),
(168, 1500, 'Roxi', '109876543210986', 0, 0, '2016-08-19', '2017-10-10', 0, 0, 1, 0, 0, 1),
(169, 1500, 'Tomi', '567890987654328', 0, 0, '2018-09-25', '2019-11-03', 0, 1, 0, 0, 0, 1),
(170, 1500, 'Tücsi', '765432109876540', 0, 0, '2020-11-29', '2022-02-14', 0, 0, 1, 0, 0, 1),
(171, 1500, 'Berci', '432109876543216', 0, 0, '2017-03-17', '2018-09-09', 0, 1, 0, 0, 0, 1),
(172, 1500, 'Bambi', '654321098765439', 0, 0, '2015-12-23', '2017-03-18', 0, 0, 1, 0, 0, 1),
(173, 1500, 'Zénó', '876543210987652', 0, 0, '2018-07-22', '2020-01-11', 0, 1, 0, 1, 0, 1),
(174, 1500, 'Sammy', '321098765432112', 0, 0, '2020-04-18', '2021-07-05', 0, 0, 1, 1, 0, 1),
(175, 1500, 'Dante', '109876543210984', 0, 0, '2019-10-09', '2021-04-12', 0, 1, 0, 0, 0, 1),
(176, 1500, 'Tobi', '234567890123457', 0, 0, '2016-05-06', '2017-07-28', 0, 0, 1, 0, 0, 1),
(177, 1500, 'Cézár', '567890987654327', 0, 0, '2021-08-31', '2023-01-20', 0, 1, 0, 0, 0, 1),
(178, 1500, 'Mia', '543210987654328', 0, 0, '2019-06-07', '2020-08-17', 0, 0, 1, 0, 0, 1),
(179, 1500, 'Léna', '876543210987651', 0, 0, '2020-12-10', '2022-05-23', 0, 1, 0, 0, 0, 1),
(180, 1500, 'Tücsi', '765432109876537', 0, 0, '2018-02-02', '2019-09-26', 0, 0, 1, 0, 0, 1),
(181, 1500, 'Morzsi', '345678901234569', 0, 0, '2023-01-05', '2023-09-12', 0, 1, 0, 0, 0, 1),
(182, 1500, 'Zizi', '654321098765430', 0, 0, '2023-04-18', '2023-09-01', 0, 0, 1, 0, 0, 1),
(183, 1500, 'Ropi', '876543210987653', 0, 0, '2022-12-29', '2023-08-15', 0, 1, 0, 0, 0, 1),
(184, 1500, 'Mimi', '109876543210981', 0, 0, '2019-06-17', '2021-02-13', 0, 0, 1, 0, 0, 1),
(185, 1500, 'Zeusz', '567890987654325', 0, 0, '2017-08-21', '2018-09-20', 0, 1, 0, 0, 0, 1),
(186, 1500, 'Sziszi', '765432109876536', 0, 0, '2020-02-26', '2022-01-14', 0, 0, 1, 0, 0, 1),
(187, 1500, 'Kormi', '210987654321091', 0, 0, '2022-05-25', '2023-02-28', 0, 1, 0, 0, 0, 1),
(188, 1500, 'Bobi', '543210987654327', 0, 0, '2020-12-11', '2021-09-10', 0, 0, 1, 0, 0, 1),
(189, 1500, 'Luna', '432109876543211', 0, 0, '2021-03-19', '2022-06-11', 0, 1, 0, 0, 0, 1),
(190, 1500, 'Rexi', '876543210987656', 0, 0, '2017-11-09', '2019-05-07', 0, 0, 1, 0, 0, 1),
(191, 1500, 'Kira', '234567890123459', 0, 0, '2021-08-30', '2022-11-01', 0, 1, 0, 0, 0, 1),
(192, 1500, 'Füge', '654321098765432', 0, 0, '2018-07-21', '2019-10-22', 0, 0, 1, 0, 0, 1),
(193, 1500, 'Csillag', '345678901234562', 0, 0, '2020-05-15', '2021-06-12', 0, 1, 0, 0, 0, 1),
(194, 1500, 'Lizi', '543210987654322', 0, 0, '2022-09-12', '2023-04-18', 0, 0, 1, 1, 0, 1),
(195, 1500, 'Fickó', '876543210987654', 0, 0, '2019-11-23', '2020-12-05', 0, 1, 0, 0, 0, 1),
(196, 1500, 'Cuki', '765432109876532', 0, 0, '2016-03-13', '2017-07-27', 0, 0, 1, 0, 0, 1),
(197, 1500, 'Néró', '432109876543218', 0, 0, '2018-04-04', '2019-02-19', 0, 1, 0, 0, 0, 1),
(198, 1500, 'Héra', '109876543210982', 0, 0, '2022-11-14', '2023-06-23', 0, 0, 1, 0, 0, 1),
(199, 1500, 'Manó', '987654321098765', 0, 0, '2015-09-28', '2017-01-03', 0, 1, 0, 0, 0, 1),
(200, 1500, 'Zsömle', '123456789012346', 0, 0, '2022-02-24', '2023-05-18', 0, 0, 1, 0, 0, 1),
(201, 1500, 'Nyafi', '876543210987657', 0, 0, '2020-06-18', '2021-10-12', 0, 1, 0, 0, 0, 1),
(202, 1500, 'Csutka', '765432109876531', 0, 0, '2019-01-06', '2020-07-15', 0, 0, 1, 0, 0, 1),
(203, 1500, 'Rex', '543210987654324', 0, 0, '2021-10-10', '2023-01-22', 0, 1, 0, 0, 0, 1),
(204, 1500, 'Tina', '234567890123450', 0, 0, '2016-12-01', '2018-04-14', 0, 0, 1, 0, 0, 1),
(205, 1500, 'Dolly', '876543210987659', 0, 0, '2019-02-09', '2020-08-05', 0, 1, 0, 0, 0, 1),
(206, 1500, 'Mázli', '543210987654323', 0, 0, '2023-02-10', '2023-08-11', 0, 0, 1, 0, 0, 1),
(207, 1500, 'Sam', '432109876543215', 0, 0, '2018-06-27', '2019-12-09', 0, 1, 0, 0, 0, 1),
(208, 1500, 'Áfonya', '210987654321093', 0, 0, '2020-08-30', '2022-03-01', 0, 0, 1, 0, 0, 1),
(209, 1500, 'Pocak', '765432109876530', 0, 0, '2022-01-17', '2023-04-09', 0, 1, 0, 0, 0, 1),
(210, 1500, 'Nala', '109876543210980', 0, 0, '2021-12-08', '2022-11-12', 0, 0, 1, 0, 0, 1),
(211, 1500, 'Pici', '345678901234590', 0, 0, '2022-09-10', '2023-08-05', 0, 1, 0, 0, 0, 1),
(212, 1500, 'Mazsola', '654321098765412', 0, 0, '2023-01-15', '2023-08-10', 0, 0, 1, 0, 0, 1),
(213, 1500, 'Bogár', '876543210987652', 0, 0, '2022-11-25', '2023-06-18', 0, 1, 0, 0, 0, 1),
(214, 1500, 'Luna', '543210987654311', 0, 0, '2020-07-10', '2021-09-20', 0, 0, 1, 1, 0, 1),
(215, 1500, 'Zsófi', '123456789012398', 0, 0, '2021-03-03', '2022-02-01', 0, 1, 0, 0, 0, 1),
(216, 1500, 'Tigris', '210987654321098', 0, 0, '2019-04-12', '2020-05-15', 0, 0, 1, 0, 0, 1),
(217, 1500, 'Zorro', '109876543210987', 0, 0, '2020-10-25', '2021-11-12', 0, 1, 0, 0, 0, 1),
(218, 1500, 'Pamacs', '987654321098765', 0, 0, '2023-02-01', '2023-09-10', 0, 0, 1, 0, 0, 1),
(219, 1500, 'Morgó', '765432109876543', 0, 0, '2018-12-20', '2019-08-10', 0, 1, 0, 0, 0, 1),
(220, 1500, 'Nyufi', '654321098765423', 0, 0, '2021-08-15', '2022-11-20', 0, 0, 1, 0, 0, 1),
(221, 1500, 'Barna', '876543210987643', 0, 0, '2020-09-10', '2021-12-18', 0, 1, 0, 0, 0, 1),
(222, 1500, 'Cirmi', '543210987654399', 0, 0, '2019-06-20', '2020-05-17', 0, 0, 1, 0, 0, 1),
(223, 1500, 'Döme', '345678901234532', 0, 0, '2022-07-08', '2023-04-05', 0, 1, 0, 0, 0, 1),
(224, 1500, 'Huszi', '123456789012301', 0, 0, '2017-02-02', '2018-03-10', 0, 0, 1, 0, 0, 1),
(225, 1500, 'Füles', '765432109876598', 0, 0, '2016-11-19', '2017-12-20', 0, 1, 0, 0, 0, 1),
(226, 1500, 'Bütyök', '234567890123456', 0, 0, '2021-05-07', '2022-07-25', 0, 0, 1, 0, 0, 1),
(227, 1500, 'Panka', '654321098765411', 0, 0, '2022-03-01', '2023-01-18', 0, 1, 0, 0, 0, 1),
(228, 1500, 'Szimat', '109876543210932', 0, 0, '2021-06-19', '2022-10-15', 0, 0, 1, 0, 0, 1),
(229, 1500, 'Maci', '210987654321011', 0, 0, '2019-05-22', '2020-08-10', 0, 1, 0, 0, 0, 1),
(230, 1500, 'Csoki', '432109876543221', 0, 0, '2017-03-10', '2018-06-05', 0, 0, 1, 0, 0, 1),
(231, 1500, 'Ropi', '876543210987634', 0, 0, '2020-11-30', '2021-12-01', 0, 1, 0, 0, 0, 1),
(232, 1500, 'Zizi', '345678901234576', 0, 0, '2022-10-05', '2023-05-12', 0, 0, 1, 0, 0, 1),
(233, 1500, 'Sára', '543210987654345', 0, 0, '2016-08-10', '2017-09-22', 0, 1, 0, 0, 0, 1),
(234, 1500, 'Buksi', '123456789012344', 0, 0, '2019-02-15', '2020-03-05', 0, 0, 1, 0, 0, 1),
(235, 1500, 'Hógolyó', '765432109876512', 1, 0, '2022-05-20', '2023-02-28', 0, 1, 0, 0, 0, 1),
(236, 1500, 'Vacak', '210987654321024', 0, 0, '2020-04-18', '2021-06-30', 0, 0, 1, 0, 0, 1),
(237, 1500, 'Fanni', '987654321098745', 0, 0, '2022-11-12', '2023-08-25', 0, 1, 0, 0, 0, 1),
(238, 1500, 'Bella', '432109876543212', 0, 0, '2018-09-10', '2019-11-15', 0, 0, 1, 0, 0, 1),
(239, 1500, 'Tigris', '876543210987612', 0, 0, '2020-01-23', '2021-03-22', 0, 1, 0, 0, 0, 1),
(240, 1500, 'Manci', '654321098765492', 0, 0, '2022-08-14', '2023-07-12', 0, 0, 1, 0, 0, 1),
(241, 1500, 'százszorszép', '34352354', 0, 1, '2020-02-10', '2024-09-06', 1, 0, 0, 0, 20, 1),
(242, 1500, 'Tamáska', '699999', 1, 2, '2017-01-01', '2024-09-25', 0, 1, 0, 1, 23, 1),
(243, 1500, 'kutya5', '53242', 0, 0, '2024-05-18', '2024-09-26', 1, 0, 0, 1, 1, 1),
(244, 1500, 'kutya10', '3431543', 0, 0, '2024-02-15', '2024-09-26', 1, 0, 0, 0, 1, 1),
(245, 1500, 'kutya34324', '43124143', 0, 0, '2024-09-12', '2024-09-26', 1, 0, 0, 0, 1, 1),
(246, 1500, 'k1', '43542', 1, 0, '2024-06-28', '2024-09-29', 1, 0, 0, 0, 1, 1),
(247, 1500, 'kokker', '11', 0, 0, '2024-09-11', '2024-09-29', 1, 0, 0, 0, 1, 1),
(248, 1500, 'Pitypang', '1111', 1, 2, '2022-01-29', '2024-09-29', 1, 0, 0, 0, 1, 1),
(249, 1500, 'Mazsolamorzsa', '11', 1, 1, '2024-08-29', '2024-09-29', 1, 0, 0, 0, 1, 1),
(250, 1500, 'vacak', '32432', 1, 0, '2024-09-12', '2024-09-29', 0, 1, 0, 1, 24, 1),
(4408, 2200, 'Gyula', '345324234', 0, 0, '2024-09-08', '2024-09-08', 0, 0, 0, 0, 0, 1),
(4411, 1500, 'Lajos', '50000', 0, 0, '2020-10-10', '2020-10-10', 0, 0, 0, 0, 0, 0),
(4412, 1500, 'Oszkár', '50000', 0, 0, '2020-10-10', '2020-10-10', 0, 0, 0, 0, 0, 0),
(4413, 1500, 'Lajos', '50000', 0, 0, '2020-10-10', '2020-10-10', 0, 0, 0, 0, 0, 0),
(4414, 1500, 'Démon', '50000', 0, 0, '2020-10-10', '2020-10-10', 0, 0, 0, 0, 0, 0),
(4415, 1500, 'Démon', '50000', 0, 0, '2020-10-10', '2020-10-10', 0, 0, 0, 0, 0, 0),
(4416, 1500, 'Osztkár2', '50000', 0, 0, '2020-10-10', '2020-10-10', 0, 0, 0, 0, 0, 0),
(4417, 1500, 'Osztkár2', '50000', 0, 0, '2020-10-10', '2020-10-10', 0, 0, 0, 0, 0, 0),
(4418, 1500, 'Osztkár2', '50000', 0, 0, '2020-10-10', '2020-10-10', 0, 0, 0, 0, 0, 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kutyakep`
--

CREATE TABLE `kutyakep` (
  `id` int(11) NOT NULL,
  `kutyaid` int(11) NOT NULL,
  `nev` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `kutyakep`
--

INSERT INTO `kutyakep` (`id`, `kutyaid`, `nev`) VALUES
(4, 113, 'minimal-sunset-landscape-4k-w5-1366x768.jpg'),
(7, 115, '667fb8a4-0ee2-43f2-951c-55b49fac8252.jfif'),
(11, 115, 'boldog-kutya.jpg'),
(12, 116, '0da2d6db-8120-4a17-9051-ae9f0364e6fc.jfif'),
(14, 118, '2024.05.18.ZM-42.jpg'),
(15, 118, '2024.05.18.ZM-45.jpg'),
(16, 79, '75d309fb-4c0e-4366-97ac-fd0bc918d424.webp'),
(17, 116, '2024.05.18.ZM-45.jpg'),
(18, 120, '0da2d6db-8120-4a17-9051-ae9f0364e6fc.jfif'),
(19, 79, '29e41bf3-d3c1-4476-91d6-b0af72e7f389.jfif'),
(20, 241, '0da2d6db-8120-4a17-9051-ae9f0364e6fc.jfif'),
(21, 129, '2024.05.18.ZM-45.jpg'),
(23, 242, 'att.UMX3gI-yzfJvCdRfVYltlgnTnsIKniRWmKgG8jVxIUI.jpg'),
(24, 250, 'IMG_20240930_204344.jpg');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `randomnev`
--

CREATE TABLE `randomnev` (
  `id` int(11) NOT NULL,
  `nev` varchar(255) NOT NULL,
  `ivar` int(1) NOT NULL,
  `foglalt` int(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `randomnev`
--

INSERT INTO `randomnev` (`id`, `nev`, `ivar`, `foglalt`) VALUES
(1, 'Sasha', 0, 0),
(2, 'Finn', 1, 0),
(3, 'Stella', 0, 0),
(4, 'Jax', 1, 0),
(5, 'Mia', 0, 0),
(6, 'Harley', 1, 0),
(7, 'Penny', 0, 0),
(8, 'Bentley', 1, 0),
(9, 'Ellie', 0, 0),
(10, 'Beau', 1, 0),
(11, 'Hazel', 0, 0),
(12, 'Oscar', 1, 0),
(13, 'Abby', 0, 0),
(14, 'Rex', 1, 0),
(15, 'Layla', 0, 0),
(16, 'Roscoe', 1, 0),
(17, 'Pepper', 0, 0),
(18, 'Samson', 1, 0),
(19, 'Annie', 0, 0),
(20, 'Otis', 1, 0),
(21, 'Ginger', 0, 0),
(22, 'Ace', 1, 0),
(23, 'Willow', 0, 0),
(24, 'Boomer', 1, 0),
(25, 'Holly', 0, 0),
(26, 'Bandit', 1, 0),
(27, 'Dixie', 0, 0),
(28, 'Marley', 1, 0),
(29, 'Belle', 0, 0),
(30, 'Scout', 1, 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `udvar`
--

CREATE TABLE `udvar` (
  `id` int(11) NOT NULL,
  `udvarnev` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `udvar`
--

INSERT INTO `udvar` (`id`, `udvarnev`) VALUES
(1, 'elöli'),
(2, 'kölyökudvar'),
(3, 'dühöngő'),
(4, 'parkoló'),
(5, 'hátsó'),
(6, 'nincs udvar');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `admin` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `admin`) VALUES
(1, 'admin', '0', 1);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `changelog`
--
ALTER TABLE `changelog`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `error`
--
ALTER TABLE `error`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `kennel`
--
ALTER TABLE `kennel`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `kutyak`
--
ALTER TABLE `kutyak`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `kutyakep`
--
ALTER TABLE `kutyakep`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `randomnev`
--
ALTER TABLE `randomnev`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `udvar`
--
ALTER TABLE `udvar`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `changelog`
--
ALTER TABLE `changelog`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT a táblához `error`
--
ALTER TABLE `error`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT a táblához `kennel`
--
ALTER TABLE `kennel`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=218;

--
-- AUTO_INCREMENT a táblához `kutyak`
--
ALTER TABLE `kutyak`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4419;

--
-- AUTO_INCREMENT a táblához `kutyakep`
--
ALTER TABLE `kutyakep`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT a táblához `randomnev`
--
ALTER TABLE `randomnev`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT a táblához `udvar`
--
ALTER TABLE `udvar`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
