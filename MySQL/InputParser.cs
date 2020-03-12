using System;
using System.Collections.Generic;

namespace MySQL
{

  internal static class InputParser
  {

    private static Dictionary<string, Func<string[], bool>> commands = new Dictionary<string, Func<string[], bool>> { { "create_movie", CreateMovie } };
    private static bool CreateMovie(string[] args) {
      return true;
    }

    private static void ParseInput(string input) { }

  }

}