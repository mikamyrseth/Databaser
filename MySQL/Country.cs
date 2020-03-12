namespace MySQL
{

  public class Country : DatabaseObject
  {

    public override string RowForm() {
      return $"ID: {this.ID}, Name: {this.Name}";
    }

  }

}