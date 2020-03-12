using System;

namespace MySQL
{

  public class Series : DatabaseObject
  {

    public string Description { get; private set; }

    public override void Initialize(params object[] fields) {
      if (fields.Length != 3) {
        throw new FormatException($"{this.GetType().Name} cannot be initialized with {fields.Length} dynamic values!");
      }
      base.Initialize(fields[0], fields[2]);
      this.Description = (string)fields[1];
    }
    public override string RowForm() {
      return $"ID: {this.ID}, Title: {this.Name}, Description: {this.Description}";
    }

  }

}