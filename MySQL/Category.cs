namespace MySQL
{

  public class Category : DatabaseObject
  {

    public override string RowForm() {
      return $"ID: {this.ID}, Category name: {this.Name}";
    }

  }

}