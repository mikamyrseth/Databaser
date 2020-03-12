using System;

namespace MySQL
{

  public class Movie : DatabaseObject
  {

    public short ReleaseYear { get; private set; }
    public int Duration { get; private set; }
    public string Description { get; private set; }
    public int SeriesID { get; private set; }
    public short SeasonNumber { get; private set; }

    public override void Initialize(params object[] fields) {
      if (fields.Length != 7) {
        throw new FormatException($"{this.GetType().Name} cannot be initialized with {fields.Length} dynamic values!");
      }
      base.Initialize(fields[0], fields[1]);
      this.ReleaseYear = (short)fields[2];
      this.Duration = (int)fields[3];
      this.Description = (string)fields[4];
      if (fields[5] is DBNull) {
        this.SeriesID = -1;
        this.SeasonNumber = -1;
      } else {
        this.SeriesID = (int)fields[5];
        this.SeasonNumber = (short)fields[6];
      }
    }
    public override string RowForm() {
      if (this.SeriesID == -1) {
        return $"ID: {this.ID}, Title: {this.Name}, Release year: {this.ReleaseYear}";
      }
      return $"ID: {this.ID}, Title: {this.Name}, Release year: {this.ReleaseYear}, Series ID: {this.SeriesID}, Season number: {this.SeasonNumber}";
    }

  }

}