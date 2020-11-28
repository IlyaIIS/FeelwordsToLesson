using System;
using System.Text;

namespace Fillwords
{
    class Printer
    {
        static public void DrawMenu()
        {
            Console.Clear();
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

        static public void DrawField(Field field)
        {
            DrawFieldLine("┌", "─", "┬", "┐", field.xSize);
            Console.WriteLine();

            for (int y = 0; y < field.ySize; y++)
            {
                Console.Write('│');
                for (int x = 0; x < field.xSize; x++)
                {
                    Console.Write(" ");
                    Console.BackgroundColor = field.cellColor[x, y, 0];
                    Console.ForegroundColor = field.cellColor[x, y, 1];
                    Console.Write(field.cellLetter[x, y]);
                    Console.ResetColor();
                    Console.Write(" " + "│");
                }
                Console.WriteLine();
                DrawFieldLine("├", "─", "┼", "┤", field.xSize);
                Console.WriteLine();
            }

            Console.SetCursorPosition(0, Console.CursorTop - 1);
            DrawFieldLine("└", "─", "┴", "┘", field.xSize);
        }

        static void DrawFieldLine(string sign1, string sign2, string sign3, string sign4, int num)
        {
            Console.Write(sign1 + sign2 + sign2 + sign2);
            for (int i = 0; i < num - 1; i++) Console.Write(sign3 + sign2 + sign2 + sign2);
            Console.Write(sign4);
        }

        static public void DrawFieldItem(int x, int y, dynamic color1, dynamic color2, Field field)
        {
            Console.SetCursorPosition(x * 4 + 2, y * 2 + 1);
            Console.BackgroundColor = color1;
            Console.ForegroundColor = color2;
            Console.Write(field.cellLetter[x, y]);
            Console.ResetColor();
        }

        static public void DrawText(string text, int num)
        {
            Console.SetCursorPosition(Settings.xSize*4 + 2, num + 1);
            Console.Write(text);
        }

        static public void DrawScore(int score)
        {
            Console.SetCursorPosition(0, Settings.ySize + Settings.ySize *Settings.cellSize + 3);
            Console.Write("Счёт: " + score);
        }

        static public void DrawPopupWindow(string text)
        {
            Console.SetCursorPosition(Console.WindowWidth/2 - text.Length / 2 - 1, Console.WindowHeight / 2 - 1);
            Console.Write("╔" + new string('═', text.Length) + "╗");
            Console.SetCursorPosition(Console.WindowWidth/2 - text.Length / 2 - 1, Console.WindowHeight / 2);
            Console.Write("║" + text + "║");
            Console.SetCursorPosition(Console.WindowWidth/2 - text.Length / 2 - 1, Console.WindowHeight / 2 + 1);
            Console.Write("╚" + new string('═', text.Length) + "╝");
        }

        static public void DrawTableOfRecords()
        {
            DrawMenuItem(3, true);
            foreach(var user in DataWorker.userScoreDict)
            {
                Console.WriteLine(user.Key + ": " + user.Value);
            }
        }
    }
}
