using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fillwords
{
    static class DataWorker
    {
        static public WordsSet wordsSet;
        static public Dictionary<string, int> userScoreDict = new Dictionary<string, int>();

        static public void ReadWordsFromFile(string path)
        {
            var text = File.ReadLines(path);

            string[] output = new string[text.Count()];

            int i = 0;
            foreach(var word in text)
            {
                output[i] = word;
                i++;
            }

            wordsSet = new WordsSet(output);
        }

        static public void ReadUserScoreFromFile(string path)
        {
            var text = File.ReadLines(path);

            string[] fileStrings = new string[text.Count()];

            foreach (var word in text)
            {
                string local = string.Empty;
                userScoreDict.Add(word.Split(' ')[0], Convert.ToInt32(word.Split(' ')[1])); 
            }
        }

        static public void UpdateUsetScoreFile(string path)
        {
            string output = string.Empty;
            foreach (var user in userScoreDict)
            {
                output += user.Key + " " + user.Value + "\n";
            }

            output.Remove(output.Length - 1);

            File.WriteAllText(path, output);
        }
    }

    struct WordsSet
    {

        public string[] allWords;            //Массив из всех слов
        public List<List<string>> wordsSet;  //Массив массивов слов, сгрупперованных по длине
        public WordsSet(string[] input) 
        {
            this.allWords = input;
            this.wordsSet = new List<List<string>>();
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i].Length > this.wordsSet.Count - 1)
                    {
                        do
                        {
                            this.wordsSet.Add(new List<string>());
                        } while (this.wordsSet.Count - 1 < input[i].Length);
                    }

                    this.wordsSet[input[i].Length].Add(input[i]);
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
