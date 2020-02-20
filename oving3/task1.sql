CREATE TABLE Regissør (
  RegissørID int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Navn varchar(40)
);

CREATE TABLE Film (
  FilmID int PRIMARY KEY,
  Tittel varchar(40),
  Produksjonsår int,
  RegissørID INT,
  CONSTRAINT fk_regissør
  	FOREIGN KEY (RegissørID) 
  		REFERENCES Regissør (RegissørID)
);

CREATE TABLE Sjanger (
  SjangerID int PRIMARY KEY,
  Navn varchar(40),
  Beskrivelse varchar(140)
);

CREATE TABLE SjangerForFilm (
  FilmID int,
  SjangerID int,
  PRIMARY KEY (FilmID, SjangerID),
  CONSTRAINT fk_sjanger
  	FOREIGN KEY (SjangerID)
  		REFERENCES Sjanger (SjangerID),
  CONSTRAINT fk_film
  	FOREIGN KEY (FilmID)
  		REFERENCES Film (FilmID)
  		ON DELETE CASCADE
);

CREATE TABLE Skuespiller (
  SkuespillerID int PRIMARY KEY,
  Navn varchar(40),
  Fødselsår int
);

CREATE TABLE SkuespillerIFilm (
  FilmID int,
  SkuespillerID int,
  Rolle varchar(40), 
  PRIMARY KEY (FilmID, SkuespillerID),
  CONSTRAINT fk_skuespiller
  	FOREIGN KEY (SkuespillerID)
  		REFERENCES Skuespiller (SkuespillerID)
);

INSERT INTO Regissør (RegissørID, Navn)
VALUES
(1, 'Peyton Reed'),
(2, 'Tom Shadyac');
        
INSERT INTO Skuespiller (SkuespillerID, Navn, Fødselsår)
VALUES
(1, 'Jim Carrey', 1962);

INSERT INTO Film (FilmID, Tittel, Produksjonsår, RegissørID)
VALUES
(1, 'Yes Man', 2008, 1);

INSERT INTO SkuespillerIFilm(FilmID, SkuespillerID, Rolle)
VALUES
(1, 1, 'Card');

UPDATE Skuespiller
SET Navn = 'James Eugene Carrey'
WHERE SkuespillerID = 1;

DELETE FROM Regissør 
WHERE RegissørID = 2;
