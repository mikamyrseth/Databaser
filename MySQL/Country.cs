namespace MySQL
{

  public readonly struct Country : IDatabaseObject
  {

    public int ID { get; }
    public string Name { get; }

    public Country(int id, int birthYear, string name) {
      this.ID = id;
      this.Name = name;
    }

  }

}