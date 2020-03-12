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
      MySqlConnection conn = new MySqlConnection(connStr);
      bool SQLSuccess = true;
      try {
        Console.WriteLine("Connecting to MySQL...");
        conn.Open();
        string sql = $"INSERT INTO Film (filmTittel, utgivelesår, lengde, filmbeskrivelse) VALUES ('{title}', {publishingYear}, {duration}, '{description}');";
        Console.WriteLine(sql);
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected != 1) {
          SQLSuccess = false;
        }

        long movieID = cmd.LastInsertedId;

        string sql2 = $"INSERT INTO RegissørIFilm (FilmID, KreatørID) VALUES ({movieID}, {directorID});INTO ManusforfatterIFilm (FilmID, KreatørID) VALUES ({movieID}, {scriptWriterID});";
        Console.WriteLine(sql2);

        cmd = new MySqlCommand(sql2, conn);
        int rowsAffected2 = cmd.ExecuteNonQuery();
        if (rowsAffected2 == 0) {
          SQLSuccess = false;
        }

        // string sql3 = $"INSERT INTO ManusforfatterIFilm (FilmID, KreatørID) VALUES ({movieID}, {scriptWriterID});";
        // Console.WriteLine(sql3);
// 
        // cmd = new MySqlCommand(sql3, conn);
        // int rowsAffected3 = cmd.ExecuteNonQuery();
        // if (rowsAffected3 != 1) {
        //   SQLSuccess = false;
        // }
      } catch (Exception ex) {
        SQLSuccess = false;
        Console.WriteLine(ex.ToString());
      } finally {
        conn.Close();
        Console.WriteLine("Done.");
      }
      return SQLSuccess;
    }

    public static bool CreateNewCreator(string Name, int birthYear, int CountryID){
        string sql = $"INSERT INTO Kreatør (kreatørNavn, fødselsår, landID) VALUES ('{Name}', {birthYear}, {CountryID});";
        return SQLInsert(sql);
    }

    public static bool AddActorToMovie(int creatorID, int movieID, string role) {
      string sql = $"INSERT INTO  SkuespillerIFilm (FilmID, KreatørID, rolle) VALUES ({movieID}, {creatorID}, '{role}');";
      return SQLInsert(sql);
    }

    public static bool CreateCompany(string companyName, int countryID) {
      string sql = $"INSERT INTO  SkuespillerIFilm (selskapsnavn, LandID) VALUES ('{companyName}', {countryID});";
      return SQLInsert(sql);
    }

    public static bool AddCompanyAsPublisher(int movieID, int companyID) {
      string sql = $"INSERT INTO  Utgivelser (FilmselskapID, FilmID) VALUES ({movieID}, {companyID});";
      return SQLInsert(sql);
    }

    private static bool SQLInsert(string sql){
      MySqlConnection conn = new MySqlConnection(connStr);
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