using System;

namespace MySQL
{

  public class Creator : DatabaseObject
  {

    public short BirthYear { get; private set; }
    public int CountryID { get; private set; }

    public override void Initialize(params object[] fields) {
      if (fields.Length != 4) {
        throw new FormatException($"{this.GetType().Name} cannot be initialized with {fields.Length} dynamic values!");
      }
      base.Initialize(fields[0], fields[2]);
      this.BirthYear = (short)fields[1];
      this.CountryID = (int)fields[3];
    }

    public override string RowForm() {
      return $"ID: {this.ID}, Birth year: {this.BirthYear}, Name: {this.Name}, Country ID: {this.CountryID}";
    }

  }

}