using System;
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
}
