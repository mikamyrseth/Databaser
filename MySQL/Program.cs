using System;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace MySQL
{

  class Program
  {

    static void Main(string[] args) {
      string connStr = "server=localhost;user=root;database=superduperdatabase;port=3306;password=root";
      MySqlConnection conn = new MySqlConnection(connStr);
      try {
        Console.WriteLine("Connecting to MySQL...");
        conn.Open();
        
        string sql = "SELECT * FROM film;";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
          Console.WriteLine(rdr[0] + " -- " + rdr[1]);
        }
        rdr.Close();
      } catch (Exception ex) {
        Console.WriteLine(ex.ToString());
      } finally {
        conn.Close();
        Console.WriteLine("Done.");
      }
    }

  }

}