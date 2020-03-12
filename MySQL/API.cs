using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;

namespace MySQL
{

  public static class API
  {

    private const string ConnectionString = "server=localhost;user=root;database=MySQL;port=3306;password=root;";

    public static Creator GetCreatorByID(int id) {
      Creator creator = new Creator();
      creator.Initialize(1, 1999, "Mika Myrseth");
      return creator;
    }

    public static List<Creator> GetCreatorsByName(string name) {
      List<Creator> creators = new List<Creator>();
      Creator creator = new Creator();
      creator.Initialize(1, 1999, "Mika Myrseth");
      creators.Add(creator);
      return creators;
    }

    public static ICollection<T> GetObjectsByName<T>(string name, string columnName, string tableName)
      where T : DatabaseObject, new() {
      MySqlConnection connection = null;
      List<T> list = new List<T>();
      try {
        connection = new MySqlConnection(ConnectionString);
        connection.Open();

        string query = $"SELECT * FROM {tableName} WHERE {tableName}.{columnName} = '{name}';";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read()) {
          object[] fields = new object[reader.FieldCount];
          for (int i = 0; i < fields.Length; i++) {
            fields[i] = reader[i];
          }
          T @new = new T();
          @new.Initialize(fields);
          list.Add(@new);
        }
        reader.Close();
      } catch (Exception e) {
        Console.WriteLine(e);
      } finally {
        connection?.Close();
      }
      return list;
    }

    public static bool CreateNewMovie(string title, int publishingYear, int duration, string description, int directorID, int scriptWriterID) {
      MySqlConnection conn = new MySqlConnection(ConnectionString);
      bool SQLSuccess = true;
      try {
        Console.WriteLine("Connecting to MySQL...");
        conn.Open();
        string sql = $"INSERT INTO Film (filmTittel, utgivelesår, lengde, filmbeskrivelse) VALUES ('{title}', {publishingYear}, {duration}, '{description}');";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected != 1) {
          SQLSuccess = false;
        }

        long movieID = cmd.LastInsertedId;

        string sql2 = $"INSERT INTO RegissørIFilm (FilmID, KreatørID) VALUES ({movieID}, {directorID});";

        cmd = new MySqlCommand(sql2, conn);
        int rowsAffected2 = cmd.ExecuteNonQuery();
        if (rowsAffected2 == 0) {
          SQLSuccess = false;
        }

        string sql3 = $"INSERT INTO ManusforfatterIFilm (FilmID, KreatørID) VALUES ({movieID}, {scriptWriterID});";
        // Console.WriteLine(sql3);
        
        cmd = new MySqlCommand(sql3, conn);
        int rowsAffected3 = cmd.ExecuteNonQuery();
        if (rowsAffected3 != 1) {
          SQLSuccess = false;
        }
      } catch (Exception ex) {
        SQLSuccess = false;
        Console.WriteLine(ex.ToString());
      } finally {
        conn.Close();
        Console.WriteLine("Done.");
      }
      return SQLSuccess;
    }

    public static bool CreateNewCreator(string Name, int birthYear, int CountryID) {
      string sql = $"INSERT INTO Kreatør (kreatørNavn, fødeslsår, landID) VALUES ('{Name}', {birthYear}, {CountryID});";
      return SQLInsert(sql);
    }

    public static bool AddActorToMovie(int creatorID, int movieID, string role) {
      string sql = $"INSERT INTO  SkuespillerIFilm (FilmID, KreatørID, rolle) VALUES ({movieID}, {creatorID}, '{role}');";
      return SQLInsert(sql);
    }

    public static bool CreateCompany(string companyName, int countryID) {
      string sql = $"INSERT INTO Filmselskap (selskapsnavn, LandID) VALUES ('{companyName}', {countryID});";
      return SQLInsert(sql);
    }

    public static bool AddCompanyAsPublisher(int movieID, int companyID) {
      string sql = $"INSERT INTO  Utgivelser (FilmselskapID, FilmID) VALUES ({movieID}, {companyID});";
      return SQLInsert(sql);
    }

    public static bool CreateNewSeries(string title, string description) {
      string sql = $"INSERT INTO Serie (serieTittel, seriebeskrivelse) VALUES ('{title}', '{description}');";
      return SQLInsert(sql);
    }

    public static bool CreateNewUser(string email) {
      string sql = $"INSERT INTO  Seer (epost) VALUES ('{email}');";
      return SQLInsert(sql);
    }

    public static bool CreateNewMovieReview(int userID, int movieID, string comment, int rating) {
      string sql = $"INSERT INTO  Filmanmeldelse (SeerID, FilmID, filmKommentar, filmVurdering) VALUES ({userID}, {movieID}, '{comment}', {rating});";
      return SQLInsert(sql);
    }

    public static bool CreateNewCategory(string name){
      string sql = $"INSERT INTO  Kategori (kategoriNavn) VALUES ('{name}');";
      return SQLInsert(sql);
    }

    public static bool AddCategoryToMovie(int categoryID, int movieID){
      string sql = $"INSERT INTO FilmIKategori (FilmID, KategoriID) VALUES ({movieID}, {categoryID});";
      return SQLInsert(sql);
    }

    private static bool SQLInsert(string sql) {
      MySqlConnection conn = new MySqlConnection(ConnectionString);
      bool SQLSuccess = true;
      try {
        Console.WriteLine("Connecting to MySQL...");
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected != 1) {
          SQLSuccess = false;
        }

      } catch (Exception ex) {
        SQLSuccess = false;
        Console.WriteLine(ex.ToString());
      } finally {
        conn.Close();
        Console.WriteLine("Done.");
      }
      return SQLSuccess;
    }

  }

}
