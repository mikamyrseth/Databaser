using System;

namespace MySQL
{

  public class Company : DatabaseObject
  {

    public int CountryID { get; private set; }

    public override void Initialize(params object[] fields) {
      if (fields.Length != 2) {
        throw new FormatException($"{this.GetType().Name} cannot be initialized with {fields.Length} dynamic values!");
      }
      base.Initialize(fields[0], fields[1]);
      this.CountryID = (int)fields[2];
    }
    public override string RowForm() {
      return $"ID: {this.ID}, Name: {this.Name}, CountryID: {this.CountryID}";
    }

  }

}