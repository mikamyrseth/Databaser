namespace MySQL
{

  public class User : DatabaseObject
  {

    public override string RowForm() {
      return $"ID: {this.ID}, Email: {this.Name}";
    }

  }

}