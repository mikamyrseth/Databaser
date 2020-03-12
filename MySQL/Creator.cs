namespace MySQL
{

  public readonly struct Creator{
      public int ID { get; }
      public int BirthYear { get; }
      
      public string Name { get; }

      public Creator(int id, int birthYear, string name){
          ID = id;
          BirthYear = birthYear;
          Name = name;
      }
  }

}