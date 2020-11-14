using System;

namespace Fillwords
{
    class ConsoleInteractions
    {
        static public void DoMenuActions()
        {
            ConsoleKeyInfo key;
            int position = 1;
            int prePosition = 1;

            do
            {
                key = Console.ReadKey(true);

                if (position > 1 && (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)) position--;
                if (position < 4 && (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)) position++;

                if (position != prePosition)
                {
                    Console.SetCursorPosition(0, 4 + prePosition * 5);
                    ConsolePrinter.DrawMenuItem(prePosition, false);

                    Console.SetCursorPosition(0, 4 + position * 5);
                    ConsolePrinter.DrawMenuItem(position, true);

                    Console.SetCursorPosition(0, 28);

                    prePosition = position;
                }
            } while (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Spacebar);

            Console.Clear();

            if (position == 1) GetUserName();
            if (position == 2) Console.WriteLine("Тут однажды будет Продолжить");
            if (position == 3) Console.WriteLine("Тут однажды будет Рейтинг");
            //if (position == 4) Console.WriteLine("Тут однажды будет Выход");
        }

        static void GetUserName()
        {
            string userName;

            Console.SetCursorPosition(ConsolePrinter.GetIndent(28).Length, Console.WindowHeight / 2);
            Console.Write("Введите имя: ");
            do
            {
                Console.SetCursorPosition(ConsolePrinter.GetIndent(2).Length, Console.WindowHeight / 2);
                userName = Console.ReadLine();
            } while (userName.Length == 0);
        }
    }
}
