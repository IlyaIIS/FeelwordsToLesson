using System;
using System.Collections.Generic;
using System.Text;

namespace Fillwords
{
    class ConsolePrinter
    {
        static public void DrawMenu()
        {
            Console.SetWindowSize(120, 30);
            DrawTitle();
            DrawMenuItem(1, true);
            DrawMenuItem(2, false);
            DrawMenuItem(3, false);
            DrawMenuItem(4, false);
        }
        static void DrawTitle()
        {
            string indent = GetIndent(68);

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine();
            Console.WriteLine(indent + "██████      ▄▄  ▄▄                                        ▄▄        ");
            Console.WriteLine(indent + "██      ██  ██  ██  ██          ██   ▄████▄   ██ ▄▄▄▄     ██  ▄█████");
            Console.WriteLine(indent + "██████      ██  ██  ██▄   ▄▄   ▄██  ██▀  ▀██  ████▀▀▀     ██  ██    ");
            Console.WriteLine(indent + "██      ██  ██  ██   ██   ██   ██   ██    ██  ██▀     ▄█████  ▀████▄");
            Console.WriteLine(indent + "██      ██  ██  ██   ▀██▄████▄██▀   ██▄  ▄██  ██      ██  ██      ██");
            Console.WriteLine(indent + "██      ██  ██  ██    ▀██▀  ▀██▀     ▀████▀   ██      ▀█████  █████▀");
            Console.WriteLine();
            Console.WriteLine();

            Console.ResetColor();
        }

        static public void DrawMenuItem(int num, bool highlighting)
        {
            string indent = GetIndent(47);

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

        static public string GetIndent(int textSize)
        {
            return new string(' ', (Console.WindowWidth - textSize) / 2);
        }
    }
}
