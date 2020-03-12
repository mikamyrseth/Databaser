CREATE TABLE Land (
    LandID INT AUTO_INCREMENT PRIMARY KEY,
    landNavn VARCHAR(40)
);

CREATE TABLE Kreatør (
    KreatørID INT AUTO_INCREMENT PRIMARY KEY,
    fødeslsår SMALLINT,
    kreatørNavn VARCHAR(40),
    LandID INT NOT NULL,
    CONSTRAINT fk_fødselsland
        FOREIGN KEY (LandID)
            REFERENCES Land (LandID)
            ON DELETE CASCADE
);

CREATE TABLE Filmselskap (
    FilmselskapID INT AUTO_INCREMENT PRIMARY KEY,
    selskapsnavn VARCHAR(40),
    LandID INT NOT NULL,
    CONSTRAINT fk_filmselskap
        FOREIGN KEY (LandID)
            REFERENCES Land (LandID)
            ON DELETE CASCADE
);

CREATE TABLE Kategori (
    KategoriID INT AUTO_INCREMENT PRIMARY KEY,
    kategoriNavn VARCHAR(40)
);

CREATE TABLE Serie (
    SerieID INT AUTO_INCREMENT PRIMARY KEY,
    seriebeskrivelse VARCHAR(140),
    serieTittel VARCHAR(40)
);

CREATE TABLE Stykke (
    StykkeID INT AUTO_INCREMENT PRIMARY KEY,
    stykkeTittel VARCHAR(40)
);

CREATE TABLE KomponistIStykke (
    KreatørID INT,
    StykkeID INT,
    PRIMARY KEY (KreatørID, StykkeID),
    CONSTRAINT fk_komponist
        FOREIGN KEY (KreatørID)
            REFERENCES Kreatør (KreatørID)
            ON DELETE CASCADE,
    CONSTRAINT fk_komponist_stykke
        FOREIGN KEY (StykkeID)
            REFERENCES Stykke (StykkeID)
            ON DELETE CASCADE
);

CREATE TABLE UtøverIStykke (
    KreatørID INT,
    StykkeID INT,
    PRIMARY KEY (KreatørID, StykkeID),
    CONSTRAINT fk_utøver
        FOREIGN KEY (KreatørID)
            REFERENCES Kreatør (KreatørID)
            ON DELETE CASCADE,
    CONSTRAINT fk_utøver_stykke
        FOREIGN KEY (StykkeID)
            REFERENCES Stykke (StykkeID)
            ON DELETE CASCADE
);

CREATE TABLE Sesong (
    SerieID INT,
    Sesongnummer TINYINT,
    sesongbeskrivelse VARCHAR(140),
    sesongTittel VARCHAR(40),
    PRIMARY KEY (SerieID, Sesongnummer)
);

CREATE TABLE Film (
    FilmID INT AUTO_INCREMENT PRIMARY KEY,
    filmTittel VARCHAR(40),
    utgivelesår SMALLINT,
    lengde MEDIUMINT,
    filmbeskrivelse VARCHAR(140),
    SerieID INT,
    Sesongnummer TINYINT,
    CONSTRAINT fk_filmISerie
        FOREIGN KEY (SerieID, Sesongnummer)
            REFERENCES Sesong (SerieID, Sesongnummer)
            ON DELETE CASCADE
);

CREATE TABLE SkuespillerIFilm (
    FilmID INT,
    KreatørID INT,
    rolle VARCHAR(40),
    PRIMARY KEY (KreatørID, FilmID),
    CONSTRAINT fk_skuespiller_film
        FOREIGN KEY (FilmID)
            REFERENCES Film (FilmID)
            ON DELETE CASCADE,
    CONSTRAINT fk_skuespiller
        FOREIGN KEY (KreatørID)
            REFERENCES Kreatør (KreatørID)
            ON DELETE CASCADE
);

CREATE TABLE ManusforfatterIFilm (
    FilmID INT,
    KreatørID INT,
    PRIMARY KEY (FilmID, KreatørID),
    CONSTRAINT fk_manus_film
        FOREIGN KEY (FilmID)
            REFERENCES Film (FilmID)
            ON DELETE CASCADE,
    CONSTRAINT fk_manus
        FOREIGN KEY (KreatørID)
            REFERENCES Kreatør (KreatørID)
            ON DELETE CASCADE
);

CREATE TABLE RegissørIFilm (
    FilmID INT,
    KreatørID INT,
    PRIMARY KEY (FilmID, KreatørID),
    CONSTRAINT fk_regissør_film
        FOREIGN KEY (FilmID)
            REFERENCES Film (FilmID)
            ON DELETE CASCADE,
    CONSTRAINT fk_regissør
        FOREIGN KEY (KreatørID)
            REFERENCES Kreatør (KreatørID)
            ON DELETE CASCADE
);

CREATE TABLE FilmIKategori (
    FilmID INT,
    KategoriID INT,
    PRIMARY KEY (FilmID, KategoriID),
    CONSTRAINT fk_filmKategoriFilm
        FOREIGN KEY (FilmID)
            REFERENCES Film (FilmID)
            ON DELETE CASCADE,
    CONSTRAINT fk_filmKategoriKategori
        FOREIGN KEY (KategoriID)
            REFERENCES Kategori (KategoriID)
            ON DELETE CASCADE
);

CREATE TABLE Seer (
    SeerID INT AUTO_INCREMENT PRIMARY KEY,
    epost VARCHAR(40) NOT NULL UNIQUE
);

CREATE TABLE Filmanmeldelse (
    FilmID INT,
    SeerID INT,
    filmKommentar VARCHAR(140),
    filmVurdering TINYINT,
    PRIMARY KEY (FilmID, SeerID),
    CONSTRAINT fk_anmeldelse_film
        FOREIGN KEY (FilmID)
            REFERENCES Film (FilmID)
            ON DELETE CASCADE,
    CONSTRAINT fk_filmanmeldelse_seer
        FOREIGN KEY (SeerID)
            REFERENCES Seer (SeerID)
            ON DELETE CASCADE
);

CREATE TABLE Serieanmeldelse (
    SerieID INT,
    SeerID INT,
    serieKommentar VARCHAR(140),
    serieVurdering TINYINT,
    PRIMARY KEY (SerieID, SeerID),
    CONSTRAINT fk_serieanmeldelse_film
        FOREIGN KEY (SerieID)
            REFERENCES Serie (SerieID)
            ON DELETE CASCADE,
    CONSTRAINT fk_serieanmeldelse_seer
        FOREIGN KEY (SeerID)
            REFERENCES Seer (SeerID)
            ON DELETE CASCADE
);

CREATE TABLE Utgivelser (
    FilmSelskapID INT,
    FilmID INT,
    PRIMARY KEY (FilmSelskapID, FilmID),
    CONSTRAINT fk_utgivelser_filmselskap
        FOREIGN KEY (FilmSelskapID)
            REFERENCES Filmselskap (FilmselskapID)
            ON DELETE CASCADE,
    CONSTRAINT fk_utgivelser_film
        FOREIGN KEY (FilmID)
            REFERENCES Film (FilmID)
            ON DELETE CASCADE
)
