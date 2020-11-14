using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fillwords
{
    static class DataWorker
    {
        static public string[] ReadWordsFromFile(string path)
        {
            var text = File.ReadLines(path);

            string[] output = new string[text.Count()];

            for (int i = 0; i < output.Length; i++) output[i] = text.Skip(i).First().ToUpper();

            return output;
        }
    }

    struct WordsSet
    {

        public string[] allWords;            //Массив из всех слов
        public List<List<string>> setWords;  //Массив массивов слов, сгрупперованных по длине
        public WordsSet(string[] input) 
        {
            this.allWords = input;
            this.setWords = new List<List<string>>();
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i].Length > this.setWords.Count - 1)
                    {
                        do
                        {
                            this.setWords.Add(new List<string>());
                        } while (this.setWords.Count - 1 < input[i].Length);
                    }

                    this.setWords[input[i].Length].Add(input[i]);
                }
            }
        }

    }

    static class Colors
    {
        static public dynamic[,] colorsList = 
        { 
            { ConsoleColor.Black    , ConsoleColor.White },
            { ConsoleColor.DarkBlue , ConsoleColor.White },
            { ConsoleColor.DarkGreen, ConsoleColor.White },
            { ConsoleColor.DarkCyan , ConsoleColor.White },
            { ConsoleColor.DarkRed  , ConsoleColor.White },
            { ConsoleColor.DarkMagenta, ConsoleColor.White },
            { ConsoleColor.DarkYellow , ConsoleColor.Black },
            { ConsoleColor.Blue     , ConsoleColor.Black },
            { ConsoleColor.Green    , ConsoleColor.Black },
            { ConsoleColor.Cyan     , ConsoleColor.Black },
            { ConsoleColor.Red      , ConsoleColor.White },
            { ConsoleColor.Magenta  , ConsoleColor.White },
            { ConsoleColor.Yellow   , ConsoleColor.Black }
        };

        static public dynamic[] GetRandomColor()
        {
            Random rnd = new Random();
            int randomNum = rnd.Next(12) + 1;

            return new dynamic[]{ colorsList[randomNum, 0],
                                  colorsList[randomNum, 1] };
        }

    }
}
