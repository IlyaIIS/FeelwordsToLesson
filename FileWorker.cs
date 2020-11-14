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

            for (int i = 0; i < output.Length; i++) output[i] = text.Skip(i).First();

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
                    if (input[i].Length > this.setWords.Count)
                    {
                        do
                        {
                            this.setWords.Add(new List<string>());
                        } while (this.setWords.Count < input[i].Length);
                    }

                    this.setWords[input[i].Length].Add(input[i]);
                }
            }
        }

    }

}
