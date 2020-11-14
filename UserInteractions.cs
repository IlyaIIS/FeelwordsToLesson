using System;
using System.Collections.Generic;

namespace Fillwords
{
    class UserInteractions
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
                    Printer.DrawMenuItem(prePosition, false);

                    Console.SetCursorPosition(0, 4 + position * 5);
                    Printer.DrawMenuItem(position, true);

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

            Console.SetCursorPosition(Printer.GetIndent(28).Length, Console.WindowHeight / 2);
            Console.Write("Введите имя: ");
            do
            {
                Console.SetCursorPosition(Printer.GetIndent(2).Length, Console.WindowHeight / 2);
                userName = Console.ReadLine();
            } while (userName.Length == 0);

            Console.Clear();
        }

        static void DoGameActions()
        {
            Field field = new Field();
            string[] allWords = DataWorker.ReadWordsFromFile("../../../words.txt");
            field.CreateNewField(10, 10, new WordsSet(allWords));

            Printer.DrawField(field);
            Printer.DrawFieldItem(0, 0, ConsoleColor.DarkGray, ConsoleColor.White, field);

            ConsoleKeyInfo key;
            bool isEnter = false;
            bool isCheat = false;

            Player.CreateNewPlayer();

            do
            {
                Player.preX = Player.x;
                Player.preY = Player.y;

                key = Console.ReadKey(true);

                //Движение курсора
                if (key.Key == ConsoleKey.RightArrow && Player.x < field.xSize - 1) Player.x++;
                if (key.Key == ConsoleKey.UpArrow && Player.y > 0) Player.y--;
                if (key.Key == ConsoleKey.LeftArrow && Player.x > 0) Player.x--;
                if (key.Key == ConsoleKey.DownArrow && Player.y < field.ySize - 1) Player.y++;

                //Если курсор сдвинулся
                if (Player.preX != Player.x || Player.preY != Player.y)
                {
                    if (!isEnter)
                    {
                        Printer.DrawFieldItem(Player.preX, Player.preY, field.cellColor[Player.preX, Player.preY, 0],
                                                     field.cellColor[Player.preX, Player.preY, 1], field);
                        Printer.DrawFieldItem(Player.x, Player.y, ConsoleColor.DarkGray, ConsoleColor.White, field);
                    }
                    else
                    {
                        Printer.DrawFieldItem(Player.preX, Player.preY, ConsoleColor.Gray, ConsoleColor.Black, field);
                        Printer.DrawFieldItem(Player.x, Player.y, ConsoleColor.DarkGray, ConsoleColor.White, field);
                        Player.wordNow += field.cellLetter[Player.x, Player.y];
                        Printer.DrawWord(Player.wordNow, field.xSize, Player.wordsList.Count);
                        Player.coordStory.Add(new int[] { Player.x, Player.y });
                    }
                }

                //Если enter или space
                if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                {
                    if (!isEnter)
                    {
                        Printer.DrawWord(new string(' ', Console.WindowWidth - (field.xSize * 4 + 2)), field.xSize, Player.wordsList.Count);

                        if (field.cellColor[Player.x, Player.y, 0] == ConsoleColor.Black)
                        {
                            Printer.DrawFieldItem(Player.x, Player.y, ConsoleColor.Gray, ConsoleColor.Black, field);
                            Player.wordNow += field.cellLetter[Player.x, Player.y];
                            Printer.DrawWord(Player.wordNow, field.xSize, Player.wordsList.Count);
                            Player.coordStory.Add(new int[] { Player.x, Player.y });
                        }
                    }
                    else
                    {
                        if (field.wordsList.Contains(Player.wordNow) && 
                            field.wordPos[field.wordsList.IndexOf(Player.wordNow)][Player.wordNow.Length - 1] % field.xSize == Player.x)
                        {
                            dynamic[] color = Colors.GetRandomColor();
                            for (int i = 0; i < Player.coordStory.Count; i++)
                            {
                                int x = Player.coordStory[i][0];
                                int y = Player.coordStory[i][1];

                                field.cellColor[x, y, 0] = color[0];
                                field.cellColor[x, y, 1] = color[1];
                            }

                            Player.wordsList.Add(Player.wordNow);
                        }
                        else
                        {
                            if (field.wordsList.Contains(Player.wordNow))
                                Printer.DrawWord("Вам невероятно повезло! Где-то на поле есть ещё одно такое же слово. Найдите его!", field.xSize, Player.wordsList.Count);
                            else
                            if ((allWords as IList<string>).Contains(Player.wordNow))
                                Printer.DrawWord("Это не одно из слов, которое вам нужно отгодать на этом поле ):", field.xSize, Player.wordsList.Count);
                            else
                                Printer.DrawWord("Такого слова нет в словаре", field.xSize, Player.wordsList.Count);
                        }

                        for (int i = 0; i < Player.coordStory.Count; i++)
                        {
                            int x = Player.coordStory[i][0];
                            int y = Player.coordStory[i][1];

                            Printer.DrawFieldItem(x, y, field.cellColor[x, y, 0], field.cellColor[x, y, 1], field);
                        }
                        Printer.DrawFieldItem(Player.x, Player.y, ConsoleColor.DarkGray, ConsoleColor.White, field);

                        Player.wordNow = string.Empty;
                        Player.coordStory.RemoveRange(0, Player.coordStory.Count);
                    }

                    isEnter = !isEnter;
                }

                //если shift (чит)
                if (key.Key == ConsoleKey.C)
                {
                    isCheat = true;
                    Random rnd = new Random();
                    int randomNum = rnd.Next(field.wordsList.Count);

                    for (int ii = 0; ii < field.wordsList[randomNum].Length; ii++)
                    {
                        int x = field.wordPos[randomNum][ii] % field.xSize;
                        int y = field.wordPos[randomNum][ii] / field.xSize;
                        Printer.DrawFieldItem(x, y, ConsoleColor.Yellow, ConsoleColor.Red, field);
                    }
                }
                else
                if (isCheat)
                {
                    isCheat = false;
                    for (int i = 0; i < field.wordsList.Count; i++)
                        for (int ii = 0; ii < field.wordsList[i].Length; ii++)
                        {
                            int x = field.wordPos[i][ii] % field.xSize;
                            int y = field.wordPos[i][ii] / field.xSize;
                            Printer.DrawFieldItem(x, y, field.cellColor[x, y, 0], field.cellColor[x, y, 1], field);
                        }

                    Printer.DrawFieldItem(Player.x, Player.y, ConsoleColor.DarkGray, ConsoleColor.White, field);
                }
            } while (true);

        }

        void ReadActionInGame()
        {

        }
    }
}
