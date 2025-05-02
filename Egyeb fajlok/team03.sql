-- phpMyAdmin SQL Dump
-- version 5.2.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: May 02, 2025 at 05:33 PM
-- Server version: 10.11.11-MariaDB-0ubuntu0.24.04.2
-- PHP Version: 8.3.20

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `team03`
--

-- --------------------------------------------------------

--
-- Table structure for table `changelog`
--

CREATE TABLE `changelog` (
  `id` int(11) NOT NULL,
  `category` varchar(255) NOT NULL,
  `userid` int(11) NOT NULL,
  `msg` varchar(255) NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- Dumping data for table `changelog`
--

INSERT INTO `changelog` (`id`, `category`, `userid`, `msg`, `date`) VALUES
(61, 'kutya módosítva', 1, 'Buksi - átnevezve ÁÁÁÁÁÁÁÁÁ(251) módosítva admin(1) által', '2025-05-02 04:40:51'),
(62, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 04:40:55'),
(63, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 04:42:54'),
(64, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 04:42:55'),
(65, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 04:42:58'),
(66, 'udvar létrehozva', 1, 'Új udvar 5(12) létrehozva admin(1) által', '2025-05-02 04:52:56'),
(67, 'udvar létrehozva', 1, 'Új udvar 6(13) létrehozva admin(1) által', '2025-05-02 04:53:00'),
(68, 'udvar létrehozva', 1, 'Új udvar 7(14) létrehozva admin(1) által', '2025-05-02 04:53:03'),
(69, 'udvar törölve', 1, 'Új udvar 7(14) törölve admin(1) által', '2025-05-02 04:53:09'),
(70, 'udvar törölve', 1, 'Új udvar 6(13) törölve admin(1) által', '2025-05-02 04:53:10'),
(71, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 04:53:13'),
(72, 'telephely módosítva', 1, 'vác(2) telephely módosítva admin(1) által', '2025-05-02 04:54:29'),
(73, 'telephely módosítva', 1, 'váci telephely(2) telephely módosítva admin(1) által', '2025-05-02 04:54:29'),
(74, 'telephely módosítva', 1, 'váci telephely(2) telephely módosítva admin(1) által', '2025-05-02 04:54:31'),
(75, 'telephely módosítva', 1, 'váci telephely(2) telephely módosítva admin(1) által', '2025-05-02 04:54:31'),
(76, 'kennel törölve', 1, 'Új udvar 4(11)-1 kennel(225) törölve admin(1) által', '2025-05-02 12:21:14'),
(77, 'kennel törölve', 1, 'Új udvar 4(11)-2 kennel(226) törölve admin(1) által', '2025-05-02 12:21:16'),
(78, 'kennel létrehozva', 1, 'Új udvar 5(12)-1 kennel(227) létrehozva admin(1) által', '2025-05-02 12:21:18'),
(79, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 12:21:34'),
(80, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 12:21:36'),
(81, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 12:21:37'),
(82, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 12:21:38'),
(83, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 12:21:40'),
(84, 'kennel törölve', 1, 'Új udvar (7)-1 kennel(219) törölve admin(1) által', '2025-05-02 12:23:30'),
(85, 'kennel törölve', 1, 'Új udvar (7)-2 kennel(220) törölve admin(1) által', '2025-05-02 12:23:31'),
(86, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 12:23:34'),
(87, 'kutya módosítva', 1, 'Buksi(251) módosítva admin(1) által', '2025-05-02 12:23:38'),
(88, 'udvar létrehozva', 1, 'Új udvar 6(15) létrehozva admin(1) által', '2025-05-02 12:24:28'),
(89, 'udvar létrehozva', 1, 'Új udvar 7(16) létrehozva admin(1) által', '2025-05-02 12:25:00'),
(90, 'dolgozó módosítva', 1, 'hatalmas dolgozo(3) módosítva admin(1) által', '2025-05-02 12:25:23'),
(91, 'dolgozó módosítva', 1, 'hatalmas dolgozo(3) módosítva admin(1) által', '2025-05-02 12:25:26'),
(92, 'dolgozó módosítva', 1, 'hatalmas dolgozo(3) módosítva admin(1) által', '2025-05-02 12:25:26'),
(93, 'dolgozó módosítva', 1, 'hatalmas dolgozo(3) módosítva admin(1) által', '2025-05-02 12:25:26'),
(94, 'udvar létrehozva', 1, 'Új udvar 8(17) létrehozva admin(1) által', '2025-05-02 12:25:31'),
(95, 'udvar létrehozva', 1, 'Új udvar 9(18) létrehozva admin(1) által', '2025-05-02 12:26:05');

-- --------------------------------------------------------

--
-- Table structure for table `kennel`
--

CREATE TABLE `kennel` (
  `id` int(11) NOT NULL,
  `udvarid` int(11) NOT NULL,
  `kennelszam` int(11) NOT NULL,
  `kutyak` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- Dumping data for table `kennel`
--

INSERT INTO `kennel` (`id`, `udvarid`, `kennelszam`, `kutyak`) VALUES
(221, 8, 1, ''),
(222, 8, 2, ''),
(223, 8, 3, ''),
(224, 8, 4, ''),
(227, 12, 1, '251;251');

-- --------------------------------------------------------

--
-- Table structure for table `kutyak`
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
-- Dumping data for table `kutyak`
--

INSERT INTO `kutyak` (`id`, `regszam`, `nev`, `chipszam`, `ivar`, `meret`, `szuletes`, `bekerules`, `ivaros`, `telephely`, `foglalt`, `kennel`, `indexkepid`, `status`, `visible`) VALUES
(251, 71349, 'Buksi', '72745150169', 'kan', 'kistestű', '2014-01-26', '2014-08-21', 'ivaros', 'vác', 0, 227, 24, 'Nálunk van', 1);

-- --------------------------------------------------------

--
-- Table structure for table `kutyakep`
--

CREATE TABLE `kutyakep` (
  `id` int(11) NOT NULL,
  `kutyaid` int(11) NOT NULL,
  `nev` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- Dumping data for table `kutyakep`
--

INSERT INTO `kutyakep` (`id`, `kutyaid`, `nev`) VALUES
(24, 251, '251-IMG_20250315_211025.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `menhely`
--

CREATE TABLE `menhely` (
  `nev` varchar(255) NOT NULL,
  `hely` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `telefonszam` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- Dumping data for table `menhely`
--

INSERT INTO `menhely` (`nev`, `hely`, `email`, `telefonszam`) VALUES
('Pawdmin', 'Budapest', 'menhely@gmail.com', '06302453472');

-- --------------------------------------------------------

--
-- Table structure for table `telephely`
--

CREATE TABLE `telephely` (
  `id` int(11) NOT NULL,
  `nev` varchar(255) NOT NULL,
  `hely` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `telefonszam` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- Dumping data for table `telephely`
--

INSERT INTO `telephely` (`id`, `nev`, `hely`, `email`, `telefonszam`) VALUES
(1, 'Budapest', 'Budapest', 'bp@menhely.com', '0630453324'),
(2, 'váci telephely', 'vác', 'vac@gmail.com', '0784324324');

-- --------------------------------------------------------

--
-- Table structure for table `udvar`
--

CREATE TABLE `udvar` (
  `id` int(11) NOT NULL,
  `telephelyid` int(11) NOT NULL,
  `udvarnev` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- Dumping data for table `udvar`
--

INSERT INTO `udvar` (`id`, `telephelyid`, `udvarnev`) VALUES
(1, 0, 'elöli'),
(2, 0, 'kölyökudvar'),
(3, 0, 'dühöngő'),
(4, 0, 'parkoló'),
(5, 0, 'hátsó'),
(6, 0, 'nincs udvar'),
(7, 1, 'Új udvar '),
(8, 1, 'Új udvar 1'),
(9, 2, 'Új udvar 2'),
(10, 2, 'Új udvar 3'),
(11, 2, 'Új udvar 4'),
(12, 2, 'Új udvar 5'),
(15, 2, 'Új udvar 6'),
(16, 2, 'Új udvar 7'),
(17, 2, 'Új udvar 8'),
(18, 2, 'Új udvar 9');

-- --------------------------------------------------------

--
-- Table structure for table `users`
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
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `telefonszam`, `telephely`, `admin`) VALUES
(1, 'admin', '$2y$10$UyAPWwwvV1IPAVgMTQn0s.wkbTy7nt5WPnwYOCC9fxNfsl52xc2uC', '0645324452', 'Budapest', 0),
(3, 'hatalmas dolgozo', '$2y$10$fofQvYVde/6shFIbBauKTO1D32wNw8kTTN6nVw5OYdajOJ6HD56yG', '575447', 'váci telephely', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `changelog`
--
ALTER TABLE `changelog`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `kennel`
--
ALTER TABLE `kennel`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `kutyak`
--
ALTER TABLE `kutyak`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `kutyakep`
--
ALTER TABLE `kutyakep`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `telephely`
--
ALTER TABLE `telephely`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `udvar`
--
ALTER TABLE `udvar`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `changelog`
--
ALTER TABLE `changelog`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=96;

--
-- AUTO_INCREMENT for table `kennel`
--
ALTER TABLE `kennel`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=228;

--
-- AUTO_INCREMENT for table `kutyak`
--
ALTER TABLE `kutyak`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=549;

--
-- AUTO_INCREMENT for table `kutyakep`
--
ALTER TABLE `kutyakep`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `telephely`
--
ALTER TABLE `telephely`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `udvar`
--
ALTER TABLE `udvar`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
