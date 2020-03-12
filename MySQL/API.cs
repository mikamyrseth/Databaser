using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;

namespace MySQL
{

  public static class API
  {

    private const string connStr = "server=localhost;user=root;database=superduperdatabase;port=3306;password=root;";

    public static Creator GetCreatorByID(int id) {
      return new Creator(1, 1999, "Mika Myrseth");
    }

    public static List<Creator> GetCreatorsByName(string name) {
      List<Creator> creators = new List<Creator>();
      creators.Add(new Creator(1, 1999, "Mika Myrseth"));
      return creators;
    }

    public static List<IDataBaseObject> GetObjectByName<IDataBaseObject>(string userInput, string tableName) {
      return;
    }

    public static bool CreateNewMovie(string title, int publishingYear, int duration, string description, int directorID, int scriptWriterID) {
      string sql = $"INSERT INTO Film (filmTittel, utgivelesår, lengde, filmbeskrivelse) VALUES ('{title}', {publishingYear}, {duration}, '{description}');";
      long movieID = SQLInsert(sql);

      return movieID != -1;
    }

    public static bool CreateNewEpisode(string title, int publishingYear, int duration, string description, int directorID, int scriptWriterID int seriesID, int seasonNumber){
      string sql = $"INSERT INTO Film (filmTittel, utgivelesår, lengde, filmbeskrivelse, serieID, Sesongnummer) VALUES ('{title}', {publishingYear}, {duration}, '{description}', {seriesID}, {seasonNumber});";
      return SQLInsert(sql) != -1;
    }

    public static bool CreateNewCreator(string Name, int birthYear, int CountryID){
        string sql = $"INSERT INTO Kreatør (kreatørNavn, fødselsår, landID) VALUES ('{Name}', {birthYear}, {CountryID});";
        return SQLInsert(sql) != -1;
    }

    public static bool AddActorToMovie(int creatorID, int movieID, string role) {
      string sql = $"INSERT INTO  SkuespillerIFilm (FilmID, KreatørID, rolle) VALUES ({movieID}, {creatorID}, '{role}');";
      return SQLInsert(sql) != -1;
    }

    public static bool CreateCompany(string companyName, int countryID) {
      string sql = $"INSERT INTO  SkuespillerIFilm (selskapsnavn, LandID) VALUES ('{companyName}', {countryID});";
      return SQLInsert(sql) != -1;
    }

    public static bool AddCompanyAsPublisher(int movieID, int companyID) {
      string sql = $"INSERT INTO  Utgivelser (FilmselskapID, FilmID) VALUES ({movieID}, {companyID});";
      return SQLInsert(sql) != -1;
    }

    public static bool CreateNewSeries(string title, string description){
      string sql = $"INSERT INTO  Serie (serieTittel, seriebeskrivelse) VALUES ({title}, {description});";
      return SQLInsert(sql) != -1;
    }

    public static bool CreateNewUser(string email){
      string sql = $"INSERT INTO  Seer (epost) VALUES ({email});";
      return SQLInsert(sql) != -1;
    }

    public static bool CreateNewMovieReview(int userID, int movieID, string comment, int rating){
      string sql = $"INSERT INTO  Filmanmeldelse (SeerID, FilmID, filmKommentar, filmVurdering) VALUES ({userID}, {movieID}, '{comment}', {rating});";
      return SQLInsert(sql) != -1;
    }

    public static bool CreateNewCategory(string name){
      string sql = $"INSERT INTO  Kategory (kategoriNavn) VALUES ('{name}');";
      return SQLInsert(sql) != -1;
    }

    public static bool AddCategoryToMovie(int categoryID, int movieID){
      string sql = $"INSERT INTO FilmIKategori (FilmID, KategoriID) VALUES ({movieID}, {categoryID});";
      return SQLInsert(sql) != -1;
    }

    public static bool CreateNewSeason(int seriesID, int seasonNumber, string title, string description){
      string sql = $"INSERT INTO Sesong (SerieID,  Sesongnummer, sesongbeskrivelse, sesongTittel) VALUES ({seriesID}, {seasonNumber}, '{title}', '{description}');";
      return SQLInsert(sql) != -1;
    }

    private static long SQLInsert(string sql){
      MySqlConnection conn = new MySqlConnection(connStr);
      bool SQLSuccess = true;
      long movieID = -1;
      try {
        Console.WriteLine("Connecting to MySQL...");
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected != 1) {
          SQLSuccess = false;
        }

        movieID = cmd.LastInsertedId;

      } catch (Exception ex) {
        SQLSuccess = false;
        Console.WriteLine(ex.ToString());
      } finally {
        conn.Close();
        Console.WriteLine("Done.");
      }
      if(SQLSuccess){
        return movieID;
      } else {
        return -1;
      }

  }

}