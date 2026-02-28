
-- ESTRUCTURA DE TABLAS

DROP TABLE IF EXISTS Words;
DROP TABLE IF EXISTS Groups;
DROP TABLE IF EXISTS Languages;

CREATE TABLE Languages (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    code TEXT NOT NULL UNIQUE
);

CREATE TABLE Groups (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    category TEXT NOT NULL
);

CREATE TABLE Words (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    language_id INTEGER NOT NULL,
    group_id INTEGER NOT NULL,
    text TEXT NOT NULL,
    FOREIGN KEY(language_id) REFERENCES Languages(id),
    FOREIGN KEY(group_id) REFERENCES Groups(id)
);

-- IDIOMAS

INSERT INTO Languages (id, name, code) VALUES (1, 'Español', 'es');
INSERT INTO Languages (id, name, code) VALUES (2, 'English', 'en');
INSERT INTO Languages (id, name, code) VALUES (3, 'Italiano', 'it');
INSERT INTO Languages (id, name, code) VALUES (4, 'Français', 'fr');
INSERT INTO Languages (id, name, code) VALUES (5, 'Deutsch', 'de');
INSERT INTO Languages (id, name, code) VALUES (6, 'Nederlands', 'nl');


-- VOCABULARIO

-- =========================
-- CATEGORÍA: ANIMALES (1–25)
-- =========================

-- Grupo 1
INSERT INTO Groups (id, category) VALUES (1, 'Animales');
INSERT INTO Words VALUES (NULL, 1, 1, 'perro');
INSERT INTO Words VALUES (NULL, 2, 1, 'dog');
INSERT INTO Words VALUES (NULL, 3, 1, 'cane');
INSERT INTO Words VALUES (NULL, 4, 1, 'chien');
INSERT INTO Words VALUES (NULL, 5, 1, 'Hund');
INSERT INTO Words VALUES (NULL, 6, 1, 'hond');

-- Grupo 2
INSERT INTO Groups (id, category) VALUES (2, 'Animales');
INSERT INTO Words VALUES (NULL, 1, 2, 'gato');
INSERT INTO Words VALUES (NULL, 2, 2, 'cat');
INSERT INTO Words VALUES (NULL, 3, 2, 'gatto');
INSERT INTO Words VALUES (NULL, 4, 2, 'chat');
INSERT INTO Words VALUES (NULL, 5, 2, 'Katze');
INSERT INTO Words VALUES (NULL, 6, 2, 'kat');

-- Grupo 3
INSERT INTO Groups (id, category) VALUES (3, 'Animales');
INSERT INTO Words VALUES (NULL, 1, 3, 'pájaro');
INSERT INTO Words VALUES (NULL, 2, 3, 'bird');
INSERT INTO Words VALUES (NULL, 3, 3, 'uccello');
INSERT INTO Words VALUES (NULL, 4, 3, 'oiseau');
INSERT INTO Words VALUES (NULL, 5, 3, 'Vogel');
INSERT INTO Words VALUES (NULL, 6, 3, 'vogel');

-- Grupo 4
INSERT INTO Groups (id, category) VALUES (4, 'Animales');
INSERT INTO Words VALUES (NULL, 1, 4, 'pez');
INSERT INTO Words VALUES (NULL, 2, 4, 'fish');
INSERT INTO Words VALUES (NULL, 3, 4, 'pesce');
INSERT INTO Words VALUES (NULL, 4, 4, 'poisson');
INSERT INTO Words VALUES (NULL, 5, 4, 'Fisch');
INSERT INTO Words VALUES (NULL, 6, 4, 'vis');

-- Grupo 5
INSERT INTO Groups (id, category) VALUES (5, 'Animales');
INSERT INTO Words VALUES (NULL, 1, 5, 'caballo');
INSERT INTO Words VALUES (NULL, 2, 5, 'horse');
INSERT INTO Words VALUES (NULL, 3, 5, 'cavallo');
INSERT INTO Words VALUES (NULL, 4, 5, 'cheval');
INSERT INTO Words VALUES (NULL, 5, 5, 'Pferd');
INSERT INTO Words VALUES (NULL, 6, 5, 'paard');

-- Grupo 6
INSERT INTO Groups (id, category) VALUES (6, 'Animales');
INSERT INTO Words VALUES (NULL, 1, 6, 'vaca');
INSERT INTO Words VALUES (NULL, 2, 6, 'cow');
INSERT INTO Words VALUES (NULL, 3, 6, 'mucca');
INSERT INTO Words VALUES (NULL, 4, 6, 'vache');
INSERT INTO Words VALUES (NULL, 5, 6, 'Kuh');
INSERT INTO Words VALUES (NULL, 6, 6, 'koe');

-- Grupo 7
INSERT INTO Groups (id, category) VALUES (7, 'Animales');
INSERT INTO Words VALUES (NULL, 1, 7, 'oveja');
INSERT INTO Words VALUES (NULL, 2, 7, 'sheep');
INSERT INTO Words VALUES (NULL, 3, 7, 'pecora');
INSERT INTO Words VALUES (NULL, 4, 7, 'mouton');
INSERT INTO Words VALUES (NULL, 5, 7, 'Schaf');
INSERT INTO Words VALUES (NULL, 6, 7, 'schaap');

-- Grupo 8
INSERT INTO Groups (id, category) VALUES (8, 'Animales');
INSERT INTO Words VALUES (NULL, 1, 8, 'cerdo');
INSERT INTO Words VALUES (NULL, 2, 8, 'pig');
INSERT INTO Words VALUES (NULL, 3, 8, 'maiale');
INSERT INTO Words VALUES (NULL, 4, 8, 'cochon');
INSERT INTO Words VALUES (NULL, 5, 8, 'Schwein');
INSERT INTO Words VALUES (NULL, 6, 8, 'varken');

-- Grupo 9
INSERT INTO Groups (id, category) VALUES (9, 'Animales');
INSERT INTO Words VALUES (NULL, 1, 9, 'pato');
INSERT INTO Words VALUES (NULL, 2,9, 'duck');
INSERT INTO Words VALUES (NULL, 3,9, 'anatra');
INSERT INTO Words VALUES (NULL, 4,9, 'canard');
INSERT INTO Words VALUES (NULL, 5,9, 'Ente');
INSERT INTO Words VALUES (NULL, 6,9, 'eend');

-- Grupo 10
INSERT INTO Groups (id, category) VALUES (10, 'Animales');
INSERT INTO Words VALUES (NULL, 1,10, 'pollo');
INSERT INTO Words VALUES (NULL, 2,10, 'chicken');
INSERT INTO Words VALUES (NULL, 3,10, 'pollo');
INSERT INTO Words VALUES (NULL, 4,10, 'poulet');
INSERT INTO Words VALUES (NULL, 5,10, 'Huhn');
INSERT INTO Words VALUES (NULL, 6,10, 'kip');

-- Grupo 11
INSERT INTO Groups (id, category) VALUES (11, 'Animales');
INSERT INTO Words VALUES (NULL, 1,11, 'león');
INSERT INTO Words VALUES (NULL, 2,11, 'lion');
INSERT INTO Words VALUES (NULL, 3,11, 'leone');
INSERT INTO Words VALUES (NULL, 4,11, 'lion');
INSERT INTO Words VALUES (NULL, 5,11, 'Löwe');
INSERT INTO Words VALUES (NULL, 6,11, 'leeuw');

-- Grupo 12
INSERT INTO Groups (id, category) VALUES (12, 'Animales');
INSERT INTO Words VALUES (NULL, 1,12, 'tigre');
INSERT INTO Words VALUES (NULL, 2,12, 'tiger');
INSERT INTO Words VALUES (NULL, 3,12, 'tigre');
INSERT INTO Words VALUES (NULL, 4,12, 'tigre');
INSERT INTO Words VALUES (NULL, 5,12, 'Tiger');
INSERT INTO Words VALUES (NULL, 6,12, 'tijger');

-- Grupo 13
INSERT INTO Groups (id, category) VALUES (13, 'Animales');
INSERT INTO Words VALUES (NULL, 1,13, 'elefante');
INSERT INTO Words VALUES (NULL, 2,13, 'elephant');
INSERT INTO Words VALUES (NULL, 3,13, 'elefante');
INSERT INTO Words VALUES (NULL, 4,13, 'éléphant');
INSERT INTO Words VALUES (NULL, 5,13, 'Elefant');
INSERT INTO Words VALUES (NULL, 6,13, 'olifant');

-- Grupo 14
INSERT INTO Groups (id, category) VALUES (14, 'Animales');
INSERT INTO Words VALUES (NULL, 1,14, 'oso');
INSERT INTO Words VALUES (NULL, 2,14, 'bear');
INSERT INTO Words VALUES (NULL, 3,14, 'orso');
INSERT INTO Words VALUES (NULL, 4,14, 'ours');
INSERT INTO Words VALUES (NULL, 5,14, 'Bär');
INSERT INTO Words VALUES (NULL, 6,14, 'beer');

-- Grupo 15
INSERT INTO Groups (id, category) VALUES (15, 'Animales');
INSERT INTO Words VALUES (NULL, 1,15, 'lobo');
INSERT INTO Words VALUES (NULL, 2,15, 'wolf');
INSERT INTO Words VALUES (NULL, 3,15, 'lupo');
INSERT INTO Words VALUES (NULL, 4,15, 'loup');
INSERT INTO Words VALUES (NULL, 5,15, 'Wolf');
INSERT INTO Words VALUES (NULL, 6,15, 'wolf');

-- Grupo 16
INSERT INTO Groups (id, category) VALUES (16, 'Animales');
INSERT INTO Words VALUES (NULL, 1,16, 'zorro');
INSERT INTO Words VALUES (NULL, 2,16, 'fox');
INSERT INTO Words VALUES (NULL, 3,16, 'volpe');
INSERT INTO Words VALUES (NULL, 4,16, 'renard');
INSERT INTO Words VALUES (NULL, 5,16, 'Fuchs');
INSERT INTO Words VALUES (NULL, 6,16, 'vos');

-- Grupo 17
INSERT INTO Groups (id, category) VALUES (17, 'Animales');
INSERT INTO Words VALUES (NULL, 1,17, 'conejo');
INSERT INTO Words VALUES (NULL, 2,17, 'rabbit');
INSERT INTO Words VALUES (NULL, 3,17, 'coniglio');
INSERT INTO Words VALUES (NULL, 4,17, 'lapin');
INSERT INTO Words VALUES (NULL, 5,17, 'Kaninchen');
INSERT INTO Words VALUES (NULL, 6,17, 'konijn');

-- Grupo 18
INSERT INTO Groups (id, category) VALUES (18, 'Animales');
INSERT INTO Words VALUES (NULL, 1,18, 'ratón');
INSERT INTO Words VALUES (NULL, 2,18, 'mouse');
INSERT INTO Words VALUES (NULL, 3,18, 'topo');
INSERT INTO Words VALUES (NULL, 4,18, 'souris');
INSERT INTO Words VALUES (NULL, 5,18, 'Maus');
INSERT INTO Words VALUES (NULL, 6,18, 'muis');

-- Grupo 19
INSERT INTO Groups (id, category) VALUES (19, 'Animales');
INSERT INTO Words VALUES (NULL, 1,19, 'serpiente');
INSERT INTO Words VALUES (NULL, 2,19, 'snake');
INSERT INTO Words VALUES (NULL, 3,19, 'serpente');
INSERT INTO Words VALUES (NULL, 4,19, 'serpent');
INSERT INTO Words VALUES (NULL, 5,19, 'Schlange');
INSERT INTO Words VALUES (NULL, 6,19, 'slang');

-- Grupo 20
INSERT INTO Groups (id, category) VALUES (20, 'Animales');
INSERT INTO Words VALUES (NULL, 1,20, 'rana');
INSERT INTO Words VALUES (NULL, 2,20, 'frog');
INSERT INTO Words VALUES (NULL, 3,20, 'rana');
INSERT INTO Words VALUES (NULL, 4,20, 'grenouille');
INSERT INTO Words VALUES (NULL, 5,20, 'Frosch');
INSERT INTO Words VALUES (NULL, 6,20, 'kikker');

-- Grupo 21
INSERT INTO Groups (id, category) VALUES (21, 'Animales');
INSERT INTO Words VALUES (NULL, 1,21, 'tortuga');
INSERT INTO Words VALUES (NULL, 2,21, 'turtle');
INSERT INTO Words VALUES (NULL, 3,21, 'tartaruga');
INSERT INTO Words VALUES (NULL, 4,21, 'tortue');
INSERT INTO Words VALUES (NULL, 5,21, 'Schildkröte');
INSERT INTO Words VALUES (NULL, 6,21, 'schildpad');

-- Grupo 22
INSERT INTO Groups (id, category) VALUES (22, 'Animales');
INSERT INTO Words VALUES (NULL, 1,22, 'delfín');
INSERT INTO Words VALUES (NULL, 2,22, 'dolphin');
INSERT INTO Words VALUES (NULL, 3,22, 'delfino');
INSERT INTO Words VALUES (NULL, 4,22, 'dauphin');
INSERT INTO Words VALUES (NULL, 5,22, 'Delfin');
INSERT INTO Words VALUES (NULL, 6,22, 'dolfijn');

-- Grupo 23
INSERT INTO Groups (id, category) VALUES (23, 'Animales');
INSERT INTO Words VALUES (NULL, 1,23, 'tiburón');
INSERT INTO Words VALUES (NULL, 2,23, 'shark');
INSERT INTO Words VALUES (NULL, 3,23, 'squalo');
INSERT INTO Words VALUES (NULL, 4,23, 'requin');
INSERT INTO Words VALUES (NULL, 5,23, 'Hai');
INSERT INTO Words VALUES (NULL, 6,23, 'haai');

-- Grupo 24
INSERT INTO Groups (id, category) VALUES (24, 'Animales');
INSERT INTO Words VALUES (NULL, 1,24, 'ballena');
INSERT INTO Words VALUES (NULL, 2,24, 'whale');
INSERT INTO Words VALUES (NULL, 3,24, 'balena');
INSERT INTO Words VALUES (NULL, 4,24, 'baleine');
INSERT INTO Words VALUES (NULL, 5,24, 'Wal');
INSERT INTO Words VALUES (NULL, 6,24, 'walvis');

-- Grupo 25
INSERT INTO Groups (id, category) VALUES (25, 'Animales');
INSERT INTO Words VALUES (NULL, 1,25, 'oso polar');
INSERT INTO Words VALUES (NULL, 2,25, 'polar bear');
INSERT INTO Words VALUES (NULL, 3,25, 'orso polare');
INSERT INTO Words VALUES (NULL, 4,25, 'ours polaire');
INSERT INTO Words VALUES (NULL, 5,25, 'Eisbär');
INSERT INTO Words VALUES (NULL, 6,25, 'ijsbeer');

-- =========================
-- CATEGORÍA: COLORES (26–50)
-- =========================

-- Grupo 26: rojo / red / rosso / rouge / rot / rood
INSERT INTO Groups (id, category) VALUES (26, 'Colores');
INSERT INTO Words VALUES (NULL, 1, 26, 'rojo');
INSERT INTO Words VALUES (NULL, 2, 26, 'red');
INSERT INTO Words VALUES (NULL, 3, 26, 'rosso');
INSERT INTO Words VALUES (NULL, 4, 26, 'rouge');
INSERT INTO Words VALUES (NULL, 5, 26, 'rot');
INSERT INTO Words VALUES (NULL, 6, 26, 'rood');

-- Grupo 27: azul / blue / blu / bleu / blau / blauw
INSERT INTO Groups VALUES (27, 'Colores');
INSERT INTO Words VALUES (NULL, 1, 27, 'azul');
INSERT INTO Words VALUES (NULL, 2, 27, 'blue');
INSERT INTO Words VALUES (NULL, 3, 27, 'blu');
INSERT INTO Words VALUES (NULL, 4, 27, 'bleu');
INSERT INTO Words VALUES (NULL, 5, 27, 'blau');
INSERT INTO Words VALUES (NULL, 6, 27, 'blauw');

-- Grupo 28: verde / green / verde / vert / grün / groen
INSERT INTO Groups VALUES (28, 'Colores');
INSERT INTO Words VALUES (NULL, 1, 28, 'verde');
INSERT INTO Words VALUES (NULL, 2, 28, 'green');
INSERT INTO Words VALUES (NULL, 3, 28, 'verde');
INSERT INTO Words VALUES (NULL, 4, 28, 'vert');
INSERT INTO Words VALUES (NULL, 5, 28, 'grün');
INSERT INTO Words VALUES (NULL, 6, 28, 'groen');

-- Grupo 29: amarillo / yellow / giallo / jaune / gelb / geel
INSERT INTO Groups VALUES (29, 'Colores');
INSERT INTO Words VALUES (NULL, 1, 29, 'amarillo');
INSERT INTO Words VALUES (NULL, 2, 29, 'yellow');
INSERT INTO Words VALUES (NULL, 3, 29, 'giallo');
INSERT INTO Words VALUES (NULL, 4, 29, 'jaune');
INSERT INTO Words VALUES (NULL, 5, 29, 'gelb');
INSERT INTO Words VALUES (NULL, 6, 29, 'geel');

-- Grupo 30: negro / black / nero / noir / schwarz / zwart
INSERT INTO Groups VALUES (30, 'Colores');
INSERT INTO Words VALUES (NULL, 1, 30, 'negro');
INSERT INTO Words VALUES (NULL, 2, 30, 'black');
INSERT INTO Words VALUES (NULL, 3, 30, 'nero');
INSERT INTO Words VALUES (NULL, 4, 30, 'noir');
INSERT INTO Words VALUES (NULL, 5, 30, 'schwarz');
INSERT INTO Words VALUES (NULL, 6, 30, 'zwart');

-- Grupo 31: blanco / white / bianco / blanc / weiß / wit
INSERT INTO Groups VALUES (31, 'Colores');
INSERT INTO Words VALUES (NULL, 1, 31, 'blanco');
INSERT INTO Words VALUES (NULL, 2, 31, 'white');
INSERT INTO Words VALUES (NULL, 3, 31, 'bianco');
INSERT INTO Words VALUES (NULL, 4, 31, 'blanc');
INSERT INTO Words VALUES (NULL, 5, 31, 'weiß');
INSERT INTO Words VALUES (NULL, 6, 31, 'wit');

-- Grupo 32: rosa / pink / rosa / rose / rosa / roze
INSERT INTO Groups VALUES (32, 'Colores');
INSERT INTO Words VALUES (NULL, 1, 32, 'rosa');
INSERT INTO Words VALUES (NULL, 2, 32, 'pink');
INSERT INTO Words VALUES (NULL, 3, 32, 'rosa');
INSERT INTO Words VALUES (NULL, 4, 32, 'rose');
INSERT INTO Words VALUES (NULL, 5, 32, 'rosa');
INSERT INTO Words VALUES (NULL, 6, 32, 'roze');

-- Grupo 33: gris / grey / grigio / gris / grau / grijs
INSERT INTO Groups VALUES (33, 'Colores');
INSERT INTO Words VALUES (NULL, 1, 33, 'gris');
INSERT INTO Words VALUES (NULL, 2, 33, 'grey');
INSERT INTO Words VALUES (NULL, 3, 33, 'grigio');
INSERT INTO Words VALUES (NULL, 4, 33, 'gris');
INSERT INTO Words VALUES (NULL, 5, 33, 'grau');
INSERT INTO Words VALUES (NULL, 6, 33, 'grijs');

-- Grupo 34: marrón / brown / marrone / marron / braun / bruin
INSERT INTO Groups VALUES (34, 'Colores');
INSERT INTO Words VALUES (NULL, 1, 34, 'marrón');
INSERT INTO Words VALUES (NULL, 2, 34, 'brown');
INSERT INTO Words VALUES (NULL, 3, 34, 'marrone');
INSERT INTO Words VALUES (NULL, 4, 34, 'marron');
INSERT INTO Words VALUES (NULL, 5, 34, 'braun');
INSERT INTO Words VALUES (NULL, 6, 34, 'bruin');

-- Grupo 35: morado / purple / viola / violet / lila / paars
INSERT INTO Groups VALUES (35, 'Colores');
INSERT INTO Words VALUES (NULL, 1, 35, 'morado');
INSERT INTO Words VALUES (NULL, 2, 35, 'purple');
INSERT INTO Words VALUES (NULL, 3, 35, 'viola');
INSERT INTO Words VALUES (NULL, 4, 35, 'violet');
INSERT INTO Words VALUES (NULL, 5, 35, 'lila');
INSERT INTO Words VALUES (NULL, 6, 35, 'paars');

-- =========================
-- CATEGORÍA: PARTES DEL CUERPO (36–50)
-- =========================

-- Grupo 36: cabeza / head / testa / tête / Kopf / hoofd
INSERT INTO Groups (id, category) VALUES (36, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 36, 'cabeza');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 36, 'head');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 36, 'testa');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 36, 'tête');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 36, 'Kopf');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 36, 'hoofd');

-- Grupo 37: mano / hand / mano / main / Hand / hand
INSERT INTO Groups (id, category) VALUES (37, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 37, 'mano');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 37, 'hand');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 37, 'mano');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 37, 'main');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 37, 'Hand');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 37, 'hand');

-- Grupo 38: pie / foot / piede / pied / Fuß / voet
INSERT INTO Groups (id, category) VALUES (38, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 38, 'pie');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 38, 'foot');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 38, 'piede');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 38, 'pied');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 38, 'Fuß');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 38, 'voet');

-- Grupo 39: brazo / arm / braccio / bras / Arm / arm
INSERT INTO Groups (id, category) VALUES (39, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 39, 'brazo');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 39, 'arm');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 39, 'braccio');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 39, 'bras');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 39, 'Arm');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 39, 'arm');

-- Grupo 40: pierna / leg / gamba / jambe / Bein / been
INSERT INTO Groups (id, category) VALUES (40, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 40, 'pierna');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 40, 'leg');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 40, 'gamba');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 40, 'jambe');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 40, 'Bein');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 40, 'been');

-- Grupo 41: ojo / eye / occhio / oeil / Auge / oog
INSERT INTO Groups (id, category) VALUES (41, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 41, 'ojo');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 41, 'eye');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 41, 'occhio');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 41, 'oeil');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 41, 'Auge');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 41, 'oog');

-- Grupo 42: nariz / nose / naso / nez / Nase / neus
INSERT INTO Groups (id, category) VALUES (42, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 42, 'nariz');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 42, 'nose');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 42, 'naso');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 42, 'nez');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 42, 'Nase');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 42, 'neus');

-- Grupo 43: boca / mouth / bocca / bouche / Mund / mond
INSERT INTO Groups (id, category) VALUES (43, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 43, 'boca');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 43, 'mouth');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 43, 'bocca');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 43, 'bouche');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 43, 'Mund');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 43, 'mond');

-- Grupo 44: oreja / ear / orecchio / oreille / Ohr / oor
INSERT INTO Groups (id, category) VALUES (44, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 44, 'oreja');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 44, 'ear');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 44, 'orecchio');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 44, 'oreille');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 44, 'Ohr');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 44, 'oor');

-- Grupo 45: espalda / back / schiena / dos / Rücken / rug
INSERT INTO Groups (id, category) VALUES (45, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 45, 'espalda');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 45, 'back');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 45, 'schiena');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 45, 'dos');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 45, 'Rücken');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 45, 'rug');

-- Grupo 46: pecho / chest / petto / poitrine / Brust / borst
INSERT INTO Groups (id, category) VALUES (46, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 46, 'pecho');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 46, 'chest');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 46, 'petto');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 46, 'poitrine');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 46, 'Brust');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 46, 'borst');

-- Grupo 47: estómago / stomach / stomaco / estomac / Magen / maag
INSERT INTO Groups (id, category) VALUES (47, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 47, 'estómago');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 47, 'stomach');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 47, 'stomaco');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 47, 'estomac');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 47, 'Magen');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 47, 'maag');

-- Grupo 48: dedo / finger / dito / doigt / Finger / vinger
INSERT INTO Groups (id, category) VALUES (48, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 48, 'dedo');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 48, 'finger');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 48, 'dito');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 48, 'doigt');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 48, 'Finger');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 48, 'vinger');

-- Grupo 49: rodilla / knee / ginocchio / genou / Knie / knie
INSERT INTO Groups (id, category) VALUES (49, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 49, 'rodilla');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 49, 'knee');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 49, 'ginocchio');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 49, 'genou');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 49, 'Knie');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 49, 'knie');

-- Grupo 50: hombro / shoulder / spalla / épaule / Schulter / schouder
INSERT INTO Groups (id, category) VALUES (50, 'PartesDelCuerpo');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 50, 'hombro');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 50, 'shoulder');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 50, 'spalla');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 50, 'épaule');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 50, 'Schulter');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 50, 'schouder');

-- =========================
-- CATEGORÍA: OBJETOS (51–80)
-- =========================

-- Grupo 51: mesa / table / tavolo / table / Tisch / tafel
INSERT INTO Groups (id, category) VALUES (51, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 51, 'mesa');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 51, 'table');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 51, 'tavolo');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 51, 'table');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 51, 'Tisch');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 51, 'tafel');

-- Grupo 52: silla / chair / sedia / chaise / Stuhl / stoel
INSERT INTO Groups (id, category) VALUES (52, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 52, 'silla');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 52, 'chair');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 52, 'sedia');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 52, 'chaise');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 52, 'Stuhl');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 52, 'stoel');

-- Grupo 53: cama / bed / letto / lit / Bett / bed
INSERT INTO Groups (id, category) VALUES (53, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 53, 'cama');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 53, 'bed');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 53, 'letto');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 53, 'lit');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 53, 'Bett');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 53, 'bed');

-- Grupo 54: puerta / door / porta / porte / Tür / deur
INSERT INTO Groups (id, category) VALUES (54, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 54, 'puerta');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 54, 'door');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 54, 'porta');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 54, 'porte');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 54, 'Tür');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 54, 'deur');

-- Grupo 55: ventana / window / finestra / fenêtre / Fenster / raam
INSERT INTO Groups (id, category) VALUES (55, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 55, 'ventana');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 55, 'window');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 55, 'finestra');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 55, 'fenêtre');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 55, 'Fenster');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 55, 'raam');

-- Grupo 56: libro / book / libro / livre / Buch / boek
INSERT INTO Groups (id, category) VALUES (56, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 56, 'libro');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 56, 'book');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 56, 'libro');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 56, 'livre');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 56, 'Buch');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 56, 'boek');

-- Grupo 57: bolígrafo / pen / penna / stylo / Stift / pen
INSERT INTO Groups (id, category) VALUES (57, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 57, 'bolígrafo');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 57, 'pen');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 57, 'penna');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 57, 'stylo');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 57, 'Stift');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 57, 'pen');

-- Grupo 58: cuaderno / notebook / quaderno / cahier / Heft / schrift
INSERT INTO Groups (id, category) VALUES (58, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 58, 'cuaderno');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 58, 'notebook');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 58, 'quaderno');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 58, 'cahier');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 58, 'Heft');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 58, 'schrift');

-- Grupo 59: teléfono / phone / telefono / téléphone / Telefon / telefoon
INSERT INTO Groups (id, category) VALUES (59, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 59, 'teléfono');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 59, 'phone');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 59, 'telefono');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 59, 'téléphone');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 59, 'Telefon');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 59, 'telefoon');

-- Grupo 60: ordenador / computer / computer / ordinateur / Computer / computer
INSERT INTO Groups (id, category) VALUES (60, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 60, 'ordenador');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 60, 'computer');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 60, 'computer');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 60, 'ordinateur');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 60, 'Computer');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 60, 'computer');

-- Grupo 61: coche / car / auto / voiture / Auto / auto
INSERT INTO Groups (id, category) VALUES (61, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 61, 'coche');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 61, 'car');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 61, 'auto');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 61, 'voiture');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 61, 'Auto');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 61, 'auto');

-- Grupo 62: bicicleta / bicycle / bicicletta / bicyclette / Fahrrad / fiets
INSERT INTO Groups (id, category) VALUES (62, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 62, 'bicicleta');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 62, 'bicycle');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 62, 'bicicletta');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 62, 'bicyclette');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 62, 'Fahrrad');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 62, 'fiets');

-- Grupo 63: bolsa / bag / borsa / sac / Tasche / tas
INSERT INTO Groups (id, category) VALUES (63, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 63, 'bolsa');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 63, 'bag');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 63, 'borsa');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 63, 'sac');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 63, 'Tasche');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 63, 'tas');

-- Grupo 64: llave / key / chiave / clé / Schlüssel / sleutel
INSERT INTO Groups (id, category) VALUES (64, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 64, 'llave');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 64, 'key');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 64, 'chiave');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 64, 'clé');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 64, 'Schlüssel');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 64, 'sleutel');

-- Grupo 65: reloj / watch / orologio / montre / Uhr / horloge
INSERT INTO Groups (id, category) VALUES (65, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 65, 'reloj');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 65, 'watch');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 65, 'orologio');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 65, 'montre');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 65, 'Uhr');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 65, 'horloge');

-- Grupo 66: vaso / glass / bicchiere / verre / Glas / glas
INSERT INTO Groups (id, category) VALUES (66, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 66, 'vaso');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 66, 'glass');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 66, 'bicchiere');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 66, 'verre');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 66, 'Glas');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 66, 'glas');

-- Grupo 67: taza / cup / tazza / tasse / Tasse / kopje
INSERT INTO Groups (id, category) VALUES (67, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 67, 'taza');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 67, 'cup');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 67, 'tazza');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 67, 'tasse');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 67, 'Tasse');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 67, 'kopje');

-- Grupo 68: plato / plate / piatto / assiette / Teller / bord
INSERT INTO Groups (id, category) VALUES (68, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 68, 'plato');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 68, 'plate');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 68, 'piatto');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 68, 'assiette');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 68, 'Teller');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 68, 'bord');

-- Grupo 69: cuchillo / knife / coltello / couteau / Messer / mes
INSERT INTO Groups (id, category) VALUES (69, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 69, 'cuchillo');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 69, 'knife');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 69, 'coltello');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 69, 'couteau');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 69, 'Messer');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 69, 'mes');

-- Grupo 70: tenedor / fork / forchetta / fourchette / Gabel / vork
INSERT INTO Groups (id, category) VALUES (70, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 70, 'tenedor');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 70, 'fork');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 70, 'forchetta');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 70, 'fourchette');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 70, 'Gabel');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 70, 'vork');

-- Grupo 71: cuchara / spoon / cucchiaio / cuillère / Löffel / lepel
INSERT INTO Groups (id, category) VALUES (71, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 71, 'cuchara');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 71, 'spoon');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 71, 'cucchiaio');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 71, 'cuillère');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 71, 'Löffel');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 71, 'lepel');

-- Grupo 72: televisión / television / televisione / télévision / Fernseher / televisie
INSERT INTO Groups (id, category) VALUES (72, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 72, 'televisión');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 72, 'television');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 72, 'televisione');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 72, 'télévision');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 72, 'Fernseher');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 72, 'televisie');

-- Grupo 73: lámpara / lamp / lampada / lampe / Lampe / lamp
INSERT INTO Groups (id, category) VALUES (73, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 73, 'lámpara');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 73, 'lamp');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 73, 'lampada');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 73, 'lampe');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 73, 'Lampe');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 73, 'lamp');

-- Grupo 74: mochila / backpack / zaino / sac à dos / Rucksack / rugzak
INSERT INTO Groups (id, category) VALUES (74, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 74, 'mochila');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 74, 'backpack');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 74, 'zaino');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 74, 'sac à dos');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 74, 'Rucksack');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 74, 'rugzak');

-- Grupo 75: camiseta / T-shirt / maglietta / tee-shirt / T-Shirt / T-shirt
INSERT INTO Groups (id, category) VALUES (75, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 75, 'camiseta');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 75, 'T-shirt');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 75, 'maglietta');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 75, 'tee-shirt');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 75, 'T-Shirt');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 75, 'T-shirt');

-- Grupo 76: pantalones / trousers / pantaloni / pantalon / Hose / broek
INSERT INTO Groups (id, category) VALUES (76, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 76, 'pantalones');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 76, 'trousers');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 76, 'pantaloni');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 76, 'pantalon');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 76, 'Hose');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 76, 'broek');

-- Grupo 77: zapato / shoe / scarpa / chaussure / Schuh / schoen
INSERT INTO Groups (id, category) VALUES (77, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 77, 'zapato');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 77, 'shoe');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 77, 'scarpa');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 77, 'chaussure');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 77, 'Schuh');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 77, 'schoen');

-- Grupo 78: sombrero / hat / cappello / chapeau / Hut / hoed
INSERT INTO Groups (id, category) VALUES (78, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 78, 'sombrero');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 78, 'hat');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 78, 'cappello');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 78, 'chapeau');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 78, 'Hut');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 78, 'hoed');

-- Grupo 79: pelota / ball / palla / balle / Ball / bal
INSERT INTO Groups (id, category) VALUES (79, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 79, 'pelota');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 79, 'ball');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 79, 'palla');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 79, 'balle');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 79, 'Ball');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 79, 'bal');

-- Grupo 80: botella / bottle / bottiglia / bouteille / Flasche / fles
INSERT INTO Groups (id, category) VALUES (80, 'Objetos');
INSERT INTO Words (language_id, group_id, text) VALUES (1, 80, 'botella');
INSERT INTO Words (language_id, group_id, text) VALUES (2, 80, 'bottle');
INSERT INTO Words (language_id, group_id, text) VALUES (3, 80, 'bottiglia');
INSERT INTO Words (language_id, group_id, text) VALUES (4, 80, 'bouteille');
INSERT INTO Words (language_id, group_id, text) VALUES (5, 80, 'Flasche');
INSERT INTO Words (language_id, group_id, text) VALUES (6, 80, 'fles');
