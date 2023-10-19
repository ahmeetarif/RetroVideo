use master;

drop database if exists RetroVideo;

CREATE DATABASE Retrovideo;
go

if not exists(select loginname from master.dbo.syslogins where name='cursist')
begin
create login cursist with password ='cursist', check_expiration=off, check_policy=off;
end;

use RetroVideo;
go

CREATE TABLE Genres (
  id int NOT NULL IDENTITY (1,1) primary key,
  naam varchar(20) NOT NULL unique
);

INSERT INTO Genres(naam) VALUES
 ('Aktiefilm'),
 ('Avontuur'),
 ('Cowboyfilm'),
 ('Erotiek'),
 ('Griezel'),
 ('Humor'),
 ('Kinderfilm'),
 ('Oorlog'),
 ('Piratenfilm'),
 ('Science fiction'),
 ('Sentimenteel'),
 ('Speelfilm'),
 ('Thriller');

CREATE TABLE Films (
  id int NOT NULL IDENTITY (1,1) primary key,
  genreId int NOT NULL,
  titel varchar(30) NOT NULL,
  voorraad int NOT NULL check(voorraad>=0),
  gereserveerd int NOT NULL check(gereserveerd>=0),
  prijs decimal(9,2) check(prijs>=0) NOT NULL,
  CONSTRAINT filmsGenres FOREIGN KEY (genreid) REFERENCES genres(id)
);

create index idxFilmsTitel on Films(titel);

INSERT INTO Films(genreId,titel,voorraad,gereserveerd,prijs) VALUES
 (2,'Raiders of the lost ark',3,0,3.5),
 (7,'Harry Potter',3,0,3),
 (11,'Love story',1,0,3),
 (4,'Two moon junction',8,0,3.5),
 (6,'Police academy',3,0,3.5),
 (3,'Once upon a time in the west',2,0,3),
 (2,'In de ban van de ring',3,0,3.5),
 (7,'Babe',2,0,3),
 (2,'Zorro',2,0,3.5),
 (6,'Hector',2,0,3.5),
 (3,'High noon',4,0,3),
 (9,'Captain blood',2,0,3),
 (2,'The last emperor',3,0,3.5),
 (12,'The deer hunter',9,0,3.5),
 (6,'The gods must be crazy',6,0,3.5),
 (13,'Silent night, deadly night',4,0,3),
 (13,'The gangs of new york',4,0,3),
 (13,'Kickboxer',4,0,3),
 (2,'Batman',12,0,3.5),
 (11,'Cramer vs Cramer',1,0,3),
 (11,'Titanic',5,0,3),
 (3,'El gringo',5,0,3),
 (11,'The graduate',3,0,3),
 (13,'The omen',5,0,3),
 (4,'Sex,lies and videotapes',0,0,3.5),
 (1,'Chicago',7,0,3),
 (7,'De smurfen',6,0,3),
 (13,'First blood',3,0,3),
 (4,'Her alibi',5,0,3.5),
 (8,'De langste dag',3,0,3.5),
 (8,'The guns of navarone',2,0,3.5),
 (2,'The revenge of jaws',6,0,3.5),
 (13,'Lock up',3,0,3),
 (5,'Hellraiser',5,0,3),
 (5,'The exorcist',2,0,3),
 (13,'Road house',5,0,3),
 (11,'Matador',5,0,3),
 (8,'Missing in action',4,0,3.5),
 (2,'Licence to kill',6,0,3.5);


CREATE TABLE Klanten(
  id int NOT NULL IDENTITY (1,1) primary key,
  familienaam varchar(30) NOT NULL,
  voornaam varchar(20) NOT NULL,
  straat varchar(30) NOT NULL,
  huisnummer varchar(5) NOT NULL,
  bus varchar(5) null,
  postcode varchar(10) NOT NULL,
  gemeente varchar(30) NOT NULL
);

create index idxKlantenFamilienaam on Klanten(familienaam);

INSERT INTO Klanten(familienaam,voornaam,straat,huisnummer,bus,postcode,gemeente) VALUES
 ('Heiremans','Inge','Koekelbergstraat','32','A','9330','Dendermonde'),
 ('Goessens','Joris','Diepeweg','1',null,'9000','Gent'),
 ('Van delsen','Lode','Kouterstraat','10',null,'9263','Bavegem'),
 ('Van den berghe','Piet','Melkerijstraat','34',null,'8900','Ieper'),
 ('Van den bosche','Christel','Heirbaan','34',null,'9311','Impe'),
 ('Verbiest','Karen','Dorpsstraat','35',null,'9000','Gent'),
 ('Boelens','Luc','Gravenstraat','23',null,'9402','Meerbeke'),
 ('Verplancken','Mia','Kempeland','3',null,'9200','Wetteren'),
 ('Meert','Sabine','Oosthoek','23',null,'9230','Melle'),
 ('Boelens','Kristel','Koekoekstraat','2',null,'9000','Gent'),
 ('De Clerq','Hilde','Molenstraat','23',null,'9140','Zele'),
 ('De Coninck','Philippe','Stationstraat','23',null,'9402','Meerbeke'),
 ('Cousaert','Nathalie','Stationstraat','234',null,'9300','Aalst'),
 ('De coninck','Kathleen','Vogelzang','34',null,'9000','Gent'),
 ('Lorez','Veronique','Beverhoekstraat','23',null,'9200','Wetteren'),
 ('Heyman','Lieve','Dendermondse stwg','112','15','9010','Gentbrugge'),
 ('Huysman','Ann','Noordlaan','12','E002','9300','Aalst'),
 ('Gevaert','Jan','Wortegemstraat','3',null,'1890','Opwijk'),
 ('Nijs','Pascal','Lindestraat','23',null,'9200','Wetteren'),
 ('Coppens','Roland','Dorp','6',null,'9411','Erondegem'),
 ('Gysels','Rita','Kasteeldreef','45',null,'9000','Gent'),
 ('Janssens','Etienne','Blikstraat','21','E','9370','Lebbeke'),
 ('Goeman','Christine','Eikelstraat','345',null,'9160','Hamme'),
 ('Van de sompel','Luc','Voermanstraat','45',null,'9170','Waasmunster'),
 ('Van de Poele','Trees','Stationstraat','11','003','9000','Gent'),
 ('Matthijs','Paul','Sticheldreef','37',null,'9140','Zele'),
 ('Lefever','Hendrik','Lijsterstraat','2',null,'9290','Berlare'),
 ('Lenaerds','Marc','Dragonderwegel','23',null,'9281','Overmere'),
 ('Lampens','Lieven','Drapstraat','45',null,'9282','Uitbergen'),
 ('Verpoest','Steven','Dammenlaan','87',null,'9200','Dendermonde');

CREATE TABLE Reservaties(
  klantId int NOT NULL,
  filmId int NOT NULL,
  reservatie datetime NOT NULL,
  PRIMARY KEY (klantId,filmId,reservatie),
  CONSTRAINT reservatiesFilms FOREIGN KEY (filmId) REFERENCES Films(id),
  CONSTRAINT reservatiesKlanten FOREIGN KEY (klantId) REFERENCES Klanten(id)
);

create user cursist for login cursist;

grant insert,select,update on films to cursist;
grant insert,select on genres to cursist;
grant insert,select on klanten to cursist;
grant insert,select on reservaties to cursist;