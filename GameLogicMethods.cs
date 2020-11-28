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
                Printer.DrawWord(Player.wordNow, field.xSize, Player.wordsList.Count);
                Player.coordStory.Add(new int[] { Player.x, Player.y });
            }
        }

        //Действия при нажатии Enter
        static public void PlayerEnterAction(Field field, ref bool isEnter, string[] allWords)
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
                else
                    isEnter = !isEnter;
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
                        Printer.DrawWord("Попробуйте записать это слово наоборот или найти ещё одно такое же на поле", field.xSize, Player.wordsList.Count);
                    else
                    if ((allWords as IList<string>).Contains(Player.wordNow))
                        Printer.DrawWord("Это не одно из слов, которое вам нужно отгодать на этом поле ):", field.xSize, Player.wordsList.Count);
                    else
                        Printer.DrawWord("Такого слова нет в словаре", field.xSize, Player.wordsList.Count);
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
