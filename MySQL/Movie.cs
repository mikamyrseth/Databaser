namespace MySQL
{

  public readonly struct Movie : IDatabaseObject
  {

    public int ID { get; }
    public int publishingYear { get; }

    public string Name { get; }

    public Movie(int id, int publishingYear, string name) {
      this.ID = id;
      this.Name = name;
      this.publishingYear = publishingYear;
    }

  }

}