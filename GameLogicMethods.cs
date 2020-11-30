using System;
using System.Collections.Generic;
using System.Text;

namespace Fillwords
{
    static class GameLogicMethods
    {
        //Действия при движении курсора по полю
        static public void PlayerMoveAction(Field field, bool isEnter)
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
                Printer.DrawText(Player.wordNow, Player.wordsList.Count);
                Player.coordStory.Add(new int[] { Player.x, Player.y });
            }
        }

        //Действия при нажатии Enter
        static public void PlayerEnterAction(Field field, ref bool isEnter, string[] allWords)
        {
            if (!isEnter)
            {
                Printer.DrawText(new string(' ', Console.WindowWidth - (field.xSize * 4 + 2)), Player.wordsList.Count);

                if (field.cellColor[Player.x, Player.y, 0] == ConsoleColor.Black)
                {
                    Printer.DrawFieldItem(Player.x, Player.y, ConsoleColor.Gray, ConsoleColor.Black, field);
                    Player.wordNow += field.cellLetter[Player.x, Player.y];
                    Printer.DrawText(Player.wordNow, Player.wordsList.Count);
                    Player.coordStory.Add(new int[] { Player.x, Player.y });
                }
                else
                    isEnter = !isEnter;
            }
            else
            {
                if (field.wordsList.Contains(Player.wordNow) &&
                    field.wordPos[field.wordsList.IndexOf(Player.wordNow)][Player.wordNow.Length - 1] % field.xSize == Player.x)
                {
                    dynamic[] color = Settings.Colors.GetRandomColor();
                    for (int i = 0; i < Player.coordStory.Count; i++)
                    {
                        int x = Player.coordStory[i][0];
                        int y = Player.coordStory[i][1];

                        field.cellColor[x, y, 0] = color[0];
                        field.cellColor[x, y, 1] = color[1];
                    }

                    Player.wordsList.Add(Player.wordNow);
                    Player.score += (int)Math.Pow(Player.wordNow.Length, 1.5);
                    Printer.DrawScore(Player.score);
                }
                else
                {
                    if (field.wordsList.Contains(Player.wordNow))
                        Printer.DrawText("Попробуйте записать это слово наоборот или найти ещё одно такое же на поле",  Player.wordsList.Count);
                    else
                    if ((allWords as IList<string>).Contains(Player.wordNow))
                        Printer.DrawText("Это не одно из слов, которое вам нужно отгодать на этом поле ):", Player.wordsList.Count);
                    else
                        Printer.DrawText("Такого слова нет в словаре", Player.wordsList.Count);
                }

                BrakeFilling(field);
            }

            isEnter = !isEnter;
        }

        //Прекращение заполнения слова
        static public void BrakeFilling(Field field)
        {
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

    }
}
