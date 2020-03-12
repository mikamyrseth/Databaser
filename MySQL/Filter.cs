namespace MySQL
{

  public abstract class Filter<T>
  {
      public string ErrorMessage { get; }

      public Filter(string errorMessage) {
          this.ErrorMessage = errorMessage;
      }

      public abstract bool Condition(T subject);

  }

}