namespace MySQL
{

  public abstract class Filter<T>
  {

    public Filter(string errorMessage) {
      this.ErrorMessage = errorMessage;
    }
    public string ErrorMessage { get; }

    public abstract bool Condition(T subject);

  }

}