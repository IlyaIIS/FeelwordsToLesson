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
            foreach (var word in text)
            {
                output[i] = word;
                i++;
            }

            wordsSet = new WordsSet(output);
        }

        static public void ReadUserScoreFromFile(string path)
        {
            var text = File.ReadLines(path);

            foreach (var word in text)
                userScoreDict.Add(word.Split(' ')[0], Convert.ToInt32(word.Split(' ')[1]));
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

        static public void ReadSettingsFromFile(string path)
        {
            var text = File.ReadLines(path);

            int i = 0;
            foreach (var word in text)
            {
                if (i != 7) Settings.property[i] = Int32.Parse(word);
                else Settings.property[i] = bool.Parse(word);
                i++;
            }
        }

        static public void UpdateSettingsFile(string path)
        {
            string output = string.Empty;
            for (int i = 0; i <= Settings.property.lenght; i++)
            {
                output += Settings.property[i] + "\n";
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

    struct MyVector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MyVector2(int xInput, int yInput)
        {
            X = xInput;
            Y = yInput;
        }
    }
}
