using System;

namespace MySQL
{

  public abstract class DatabaseObject
  {

    public int ID { get; private set; }
    public string Name { get; private set; }

    public virtual void Initialize(params object[] fields) {
      if (fields.Length != 2) {
        throw new FormatException($"{this.GetType().Name} cannot be initialized with {fields.Length} dynamic values!");
      }
      this.ID = (int)fields[0];
      this.Name = (string)fields[1];
    }

    public abstract string RowForm();

  }

}