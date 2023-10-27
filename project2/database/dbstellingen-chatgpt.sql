-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 24 okt 2023 om 15:10
-- Serverversie: 10.4.27-MariaDB
-- PHP-versie: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbstellingen`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `partijen`
--

CREATE TABLE `partijen` (
  `id` int(11) NOT NULL,
  `naam` varchar(50) NOT NULL,
  `url` varchar(200) NOT NULL,
  `info_tekst` text NOT NULL,
  `partij_logo` longblob DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Gegevens worden geëxporteerd voor tabel `partijen`
--

INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(1, 'VVD', 'https://www.vvd.nl', 'Ruimte geven. Grenzen stellen. De rust terugbrengen. Zodat we straks weer trots tegen elkaar zeggen: we hebben het goed voor elkaar in Nederland.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f000000017352474200aece1ce90000000467414d410000b18f0bfc6105000000097048597300000ec300000ec301c76fa8640000ffa549444154785eecfd77b42dd97dd877ee50e19c9b5fee8c0688308c12a3452a2cc9e4122591a2244b9a7f6666cd9ae5f96f663cd95e0a2461821a8f6d7a24d38a23c96b4c4b36454aa22866090441822000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(2, 'PVV', 'https://www.pvv.nl', 'Geert Wilders is oprichter, fractievoorzitter, lijsttrekker en boegbeeld van de PVV tegelijk. De zwaarst beveiligde man van Nederland staat bekend om felle uitspraken, liefst verstuurd per tweet, en ontvangt hiervoor zowel haat als bewondering. Waar staat de PVV voor?', 0x89504e470d0a1a0a0000000d49484452000005aa0000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(3, 'CDA', 'https://www.cda.nl', 'Het CDA wil recht doen. Recht doen is onze hoopvolle agenda voor heel Nederland. Recht doen gaat over een fatsoenlijk land, waar we omzien naar elkaar en voor elkaar zorgen. Waar mensen zich thuis voelen en onderdeel zijn van een sterke gemeenschap. Waar respect en normen en waarden niet ouderwets zijn, maar gekoesterd worden.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a008020093d04a84270000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(4, 'D66', 'https://d66.nl', 'Wij willen dat onze samenleving in hoog tempo klimaatneutraal wordt. De grootste inspanningen vragen we van de grootste vervuilers, maar het gaat alleen lukken als iedereen meedoet. Daarvoor moeten we zorgen dat iedereen ook mee kán doen en dat iedereen mee wíl doen. Daarom moet het klimaatbeleid ambitieus en rechtvaardig zijn.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f000000017352474200aecefed1fca946e0000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(5, 'GroenLinks', 'https://groenlinks.nl/home', 'GroenLinks is dé klimaatpartij van Nederland. Al 34 jaar lopen wij voorop in de strijd voor een groen en duurzaam Nederland. Steun jij onze missie? Word lid. Hoe groter onze partij, hoe meer we samen voor elkaar krijgen voor het klimaat!', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c203d0507000000103d0507000000103d0507000000103d0507000000103d0507000000103d0507000000103d050700000010b9dd76fbff003167b723efce74ea0000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(6, 'SP', 'https://www.sp.nl', 'We spannen ons in voor een betrokken, open en vrije samenleving waarin solidariteit voorop staat. Vooruitgang begint bij het bestrijden van achterstand. We kiezen voor schone energie en een gezond milieu voor mens en dier.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f000000017352474200aece1ce900000780207000000109ec00100000084277000000000e1091c00000040780207000000109ec00100000084277000000000e1091c00000040780207000000109ec0010000000457a9fc7ff080df1d1d9a0a7a0000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(7, 'PvdA', 'https://www.pvda.nl', 'We vinden dat werk meer moet opleveren dan het eigen vermogen. Je zou van een minimumloon normaal moeten kunnen leven, zonder wakker te liggen om geldzorgen. Daarom stellen wij een loon voor van minstens 14 euro per uur.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f0000000173524742838000000000040ec1170000000000080d823e0000000000000b147c0010000000000628f80030000000000c49ca2fc7f0369c696788212f40000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(8, 'ChristenUnie', 'https://www.christenunie.nl', 'Het Bijbelboek Spreuken spreekt over ouderen als volgt: \"De ouderdom is een prachtige kroon, je vindt hem op de weg van de rechtvaardigheid.\"', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f000000017352474204d64ca5a65ffe30000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(9, 'Partij voor de Dieren', 'https://www.partijvoordedieren.nl', 'Met dit programma willen we het leven van mens, dier en natuur beschermen én verbeteren. We richten ons op de kernthema’s dierenrechten, natuur, milieu. En op de oorzaken en effecten van de klimaatcrisis, die steeds meer zichtbaar en voelbaar worden.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f000000017352474200aece1ce90000000467414d4ce0902449922449bde706872449922449ea3d3738244992244952efb9c12149922449927acf0d0e4992244992d4739ffad4ff0f58a47a156b69351d0000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(11, 'SGP', 'https://sgp.nl/home', 'Wij staan voor politiek bij een open Bijbel. Dat is het fundament van ons werk. Dit wijst richting en stelt grenzen. \r\nGod heeft de wereld volmaakt geschapen, maar doordat wij ons van Hem afgekeerd hebben, leven we nu in gebrokenheid waar we steeds pijnlijk mee geconfronteerd worden. In de wetenschap dat God regeert, willen we onze verantwoordelijkheid nemen. We willen bijdragen aan een menswaardig leven voor iedereen, elkaar dienen en recht doen aan alle mensen. ﻿', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f000000017352474200aece1ce900000004671c00000040e9093800000080d2137000000000a527e0000000004a4fc001000000949e8003000000283d0107000000507a020e000000a0f4041c00000040c985f0ff03de568f49dad069830000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(13, 'FVD', 'https://fvd.nl', 'Waar staat FVD als het gaat om de boeren, immigratie of de EU bijvoorbeeld? Bekijk hieronder onze plannen voor Nederland - en onze visie per deelonderwerp.Waar staat FVD als het gaat om de boeren, immigratie of de EU bijvoorbeeld? Bekijk hieronder onze plannen voor Nederland - en onze visie per deelonderwerp.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f000000017352474200aece1ce90000000467414d41000b227e000000000b227e000000000b227e000000000b227e000000000b227e000000000323799fcffd707489e04e8c1380000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(15, 'JA21', 'https://ja21.nl', 'Energie is voor veel Nederlandse huishoudens een luxeproduct geworden. De energierekening moet per direct omlaag door belastingen fors te verlagen. Fossiele brandstoffen zijn onmisbaar voor een betaalbare en betrouwbare energievoorziening. We steunen innovatie, maar zeggen resoluut nee tegen klimaatdwang voor burgers en ondernemers. Het besluit om moderne kolencentrales te sluiten, moet worden teruggedraaid. Elektriciteit wekken we niet meer op met landschapsvervuilende en onrendabele windturbines en zonnevlaktes, maar met kernenergie.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f000000017352474200aece1c00000000000ad47c00100000000005a8f80030000000000b41e01070000000000683d020e0000000000d07a041c0000000000a0e544fe7f06ddfad65c6cec720000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(17, 'Volt', 'https://voltnederland.org', 'Het is tijd om noodzakelijke keuzes te maken. We rennen van crisis naar crisis. Klimaat, wonen, toenemende armoede, stikstof, migratie – ze hebben allemaal één ding gemeen: het zijn crises geworden omdat keuzes jarenlang vooruitgeschoven zijn.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f000000017352474200ae407000000d07b121c00000040ef497000000000bd27c101000000f49e0407000000d07b121c00000040ef497000000000bd27c101000000f49e0407000000d07b121c00000040ef497000000000bd27c101000000f49e0407000000d07b121c00000040ef4970000000003d3718fcff01d2bc38e26e836cf00000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(19, 'Piratenpartij', 'https://piratenpartij.nl', 'We gebruiken de mogelijkheden van digitalisering (bijv. ten behoeve van democratisering en kennisdeling) en houden rekening met de risico’s ervan (zoals privacyschending of monopolievorming). Alle mensen bepalen zelf hoe ze leven, maar we houden wel rekening met elkaar. Piraten zijn vrijheidslievend, autonoom en verwerpen blinde gehoorzaamheid. Vrije toegang tot kennis is dan ook essentieel voor zelfbeschikking, vrijheid van meningsuiting en om goede inhoudelijke beslissingen te kunnen nemen.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a008030000009f7e14fa000000017352474200aece1ce9288aa2288aa2288aa2288aa2288aa2288aa2288aa2288aa2288aa2288aa2288a526bd86cff0f550fd3700d96ba0b0000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(20, 'LP', 'https://stemlp.nl', 'De LP gelooft in een open samenleving, waarin iedereen de vrijheid heeft zijn eigen toekomst na te streven, zonder beperkingen door overbodige regels van de overheid. De LP wil dat de overheid teruggaat naar haar kerntaken: een overheid ontdaan van allerlei kostbare en onnodige bestuurslagen. Lees hier hoe we dat willen bereiken.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f000000017352474200aece1ce90000000467414d410000b18f0bfc60103c020e0000000000103c020e0000000000103c020e0000000000103c020e0000000000103c020e0000000000103c020e0000000000103c020e0000000000103c020e0000000000103c020e0000000000103c020e0000000000103c020e0000000000103c020e0000000000103863fe7f441275c2d27db5200000000049454e44ae426082);
INSERT INTO `partijen` (`id`, `naam`, `url`, `info_tekst`, `partij_logo`) VALUES
(22, 'BBB', 'https://boerburgerbeweging.nl', 'De vier kernwaarden van BBB geven onze grondbeginselen weer. Ze beschrijven waar wij in geloven en zijn onze drijfveer voor alles wat wij samen willen bereiken.\r\nNoaberschap en de gulden regel, authentiek en professioneel.', 0x89504e470d0a1a0a0000000d49484452000005a0000005a0080200000027c2739f000000017352474200aece1ce90000000467414d410000bf82030000000000488f82030000000000488f82030000000000488f82030000000000488f82030000000000488f82030000000000488f82030000000000488f82030000000000488f82030000000000488f82030000000000488f82030000000000482e2fefff01520b064a8020ee8e0000000049454e44ae426082);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `stellingen`
--

CREATE TABLE `stellingen` (
  `id` int(11) NOT NULL,
  `stelling` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `username` varchar(15) NOT NULL,
  `password` varchar(20) NOT NULL,
  `email` varchar(30) NOT NULL,
  `profile_image` longblob DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Gegevens worden geëxporteerd voor tabel `user`
--

INSERT INTO `user` (`id`, `username`, `password`, `email`, `profile_image`) VALUES
(3, 'AnatomyKing', '123kip', 'Gracjan@gmail.com', 0xffd8ffe000104a46494600010102002500250000ffdb00430006040506050406060506070706080a100a0a09090a140e0f0c10171418181a3cc7a7ffd9);
INSERT INTO `user` (`id`, `username`, `password`, `email`, `profile_image`) VALUES
(4, 'Gracjan', '123kip', 'hallo@gmail.com', 0x89504e470d0a1a0a0000000d49484452000001ab000001f908060000003f5711910000000173524006b9cfb33897823a6205cc68eef32ca6294441ac800ca63ecf225288865801198d5d0d12294445ac80cc869e67712e85c888155048dff32ca6290040357efa97777ff3a77f796a75e5d99f5fddf8bf8faeae3fff21e2046c2c16ff1ff07d82443441340a0000000049454e44ae426082);
INSERT INTO `user` (`id`, `username`, `password`, `email`, `profile_image`) VALUES
(5, 'Mischa', 'gracjan', 'kakerlaky@gmail.com', 0x89504e470d0a1a0a0000000d49484452000000800000008008020000004c5cf69c0000002063303a30305e5f28d50000000049454e44ae426082);
INSERT INTO `user` (`id`, `username`, `password`, `email`, `profile_image`) VALUES
(6, 'Jaap', 'hallo', 'hallo@gmail.com', 0xffd8ffe000104a46494600010101004800480000ffdb0043000302020302020303030304030304050805050404050a070706080c0a0c0c0b0a0b0b144633b7d007aefd3663678564e77eef11d1b4748da1e330303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030303030fffd9);
INSERT INTO `user` (`id`, `username`, `password`, `email`, `profile_image`) VALUES
(7, 'Rabia', 'lol', 'Lol@gmail.com', 0x47494638396130028201f700007f4343864e4d9f4c4c8b56559a5657905d5ca95756b05e5d9f5e608c66668f6d6d9563629968669b6c6a9e706e91727e339165201f83bc04b83244aabbf4c8b0347d03f0bbc27472084ea33060d0c400fbc1d3aa9048d513fb3b3347563a1136840f98340c99040a173b7ae0908003b);

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `partijen`
--
ALTER TABLE `partijen`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `stellingen`
--
ALTER TABLE `stellingen`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `partijen`
--
ALTER TABLE `partijen`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT voor een tabel `stellingen`
--
ALTER TABLE `stellingen`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
