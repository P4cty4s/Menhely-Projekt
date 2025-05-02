-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Ápr 18. 15:16
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
-- Adatbázis: `pawdmin`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `changelog`
--

CREATE TABLE `changelog` (
  `id` int(11) NOT NULL,
  `category` varchar(255) NOT NULL,
  `userid` int(11) NOT NULL,
  `msg` varchar(255) NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

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

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kutyak`
--

CREATE TABLE `kutyak` (
  `id` int(11) NOT NULL,
  `regszam` int(5) NOT NULL,
  `nev` varchar(255) NOT NULL,
  `chipszam` varchar(255) NOT NULL,
  `ivar` varchar(255) NOT NULL,
  `meret` varchar(255) NOT NULL,
  `szuletes` date NOT NULL,
  `bekerules` date NOT NULL,
  `ivaros` varchar(255) NOT NULL,
  `telephely` varchar(255) NOT NULL,
  `foglalt` int(1) NOT NULL,
  `kennel` int(1) NOT NULL,
  `indexkepid` int(11) DEFAULT NULL,
  `status` varchar(255) NOT NULL,
  `visible` int(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `kutyak`
--

INSERT INTO `kutyak` (`id`, `regszam`, `nev`, `chipszam`, `ivar`, `meret`, `szuletes`, `bekerules`, `ivaros`, `telephely`, `foglalt`, `kennel`, `indexkepid`, `status`, `visible`) VALUES
(251, 71349, 'Buksi', '72745150169', 'kan', 'kistestű', '2014-01-26', '2014-08-21', 'ivaros', 'vác', 0, 0, 0, 'Nálunk van', 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kutyakep`
--

CREATE TABLE `kutyakep` (
  `id` int(11) NOT NULL,
  `kutyaid` int(11) NOT NULL,
  `nev` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `menhely`
--

CREATE TABLE `menhely` (
  `nev` varchar(255) NOT NULL,
  `hely` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `telefonszam` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `menhely`
--

INSERT INTO `menhely` (`nev`, `hely`, `email`, `telefonszam`) VALUES
('Pawdmin', 'Budapest', 'menhely@gmail.com', '06302453472');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `telephely`
--

CREATE TABLE `telephely` (
  `id` int(11) NOT NULL,
  `nev` varchar(255) NOT NULL,
  `hely` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `telefonszam` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `telephely`
--

INSERT INTO `telephely` (`id`, `nev`, `hely`, `email`, `telefonszam`) VALUES
(1, 'Budapest', 'Budapest', 'bp@menhely.com', '0630453324'),
(2, 'vác', 'vác', 'vac@gmail.com', '0784324324');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `udvar`
--

CREATE TABLE `udvar` (
  `id` int(11) NOT NULL,
  `telephelyid` int(11) NOT NULL,
  `udvarnev` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `udvar`
--

INSERT INTO `udvar` (`id`, `telephelyid`, `udvarnev`) VALUES
(1, 0, 'elöli'),
(2, 0, 'kölyökudvar'),
(3, 0, 'dühöngő'),
(4, 0, 'parkoló'),
(5, 0, 'hátsó'),
(6, 0, 'nincs udvar');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `telefonszam` varchar(255) NOT NULL,
  `telephely` varchar(255) NOT NULL,
  `admin` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `telefonszam`, `telephely`, `admin`) VALUES
(1, 'admin', '$2y$10$UyAPWwwvV1IPAVgMTQn0s.wkbTy7nt5WPnwYOCC9fxNfsl52xc2uC', '0645324452', 'Budapest', 0);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `changelog`
--
ALTER TABLE `changelog`
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
-- A tábla indexei `telephely`
--
ALTER TABLE `telephely`
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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=61;

--
-- AUTO_INCREMENT a táblához `kennel`
--
ALTER TABLE `kennel`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=219;

--
-- AUTO_INCREMENT a táblához `kutyak`
--
ALTER TABLE `kutyak`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=549;

--
-- AUTO_INCREMENT a táblához `kutyakep`
--
ALTER TABLE `kutyakep`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT a táblához `telephely`
--
ALTER TABLE `telephely`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `udvar`
--
ALTER TABLE `udvar`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
