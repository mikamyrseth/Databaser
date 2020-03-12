using System;
using MySql.Data;
using MySql.Data.MySqlClient;
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
      this._commands.Add("Create episode review", this.CreateEpisodeReview);
      this._commands.Add("create genre", this.CreateCategory);
      //this._commands.Add("create series review", this.CreateSeriesReview);
      this._commands.Add("help", this.Help);
      this._commands.Add("exit", this.Quit);
      this._commands.Add("quit", this.Quit);
      this.Start();
    }

    private void CreateCategory(){
      Console.WriteLine("Please enter a name");
      string name;
      if(!this.PromptForString(out name, new MaxLengthFilter(40))){
        return;
      }

      if (API.CreateNewCategory(name)) {
        Console.WriteLine("You have added a category 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      } 

    }

    private void AddCategoryToMovie(){
      Console.WriteLine("Please choose a category");
      int categoryID;
      if (!this.PromptForDatabaseObject<Category>("Kategori", out categoryID)){
        return;
      }

      Console.WriteLine("Please choose a movie");
      int movieID;
      if(!this.PromptForDatabaseObject<Movie>("Film", out movieID)){
        return;
      }

      if (API.AddCategoryToMovie(categoryID, movieID)) {
        Console.WriteLine("You have added this category to movie 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }

    }

    private void CreateEpisodeReview(){
      Console.WriteLine("Please enter a user");
      int userID;
      if(!this.PromptForDatabaseObject<User>("Seer", out userID)){
        return;
      }

      Console.WriteLine("Please choose an episode");
      int movieID;
      if(!this.PromptForDatabaseObject<Movie>("Film", out movieID)){
        return;
      }

      Console.WriteLine("Please add comment");
      string comment;
      if(!this.PromptForString(out comment, new MaxLengthFilter(140))){
        return;
      }

      Console.WriteLine("Please add a rating between 1 to 10");
      int rating;
      if(!this.PromptForInt(out rating, new RangeFilter(1, 10))){
        return;
      }

      if (API.CreateNewMovieReview(userID, movieID, comment, rating)) {
        Console.WriteLine("You have added a review 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      } 


    }

    private void CreateMovieReview(){
      Console.WriteLine("Please enter a user");
      int userID;
      if(!this.PromptForDatabaseObject<User>("Seer", out userID)){
        return;
      }

      Console.WriteLine("Please choose a movie");
      int movieID;
      if(!this.PromptForDatabaseObject<Movie>("Film", out movieID)){
        return;
      }

      Console.WriteLine("Please add comment");
      string comment;
      if(!this.PromptForString(out comment, new MaxLengthFilter(140))){
        return;
      }

      Console.WriteLine("Please add a rating between 1 to 10");
      int rating;
      if(!this.PromptForInt(out rating, new RangeFilter(1, 10))){
        return;
      }

      if (API.CreateNewMovieReview(userID, movieID, comment, rating)) {
        Console.WriteLine("You have added a review 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      } 

    }

    private void CreateUser(){
      Console.WriteLine("Please enter your email");
      string email;
      if (!this.PromptForString(out email, new MaxLengthFilter(40))){
        return;
      }

      if (API.CreateNewUser(email)) {
        Console.WriteLine("You have added a new user 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      } 

    }

    private void CreateSeries(){
      Console.WriteLine("Enter the title");
      string title;
      if (!this.PromptForString(out title, new MaxLengthFilter(40))){
        return;
      }

      Console .WriteLine("Enter a description");
      string description;
      if (!this.PromptForString(out description, new MaxLengthFilter(140))){
        return;
      }

      if (API.CreateNewSeries(title, description)) {
        Console.WriteLine("You have added a new series 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }  

    }

    private void AddCompanyAsPublisher(){
      Console.WriteLine("Enter a movie");
      int movieID;
      if(!this.PromptForDatabaseObject<Country>("Film", out movieID)){
        return;
      }

      Console.WriteLine("Enter a company");
      int companyID;
      if(!this.PromptForDatabaseObject<Company>("Filmselskap", out companyID)){
        return;
      }

      if (API.AddCompanyAsPublisher(movieID, companyID)) {
        Console.WriteLine("You have added a published to the movie 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }  

    }

    private void CreateCompany(){
      Console.WriteLine("Enter a name");
      string companyName;
      if(!this.PromptForString(out companyName, new MaxLengthFilter(40))){
        return;
      }
      
      Console.WriteLine("Enter a country");
      int countryID;
      if(!this.PromptForDatabaseObject<Country>("Land", out countryID)){
        return;
      }

      if (API.CreateCompany(companyName, countryID)) {
        Console.WriteLine("You have added a new Company 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }  

    }

    private void AddActorToMovie(){
      Console.WriteLine("Enter a creator");
      int creatorID;
      if(!this.PromptForDatabaseObject<Creator>("Kreatør", out creatorID)){
        return;
      }

      Console.WriteLine("Enter a movie");
      int movieID;
      if(!this.PromptForDatabaseObject<Movie>("Film", out movieID)){
        return;
      }

      Console.WriteLine("Please enter the role of this actor in this movie.");
      string role;
      if(!this.PromptForString(out role, new MaxLengthFilter(40))){
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
      if (!this.PromptForCreator(out int directorID)) {
        return;
      }

      Console.WriteLine("Please enter the script writer of the movie");
      if (!this.PromptForCreator(out int scriptWriterID)) {
        return;
      }

      if (API.CreateNewMovie(title, publishingYear, duration, description, directorID, scriptWriterID)) {
        Console.WriteLine("You have added a new movie 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }
    }

    private void CreateCreator(){
      Console.WriteLine("Please enter a name");
      string creatorName;
      if(!PromptForString(out creatorName, new MaxLengthFilter(40))){
        return;
      }

      Console.WriteLine("Please enter the creators birthyear");
      int birthYear;
      if(!PromptForInt(out birthYear, new ReasonableYearFilter(0))){
        return;
      }

      Console.WriteLine("Please enter the Country of the creator");
      int countryID;
      if(!PromptForDatabaseObject<Country>("Land", out countryID)){
        return;
      }

      if (API.CreateNewCreator(creatorName, birthYear, countryID)) {
        Console.WriteLine("You have added a new creator 🙌");
      } else {
        Console.WriteLine("Oops. Something went hooribly wrong... 😢");
      }

    }
    private bool PromptForDatabaseObject<IDataBaseObject>(string tableName, out int objectID) {
      while(true) {
        Console.WriteLine("Enter name");
        string userInput = Console.ReadLine();
        if (userInput == "cancel") {
          Console.WriteLine("Command cancelled");
          objectID = -1;
          return false;
        }
        List<IDatabaseObject> objectList = API.GetObjectByName<IDataBaseObject>(userInput, tableName);
        if (objectList.Count > 1) {
          Console.WriteLine("Please choose one of the following:");
          List<int> IDs = new List<int>();
          foreach (Creator creator in objectList) {
            Console.WriteLine($"\tID: {creator.ID}, Name: {creator.Name}, Birth year: {creator.BirthYear}");
            IDs.Add(creator.ID);
          }
          Console.WriteLine("Enter the ID");
          if (!this.PromptForInt(out objectID, new InIntCollectionFilter(IDs))) { }
        } else if (objectList.Count == 0) {
          Console.WriteLine("No results with that name");
        } else {
          objectID = objectList[0].ID;
          return true;
        }

      }

    }

    private bool PromptForCreator(out int creatorID) {
      while (true) {
        Console.WriteLine("Enter name");
        string userInput = Console.ReadLine();
        if (userInput == "cancel") {
          Console.WriteLine("Command cancelled");
          creatorID = -1;
          return false;
        }
        List<Creator> creatorsList = API.GetCreatorsByName(userInput);
        if (creatorsList.Count > 1) {
          Console.WriteLine("Please choose one of the following creators:");
          List<int> IDs = new List<int>();
          foreach (Creator creator in creatorsList) {
            Console.WriteLine($"\tID: {creator.ID}, Name: {creator.Name}, Birth year: {creator.BirthYear}");
            IDs.Add(creator.ID);
          }
          Console.WriteLine("Enter the creatorID");
          if (!this.PromptForInt(out creatorID, new InIntCollectionFilter(IDs))) { }
        } else if (creatorsList.Count == 0) {
          Console.WriteLine("No creators with that name");
        } else {
          creatorID = creatorsList[0].ID;
          return true;
        }
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
            if (!filter.Condition(inputInt)) {
              Console.WriteLine(filter.ErrorMessage);
              filtersPassed = false;
            }
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