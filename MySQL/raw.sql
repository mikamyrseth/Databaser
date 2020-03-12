SELECT RelevantCompanies.FilmselskapID, RelevantCompanies.selskapsnavn, MAX(RelevantCompanies.Count) FROM
    (SELECT RelevantUtgivelse.FilmselskapID, RelevantUtgivelse.selskapsnavn, COUNT(FilmID) as Count  FROM
        (SELECT filmselskap.FilmselskapID, filmselskap.selskapsnavn, Relevant.FilmID FROM
            filmselskap
                JOIN utgivelser ON filmselskap.FilmselskapID = utgivelser.FilmSelskapID
                JOIN
            (SELECT FilmID FROM
                Film
             WHERE Film.SerieID IS NULL
            ) AS Movies
            ON utgivelser.FilmID = Movies.FilmID
                JOIN
            (SELECT * FROM
                filmikategori
             WHERE filmikategori.KategoriID = 1
            ) AS Relevant
            ON Movies.FilmID = Relevant.FilmID
        ) AS RelevantUtgivelse
     GROUP BY RelevantUtgivelse.FilmselskapID, RelevantUtgivelse.selskapsnavn
    ) AS RelevantCompanies
GROUP BY RelevantCompanies.FilmselskapID, RelevantCompanies.selskapsnavn
;

SELECT rolle FROM
    (SELECT KreatørID FROM
        Kreatør
     WHERE KreatørID = 2
    ) AS riktigkreatør
        JOIN
    SkuespillerIFilm
    ON riktigkreatør.KreatørID = SkuespillerIFilm.KreatørID
;

SELECT filmTittel FROM
    (
        SELECT FilmID FROM SkuespillerIFilm WHERE KreatørID = 2
    ) as riktigkreatør
    JOIN Film ON Film.FilmID = riktigkreatør.FilmID
;