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
      this._commands.Add("help", this.Help);
      this._commands.Add("exit", this.Quit);
      this._commands.Add("quit", this.Quit);
      this.Start();
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

    private void CreateCreator() { }

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