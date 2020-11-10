using System;

namespace Feelwords
{
    static class ConsoleMenu
    {
        static public void DrawMenu()
        {
            Console.SetWindowSize(120,30);
            DrawTitle();
            DrawMenuItem(1, true);
            DrawMenuItem(2, false);
            DrawMenuItem(3, false);
            DrawMenuItem(4, false);

            ReadAction();
        }

        static void ReadAction()
        {
            ConsoleKeyInfo key;
            int position = 1;
            int prePosition = 1;

            do
            {
                key = Console.ReadKey();

                if (position > 1 && (key.Key == ConsoleKey.UpArrow   || key.Key == ConsoleKey.W)) position--;
                if (position < 4 && (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)) position++;

                if (position != prePosition)
                {
                    Console.SetCursorPosition(0, 4 + prePosition * 5);
                    DrawMenuItem(prePosition, false);

                    Console.SetCursorPosition(0 , 4 + position * 5);
                    DrawMenuItem(position, true);

                    Console.SetCursorPosition(0, 28);

                    prePosition = position;
                }
            } while (key.Key != ConsoleKey.Enter || key.Key != ConsoleKey.Spacebar);
        }

        static void DrawTitle()
        {
            string indent = new string(' ', (Console.WindowWidth - 81) / 2);

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine();
            Console.WriteLine(indent + "██████                      ▄▄                                         ▄▄        ");
            Console.WriteLine(indent + "██       ▄████▄    ▄████▄   ██   ██          ██   ▄████▄   ██ ▄▄▄▄     ██  ▄█████");
            Console.WriteLine(indent + "██████  ██▀  ▀██  ██▀  ▀██  ██   ██▄   ▄▄   ▄██  ██▀  ▀██  ████▀▀▀     ██  ██    ");
            Console.WriteLine(indent + "██      ███████▀  ███████▀  ██    ██   ██   ██   ██    ██  ██▀     ▄█████  ▀████▄");
            Console.WriteLine(indent + "██      ██▄       ██▄       ██    ▀██▄████▄██▀   ██▄  ▄██  ██      ██  ██      ██");
            Console.WriteLine(indent + "██       ▀█████    ▀█████   ██     ▀██▀  ▀██▀     ▀████▀   ██      ▀█████  █████▀");
            Console.WriteLine();
            Console.WriteLine();
            
            Console.ResetColor();
        }

        static void DrawMenuItem(int num, bool highlighting)
        {
            string indent = new string(' ', (Console.WindowWidth - 47) / 2);

            if (!highlighting) Console.ForegroundColor = ConsoleColor.DarkGray;

            if (num == 1)
            {
                Console.WriteLine();
                Console.WriteLine(indent + "    █  █ ▄▀▀▄ █▀▄ ▄▀▄ █▀▄  █  █ ███ █▀▄ ▄▀▄");
                Console.WriteLine(indent + "    █▄▄█ █  █ █▀▄ █▄█ █▄▀  █▄▀█ █   █▄▀ █▄█");
                Console.WriteLine(indent + "    █  █ ▀▄▄▀ █▄▀ █ █ █ █  █  █ █   █   █ █");
                Console.WriteLine();
            }

            if (num == 2)
            {
                Console.WriteLine();
                Console.WriteLine(indent + "████ █▀▄ ▄▀▀▄  ▄▀█  ▄▀▀▄ ▄▀▄ █ █ █ █  █ ███ █  ");
                Console.WriteLine(indent + "█  █ █▄▀ █  █  █ █  █  █ █ █  ███  █▄▀█  █  █▀▄");
                Console.WriteLine(indent + "█  █ █   ▀▄▄▀ █▀▀▀█ ▀▄▄▀ █ █ █ █ █ █  █  █  █▄▀");
                Console.WriteLine();
            }

            if (num == 3)
            {
                Console.WriteLine(indent + "                 ▀▀                   ");
                Console.WriteLine(indent + "        █▀▄ █▀▀ █  █ ███ █  █ █  █ ███");
                Console.WriteLine(indent + "        █▄▀ █▄▄ █▄▀█  █  █▄▀█ █▄▄█ █  ");
                Console.WriteLine(indent + "        █   █▄▄ █  █  █  █  █ █  █ █  ");
                Console.WriteLine();
            }

            if (num == 4)
            {
                Console.WriteLine();
                Console.WriteLine(indent + "           █▀█ █   █ █ █ ▄▀▀▄  ▄▀█ ");
                Console.WriteLine(indent + "           █▀▄ █▀▄ █  █  █  █  █ █ ");
                Console.WriteLine(indent + "           █▄█ █▄▀ █ █ █ ▀▄▄▀ █▀▀▀█");
                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }
}
