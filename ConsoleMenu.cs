using System;
using System.Collections.Generic;
using System.Text;

namespace Feelwords
{
    static class ConsoleMenu
    {
        static public void DrawMenu()
        {
            Console.SetWindowSize(120,30);
            DrawTitle();
            DrawMenuItems();
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
            Console.WriteLine();
            Console.ResetColor();
        }

        static void DrawMenuItems()
        {
            string indent = new string(' ', (Console.WindowWidth - 45) / 2);

            Console.WriteLine(indent + "█  █ ▄▀▀▄ █▀▄ ▄▀▄ █▀▄  █  █ ███ █▀▄ ▄▀▄");
            Console.WriteLine(indent + "█▄▄█ █  █ █▀▄ █▄█ █▄▀  █▄▀█ █   █▄▀ █▄█");
            Console.WriteLine(indent + "█  █ ▀▄▄▀ █▄▀ █ █ █ █  █  █ █   █   █ █");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(indent + "████ █▀▄ ▄▀▀▄  ▄▀█  ▄▀▀▄ ▄▀▄ █ █ █ █  █ ███ █  ");
            Console.WriteLine(indent + "█  █ █▄▀ █  █  █ █  █  █ █ █  ███  █▄▀█  █  █▀▄");
            Console.WriteLine(indent + "█  █ █   ▀▄▄▀ █▀▀▀█ ▀▄▄▀ █ █ █ █ █ █  █  █  █▄▀");
            Console.WriteLine();
            Console.WriteLine(indent + "         ▀▀                   ");
            Console.WriteLine(indent + "█▀▄ █▀▀ █  █ ███ █  █ █  █ ███");
            Console.WriteLine(indent + "█▄▀ █▄▄ █▄▀█  █  █▄▀█ █▄▄█ █  ");
            Console.WriteLine(indent + "█   █▄▄ █  █  █  █  █ █  █ █  ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(indent + "█▀█ █   █ █ █ ▄▀▀▄  ▄▀█ ");
            Console.WriteLine(indent + "█▀▄ █▀▄ █  █  █  █  █ █ ");
            Console.WriteLine(indent + "█▄█ █▄▀ █ █ █ ▀▄▄▀ █▀▀▀█");
        }
    }
}
