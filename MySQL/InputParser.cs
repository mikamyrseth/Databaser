using System.Collections.Generic;
using System;

namespace MySQL
{

    static class InputParser
    {
        private static Dictionary<string, Func<string[], bool>> commands = new Dictionary<string, Func<string[], bool>>{
            {"create_movie", CreateMovie},
        };
        private static bool CreateMovie(string[] args){
            return true;
        }

        private static void ParseInput(string input){
            
        }
    
    }

}