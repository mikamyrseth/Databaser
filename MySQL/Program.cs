using System;
using System.Collections.Generic;

namespace MySQL
{

  internal class Program
  {

    private Dictionary<string, command> _commands = new Dictionary<string, command>();

    private bool _running = true;

    private Program() {
      this._commands.Add("create movie", this.CreateMovie);
      this._commands.Add("create creator", this.CreateCreator);
      this._commands.Add("add actor to movie", this.AddActorToMovie);
      this._commands.Add("create company", this.CreateCompany);
      this._commands.Add("add company as publisher", this.AddCompanyAsPublisher);
      this._commands.Add("create series", this.CreateSeries);
      this._commands.Add("create user", this.CreateUser);
      this._commands.Add("create movie review", this.CreateMovieReview);
      this._commands.Add("create season", this.CreateSeason);
      this._commands.Add("create episode review", this.CreateEpisodeReview);
      this._commands.Add("create category", this.CreateCategory);
      this._commands.Add("add category to movie", this.AddCategoryToMovie);
      this._commands.Add("create episode", this.CreateEpisode);
      this._commands.Add("see actor roles", this.SeeActorRoles);
      this._commands.Add("see company with most movies in category", SeeCompanyWithMostMoviesInCategory);
      //this._commands.Add("create series review", this.CreateSeriesReview);
      this._commands.Add("help", this.Help);
      this._commands.Add("exit", this.Quit);
      this._commands.Add("quit", this.Quit);
      this.Start();
    }

    private void SeeCompanyWithMostMoviesInCategory(){
      Console.WriteLine("Please choose a company");
      if(!this.PromptForDatabaseObject<Company>("selskapsnavn", "Filmselskap", out int companyID)){
        return;
      }

      Console.WriteLine("Please choose a cateogry");
      if(!this.PromptForDatabaseObject<Category>("kategoriNavn", "Kateogry", out int categoryID)){
        return;
      }

      API.SeeCompanyWithMostMoviesInCategory(companyID, categoryID);
      
    }

    private void SeeActorRoles() {
      Console.WriteLine("Enter actor");
      if(!this.PromptForDatabaseObject<Creator>("kreatørNav", "Kreatør", out int actorID))
      
      API.SeeActorRoles(actorID);
    }

    private void CreateEpisode() {
      Console.WriteLine("Enter episode title");
      if (!this.PromptForString(out string title, new MaxLengthFilter(40))) {
        return;
      }

      Console.WriteLine("Enter episode publishing year");
      if (!this.PromptForInt(out int publishingYear, new ReasonableYearFilter(10))) {
        return;
      }

      Console.WriteLine("Enter episode length in seconds");
      if (!this.PromptForInt(out int duration, new PositiveIntFilter(), new MediumIntFilter())) {
        return;
      }

      Console.WriteLine("Enter episode description");
      if (!this.PromptForString(out string description, new MaxLengthFilter(140))) {
        return;
      }

      Console.WriteLine("Please enter the director of the episode");
      if (!this.PromptForDatabaseObject<Creator>("kreatørNavn", "Kreatør", out int directorID)) {
        return;
      }

      Console.WriteLine("Please enter the script writer of the episode");
      if (!this.PromptForDatabaseObject<Creator>("kreatørNavn", "Kreatør", out int scriptWriterID)) {
        return;
      }

      Console.WriteLine("Please Enter the series that the episode belowngs to");
      if (!this.PromptForDatabaseObject<Series>("serieTittel", "Serie", out int seriesID)) {
        return;
      }

      Console.WriteLine("Please enter the season the episode belongs to.");
      if (!this.PromptForDatabaseObject<Season>("sesongTittel", "Sesong", out int seasonNumber)) {
        return;
      }

      if (API.CreateNewEpisode(title, publishingYear, duration, description, directorID, scriptWriterID, seriesID, seasonNumber)) {
        Console.WriteLine("You have added a new movie 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void CreateSeason() {
      Console.WriteLine("Please choose a series");
      if (!this.PromptForDatabaseObject<Series>("serieTittel", "Serie", out int seriesID)) {
        return;
      }

      Console.WriteLine("Please enter season number");
      if (!this.PromptForInt(out int seasonNumber, new RangeFilter(1, 100))) {
        return;
      }

      Console.WriteLine("Please enter a title");
      if (!this.PromptForString(out string title, new MaxLengthFilter(40))) {
        return;
      }

      Console.WriteLine("please enter a description");
      if (!this.PromptForString(out string description, new MaxLengthFilter(140))) {
        return;
      }

      if (API.CreateNewSeason(seriesID, seasonNumber, title, description)) {
        Console.WriteLine("You have added a new season 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void CreateCategory() {
      Console.WriteLine("Please enter a name");
      string name;
      if (!this.PromptForString(out name, new MaxLengthFilter(40))) {
        return;
      }

      if (API.CreateNewCategory(name)) {
        Console.WriteLine("You have added a category 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void AddCategoryToMovie() {
      Console.WriteLine("Please choose a category");
      if (!this.PromptForDatabaseObject<Category>("kategoriNavn", "Kategori", out int categoryID)) {
        return;
      }

      Console.WriteLine("Please choose a movie");
      if (!this.PromptForDatabaseObject<Movie>("filmTittel", "Film", out int movieID)) {
        return;
      }

      if (API.AddCategoryToMovie(categoryID, movieID)) {
        Console.WriteLine("You have added this category to movie 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void CreateEpisodeReview() {
      Console.WriteLine("Please enter a user");
      if (!this.PromptForDatabaseObject<User>("epost", "Seer", out int userID)) {
        return;
      }

      Console.WriteLine("Please choose an episode");
      if (!this.PromptForDatabaseObject<Movie>("filmTittel", "Film", out int movieID)) {
        return;
      }

      Console.WriteLine("Please add comment");
      if (!this.PromptForString(out string comment, new MaxLengthFilter(140))) {
        return;
      }

      Console.WriteLine("Please add a rating between 1 to 10");
      if (!this.PromptForInt(out int rating, new RangeFilter(1, 10))) {
        return;
      }

      if (API.CreateNewMovieReview(userID, movieID, comment, rating)) {
        Console.WriteLine("You have added a review 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void CreateMovieReview() {
      Console.WriteLine("Please enter a user");
      if (!this.PromptForDatabaseObject<User>("epost", "Seer", out int userID)) {
        return;
      }

      Console.WriteLine("Please choose a movie");
      if (!this.PromptForDatabaseObject<Movie>("filmTittel", "Film", out int movieID)) {
        return;
      }

      Console.WriteLine("Please add comment");
      if (!this.PromptForString(out string comment, new MaxLengthFilter(140))) {
        return;
      }

      Console.WriteLine("Please add a rating between 1 to 10");
      if (!this.PromptForInt(out int rating, new RangeFilter(1, 10))) {
        return;
      }

      if (API.CreateNewMovieReview(userID, movieID, comment, rating)) {
        Console.WriteLine("You have added a review 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void CreateUser() {
      Console.WriteLine("Please enter your email");
      if (!this.PromptForString(out string email, new MaxLengthFilter(40))) {
        return;
      }

      if (API.CreateNewUser(email)) {
        Console.WriteLine("You have added a new user 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void CreateSeries() {
      Console.WriteLine("Enter the title");
      if (!this.PromptForString(out string title, new MaxLengthFilter(40))) {
        return;
      }

      Console.WriteLine("Enter a description");
      if (!this.PromptForString(out string description, new MaxLengthFilter(140))) {
        return;
      }

      if (API.CreateNewSeries(title, description)) {
        Console.WriteLine("You have added a new series 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void AddCompanyAsPublisher() {
      Console.WriteLine("Enter a movie");
      if (!this.PromptForDatabaseObject<Movie>("filmTittel", "Film", out int movieID)) {
        return;
      }

      Console.WriteLine("Enter a company");
      if (!this.PromptForDatabaseObject<Company>("selskapsnavn", "Filmselskap", out int companyID)) {
        return;
      }

      if (API.AddCompanyAsPublisher(movieID, companyID)) {
        Console.WriteLine("You have added a published to the movie 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void CreateCompany() {
      Console.WriteLine("Enter a name");
      if (!this.PromptForString(out string companyName, new MaxLengthFilter(40))) {
        return;
      }

      Console.WriteLine("Enter a country");
      if (!this.PromptForDatabaseObject<Country>("landNavn", "Land", out int countryID)) {
        return;
      }

      if (API.CreateCompany(companyName, countryID)) {
        Console.WriteLine("You have added a new Company 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void AddActorToMovie() {
      Console.WriteLine("Enter a creator");
      if (!this.PromptForDatabaseObject<Creator>("kreatørNavn", "Kreatør", out int creatorID)) {
        return;
      }

      Console.WriteLine("Enter a movie");
      if (!this.PromptForDatabaseObject<Movie>("filmTittel", "Film", out int movieID)) {
        return;
      }

      Console.WriteLine("Please enter the role of this actor in this movie.");
      if (!this.PromptForString(out string role, new MaxLengthFilter(40))) {
        return;
      }

      if (API.AddActorToMovie(creatorID, movieID, role)) {
        Console.WriteLine("You have added actor to movie 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void CreateMovie() {
      Console.WriteLine("Enter movie title");
      if (!this.PromptForString(out string title, new MaxLengthFilter(40))) {
        return;
      }

      Console.WriteLine("Enter movie publishing year");
      if (!this.PromptForInt(out int publishingYear, new ReasonableYearFilter(10))) {
        return;
      }

      Console.WriteLine("Enter movie length in seconds");
      if (!this.PromptForInt(out int duration, new PositiveIntFilter(), new MediumIntFilter())) {
        return;
      }

      Console.WriteLine("Enter movie description");
      if (!this.PromptForString(out string description, new MaxLengthFilter(140))) {
        return;
      }

      Console.WriteLine("Please enter the director of the movie");
      if (!this.PromptForDatabaseObject<Creator>("kreatørNavn", "Kreatør", out int directorID)) {
        return;
      }

      Console.WriteLine("Please enter the script writer of the movie");
      if (!this.PromptForDatabaseObject<Creator>("kreatørNavn", "Kreatør", out int scriptWriterID)) {
        return;
      }

      if (API.CreateNewMovie(title, publishingYear, duration, description, directorID, scriptWriterID)) {
        Console.WriteLine("You have added a new movie 🙌");
      } else {
        Console.WriteLine("Oops. Something went horribly wrong... 😢");
      }
    }

    private void CreateCreator() {
      Console.WriteLine("Please enter a name");
      if (!this.PromptForString(out string creatorName, new MaxLengthFilter(40))) {
        return;
      }

      Console.WriteLine("Please enter the creator's birth year");
      if (!this.PromptForInt(out int birthYear, new ReasonableYearFilter(0))) {
        return;
      }

      Console.WriteLine("Please enter the Country of the creator");
      if (!this.PromptForDatabaseObject<Country>("landNavn", "Land", out int countryID)) {
        return;
      }

      if (API.CreateNewCreator(creatorName, birthYear, countryID)) {
        Console.WriteLine("You have added a new creator 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }
    
    private bool PromptForDatabaseObject<T>(string columnName, string tableName, out int objectID)
      where T : DatabaseObject, new() {
      while (true) {
        Console.WriteLine("Enter name");
        string userInput = Console.ReadLine();
        if (userInput == "cancel") {
          Console.WriteLine("Command cancelled");
          objectID = -1;
          return false;
        }
        List<T> objectList = new List<T>(API.GetObjectsByName<T>(userInput, columnName, tableName));
        if (objectList.Count > 1) {
          Console.WriteLine("Please choose one of the following:");
          List<int> IDs = new List<int>();
          foreach (T @object in objectList) {
            Console.WriteLine(@object.RowForm());
            IDs.Add(@object.ID);
          }
          Console.WriteLine("Enter the ID");
          if (!this.PromptForInt(out objectID, new InIntCollectionFilter(IDs))) {
            continue;
          }
          return true;
        }
        if (objectList.Count == 0) {
          Console.WriteLine("No results with that name");
        } else {
          objectID = objectList[0].ID;
          return true;
        }
      }
    }

    private bool PromptForInt(out int inputInt, params Filter<int>[] filters) {
      while (true) {
        string userInput = Console.ReadLine();
        if (userInput == "cancel") {
          Console.WriteLine("Command cancelled");
          inputInt = 0;
          return false;
        }
        if (int.TryParse(userInput, out inputInt)) {
          bool filtersPassed = true;
          foreach (Filter<int> filter in filters) {
            if (filter.Condition(inputInt)) {
              continue;
            }
            Console.WriteLine(filter.ErrorMessage);
            filtersPassed = false;
          }
          if (filtersPassed) {
            return true;
          }
        } else {
          Console.WriteLine("Value must be an integer!");
        }
        Console.WriteLine("Type \"cancel\" to cancel");
      }
    }

    private bool PromptForString(out string inputString, params Filter<string>[] filters) {
      while (true) {
        inputString = Console.ReadLine();
        if (inputString == "cancel") {
          Console.WriteLine("Command cancelled");
          return false;
        }
        bool filtersPassed = true;
        foreach (Filter<string> filter in filters) {
          if (!filter.Condition(inputString)) {
            Console.WriteLine(filter.ErrorMessage);
            filtersPassed = false;
          }
        }
        if (filtersPassed) {
          return true;
        }
        Console.WriteLine("Type \"cancel\" to cancel");
      }
    }

    private void Help() {
      List<string> validCommands = new List<string>(this._commands.Keys);
      Console.WriteLine("Valid commands are: ");
      foreach (string command in validCommands) {
        Console.WriteLine("\t" + command);
      }
    }

    private void Quit() {
      this._running = false;
    }

    private void Start() {
      while (this._running) {
        Console.Write(">>> ");
        string command = Console.ReadLine();
        try {
          this._commands[command]();
        } catch {
          Console.WriteLine("Command not found");
        }
      }
    }

    private static void Main(string[] args) {
      Program p = new Program();
    }

    private delegate void command();

  }

}
