using System;

namespace Fillwords
{
    class Program
    {
        static void Main(string[] args)
        {
            //Главный цикл
            do
            {
                Printer.DrawMenu();
                UserInteractions.DoMenuActions();
            } while (true);
        }
    }
}
