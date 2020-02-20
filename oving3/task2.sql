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