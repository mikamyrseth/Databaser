using System;
using System.Collections.Generic;

namespace MySQL
{

  public class MaxLengthFilter : Filter<string>{

      private int _maxLength;
      public MaxLengthFilter(int maxLength) : base($"Value cannot be more than {maxLength} characters") {
        _maxLength = maxLength;
      }

      public override bool Condition(string subject){
          return subject.Length <= _maxLength;
      }



  }

  public class ReasonableYearFilter : Filter<int>{

      private int _futureBuffer;
      public ReasonableYearFilter(int futureBuffer) : base("Value is not a reasonable year >: ^ ( | )"){
          this._futureBuffer = futureBuffer;
      }

      public override bool Condition(int subject){
          return subject > 1800 && subject < DateTime.Today.Year + _futureBuffer;
      }
  }

  public class MediumIntFilter : Filter<int>{
      public MediumIntFilter() : base("Value must be beneath 8388607"){

      }

      public override bool Condition(int subject){
          return subject > -8388608 && subject < 8388607;
      }
  }

  public class PositiveIntFilter : Filter<int>{
      public PositiveIntFilter() : base ("Value must be positive"){

      }

      public override bool Condition(int subject){
          return subject > 0;
      }
  }

  public class InIntCollectionFilter : Filter<int>{
      private ICollection<int> _collection;
      public InIntCollectionFilter(ICollection<int> collection) : base("Value was not in collection"){
          _collection = collection;
      }

      public override bool Condition(int subject){
          return _collection.Contains(subject);
      }
  }

}

