using System;
using System.Text;

namespace Fillwords
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            //ConsolePrinter.DrawMenu();
            //ConsoleInteractions.DoMenuActions();
            Field field = new Field();
            field.CreateNewField(10, 10, new WordsSet(DataWorker.ReadWordsFromFile(@"C:\Users\Илья\Documents\GitHub\FillwordsToLesson\words.txt")));
        }
    }
}
