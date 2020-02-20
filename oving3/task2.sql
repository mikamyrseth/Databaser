SELECT FilmID, Tittel, Produksjonsår, RegissørID FROM film;

SELECT Navn FROM skuespiller
    WHERE 1960 < Fødselsår;

SELECT Navn FROM skuespiller
    WHERE 1979 < Fødselsår AND Fødselsår < 1990
    ORDER BY Navn;

SELECT Tittel, Rolle FROM
    (film JOIN
        (SELECT FilmID, Rolle FROM
            (skuespillerifilm JOIN
                (SELECT SkuespillerID FROM skuespiller
                    WHERE Navn = 'Morgan Freeman'
                ) AS morgan ON skuespillerifilm.SkuespillerID = morgan.SkuespillerID
            )
        ) AS filmrolle ON film.FilmID = filmrolle.FilmID
    )
;