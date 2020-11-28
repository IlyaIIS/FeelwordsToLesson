using System;
using System.Collections.Generic;

namespace Fillwords
{
    class KeyInteractions
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

            SelectMenuItem(position);
        }

        static private void SelectMenuItem(int position)
        {
            Console.Clear();

            if (position == 1)
            {
                GetUserName();
                DoGameActions();
            }
            if (position == 2)
            {
                Console.WriteLine("Тут однажды будет Продолжить");
                Console.ReadKey(true);
            }
            if (position == 3)
            {
                Printer.DrawTableOfRecords();
                Console.ReadKey(true);
            }
            if (position == 4) Environment.Exit(0);
        }

        static private void GetUserName()
        {
            string userName;

            Console.SetCursorPosition(Printer.GetIndent(28).Length, Console.WindowHeight / 2);
            Console.Write("Введите имя: ");
            do
            {
                Console.SetCursorPosition(Printer.GetIndent(2).Length, Console.WindowHeight / 2);
                userName = Console.ReadLine();
            } while (userName.Length == 0);

            Player.name = userName;

            Console.Clear();
        }

        static private void DoGameActions()
        {
            string[] allWords = DataWorker.wordsSet.allWords;

            Field field = new Field();
            field.CreateNewField(Settings.xSize, Settings.ySize, new WordsSet(allWords)); 
            
            Printer.DrawField(field);
            Printer.DrawFieldItem(0, 0, ConsoleColor.DarkGray, ConsoleColor.White, field);
            Printer.DrawScore(0);

            ConsoleKeyInfo key;
            bool isEnter = false;
            bool isCheat = false;

            Player.CreateNewPlayer();

            do
            {
                Player.preX = Player.x;
                Player.preY = Player.y;

                key = Console.ReadKey(true);

                MoveCursor(field,  key);

                //Если курсор сдвинулся
                if (Player.preX != Player.x || Player.preY != Player.y)
                    GameLogicMethods.PlayerMoveAction(field, isEnter);

                //Действия при enter или space
                if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                    GameLogicMethods.PlayerEnterAction(field, ref isEnter, allWords);

                //Действия при esc
                if (key.Key == ConsoleKey.Escape)
                {
                    if (isEnter)
                    {
                        GameLogicMethods.BrakeFilling(field);
                        
                        Printer.DrawText(new string(' ', Console.WindowWidth - (field.xSize * 4 + 2)), Player.wordsList.Count);

                        isEnter = false;
                    }
                    else
                    {
                        Printer.DrawPopupWindow("Вы уверены, что хотите выйти? (Прогресс будет потерян)");

                        key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                        {
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Printer.DrawField(field);
                            Printer.DrawFieldItem(Player.x, Player.y, ConsoleColor.DarkGray, ConsoleColor.White, field);
                            for (int i = 0; i < Player.wordsList.Count; i++)
                                Printer.DrawText(Player.wordsList[i], i);
                        }
                    }
                }

                //Проверка на победу
                if (Player.wordsList.Count == field.wordsList.Count)
                {
                    if (DataWorker.userScoreDict.ContainsKey(Player.name))
                        DataWorker.userScoreDict[Player.name] += Player.score;
                    else
                        DataWorker.userScoreDict.Add(Player.name, Player.score);

                    DataWorker.UpdateUsetScoreFile("../../../users_score.txt");

                    Printer.DrawPopupWindow("Вы отгодали все слова!");
                    Console.ReadKey(true);
                    break;
                }

                //Если С (чит)
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

        static private void MoveCursor(Field field, ConsoleKeyInfo key)
        {
            if (Player.x < field.xSize - 1 && (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)) Player.x++;
            if (Player.y > 0               && (key.Key == ConsoleKey.UpArrow    || key.Key == ConsoleKey.W)) Player.y--;
            if (Player.x > 0               && (key.Key == ConsoleKey.LeftArrow  || key.Key == ConsoleKey.A)) Player.x--;
            if (Player.y < field.ySize - 1 && (key.Key == ConsoleKey.DownArrow  || key.Key == ConsoleKey.S)) Player.y++;
        }
    }
}
