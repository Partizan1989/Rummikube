--
-- Скрипт сгенерирован Devart dbForge Studio for MySQL, Версия 7.2.53.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/mysql/studio
-- Дата скрипта: 09.09.2017 1:06:30
-- Версия сервера: 5.5.23
-- Версия клиента: 4.1
--


-- 
-- Отключение внешних ключей
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Установить режим SQL (SQL mode)
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Установка базы данных по умолчанию
--
USE rummicube;

--
-- Описание для таблицы errors
--
DROP TABLE IF EXISTS errors;
CREATE TABLE errors (
  id INT(11) NOT NULL AUTO_INCREMENT,
  RU VARCHAR(255) DEFAULT NULL,
  phraseId INT(11) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB
AUTO_INCREMENT = 1001
AVG_ROW_LENGTH = 5461
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы languages
--
DROP TABLE IF EXISTS languages;
CREATE TABLE languages (
  id INT(11) NOT NULL AUTO_INCREMENT,
  name MEDIUMTEXT DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB
AUTO_INCREMENT = 3
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы phrases
--
DROP TABLE IF EXISTS phrases;
CREATE TABLE phrases (
  id INT(11) NOT NULL AUTO_INCREMENT,
  phrase MEDIUMTEXT DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB
AUTO_INCREMENT = 4
AVG_ROW_LENGTH = 5461
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы players
--
DROP TABLE IF EXISTS players;
CREATE TABLE players (
  Id INT(11) NOT NULL AUTO_INCREMENT,
  Login MEDIUMTEXT NOT NULL,
  PassMD5 VARCHAR(255) NOT NULL,
  Wins_cnt INT(11) DEFAULT 0,
  Loss_cnt INT(11) DEFAULT 0,
  Balance BIGINT(20) DEFAULT 0,
  Dt_create DATETIME NOT NULL,
  Block TINYINT(1) DEFAULT 0,
  PRIMARY KEY (Id)
)
ENGINE = INNODB
AUTO_INCREMENT = 2
AVG_ROW_LENGTH = 16384
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы translate_text
--
DROP TABLE IF EXISTS translate_text;
CREATE TABLE translate_text (
  id INT(11) NOT NULL AUTO_INCREMENT,
  textId INT(11) DEFAULT NULL,
  langId INT(11) DEFAULT NULL,
  translate MEDIUMTEXT DEFAULT NULL,
  PRIMARY KEY (id),
  CONSTRAINT FK_message_text_langId FOREIGN KEY (langId)
    REFERENCES languages(id) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT FK_translate_text_textId FOREIGN KEY (textId)
    REFERENCES phrases(id) ON DELETE CASCADE ON UPDATE CASCADE
)
ENGINE = INNODB
AUTO_INCREMENT = 7
AVG_ROW_LENGTH = 2730
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

DELIMITER $$

--
-- Описание для процедуры check_credentials
--
DROP PROCEDURE IF EXISTS check_credentials$$
CREATE DEFINER = 'root'@'localhost'
PROCEDURE check_credentials(IN arg_login VARCHAR(255), IN arg_passMd5 VARCHAR(255))
BEGIN
  SELECT * FROM players p WHERE p.Login = arg_login AND p.PassMD5 = arg_passMd5;
END
$$

--
-- Описание для процедуры create_player
--
DROP PROCEDURE IF EXISTS create_player$$
CREATE DEFINER = 'root'@'localhost'
PROCEDURE create_player(IN arg_login VARCHAR(255), IN arg_passMD5 VARCHAR(255))
BEGIN
  DECLARE Login varchar(255);
  DECLARE ErrorId int(11);
  DECLARE Id int(11);
  DECLARE Msg varchar(255);
    set ErrorId = 1;
    set Msg = 'Успешно';
    set Id = 0;
  SELECT p.Login INTO Login FROM players p WHERE p.Login = arg_login;
  IF login IS NOT NULL THEN
    set ErrorId = 2;
    set Msg = 'Login уже занят';
  ELSE
    INSERT INTO players(Login,PassMD5, Dt_create) VALUES (arg_login, arg_passMD5, NOW());
    set id = @@identity;
  END IF;
  SELECT ErrorId, Msg, Id;
  
END
$$

--
-- Описание для процедуры Get_Error_Transate
--
DROP PROCEDURE IF EXISTS Get_Error_Transate$$
CREATE DEFINER = 'root'@'localhost'
PROCEDURE Get_Error_Transate()
BEGIN
  SELECT e.id AS ErrorId, tt.textId, tt.langId, tt.translate FROM errors e LEFT JOIN translate_text tt  ON e.phraseId = tt.textId;
END
$$

DELIMITER ;

-- 
-- Вывод данных для таблицы errors
--
INSERT INTO errors VALUES
(1, 'Успешно', 1),
(2, 'Login уже занят', 2),
(1000, 'Ошибка базы', 3);

-- 
-- Вывод данных для таблицы languages
--
INSERT INTO languages VALUES
(1, 'Русский'),
(2, 'Английский');

-- 
-- Вывод данных для таблицы phrases
--
INSERT INTO phrases VALUES
(1, 'Успешно'),
(2, 'Логин занят'),
(3, 'Ошибка базы данных');

-- 
-- Вывод данных для таблицы players
--
INSERT INTO players VALUES
(1, 'dev', '698d51a19d8a121ce581499d7b701668', 0, 0, 0, '2017-02-08 22:00:00', 0);

-- 
-- Вывод данных для таблицы translate_text
--
INSERT INTO translate_text VALUES
(1, 1, 1, 'Успешно'),
(2, 1, 2, 'Successfully'),
(3, 2, 1, 'Логин занят'),
(4, 2, 2, 'Login is busy'),
(5, 3, 1, 'Ошибка базы данных'),
(6, 3, 2, 'Error database');

-- 
-- Восстановить предыдущий режим SQL (SQL mode)
-- 
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Включение внешних ключей
-- 
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;