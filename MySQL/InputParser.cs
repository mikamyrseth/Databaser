using System.Collections.Generic;
using System;

namespace MySQL
{

    static class InputParser
    {
        private static Dictionary<string, Func<string[], bool>> commands = new Dictionary<string, Func<string[], bool>>{
            {"CreateMovie", CreateMovie},
        };
        private static bool CreateMovie(string[] args){
            return true;
        }

        private static void ParseInput(string input){
            string[] inputargs = input.Split(" ");
            string command = inputargs[0];
            string[] args = inputargs;
            try {
                commands[command](args);
            }
            catch (KeyNotFoundException e){
                Console.WriteLine("Command not found");
            }
        }
    
    }

}