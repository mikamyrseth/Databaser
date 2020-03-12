namespace MySQL
{

  public readonly struct Creator
  {

    public int ID { get; }
    public int BirthYear { get; }

    public string Name { get; }

    public Creator(int id, int birthYear, string name) {
      this.ID = id;
      this.BirthYear = birthYear;
      this.Name = name;
    }

  }

}