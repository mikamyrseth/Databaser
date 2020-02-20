#a
SELECT FilmID, Tittel, Produksjonsår, RegissørID FROM film;

#b
SELECT Navn FROM skuespiller
    WHERE 1960 < Fødselsår;

#c
SELECT Navn FROM skuespiller
    WHERE 1979 < Fødselsår AND Fødselsår < 1990
    ORDER BY Navn;

#d
SELECT Tittel, Rolle FROM
    (film JOIN
        (SELECT FilmID, Rolle FROM
            (skuespillerifilm JOIN
                (SELECT SkuespillerID FROM skuespiller
                    WHERE Navn = 'Morgan Freeman'
                ) AS morgan
            ON skuespillerifilm.SkuespillerID = morgan.SkuespillerID)
        ) AS filmrolle
    ON film.FilmID = filmrolle.FilmID)
;

#e
SELECT DISTINCT Tittel FROM
    (skuespillerifilm JOIN
        (SELECT FilmID, Tittel, SkuespillerID FROM
            (film JOIN
                (SELECT RegissørID, SkuespillerID FROM
                    (regissør JOIN
                        skuespiller
                    ON regissør.Navn = skuespiller.Navn)
                ) AS par
            ON film.RegissørID = par.RegissørID)
        ) as filmskuespiller
    ON skuespillerifilm.SkuespillerID = filmskuespiller.SkuespillerID
    AND skuespillerifilm.FilmID = filmskuespiller.FilmID)
;

#f
SELECT COUNT(Navn) FROM
    skuespiller
    WHERE Navn LIKE 'C%'
;

#g
SELECT Navn, COUNT(Navn) FROM
    (sjanger JOIN
        (SELECT SjangerID, FilmID FROM sjangerforfilm) AS sjangerfilm
    ON sjanger.SjangerID = sjangerfilm.SjangerID)
    GROUP BY Navn
;

#h
Select Navn FROM
    (skuespiller JOIN
        (SELECT SkuespillerID FROM
            (skuespillerifilm JOIN
                (SELECT FilmID FROM film
                    WHERE film.Tittel = 'Ace Ventura: Pet Detective'
                ) AS pt
            ON skuespillerifilm.FilmID = pt.FilmID)
            WHERE SkuespillerID NOT IN
                (SELECT SkuespillerID FROM
                    (skuespillerifilm JOIN
                        (SELECT FilmID FROM film
                            WHERE film.Tittel = 'Ace Ventura: When Nature Calls'
                        ) AS wnc
                    ON skuespillerifilm.FilmID = wnc.FilmID)
                )
        ) as skuespilleririktigfilm
    ON skuespiller.SkuespillerID = skuespilleririktigfilm.SkuespillerID)
;

#i
SELECT Tittel, film.FilmID, AVG(Fødselsår) AS aby FROM
    (film JOIN
        (skuespillerifilm JOIN
            skuespiller
        ON skuespillerifilm.SkuespillerID = skuespiller.SkuespillerID)
    ON film.FilmID = skuespillerifilm.SkuespillerID)
    GROUP BY film.FilmID
    HAVING aby > ALL(SELECT AVG(Fødselsår) FROM skuespiller)
;