using System;
using System.Collections.Generic;

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

            if (position == 1)
            {
                GetUserName();
                DoGameActions();
            }
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

            Console.Clear();
        }

        static void DoGameActions()
        {
            Field field = new Field();
            field.CreateNewField(10, 10, new WordsSet(DataWorker.ReadWordsFromFile("../../../words.txt")));

            ConsolePrinter.DrawField(field);
            ConsolePrinter.DrawFieldItem(0, 0, ConsoleColor.DarkGray, ConsoleColor.White, field);

            ConsoleKeyInfo key;
            int playerX = 0;
            int playerY = 0;
            List<int[]> coordStory = new List<int[]>();
            bool isEnter = false;
            string playerWord = string.Empty;

            do
            {
                int prePlayerX = playerX;
                int prePlayerY = playerY;
                
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.RightArrow && playerX < field.xSize - 1) playerX++;
                if (key.Key == ConsoleKey.UpArrow && playerY > 0) playerY--;
                if (key.Key == ConsoleKey.LeftArrow && playerX > 0) playerX--;
                if (key.Key == ConsoleKey.DownArrow && playerY < field.ySize - 1) playerY++;

                if (prePlayerX != playerX || prePlayerY != playerY)
                {
                    if (!isEnter)
                    {
                        ConsolePrinter.DrawFieldItem(prePlayerX, prePlayerY, field.cellColor[prePlayerX, prePlayerY, 0], 
                                                     field.cellColor[prePlayerX, prePlayerY, 1], field);
                        ConsolePrinter.DrawFieldItem(playerX, playerY, ConsoleColor.DarkGray, ConsoleColor.White, field);
                    }
                    else
                    {
                        ConsolePrinter.DrawFieldItem(prePlayerX, prePlayerY, ConsoleColor.Gray, ConsoleColor.Black, field);
                        ConsolePrinter.DrawFieldItem(playerX, playerY, ConsoleColor.DarkGray, ConsoleColor.White, field);
                        playerWord += field.cellLetter[playerX, playerY];
                        ConsolePrinter.DrawWord(playerWord, field.ySize);
                        coordStory.Add(new int[] { playerX, playerY });
                    }
                }

                if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                {
                    if (!isEnter)
                    {
                        if (field.cellColor[playerX, playerY, 0] == ConsoleColor.Black)
                        {
                            ConsolePrinter.DrawFieldItem(playerX, playerY, ConsoleColor.Gray, ConsoleColor.Black, field);
                            playerWord += field.cellLetter[playerX, playerY];
                            ConsolePrinter.DrawWord(playerWord, field.ySize);
                            coordStory.Add(new int[] { playerX, playerY });
                        }
                    }
                    else
                    {
                        if (field.wordsList.Contains(playerWord))
                        {
                            dynamic[] color = Colors.GetRandomColor();
                            for (int i = 0; i < coordStory.Count; i++)
                            {
                                int x = coordStory[i][0];
                                int y = coordStory[i][1];

                                field.cellColor[x, y, 0] = color[0];
                                field.cellColor[x, y, 1] = color[1];
                            }
                        }
                        else
                        {
                            for (int i = 0; i < coordStory.Count; i++)
                            {
                                int x = coordStory[i][0];
                                int y = coordStory[i][1];

                                field.cellColor[x, y, 0] = ConsoleColor.Black;
                                field.cellColor[x, y, 1] = ConsoleColor.White;
                            }
                        }

                        for (int i = 0; i < coordStory.Count; i++)
                        {
                            int x = coordStory[i][0];
                            int y = coordStory[i][1];

                            ConsolePrinter.DrawFieldItem(x, y, field.cellColor[x, y, 0], field.cellColor[x, y, 1], field);
                        }
                        ConsolePrinter.DrawFieldItem(playerX, playerY, ConsoleColor.DarkGray, ConsoleColor.White, field);

                        ConsolePrinter.DrawWord(new string(' ', playerWord.Length), field.ySize);

                        playerWord = string.Empty;
                        coordStory.RemoveRange(0, coordStory.Count);
                    }

                    isEnter = !isEnter;
                }
            } while (true);

        }

        void ReadActionInGame()
        {

        }
    }
}
