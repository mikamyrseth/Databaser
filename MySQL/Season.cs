using System;

namespace MySQL
{

  public class Season : DatabaseObject
  {

    public string Description { get; private set; }
    public int SeriesID { get; private set; }

    public override void Initialize(params object[] fields) {
      if (fields.Length != 4) {
        throw new FormatException($"{this.GetType().Name} cannot be initialized with {fields.Length} dynamic values!");
      }
      base.Initialize(fields[1], fields[3]);
      this.Description = (string)fields[2];
      this.SeriesID = (int)fields[0];
    }
    public override string RowForm() {
      return $"ID: {this.ID}, Season number: {this.ID}, SeriesID: {this.SeriesID}, Title: {this.Name}";
    }

  }

}